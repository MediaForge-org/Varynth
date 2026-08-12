using System.IO;
using NUnit.Framework;
using UnityEngine;
using Varynth.Core.Common;
using Varynth.Data.Loading;

namespace Varynth.Tests.EditMode.Data
{
    // End-to-end regression test against the real on-disk prototype content file
    // (Assets/StreamingAssets/Content/Roads/) -- not just unit-level mocks.
    public class RoadContentBootstrapTests
    {
        [Test]
        public void LoadRegistry_RealPrototypeContentFile_ProducesExpectedDefinition()
        {
            var contentRoot = Path.Combine(Application.dataPath, "StreamingAssets", "Content", "Roads");

            var registry = RoadContentBootstrap.LoadRegistry(contentRoot);

            Assert.AreEqual(1, registry.Count);
            Assert.IsTrue(registry.TryGet(ContentId.Parse("road.prototype.basic"), out var road));
            Assert.AreEqual(1, road.LogicalWidthCells);
            Assert.IsTrue(road.AllowsDiagonalSegments);
            Assert.IsFalse(road.AllowsCoastPlacement);
        }
    }
}
