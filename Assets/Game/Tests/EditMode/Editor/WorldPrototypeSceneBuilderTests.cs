using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using Varynth.Tooling.Editor.WorldPrototype;

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
    }
}
