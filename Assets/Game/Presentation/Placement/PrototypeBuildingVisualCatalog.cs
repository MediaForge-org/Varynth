using System;
using System.Collections.Generic;
using UnityEngine;

namespace Varynth.Presentation.Placement
{
    /// <summary>
    /// Presentation-side lookup from BuildingDefinition.PrototypeVisualId (a plain
    /// string, deliberately not a Unity reference on the definition itself) to the
    /// actual blockout mesh/material to show -- used for both the ghost preview and
    /// real placed instances. Built once by the scene builder, fixed small list.
    /// </summary>
    [Serializable]
    public sealed class PrototypeBuildingVisualEntry
    {
        public string VisualId;
        public Mesh Mesh;
        public Material Material;
    }

    public sealed class PrototypeBuildingVisualCatalog : MonoBehaviour
    {
        [SerializeField] private List<PrototypeBuildingVisualEntry> _entries = new List<PrototypeBuildingVisualEntry>();

        public bool TryGetVisual(string visualId, out Mesh mesh, out Material material)
        {
            foreach (var entry in _entries)
            {
                if (entry.VisualId == visualId)
                {
                    mesh = entry.Mesh;
                    material = entry.Material;
                    return true;
                }
            }

            mesh = null;
            material = null;
            return false;
        }
    }
}
