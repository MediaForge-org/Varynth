using System.Collections.Generic;
using NUnit.Framework;
using Varynth.Core.Common;

namespace Varynth.Tests.EditMode
{
    public class ContentIdTests
    {
        [TestCase("res.occidentia.t1.f1")]
        [TestCase("good.meridia.coffee")]
        [TestCase("bld.global.market")]
        [TestCase("ship.australis.heavy.icebreaker")]
        [TestCase("resrch.ultima.aer.01")]
        public void Parse_ValidCoreId_Succeeds(string raw)
        {
            var id = ContentId.Parse(raw);
            Assert.AreEqual(raw, id.ToString());
        }

        [TestCase("mygreatmod.good.blueberry")]
        [TestCase("mygreatmod.bld.special.market")]
        public void Parse_ValidModId_Succeeds(string raw)
        {
            var id = ContentId.Parse(raw);
            Assert.AreEqual(raw, id.ToString());
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        [TestCase("res.Occidentia.t1")]
        [TestCase("res..t1")]
        [TestCase(".res.t1")]
        [TestCase("res.t1.")]
        [TestCase("res t1")]
        [TestCase("res.t1#")]
        [TestCase("singleSegment")]
        [TestCase("single-segment")]
        public void Parse_InvalidId_Throws(string raw)
        {
            Assert.Throws<ContentIdFormatException>(() => ContentId.Parse(raw));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("res.Occidentia.t1")]
        [TestCase("singleSegment")]
        public void TryParse_InvalidId_ReturnsFalseAndDefault(string raw)
        {
            var success = ContentId.TryParse(raw, out var id);

            Assert.IsFalse(success);
            Assert.IsTrue(id.IsDefault);
        }

        [Test]
        public void TryParse_ValidId_ReturnsTrueAndValue()
        {
            var success = ContentId.TryParse("res.occidentia.t1.f1", out var id);

            Assert.IsTrue(success);
            Assert.AreEqual("res.occidentia.t1.f1", id.ToString());
        }

        [Test]
        public void Equality_SameValue_AreEqual()
        {
            var a = ContentId.Parse("good.meridia.coffee");
            var b = ContentId.Parse("good.meridia.coffee");

            Assert.AreEqual(a, b);
            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
        }

        [Test]
        public void Equality_DifferentValue_AreNotEqual()
        {
            var a = ContentId.Parse("good.meridia.coffee");
            var b = ContentId.Parse("good.meridia.tea");

            Assert.AreNotEqual(a, b);
            Assert.IsTrue(a != b);
        }

        [Test]
        public void GetHashCode_SameValue_ProducesSameHash()
        {
            var a = ContentId.Parse("good.meridia.coffee");
            var b = ContentId.Parse("good.meridia.coffee");

            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void CanBeUsedAsDictionaryKey()
        {
            var dict = new Dictionary<ContentId, int>
            {
                [ContentId.Parse("good.meridia.coffee")] = 1
            };

            Assert.IsTrue(dict.ContainsKey(ContentId.Parse("good.meridia.coffee")));
        }
    }
}
