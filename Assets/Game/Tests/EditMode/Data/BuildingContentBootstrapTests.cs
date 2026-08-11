using System.IO;
using NUnit.Framework;
using UnityEngine;
using Varynth.Core.Common;
using Varynth.Data.Loading;

namespace Varynth.Tests.EditMode.Data
{
    // End-to-end regression test against the real on-disk prototype content files
    // (Assets/StreamingAssets/Content/Buildings/) -- not just unit-level mocks.
    public class BuildingContentBootstrapTests
    {
        [Test]
        public void LoadRegistry_RealPrototypeContentFiles_ProducesExpectedDefinitions()
        {
            var contentRoot = Path.Combine(Application.dataPath, "StreamingAssets", "Content", "Buildings");

            var registry = BuildingContentBootstrap.LoadRegistry(contentRoot);

            Assert.AreEqual(3, registry.Count);
            Assert.IsTrue(registry.TryGet(ContentId.Parse("bld.prototype.house"), out var house));
            Assert.AreEqual(2, house.FootprintWidth);
            Assert.AreEqual(2, house.FootprintLength);

            Assert.IsTrue(registry.TryGet(ContentId.Parse("bld.prototype.production_block"), out var production));
            Assert.AreEqual(3, production.FootprintWidth);
            Assert.AreEqual(2, production.FootprintLength);

            Assert.IsTrue(registry.TryGet(ContentId.Parse("bld.prototype.public_building"), out var publicBuilding));
            Assert.AreEqual(4, publicBuilding.FootprintWidth);
            Assert.AreEqual(3, publicBuilding.FootprintLength);
        }
    }
}
