using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using Varynth.Core.Common;
using Varynth.Presentation.Camera;
using Varynth.Presentation.Interaction;
using Varynth.Presentation.Visualization;
using Varynth.World.Grid;
using Varynth.World.Surface;
using Varynth.World.Terrain;

namespace Varynth.Tooling.Editor.WorldPrototype
{
    /// <summary>
    /// Reproducible, idempotent, GUID-stable builder for the WorldPrototype scene.
    /// All spatial numbers here are documented prototype values (see
    /// PHASE_2A_WORLD_CAMERA_FOUNDATION.md / PHASE_2B_ISLAND_TERRAIN_FOUNDATION.md),
    /// not canon design values. Re-running updates existing named GameObjects/assets
    /// in place instead of deleting and recreating them, so repeated runs do not
    /// churn GUIDs or produce a spurious diff.
    ///
    /// Phase 2B: generates the island list from WorldPrototypeIslands.GetDefaultConfigs()
    /// -- one Terrain/TerrainData per island (not one mega-terrain) -- and builds the
    /// per-island surface classification, aggregated buildability overlay and resource
    /// candidate markers.
    /// </summary>
    public static class WorldPrototypeSceneBuilder
    {
        internal const string ScenePath = "Assets/Game/Scenes/WorldPrototype.unity";

        private const string TerrainArtDirectory = "Assets/Game/World/Art/Terrain";
        private const string TextureArtDirectory = "Assets/Game/World/Art/Textures";

        private const string SandLayerPath = "Assets/Game/World/Art/Terrain/TestIsland_Sand.terrainlayer";
        private const string GrassLayerPath = "Assets/Game/World/Art/Terrain/TestIsland_Grass.terrainlayer";
        private const string RockLayerPath = "Assets/Game/World/Art/Terrain/TestIsland_Rock.terrainlayer";
        private const string SeabedLayerPath = "Assets/Game/World/Art/Terrain/TestIsland_Seabed.terrainlayer";
        private const string SandTexturePath = "Assets/Game/World/Art/Textures/TestIsland_Sand.asset";
        private const string GrassTexturePath = "Assets/Game/World/Art/Textures/TestIsland_Grass.asset";
        private const string RockTexturePath = "Assets/Game/World/Art/Textures/TestIsland_Rock.asset";
        private const string SeabedTexturePath = "Assets/Game/World/Art/Textures/TestIsland_Seabed.asset";
        private const string TerrainMaterialPath = "Assets/Game/World/Art/Materials/TestIslandTerrain.mat";
        private const string WaterMaterialPath = "Assets/Game/World/Art/Materials/Water.mat";
        private const string GridMaterialPath = "Assets/Game/World/Art/Materials/DebugGrid.mat";
        private const string HighlightMaterialPath = "Assets/Game/World/Art/Materials/CellHighlight.mat";
        private const string GridMeshPath = "Assets/Game/World/Art/Meshes/DebugGridMesh.asset";

        private const string BuildableOverlayMeshPath = "Assets/Game/World/Art/Meshes/SurfaceOverlay_Buildable.asset";
        private const string CoastOverlayMeshPath = "Assets/Game/World/Art/Meshes/SurfaceOverlay_Coast.asset";
        private const string RockOverlayMeshPath = "Assets/Game/World/Art/Meshes/SurfaceOverlay_RockOrSteep.asset";
        private const string BuildableOverlayMaterialPath = "Assets/Game/World/Art/Materials/SurfaceOverlay_Buildable.mat";
        private const string CoastOverlayMaterialPath = "Assets/Game/World/Art/Materials/SurfaceOverlay_Coast.mat";
        private const string RockOverlayMaterialPath = "Assets/Game/World/Art/Materials/SurfaceOverlay_RockOrSteep.mat";
        private const string ResourceMarkerMaterialPath = "Assets/Game/World/Art/Materials/ResourceCandidateMarker.mat";

