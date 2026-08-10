using UnityEngine;

namespace Varynth.Presentation.Visualization
{
    /// <summary>
    /// Owns and displays the debug grid mesh. The mesh itself is built once
    /// elsewhere (Varynth.World.Grid.GridMeshBuilder, invoked by the Editor scene
    /// builder against a fixed asset path) and handed in here -- this component
    /// only displays it, never rebuilds it per frame. No input read here --
    /// visibility is driven externally (WorldInteractionController) via SetVisible.
    /// </summary>
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public sealed class GridDisplay : MonoBehaviour
    {
        [SerializeField] private Material _material;

        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;

        // Initialize() only ever runs once, at Editor/scene-build time (never again at actual
        // Play Mode start) -- Awake() re-fetches the components so SetVisible still has valid
        // references after a real scene load. Without this, the G-toggle silently no-ops
        // because _meshRenderer stays null at runtime.
        private void Awake()
        {
            EnsureComponents();
        }

        public void Initialize(Mesh mesh)
        {
            EnsureComponents();

            _meshFilter.sharedMesh = mesh;

            if (_material != null)
            {
                _meshRenderer.sharedMaterial = _material;
            }
        }

        public void SetVisible(bool visible)
        {
            EnsureComponents();
            _meshRenderer.enabled = visible;
        }

        private void EnsureComponents()
        {
            if (_meshFilter == null)
            {
                _meshFilter = GetComponent<MeshFilter>();
            }

            if (_meshRenderer == null)
            {
                _meshRenderer = GetComponent<MeshRenderer>();
            }
        }
    }
}
