using System.Collections;
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

            var terrain = Object.FindFirstObjectByType<UnityEngine.Terrain>();
            var heightSource = new UnityTerrainHeightSource(terrain);
            var grid = new WorldGrid(4f, Vector2.zero);

            highlight.SetCell(new GridCoordinate(3, 3), grid, heightSource);

            Assert.IsTrue(meshRenderer.enabled);
            Assert.IsNotNull(meshFilter.sharedMesh);
            Assert.AreEqual(4, meshFilter.sharedMesh.vertexCount, "Expected a single quad built from the four cell corners.");

            highlight.Hide();

            Assert.IsFalse(meshRenderer.enabled);
        }
    }
}
