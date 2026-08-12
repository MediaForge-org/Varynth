using UnityEngine;

namespace Varynth.Presentation.Roads
{
    /// <summary>
    /// Owns and displays one island's road-network mesh at runtime. Never mutates
    /// the Editor/AssetDatabase-saved mesh asset the scene builder created: exactly
    /// one dedicated runtime Mesh is cloned once (Initialize), assigned to
    /// MeshFilter.sharedMesh once, and reused in place for every subsequent edit
    /// (UpdateGeometry: Clear + SetVertices/SetTriangles on the SAME instance, never
    /// a fresh Mesh allocation per edit, never AssetDatabase at runtime).
    /// </summary>
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public sealed class RoadNetworkDisplay : MonoBehaviour
    {
        [SerializeField] private Material _material;

        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;
        private Mesh _runtimeMesh;

        private void Awake()
        {
            EnsureComponents();
        }

        /// <summary>
        /// Clones sourceMeshOrNull (the scene builder's placeholder asset) exactly
        /// once into a distinct runtime Mesh instance -- the original project asset
        /// is never touched again after this call.
        /// </summary>
        public void Initialize(Mesh sourceMeshOrNull)
        {
            EnsureComponents();

            _runtimeMesh = sourceMeshOrNull != null ? Object.Instantiate(sourceMeshOrNull) : new Mesh();
            _runtimeMesh.name = "RoadNetworkRuntimeMesh";
            _meshFilter.sharedMesh = _runtimeMesh;

            if (_material != null)
            {
                _meshRenderer.sharedMaterial = _material;
            }
        }

        /// <summary>Copies newly-built geometry into the same persistent runtime Mesh instance; destroys the throwaway builder output.</summary>
        public void UpdateGeometry(Mesh builtMesh)
        {
            EnsureComponents();
            if (_runtimeMesh == null)
            {
                Initialize(null);
            }

            _runtimeMesh.Clear();
            _runtimeMesh.SetVertices(builtMesh.vertices);
            _runtimeMesh.SetTriangles(builtMesh.triangles, 0);
            _runtimeMesh.RecalculateNormals();
            _runtimeMesh.RecalculateBounds();

            Object.Destroy(builtMesh);
        }

        public void SetVisible(bool visible)
        {
            EnsureComponents();
            _meshRenderer.enabled = visible;
        }

        /// <summary>Structural test hook: the runtime mesh instance ID, to assert it never equals the original project asset's.</summary>
        public Mesh RuntimeMesh => _runtimeMesh;

        private void EnsureComponents()
        {
            if (_meshFilter == null) _meshFilter = GetComponent<MeshFilter>();
            if (_meshRenderer == null) _meshRenderer = GetComponent<MeshRenderer>();
        }
    }
}
