using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using Varynth.Tooling.Editor.WorldPrototype;
using Varynth.World.Roads;

namespace Varynth.Tests.EditMode.Editor
{
    /// <summary>
    /// Exercises the real Editor scene builder directly (not just its output via
    /// manual batchmode runs) -- confirms the configured island count is produced
    /// and that a second run against identical inputs creates no new assets under
    /// the fixed generated-asset folder (idempotency).
    /// </summary>
    public class WorldPrototypeSceneBuilderTests
    {
        [Test]
        public void Build_CreatesConfiguredIslandCount()
        {
            WorldPrototypeSceneBuilder.Build();

            var islandsRoot = GameObject.Find("World/Islands");
            Assert.IsNotNull(islandsRoot, "Expected a World/Islands root after Build().");

            var expectedCount = WorldPrototypeIslands.GetDefaultConfigs().Count;
            Assert.AreEqual(expectedCount, islandsRoot.transform.childCount);
        }

        [Test]
        public void Build_SecondRunIsIdempotent_NoNewAssetsCreated()
        {
            WorldPrototypeSceneBuilder.Build();
            var beforeGuids = AssetDatabase.FindAssets("t:Object", new[] { "Assets/Game/World/Art" });

            WorldPrototypeSceneBuilder.Build();
            var afterGuids = AssetDatabase.FindAssets("t:Object", new[] { "Assets/Game/World/Art" });

            CollectionAssert.AreEquivalent(beforeGuids, afterGuids);
        }

        [Test]
        public void PlacementGridOffset_StaysStrictlyBelowRoadRenderClearance()
        {
            // The Placement Grid overlay is transparent/ZWrite=0 (see SetupTransparent),
            // so it can only ever lose the depth test against the road's opaque
            // surface -- and only if it never sits at or above the road's own render
            // offset. Both values must come from the same documented relationship
            // rather than independently hand-picked magic numbers (see
            // RoadVisualConfig's doc comment for the full "turquoise notch" history).
            Assert.Less(WorldPrototypeSceneBuilder.PlacementOverlayHeightOffset, RoadVisualConfig.RenderClearance);
        }
    }
}