        // Phase 2A/2B prototype values -- not canon design/balancing values.
        private const float TerrainVerticalSize = 40f;
        private const float TerrainTransformY = -15f;
        private const float SeaLevelNormalized = 15f / 40f; // -TerrainTransformY / TerrainVerticalSize
        private const int HeightmapResolution = 257;
        private const float CellSize = 4f;
        private const int TextureSize = 32;
        private const float WaterMargin = 60f;
        private const float CameraBoundsMargin = 40f;
        // Initial camera zoom must fit the whole archipelago, not the fixed single-island
        // range Phase 2A tuned (15-120). ZoomMinDistance stays small so close-up zoom is
        // still available; ZoomMaxDistance/InitialZoomDistance are derived per-build from
        // the real archipelago bounds instead (see BuildCamera / CameraRigMath.ComputeFitDistance).
        private const float ArchipelagoZoomMinDistance = 15f;
        private const float ArchipelagoFramingMargin = 1.15f;
        // Matches the Main Camera's own default fieldOfView (never overridden below) --
        // kept as its own constant so the framing math doesn't depend on reading it back
        // off a Camera component that may not exist yet at the point framing is computed.
        private const float CameraVerticalFovDegrees = 60f;
        private const int ResourceMinSpacingCells = 6;
        private const float SurfaceOverlayHeightOffset = 0.03f;
        private const float ResourceMarkerHeightOffset = 0.5f;
        private const float ResourceMarkerScale = 3f;

        private sealed class IslandBuildResult
        {
            public IslandPrototypeConfig Config;
            public UnityEngine.Terrain Terrain;
            public TerrainCollider Collider;
            public IslandSurfaceMap SurfaceMap;
            public RectInt CellBounds;
            public IReadOnlyList<ResourceSlotCandidate> ResourceCandidates;
        }

        [MenuItem("Varynth/Build World Prototype")]
        public static void Build()
        {
            var scene = OpenOrCreateScene();
            var grid = new WorldGrid(CellSize, Vector2.zero);

            var worldRoot = FindOrCreateRoot("World");
            RemoveLegacySingleIsland(worldRoot);

            var islandsRoot = FindOrCreateChild(worldRoot, "Islands");
            var configs = WorldPrototypeIslands.GetDefaultConfigs();
            var islandResults = configs.Select(config => BuildIsland(islandsRoot, grid, config)).ToList();

            var archipelagoBounds = ComputeArchipelagoWorldBounds(configs);

            // Framing must be derived from where the land actually is, not the raw
            // terrain-tile footprint: each TerrainData rectangle extends far beyond its
            // island's emerged coastline (submerged "skirt"), so fitting the camera to
            // the full terrain bounding box leaves the small emerged islands looking like
            // barely-visible specks in a huge, mostly-empty view. Root-caused via a real
            // PlayMode diagnostic sampling actual above-sea-level terrain points.
            var landBounds = ComputeLandWorldBounds(grid, islandResults, archipelagoBounds);
            var (initialZoomDistance, zoomMaxDistance) = ComputeCameraFraming(landBounds);

            var waterGo = FindOrCreateChild(worldRoot, "Water");
            BuildWater(waterGo, archipelagoBounds, zoomMaxDistance);

            var lightingRoot = FindOrCreateRoot("Lighting");
            BuildLighting(lightingRoot);

            var gridRoot = FindOrCreateRoot("Grid");
            var gridDisplay = BuildGridDisplay(gridRoot, grid, islandResults);
            var highlight = BuildHighlight(gridRoot);

            var surfaceOverlayRoot = FindOrCreateRoot("SurfaceOverlay");
            var surfaceDisplays = BuildSurfaceOverlay(surfaceOverlayRoot, grid, islandResults);

            var resourceRoot = FindOrCreateRoot("ResourceCandidates");
            var resourceMarkers = BuildResourceMarkers(resourceRoot, grid, islandResults);

            var cameraRig = FindOrCreateRoot("CameraRig");
            var camera = BuildCamera(cameraRig, archipelagoBounds, initialZoomDistance, zoomMaxDistance);

            BuildInteraction(
                cameraRig,
                camera,
                islandResults.Select(r => r.Terrain).ToArray(),
                islandResults.Select(r => r.Collider).ToArray(),
                gridDisplay,
                highlight,
                surfaceDisplays,
                resourceMarkers);

            AddSceneToBuildSettings(ScenePath);

            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene, ScenePath);

            Debug.Log($"Varynth: WorldPrototype scene built at {ScenePath} ({islandResults.Count} islands)");
        }

