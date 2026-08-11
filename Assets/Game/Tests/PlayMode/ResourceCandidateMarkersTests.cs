using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Varynth.Presentation.Visualization;

namespace Varynth.Tests.PlayMode
{
    public class ResourceCandidateMarkersTests
    {
        private const string SceneName = "WorldPrototype";

        // Sum of IslandPrototypeConfig.MaxResourceCandidates across
        // WorldPrototypeIslands.GetDefaultConfigs() (5 + 3 + 2 + 3).
        private const int MaxTotalMarkers = 13;

        [UnityTest]
        public IEnumerator MarkerCount_IsWithinConfiguredCap()
        {
            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;

            var markers = Object.FindFirstObjectByType<ResourceCandidateMarkers>();
            Assert.IsNotNull(markers);

            Assert.LessOrEqual(markers.Markers.Count, MaxTotalMarkers);
            Assert.Greater(markers.Markers.Count, 0, "Expected at least one resource candidate marker across all islands.");
        }

        [UnityTest]
        public IEnumerator NoMarker_IsPositionedInWater()
        {
            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;

            var markers = Object.FindFirstObjectByType<ResourceCandidateMarkers>();

            foreach (var marker in markers.Markers)
            {
                Assert.IsNotNull(marker);
                Assert.Greater(marker.transform.position.y, 0f, $"{marker.name} is at or below sea level.");
            }
        }

        [UnityTest]
        public IEnumerator SetVisible_TogglesAllMarkers()
        {
            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;

            var markers = Object.FindFirstObjectByType<ResourceCandidateMarkers>();

            markers.SetVisible(true);
            foreach (var marker in markers.Markers)
            {
                Assert.IsTrue(marker.activeSelf);
            }

            markers.SetVisible(false);
            foreach (var marker in markers.Markers)
            {
                Assert.IsFalse(marker.activeSelf);
            }
        }
    }
}
