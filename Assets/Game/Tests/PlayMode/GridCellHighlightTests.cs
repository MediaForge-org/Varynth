using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Varynth.Core.Common;
using Varynth.Presentation.Visualization;
using Varynth.World.Grid;
using Varynth.World.Terrain;

namespace Varynth.Tests.PlayMode
{
    public class GridCellHighlightTests
    {
        private const string SceneName = "WorldPrototype";

        private static CompositeWorldHeightSource BuildCompositeHeightSource()
        {
            var terrains = Object.FindObjectsByType<UnityEngine.Terrain>(FindObjectsSortMode.None);
            var sources = new List<UnityTerrainHeightSource>();
            foreach (var terrain in terrains)
            {
                sources.Add(new UnityTerrainHeightSource(terrain));
            }
            return new CompositeWorldHeightSource(sources);
        }

        [UnityTest]
        public IEnumerator SetCell_ShowsMeshConformingToTerrain_ThenHideDisablesIt()
        {
            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;

            var highlight = Object.FindFirstObjectByType<GridCellHighlight>();
            Assert.IsNotNull(highlight);
            var meshRenderer = highlight.GetComponent<MeshRenderer>();
            var meshFilter = highlight.GetComponent<MeshFilter>();
            Assert.IsNotNull(meshRenderer);
            Assert.IsNotNull(meshFilter);

            // WorldInteractionController is live in this scene and may already have hovered a
            // cell by this frame (the real hover pipeline working as intended) -- establish a
            // known baseline explicitly rather than assuming an untouched post-load state.
            highlight.Hide();
            Assert.IsFalse(meshRenderer.enabled);

            var heightSource = BuildCompositeHeightSource();
            var grid = new WorldGrid(4f, (0f, 0f));

            // Cell (3,3) -> world (14,14), inside TestIsland_Large's footprint.
            highlight.SetCell(new GridCoordinate(3, 3), grid, heightSource);

            Assert.IsTrue(meshRenderer.enabled);
            Assert.IsNotNull(meshFilter.sharedMesh);
            Assert.AreEqual(4, meshFilter.sharedMesh.vertexCount, "Expected a single quad built from the four cell corners.");

            highlight.Hide();

            Assert.IsFalse(meshRenderer.enabled);
        }

        [UnityTest]
        public IEnumerator SetCell_SwitchingBetweenIslands_RebuildsMeshEachTime()
        {
            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;

            var highlight = Object.FindFirstObjectByType<GridCellHighlight>();
            var meshFilter = highlight.GetComponent<MeshFilter>();
            var heightSource = BuildCompositeHeightSource();
            var grid = new WorldGrid(4f, (0f, 0f));

            // Cell inside TestIsland_Large (center world (0,0)).
            highlight.SetCell(new GridCoordinate(3, 3), grid, heightSource);
            var meshOnLarge = meshFilter.sharedMesh;
            Assert.IsNotNull(meshOnLarge);

            // Cell inside TestIsland_Medium (center world (520,40)).
            highlight.SetCell(new GridCoordinate(130, 10), grid, heightSource);
            var meshOnMedium = meshFilter.sharedMesh;
            Assert.IsNotNull(meshOnMedium);

            // A real cell change must produce a rebuilt mesh instance, not the same one reused.
            Assert.AreNotSame(meshOnLarge, meshOnMedium);

            // Setting the exact same cell again must NOT rebuild (early-return on unchanged cell).
            highlight.SetCell(new GridCoordinate(130, 10), grid, heightSource);
            Assert.AreSame(meshOnMedium, meshFilter.sharedMesh);
        }
    }
}
