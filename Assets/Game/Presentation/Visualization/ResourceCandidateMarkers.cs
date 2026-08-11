using System.Collections.Generic;
using UnityEngine;

namespace Varynth.Presentation.Visualization
{
    /// <summary>
    /// Owns a small, fixed set of debug marker GameObjects (one per resource/mine
    /// slot candidate, built once by the scene builder -- not spawned/rebuilt at
    /// runtime). SetVisible toggles them all. Debug-only: these markers are not
    /// buildings and do not represent an actual resource (brief §25/§51).
    /// </summary>
    public sealed class ResourceCandidateMarkers : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _markers = new List<GameObject>();

        public IReadOnlyList<GameObject> Markers => _markers;

        public void SetVisible(bool visible)
        {
            for (var i = 0; i < _markers.Count; i++)
            {
                if (_markers[i] != null)
                {
                    _markers[i].SetActive(visible);
                }
            }
        }
    }
}