        private static Scene OpenOrCreateScene()
        {
            Directory.CreateDirectory("Assets/Game/Scenes");

            return File.Exists(ScenePath)
                ? EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single)
                : EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        }

        private static void RemoveLegacySingleIsland(GameObject worldRoot)
        {
            // Pre-2B scenes had a single "World/TestIsland" child; 2B replaces it with
            // "World/Islands/<Name>" per island. Remove the stale leftover so re-running
            // against an old scene doesn't leave an orphaned duplicate island behind.
            var legacy = worldRoot.transform.Find("TestIsland");
            if (legacy != null)
            {
                Object.DestroyImmediate(legacy.gameObject);
            }
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

        // -------------------------------------------------------------- Archipelago

        private static (Vector2 min, Vector2 max) ComputeArchipelagoWorldBounds(List<IslandPrototypeConfig> configs)
        {
            var min = new Vector2(float.MaxValue, float.MaxValue);
            var max = new Vector2(float.MinValue, float.MinValue);

            foreach (var config in configs)
            {
                var origin = IslandOrigin(config);
                var far = origin + new Vector2(config.TerrainWidth, config.TerrainLength);
                min = Vector2.Min(min, origin);
                max = Vector2.Max(max, far);
            }

            return (min, max);
        }

        private static Vector2 IslandOrigin(IslandPrototypeConfig config)
        {
            return config.Center - new Vector2(config.TerrainWidth * 0.5f, config.TerrainLength * 0.5f);
        }

        // Union of every emerged (non-Water) surface cell across all islands, in world
        // space. Deliberately excludes the submerged "skirt" every terrain tile carries
        // beyond its own coastline, so initial camera framing targets where the land
        // actually is instead of the much larger raw terrain footprint.
        private static (Vector2 min, Vector2 max) ComputeLandWorldBounds(
            WorldGrid grid, List<IslandBuildResult> islands, (Vector2 min, Vector2 max) fallback)
        {
            var min = new Vector2(float.MaxValue, float.MaxValue);
            var max = new Vector2(float.MinValue, float.MinValue);
            var any = false;

            foreach (var island in islands)
            {
                var bounds = island.CellBounds;
                for (var cz = bounds.yMin; cz < bounds.yMax; cz++)
                {
                    for (var cx = bounds.xMin; cx < bounds.xMax; cx++)
                    {
                        var cell = new GridCoordinate(cx, cz);
                        if (!island.SurfaceMap.TryGetFlags(cell, out var flags)) continue;
                        if ((flags & SurfaceCellFlags.Water) != 0) continue; // submerged, not emerged land

                        var center = grid.CellToWorldCenter(cell);
                        min = Vector2.Min(min, center);
                        max = Vector2.Max(max, center);
                        any = true;
                    }
                }
            }

            return any ? (min, max) : fallback;
        }

        private static float ComputeBoundsRadius((Vector2 min, Vector2 max) bounds)
        {
            var width = bounds.max.x - bounds.min.x;
            var depth = bounds.max.y - bounds.min.y;
            return Mathf.Sqrt(width * width + depth * depth) * 0.5f;
        }

        private static (float initialZoomDistance, float zoomMaxDistance) ComputeCameraFraming(
            (Vector2 min, Vector2 max) landBounds)
        {
            var radius = ComputeBoundsRadius(landBounds);
            var fitDistance = CameraRigMath.ComputeFitDistance(radius, CameraVerticalFovDegrees, ArchipelagoFramingMargin);
            var zoomMaxDistance = Mathf.Max(ArchipelagoZoomMinDistance, fitDistance * 1.05f);
            return (fitDistance, zoomMaxDistance);
        }

        // ---------------------------------------------------------------- Terrain

        private static IslandBuildResult BuildIsland(GameObject islandsRoot, WorldGrid grid, IslandPrototypeConfig config)
        {
            Directory.CreateDirectory(TerrainArtDirectory);
            Directory.CreateDirectory(TextureArtDirectory);

            var islandGo = FindOrCreateChild(islandsRoot, config.Name);
            var terrainDataPath = $"{TerrainArtDirectory}/{config.Name}_TerrainData.asset";

            var terrainData = AssetDatabase.LoadAssetAtPath<TerrainData>(terrainDataPath);
            var isNewTerrainData = terrainData == null;
            if (isNewTerrainData)
            {
                terrainData = new TerrainData();
            }

            // Only assign when it actually differs: Unity's heightmapResolution setter
            // resamples the current heightmap even when set to its already-current value,
            // which produced tiny floating-point drift on repeated builder runs (idempotency
            // regression) even though SetHeights below always writes fully deterministic data.
            if (terrainData.heightmapResolution != HeightmapResolution)
            {
                terrainData.heightmapResolution = HeightmapResolution;
            }
            terrainData.size = new Vector3(config.TerrainWidth, TerrainVerticalSize, config.TerrainLength);

            var heights = IslandHeightmapGenerator.Generate(
                terrainData.heightmapResolution,
                config.Seed,
                config.IslandRadius01,
                config.CoastNoiseStrength,
                config.Octaves,
                config.Persistence,
                config.Lacunarity);
            terrainData.SetHeights(0, 0, heights);

            var sandLayer = GetOrCreateTerrainLayer(SandLayerPath, SandTexturePath, new Color(0.86f, 0.78f, 0.55f));
            var grassLayer = GetOrCreateTerrainLayer(GrassLayerPath, GrassTexturePath, new Color(0.30f, 0.48f, 0.22f));
            var rockLayer = GetOrCreateTerrainLayer(RockLayerPath, RockTexturePath, new Color(0.42f, 0.40f, 0.38f));
            var seabedLayer = GetOrCreateTerrainLayer(SeabedLayerPath, SeabedTexturePath, new Color(0.06f, 0.14f, 0.20f));
            terrainData.terrainLayers = new[] { sandLayer, grassLayer, rockLayer, seabedLayer };

            PaintAlphamaps(terrainData, heights);

            if (isNewTerrainData)
            {
                AssetDatabase.CreateAsset(terrainData, terrainDataPath);
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
            // One shared material works for every island (the visual data lives in
            // TerrainData/TerrainLayers, not the material itself).
            terrain.materialTemplate = GetOrCreateTerrainMaterial(TerrainMaterialPath);

            var collider = islandGo.GetComponent<TerrainCollider>();
            if (collider == null)
            {
                collider = islandGo.AddComponent<TerrainCollider>();
            }
            collider.terrainData = terrainData;

            var origin = IslandOrigin(config);
            islandGo.transform.position = new Vector3(origin.x, TerrainTransformY, origin.y);

            AssetDatabase.SaveAssets();

            var cellBounds = WorldToCellBounds(grid, origin, new Vector2(config.TerrainWidth, config.TerrainLength));
            var heightSource = new UnityTerrainHeightSource(terrain);
            var surfaceConfig = new SurfaceClassificationConfig();
            var surfaceMap = SurfaceMapGenerator.Generate(grid, heightSource, cellBounds, surfaceConfig);
            var resourceCandidates = ResourceCandidateGenerator.Generate(
                surfaceMap, cellBounds, config.Seed, config.MaxResourceCandidates, ResourceMinSpacingCells);

            return new IslandBuildResult
            {
                Config = config,
                Terrain = terrain,
                Collider = collider,
                SurfaceMap = surfaceMap,
                CellBounds = cellBounds,
                ResourceCandidates = resourceCandidates
            };
        }

        private static RectInt WorldToCellBounds(WorldGrid grid, Vector2 origin, Vector2 size)
        {
            var min = grid.WorldToCell(origin.x, origin.y);
            var max = grid.WorldToCell(origin.x + size.x, origin.y + size.y);
            return new RectInt(min.X, min.Z, max.X - min.X, max.Z - min.Z);
        }

        private static void PaintAlphamaps(TerrainData terrainData, float[,] heights)
        {
            var alphaWidth = terrainData.alphamapWidth;
            var alphaHeight = terrainData.alphamapHeight;
            var heightRes = heights.GetLength(0);
            const int layerCount = 4; // sand, grass, rock, seabed
            var alphamaps = new float[alphaHeight, alphaWidth, layerCount];

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

                    // Below sea level, blend toward a dark seabed layer instead of letting
                    // the sand/grass/rock composition extend under the whole terrain tile --
                    // otherwise the submerged skirt of every terrain reads as a bright sand
                    // rectangle through the semi-transparent water (0.1.1 "square tiles" bug).
                    var emerged = Mathf.Clamp01(
                        Mathf.InverseLerp(SeaLevelNormalized - 0.03f, SeaLevelNormalized + 0.01f, height01));
                    var seabed = 1f - emerged;

                    var sandLand = Mathf.Clamp01(1f - Mathf.InverseLerp(SeaLevelNormalized, SeaLevelNormalized + 0.05f, height01));
                    var rockLand = Mathf.Clamp01(Mathf.InverseLerp(25f, 45f, steepness));
                    var grassLand = Mathf.Clamp01(1f - sandLand - rockLand);
                    var landTotal = sandLand + grassLand + rockLand;
                    if (landTotal <= 0f)
                    {
                        grassLand = 1f;
                        landTotal = 1f;
                    }

                    alphamaps[y, x, 0] = sandLand / landTotal * emerged;
                    alphamaps[y, x, 1] = grassLand / landTotal * emerged;
                    alphamaps[y, x, 2] = rockLand / landTotal * emerged;
                    alphamaps[y, x, 3] = seabed;
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

        private static void BuildWater(GameObject waterGo, (Vector2 min, Vector2 max) bounds, float zoomMaxDistance)
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

            // Sized to guarantee the water plane's rectangular edge never appears against
            // the sky background: even at max pan (CameraBoundsMargin beyond the
            // archipelago) and max zoom-out (up to zoomMaxDistance from the camera), the
            // farthest visible ground point is within archipelago-bounds + that margin +
            // zoomMaxDistance, so padding by exactly that sum (plus a small fixed safety
            // pad) always keeps the water plane's edge off-screen.
            var dynamicMargin = CameraBoundsMargin + zoomMaxDistance + WaterMargin;
            var size = new Vector2(bounds.max.x - bounds.min.x, bounds.max.y - bounds.min.y) + new Vector2(dynamicMargin * 2f, dynamicMargin * 2f);
            var center = (bounds.min + bounds.max) * 0.5f;
            waterGo.transform.position = new Vector3(center.x, 0f, center.y);
            waterGo.transform.localScale = new Vector3(size.x / 10f, 1f, size.y / 10f);
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

        // Debug-only geometry (grid lines, cell highlight, surface overlay, resource
        // markers) deliberately uses Unlit: the grid mesh has no meaningful normals
        // (line topology) and the highlight is a thin quad lying near-flat under a
        // Directional Light -- both produced very bright/white lighting artifacts
        // under Lit shading. Unlit renders a flat, predictable debug color regardless
        // of light direction, which is also the conventional choice for debug overlays.
        // Verified against the installed URP Unlit shader source
        // (Shaders/UnlitForwardPass.hlsl): it does not read a per-vertex COLOR
        // attribute, so overlay categories use separate flat-colored meshes/materials
        // rather than one vertex-colored mesh.
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
            // URP Lit's ShaderGUI re-validates blend state from _Blend (0=Alpha) on load/
            // reimport; leaving it unset let URP silently override the SrcBlend/keyword
            // choices below with its own Premultiply-mode defaults. Setting it explicitly
            // keeps the intended plain alpha-blend path stable across reimports.
            material.SetFloat("_Blend", 0f);
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

        private static GridDisplay BuildGridDisplay(GameObject gridRoot, WorldGrid grid, List<IslandBuildResult> islands)
        {
            var display = gridRoot.GetComponent<GridDisplay>();
            if (display == null)
            {
                display = gridRoot.AddComponent<GridDisplay>();
            }

            var mesh = BuildCombinedGridMesh(grid, islands);
            mesh = SaveOrUpdateMeshAsset(mesh, GridMeshPath);

            display.Initialize(mesh);

            var renderer = gridRoot.GetComponent<MeshRenderer>();
            renderer.sharedMaterial = GetOrCreateMaterial(
                GridMaterialPath, new Color(1f, 1f, 1f, 0.55f), UnlitShaderName, transparent: true);

            return display;
        }

        private static Mesh BuildCombinedGridMesh(WorldGrid grid, List<IslandBuildResult> islands)
        {
            // Each island's grid lines cover exactly that island's own terrain footprint
            // (matching the 0.1.0 single-island behavior), not the open ocean between
            // islands -- avoids one huge, mostly-irrelevant mesh spanning empty water.
            var vertices = new List<Vector3>();
            var indices = new List<int>();

            foreach (var island in islands)
            {
                var heightSource = new UnityTerrainHeightSource(island.Terrain);
                var islandMesh = GridMeshBuilder.Build(grid, heightSource, island.CellBounds);

                var baseIndex = vertices.Count;
                vertices.AddRange(islandMesh.vertices);
                foreach (var index in islandMesh.GetIndices(0))
                {
                    indices.Add(baseIndex + index);
                }

                Object.DestroyImmediate(islandMesh);
            }

            var mesh = new Mesh { name = "DebugGridMesh" };
            mesh.SetVertices(vertices);
            mesh.SetIndices(indices.ToArray(), MeshTopology.Lines, 0);
            mesh.RecalculateBounds();
            return mesh;
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

        // --------------------------------------------------------- Surface overlay

        private static GridDisplay[] BuildSurfaceOverlay(GameObject overlayRoot, WorldGrid grid, List<IslandBuildResult> islands)
        {
            var sources = islands
                .Select(i => new SurfaceOverlayMeshBuilder.IslandSurfaceSource(i.SurfaceMap, i.CellBounds, new UnityTerrainHeightSource(i.Terrain)))
                .ToList();

            var buildable = BuildSurfaceCategoryDisplay(
                overlayRoot, "Buildable", grid, sources, SurfaceCellFlags.Buildable,
                new Color(0.25f, 0.85f, 0.35f, 0.55f), BuildableOverlayMeshPath, BuildableOverlayMaterialPath);
            var coast = BuildSurfaceCategoryDisplay(
                overlayRoot, "Coast", grid, sources, SurfaceCellFlags.Coast,
                new Color(0.95f, 0.85f, 0.40f, 0.55f), CoastOverlayMeshPath, CoastOverlayMaterialPath);
            var rock = BuildSurfaceCategoryDisplay(
                overlayRoot, "RockOrSteep", grid, sources, SurfaceCellFlags.RockOrSteep,
                new Color(0.70f, 0.25f, 0.25f, 0.55f), RockOverlayMeshPath, RockOverlayMaterialPath);

            return new[] { buildable, coast, rock };
        }

        private static GridDisplay BuildSurfaceCategoryDisplay(
            GameObject overlayRoot,
            string name,
            WorldGrid grid,
            List<SurfaceOverlayMeshBuilder.IslandSurfaceSource> sources,
            SurfaceCellFlags flag,
            Color color,
            string meshPath,
            string materialPath)
        {
            var go = FindOrCreateChild(overlayRoot, name);
            var display = go.GetComponent<GridDisplay>();
            if (display == null)
            {
                display = go.AddComponent<GridDisplay>();
            }

            var mesh = SurfaceOverlayMeshBuilder.BuildCategoryMesh(grid, sources, flag, SurfaceOverlayHeightOffset);
            mesh = SaveOrUpdateMeshAsset(mesh, meshPath);
            display.Initialize(mesh);

            var renderer = go.GetComponent<MeshRenderer>();
            renderer.sharedMaterial = GetOrCreateMaterial(materialPath, color, UnlitShaderName, transparent: true);
            renderer.enabled = false; // hidden by default, toggled via F2

            return display;
        }

        // ------------------------------------------------------- Resource markers

        private static ResourceCandidateMarkers BuildResourceMarkers(GameObject root, WorldGrid grid, List<IslandBuildResult> islands)
        {
            var component = root.GetComponent<ResourceCandidateMarkers>();
            if (component == null)
            {
                component = root.AddComponent<ResourceCandidateMarkers>();
            }

            var markerMesh = GetOrCreatePrimitiveMesh(PrimitiveType.Cube);
            var material = GetOrCreateMaterial(
                ResourceMarkerMaterialPath, new Color(1f, 0.55f, 0.10f, 1f), UnlitShaderName, transparent: false);

            var markers = new List<GameObject>();
            var index = 0;

            foreach (var island in islands)
            {
                var heightSource = new UnityTerrainHeightSource(island.Terrain);

                foreach (var candidate in island.ResourceCandidates)
                {
                    var markerGo = FindOrCreateChild(root, $"Marker_{index}");

                    var meshFilter = markerGo.GetComponent<MeshFilter>();
                    if (meshFilter == null)
                    {
                        meshFilter = markerGo.AddComponent<MeshFilter>();
                    }
                    meshFilter.sharedMesh = markerMesh;

                    var meshRenderer = markerGo.GetComponent<MeshRenderer>();
                    if (meshRenderer == null)
                    {
                        meshRenderer = markerGo.AddComponent<MeshRenderer>();
                    }
                    meshRenderer.sharedMaterial = material;

                    var collider = markerGo.GetComponent<Collider>();
                    if (collider != null)
                    {
                        Object.DestroyImmediate(collider);
                    }

                    var center = grid.CellToWorldCenter(candidate.Cell);
                    var y = heightSource.TryGetHeight(center.x, center.y, out var height) ? height : 0f;
                    markerGo.transform.position = new Vector3(center.x, y + ResourceMarkerHeightOffset, center.y);
                    markerGo.transform.localScale = Vector3.one * ResourceMarkerScale;
                    markerGo.SetActive(false); // hidden by default, toggled via F3

                    markers.Add(markerGo);
                    index++;
                }
            }

            var serialized = new SerializedObject(component);
            var markersProperty = serialized.FindProperty("_markers");
            markersProperty.arraySize = markers.Count;
            for (var i = 0; i < markers.Count; i++)
            {
                markersProperty.GetArrayElementAtIndex(i).objectReferenceValue = markers[i];
            }
            serialized.ApplyModifiedPropertiesWithoutUndo();

            return component;
        }

        // ----------------------------------------------------------------- Camera

        private static UnityEngine.Camera BuildCamera(
            GameObject cameraRig, (Vector2 min, Vector2 max) panBounds, float initialZoomDistance, float zoomMaxDistance)
        {
            var center = (panBounds.min + panBounds.max) * 0.5f;
            cameraRig.transform.position = new Vector3(center.x, 0f, center.y);

            var yawGo = FindOrCreateChild(cameraRig, "Yaw");
            var cameraGo = FindOrCreateChild(yawGo, "Main Camera");

            var camera = cameraGo.GetComponent<UnityEngine.Camera>();
            if (camera == null)
            {
                camera = cameraGo.AddComponent<UnityEngine.Camera>();
            }
            camera.clearFlags = CameraClearFlags.Skybox;
            camera.nearClipPlane = 0.3f;
            camera.farClipPlane = 3000f;
            camera.fieldOfView = CameraVerticalFovDegrees;
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
            serialized.FindProperty("_config.BoundsMin").vector2Value = panBounds.min - new Vector2(CameraBoundsMargin, CameraBoundsMargin);
            serialized.FindProperty("_config.BoundsMax").vector2Value = panBounds.max + new Vector2(CameraBoundsMargin, CameraBoundsMargin);
            serialized.FindProperty("_config.ZoomMinDistance").floatValue = ArchipelagoZoomMinDistance;
            serialized.FindProperty("_config.ZoomMaxDistance").floatValue = zoomMaxDistance;
            serialized.FindProperty("_config.InitialZoomDistance").floatValue = initialZoomDistance;
            serialized.ApplyModifiedPropertiesWithoutUndo();

            return camera;
        }

        private static void BuildInteraction(
            GameObject cameraRig,
            UnityEngine.Camera camera,
            UnityEngine.Terrain[] terrains,
            TerrainCollider[] terrainColliders,
            GridDisplay gridDisplay,
            GridCellHighlight highlight,
            GridDisplay[] surfaceOverlayDisplays,
            ResourceCandidateMarkers resourceMarkers)
        {
            var controller = cameraRig.GetComponent<WorldInteractionController>();
            if (controller == null)
            {
                controller = cameraRig.AddComponent<WorldInteractionController>();
            }

            var serialized = new SerializedObject(controller);
            serialized.FindProperty("_camera").objectReferenceValue = camera;
            SetObjectArray(serialized, "_terrains", terrains);
            SetObjectArray(serialized, "_terrainColliders", terrainColliders);
            SetObjectArray(serialized, "_surfaceOverlayDisplays", surfaceOverlayDisplays);
            serialized.FindProperty("_resourceMarkers").objectReferenceValue = resourceMarkers;
            serialized.FindProperty("_cellSize").floatValue = CellSize;
            serialized.FindProperty("_gridOrigin").vector2Value = Vector2.zero;
            serialized.FindProperty("_highlight").objectReferenceValue = highlight;
            serialized.FindProperty("_gridDisplay").objectReferenceValue = gridDisplay;
            serialized.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void SetObjectArray(SerializedObject serialized, string propertyName, Object[] values)
        {
            var property = serialized.FindProperty(propertyName);
            property.arraySize = values.Length;
            for (var i = 0; i < values.Length; i++)
            {
                property.GetArrayElementAtIndex(i).objectReferenceValue = values[i];
            }
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
