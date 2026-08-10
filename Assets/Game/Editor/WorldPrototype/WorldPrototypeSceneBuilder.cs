using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using Varynth.Presentation.Camera;
using Varynth.Presentation.Interaction;
using Varynth.Presentation.Visualization;
using Varynth.World.Grid;
using Varynth.World.Terrain;

namespace Varynth.Tooling.Editor.WorldPrototype
{
    /// <summary>
    /// Reproducible, idempotent, GUID-stable builder for the Phase 2A / Varynth 0.1.0
    /// WorldPrototype scene. All spatial numbers here are documented prototype values
    /// (see PHASE_2A_WORLD_CAMERA_FOUNDATION.md), not canon design values. Re-running
    /// updates existing named GameObjects/assets in place instead of deleting and
    /// recreating them, so repeated runs do not churn GUIDs or produce a spurious diff.
    /// </summary>
    public static class WorldPrototypeSceneBuilder
    {
        internal const string ScenePath = "Assets/Game/Scenes/WorldPrototype.unity";

        private const string TerrainDataPath = "Assets/Game/World/Art/Terrain/TestIslandTerrainData.asset";
        private const string SandLayerPath = "Assets/Game/World/Art/Terrain/TestIsland_Sand.terrainlayer";
        private const string GrassLayerPath = "Assets/Game/World/Art/Terrain/TestIsland_Grass.terrainlayer";
        private const string RockLayerPath = "Assets/Game/World/Art/Terrain/TestIsland_Rock.terrainlayer";
        private const string SandTexturePath = "Assets/Game/World/Art/Textures/TestIsland_Sand.asset";
        private const string GrassTexturePath = "Assets/Game/World/Art/Textures/TestIsland_Grass.asset";
        private const string RockTexturePath = "Assets/Game/World/Art/Textures/TestIsland_Rock.asset";
        private const string TerrainMaterialPath = "Assets/Game/World/Art/Materials/TestIslandTerrain.mat";
        private const string WaterMaterialPath = "Assets/Game/World/Art/Materials/Water.mat";
        private const string GridMaterialPath = "Assets/Game/World/Art/Materials/DebugGrid.mat";
        private const string HighlightMaterialPath = "Assets/Game/World/Art/Materials/CellHighlight.mat";
        private const string GridMeshPath = "Assets/Game/World/Art/Meshes/DebugGridMesh.asset";

        // Phase 2A / Varynth 0.1.0 prototype values -- not canon design/balancing values.
        private const float TerrainWorldSize = 300f;
        private const float TerrainVerticalSize = 40f;
        private const float TerrainTransformY = -15f;
        private const float SeaLevelNormalized = 15f / 40f; // -TerrainTransformY / TerrainVerticalSize
        private const int HeightmapResolution = 257;
        private const int IslandSeed = 20260809;
        private const float CellSize = 4f;
        private const int TextureSize = 32;
        private const float WaterMargin = 60f;

        [MenuItem("Varynth/Build World Prototype")]
        public static void Build()
        {
            var scene = OpenOrCreateScene();

            var worldRoot = FindOrCreateRoot("World");
            var islandGo = FindOrCreateChild(worldRoot, "TestIsland");
            var terrain = BuildTestIsland(islandGo);
            var terrainCollider = islandGo.GetComponent<TerrainCollider>();

            var waterGo = FindOrCreateChild(worldRoot, "Water");
            BuildWater(waterGo);

            var lightingRoot = FindOrCreateRoot("Lighting");
            BuildLighting(lightingRoot);

            var gridRoot = FindOrCreateRoot("Grid");
            var gridDisplay = BuildGridDisplay(gridRoot, terrain);
            var highlight = BuildHighlight(gridRoot);

            var cameraRig = FindOrCreateRoot("CameraRig");
            var camera = BuildCamera(cameraRig);
            BuildInteraction(cameraRig, camera, terrain, terrainCollider, gridDisplay, highlight);

            AddSceneToBuildSettings(ScenePath);

            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene, ScenePath);

            Debug.Log($"Varynth: WorldPrototype scene built at {ScenePath}");
        }

        private static Scene OpenOrCreateScene()
        {
            Directory.CreateDirectory("Assets/Game/Scenes");

            return File.Exists(ScenePath)
                ? EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single)
                : EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        }

        private static GameObject FindOrCreateRoot(string name)
        {
            var scene = SceneManager.GetActiveScene();
            foreach (var go in scene.GetRootGameObjects())
            {
                if (go.name == name)
                {
                    return go;
                }
            }

            return new GameObject(name);
        }

        private static GameObject FindOrCreateChild(GameObject parent, string name)
        {
            var existing = parent.transform.Find(name);
            if (existing != null)
            {
                return existing.gameObject;
            }

            var go = new GameObject(name);
            go.transform.SetParent(parent.transform, false);
            return go;
        }

        // ---------------------------------------------------------------- Terrain

        private static UnityEngine.Terrain BuildTestIsland(GameObject islandGo)
        {
            Directory.CreateDirectory("Assets/Game/World/Art/Terrain");
            Directory.CreateDirectory("Assets/Game/World/Art/Textures");

            var terrainData = AssetDatabase.LoadAssetAtPath<TerrainData>(TerrainDataPath);
            var isNewTerrainData = terrainData == null;
            if (isNewTerrainData)
            {
                terrainData = new TerrainData();
            }

            terrainData.heightmapResolution = HeightmapResolution;
            terrainData.size = new Vector3(TerrainWorldSize, TerrainVerticalSize, TerrainWorldSize);

            var heights = IslandHeightmapGenerator.Generate(terrainData.heightmapResolution, IslandSeed);
            terrainData.SetHeights(0, 0, heights);

            var sandLayer = GetOrCreateTerrainLayer(SandLayerPath, SandTexturePath, new Color(0.86f, 0.78f, 0.55f));
            var grassLayer = GetOrCreateTerrainLayer(GrassLayerPath, GrassTexturePath, new Color(0.30f, 0.48f, 0.22f));
            var rockLayer = GetOrCreateTerrainLayer(RockLayerPath, RockTexturePath, new Color(0.42f, 0.40f, 0.38f));
            terrainData.terrainLayers = new[] { sandLayer, grassLayer, rockLayer };

            PaintAlphamaps(terrainData, heights);

            if (isNewTerrainData)
            {
                AssetDatabase.CreateAsset(terrainData, TerrainDataPath);
            }
            else
            {
                EditorUtility.SetDirty(terrainData);
            }

            var terrain = islandGo.GetComponent<UnityEngine.Terrain>();
            if (terrain == null)
            {
                terrain = islandGo.AddComponent<UnityEngine.Terrain>();
            }
            terrain.terrainData = terrainData;

            // Terrain created via AddComponent<Terrain>() does not auto-assign a pipeline
            // material the way the "GameObject > Terrain" menu item does; without an explicit
            // URP terrain shader it falls back to Hidden/InternalErrorShader (magenta).
            terrain.materialTemplate = GetOrCreateTerrainMaterial(TerrainMaterialPath);

            var collider = islandGo.GetComponent<TerrainCollider>();
            if (collider == null)
            {
                collider = islandGo.AddComponent<TerrainCollider>();
            }
            collider.terrainData = terrainData;

            islandGo.transform.position = new Vector3(0f, TerrainTransformY, 0f);

            AssetDatabase.SaveAssets();
            return terrain;
        }

        private static void PaintAlphamaps(TerrainData terrainData, float[,] heights)
        {
            var alphaWidth = terrainData.alphamapWidth;
            var alphaHeight = terrainData.alphamapHeight;
            var heightRes = heights.GetLength(0);
            var alphamaps = new float[alphaHeight, alphaWidth, 3];

            for (var y = 0; y < alphaHeight; y++)
            {
                for (var x = 0; x < alphaWidth; x++)
                {
                    var normX = (float)x / (alphaWidth - 1);
                    var normY = (float)y / (alphaHeight - 1);

                    var hx = Mathf.Clamp(Mathf.RoundToInt(normX * (heightRes - 1)), 0, heightRes - 1);
                    var hy = Mathf.Clamp(Mathf.RoundToInt(normY * (heightRes - 1)), 0, heightRes - 1);
                    var height01 = heights[hy, hx];

                    var steepness = terrainData.GetSteepness(normX, normY);

                    var sand = 1f - Mathf.InverseLerp(SeaLevelNormalized, SeaLevelNormalized + 0.05f, height01);
                    sand = Mathf.Clamp01(sand);
                    var rock = Mathf.Clamp01(Mathf.InverseLerp(25f, 45f, steepness));
                    var grass = Mathf.Clamp01(1f - sand - rock);

                    var total = sand + grass + rock;
                    if (total <= 0f)
                    {
                        grass = 1f;
                        total = 1f;
                    }

                    alphamaps[y, x, 0] = sand / total;
                    alphamaps[y, x, 1] = grass / total;
                    alphamaps[y, x, 2] = rock / total;
                }
            }

            terrainData.SetAlphamaps(0, 0, alphamaps);
        }

        private static TerrainLayer GetOrCreateTerrainLayer(string layerPath, string texturePath, Color baseColor)
        {
            var texture = GetOrCreateTexture(texturePath, baseColor);

            var layer = AssetDatabase.LoadAssetAtPath<TerrainLayer>(layerPath);
            var isNew = layer == null;
            if (isNew)
            {
                layer = new TerrainLayer();
            }

            layer.diffuseTexture = texture;
            layer.tileSize = new Vector2(16f, 16f);

            if (isNew)
            {
                AssetDatabase.CreateAsset(layer, layerPath);
            }
            else
            {
                EditorUtility.SetDirty(layer);
            }

            return layer;
        }

        private static Texture2D GetOrCreateTexture(string path, Color baseColor)
        {
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
            var isNew = texture == null;
            if (isNew)
            {
                texture = new Texture2D(TextureSize, TextureSize, TextureFormat.RGBA32, false)
                {
                    filterMode = FilterMode.Bilinear,
                    wrapMode = TextureWrapMode.Repeat
                };
            }

            for (var y = 0; y < TextureSize; y++)
            {
                for (var x = 0; x < TextureSize; x++)
                {
                    var noise = DeterministicNoise(x, y) * 0.08f - 0.04f;
                    var color = new Color(
                        Mathf.Clamp01(baseColor.r + noise),
                        Mathf.Clamp01(baseColor.g + noise),
                        Mathf.Clamp01(baseColor.b + noise),
                        1f);
                    texture.SetPixel(x, y, color);
                }
            }

            texture.Apply();

            if (isNew)
            {
                AssetDatabase.CreateAsset(texture, path);
            }
            else
            {
                EditorUtility.SetDirty(texture);
            }

            return texture;
        }

        private static float DeterministicNoise(int x, int y)
        {
            unchecked
            {
                var h = x * 374761393 + y * 668265263;
                h = (h ^ (h >> 13)) * 1274126177;
                h ^= h >> 16;
                return (h & 0x7fffffff) / (float)int.MaxValue;
            }
        }

        // ------------------------------------------------------------------ Water

        private static void BuildWater(GameObject waterGo)
        {
            var meshFilter = waterGo.GetComponent<MeshFilter>();
            if (meshFilter == null)
            {
                meshFilter = waterGo.AddComponent<MeshFilter>();
            }
            meshFilter.sharedMesh = GetOrCreatePrimitiveMesh(PrimitiveType.Plane);

            var meshRenderer = waterGo.GetComponent<MeshRenderer>();
            if (meshRenderer == null)
            {
                meshRenderer = waterGo.AddComponent<MeshRenderer>();
            }
            meshRenderer.sharedMaterial = GetOrCreateMaterial(
                WaterMaterialPath, new Color(0.10f, 0.35f, 0.55f, 0.75f), LitShaderName, transparent: true);

            var collider = waterGo.GetComponent<Collider>();
            if (collider != null)
            {
                Object.DestroyImmediate(collider);
            }

            var size = TerrainWorldSize + WaterMargin * 2f;
            waterGo.transform.position = new Vector3(TerrainWorldSize * 0.5f, 0f, TerrainWorldSize * 0.5f);
            waterGo.transform.localScale = new Vector3(size / 10f, 1f, size / 10f);
        }

        private static Mesh SaveOrUpdateMeshAsset(Mesh mesh, string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path) ?? "Assets/Game/World/Art/Meshes");

            var existing = AssetDatabase.LoadAssetAtPath<Mesh>(path);
            if (existing == null)
            {
                AssetDatabase.CreateAsset(mesh, path);
                return mesh;
            }

            EditorUtility.CopySerialized(mesh, existing);
            EditorUtility.SetDirty(existing);
            Object.DestroyImmediate(mesh);
            return existing;
        }

        private static Mesh GetOrCreatePrimitiveMesh(PrimitiveType primitiveType)
        {
            var temp = GameObject.CreatePrimitive(primitiveType);
            var mesh = temp.GetComponent<MeshFilter>().sharedMesh;
            Object.DestroyImmediate(temp);
            return mesh;
        }

        private static Material GetOrCreateTerrainMaterial(string path)
        {
            // Verified against the installed URP package source
            // (Shaders/Terrain/TerrainLit.shader: `Shader "Universal Render Pipeline/Terrain/Lit"`),
            // not guessed. This is the only shader that renders TerrainLayer splatmaps correctly
            // under URP; the Built-in "Nature/Terrain/Standard" shader is not URP-compatible and
            // renders as Hidden/InternalErrorShader (magenta) under this pipeline.
            const string shaderName = "Universal Render Pipeline/Terrain/Lit";
            var shader = Shader.Find(shaderName);
            if (shader == null)
            {
                throw new System.InvalidOperationException(
                    $"Shader '{shaderName}' not found. Is the URP package installed/up to date?");
            }

            var material = AssetDatabase.LoadAssetAtPath<Material>(path);
            var isNew = material == null;
            if (isNew || material.shader != shader)
            {
                if (isNew)
                {
                    material = new Material(shader);
                }
                else
                {
                    material.shader = shader;
                }
            }

            if (isNew)
            {
                AssetDatabase.CreateAsset(material, path);
            }
            else
            {
                EditorUtility.SetDirty(material);
            }

            return material;
        }

        private const string LitShaderName = "Universal Render Pipeline/Lit";

        // Debug-only geometry (grid lines, cell highlight) deliberately uses Unlit: the grid
        // mesh has no meaningful normals (line topology) and the highlight is a thin quad
        // lying near-flat under a Directional Light -- both produced very bright/white
        // lighting artifacts under Lit shading. Unlit renders a flat, predictable debug color
        // regardless of light direction, which is also the conventional choice for debug overlays.
        private const string UnlitShaderName = "Universal Render Pipeline/Unlit";

        private static Material GetOrCreateMaterial(string path, Color color, string shaderName, bool transparent)
        {
            var material = AssetDatabase.LoadAssetAtPath<Material>(path);
            var isNew = material == null;
            if (isNew || material.shader == null || material.shader.name != shaderName)
            {
                var shader = Shader.Find(shaderName);
                if (shader == null)
                {
                    throw new System.InvalidOperationException(
                        $"Shader '{shaderName}' not found. Is the URP package installed/up to date?");
                }

                if (isNew)
                {
                    material = new Material(shader);
                }
                else
                {
                    material.shader = shader;
                }
            }

            material.color = color;
            if (transparent)
            {
                SetupTransparent(material);
            }

            if (isNew)
            {
                AssetDatabase.CreateAsset(material, path);
            }
            else
            {
                EditorUtility.SetDirty(material);
            }

            return material;
        }

        private static void SetupTransparent(Material material)
        {
            material.SetFloat("_Surface", 1f);
            material.SetOverrideTag("RenderType", "Transparent");
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = (int)RenderQueue.Transparent;
        }

        // --------------------------------------------------------------- Lighting

        private static void BuildLighting(GameObject lightingRoot)
        {
            var lightGo = FindOrCreateChild(lightingRoot, "Directional Light");
            var light = lightGo.GetComponent<Light>();
            if (light == null)
            {
                light = lightGo.AddComponent<Light>();
            }
            light.type = LightType.Directional;
            light.intensity = 1.2f;
            light.shadows = LightShadows.Soft;
            lightGo.transform.rotation = Quaternion.Euler(50f, -30f, 0f);

            var volumeGo = FindOrCreateChild(lightingRoot, "Global Volume");
            var volume = volumeGo.GetComponent<Volume>();
            if (volume == null)
            {
                volume = volumeGo.AddComponent<Volume>();
            }
            volume.isGlobal = true;
            if (volume.sharedProfile == null)
            {
                volume.sharedProfile = AssetDatabase.LoadAssetAtPath<VolumeProfile>("Assets/Settings/DefaultVolumeProfile.asset");
            }
        }

        // ------------------------------------------------------------------- Grid

        private static GridDisplay BuildGridDisplay(GameObject gridRoot, UnityEngine.Terrain terrain)
        {
            var display = gridRoot.GetComponent<GridDisplay>();
            if (display == null)
            {
                display = gridRoot.AddComponent<GridDisplay>();
            }

            var grid = new WorldGrid(CellSize, Vector2.zero);
            var heightSource = new UnityTerrainHeightSource(terrain);
            var cellCount = Mathf.RoundToInt(TerrainWorldSize / CellSize);
            var bounds = new RectInt(0, 0, cellCount, cellCount);

            var mesh = GridMeshBuilder.Build(grid, heightSource, bounds);
            mesh = SaveOrUpdateMeshAsset(mesh, GridMeshPath);

            display.Initialize(mesh);

            var renderer = gridRoot.GetComponent<MeshRenderer>();
            renderer.sharedMaterial = GetOrCreateMaterial(
                GridMaterialPath, new Color(1f, 1f, 1f, 0.55f), UnlitShaderName, transparent: true);

            return display;
        }

        private static GridCellHighlight BuildHighlight(GameObject gridRoot)
        {
            // Pre-fix scene layout put GridCellHighlight directly on the Grid root (referencing
            // a separate flat quad child via a "_visual" Transform field that no longer exists).
            // Remove that stale component so re-running the builder against an old scene doesn't
            // leave a duplicate/orphaned component behind.
            var legacyHighlight = gridRoot.GetComponent<GridCellHighlight>();
            if (legacyHighlight != null)
            {
                Object.DestroyImmediate(legacyHighlight);
            }

            var highlightGo = FindOrCreateChild(gridRoot, "Highlight");

            // Visibility is controlled purely via MeshRenderer.enabled (GridCellHighlight);
            // the GameObject itself must stay active. A pre-fix scene may still have it
            // SetActive(false) from the old "hide the whole quad" approach -- reset explicitly.
            if (!highlightGo.activeSelf)
            {
                highlightGo.SetActive(true);
            }

            // GridCellMeshBuilder produces already-world-space vertices, so this GameObject's
            // own transform must stay identity -- otherwise it would double-transform the mesh.
            // A pre-fix scene may still carry the old flat-quad rotation/scale; reset explicitly.
            highlightGo.transform.localPosition = Vector3.zero;
            highlightGo.transform.localRotation = Quaternion.identity;
            highlightGo.transform.localScale = Vector3.one;

            // GridCellHighlight requires MeshFilter/MeshRenderer (RequireComponent) and builds
            // its own terrain-conforming mesh at runtime on cell change (GridCellMeshBuilder) --
            // no primitive mesh/manual transform needed here.
            var highlightComponent = highlightGo.GetComponent<GridCellHighlight>();
            if (highlightComponent == null)
            {
                highlightComponent = highlightGo.AddComponent<GridCellHighlight>();
            }

            var meshRenderer = highlightGo.GetComponent<MeshRenderer>();
            meshRenderer.sharedMaterial = GetOrCreateMaterial(
                HighlightMaterialPath, new Color(1f, 0.9f, 0.2f, 0.6f), UnlitShaderName, transparent: true);
            meshRenderer.enabled = false;

            return highlightComponent;
        }

        // ----------------------------------------------------------------- Camera

        private static UnityEngine.Camera BuildCamera(GameObject cameraRig)
        {
            cameraRig.transform.position = new Vector3(TerrainWorldSize * 0.5f, 0f, TerrainWorldSize * 0.5f);

            var yawGo = FindOrCreateChild(cameraRig, "Yaw");
            var cameraGo = FindOrCreateChild(yawGo, "Main Camera");

            var camera = cameraGo.GetComponent<UnityEngine.Camera>();
            if (camera == null)
            {
                camera = cameraGo.AddComponent<UnityEngine.Camera>();
            }
            camera.clearFlags = CameraClearFlags.Skybox;
            camera.nearClipPlane = 0.3f;
            camera.farClipPlane = 2000f;
            cameraGo.tag = "MainCamera";

            if (cameraGo.GetComponent<AudioListener>() == null)
            {
                cameraGo.AddComponent<AudioListener>();
            }

            var controller = cameraRig.GetComponent<StrategyCameraController>();
            if (controller == null)
            {
                controller = cameraRig.AddComponent<StrategyCameraController>();
            }

            var serialized = new SerializedObject(controller);
            serialized.FindProperty("_yawPivot").objectReferenceValue = yawGo.transform;
            serialized.FindProperty("_cameraTransform").objectReferenceValue = cameraGo.transform;
            serialized.ApplyModifiedPropertiesWithoutUndo();

            return camera;
        }

        private static void BuildInteraction(
            GameObject cameraRig,
            UnityEngine.Camera camera,
            UnityEngine.Terrain terrain,
            TerrainCollider terrainCollider,
            GridDisplay gridDisplay,
            GridCellHighlight highlight)
        {
            var controller = cameraRig.GetComponent<WorldInteractionController>();
            if (controller == null)
            {
                controller = cameraRig.AddComponent<WorldInteractionController>();
            }

            var serialized = new SerializedObject(controller);
            serialized.FindProperty("_camera").objectReferenceValue = camera;
            serialized.FindProperty("_terrain").objectReferenceValue = terrain;
            serialized.FindProperty("_terrainCollider").objectReferenceValue = terrainCollider;
            serialized.FindProperty("_cellSize").floatValue = CellSize;
            serialized.FindProperty("_gridOrigin").vector2Value = Vector2.zero;
            serialized.FindProperty("_highlight").objectReferenceValue = highlight;
            serialized.FindProperty("_gridDisplay").objectReferenceValue = gridDisplay;
            serialized.ApplyModifiedPropertiesWithoutUndo();
        }

        // ------------------------------------------------------------------ Build

        private static void AddSceneToBuildSettings(string scenePath)
        {
            var scenes = EditorBuildSettings.scenes.ToList();
            if (scenes.Any(s => s.path == scenePath))
            {
                return;
            }

            scenes.Add(new EditorBuildSettingsScene(scenePath, true));
            EditorBuildSettings.scenes = scenes.ToArray();
        }
    }
}
