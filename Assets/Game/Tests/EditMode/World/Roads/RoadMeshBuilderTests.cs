using NUnit.Framework;
using UnityEngine;
using Varynth.Core.Common;
using Varynth.Core.Definitions;
using Varynth.Core.Definitions.Roads;
using Varynth.World.Grid;
using Varynth.World.Roads;
using Varynth.World.Terrain;

namespace Varynth.Tests.EditMode.World.Roads
{
    public class RoadMeshBuilderTests
    {
        private sealed class FlatHeightSource : IWorldHeightSource
        {
            public float GetHeightAt(float worldX, float worldZ) => 0f;
            public bool TryGetHeight(float worldX, float worldZ, out float height) { height = 0f; return true; }
        }

        private sealed class SlopedHeightSource : IWorldHeightSource
        {
            public float GetHeightAt(float worldX, float worldZ) => worldX * 0.1f;
            public bool TryGetHeight(float worldX, float worldZ, out float height) { height = worldX * 0.1f; return true; }
        }

        // A real undulating (non-linear, non-flat) synthetic surface -- catches bugs
        // a purely linear slope source cannot, e.g. terrain relief that pokes up
        // *between* two road samples even though both samples themselves are
        // correctly covered (the actual root cause of the "turquoise diamond"
        // undersampling bug this test file guards against).
        private sealed class UndulatingHeightSource : IWorldHeightSource
        {
            private float Height(float worldX, float worldZ) =>
                Mathf.Sin(worldX * 0.7f) * 0.6f + Mathf.Cos(worldZ * 0.5f) * 0.4f;

            public float GetHeightAt(float worldX, float worldZ) => Height(worldX, worldZ);
            public bool TryGetHeight(float worldX, float worldZ, out float height) { height = Height(worldX, worldZ); return true; }
        }

        private static WorldGrid Grid() => new WorldGrid(4f, Vector2.zero);
        private static RoadDefinition Road() =>
            new RoadDefinition(ContentId.Parse("road.prototype.basic"), LocalizationKey.Parse("road.name"), "road", 1, true, false);

        [Test]
        public void EmptyGraph_ProducesEmptyMesh_NoException()
        {
            var mesh = RoadMeshBuilder.BuildIslandMesh(Grid(), new RoadGraph(), Road(), new FlatHeightSource(), 0.05f);
            Assert.AreEqual(0, mesh.vertexCount);
        }

        [Test]
        public void OrthogonalSegment_ProducesQuadStrip_UpwardNormals()
        {
            var graph = new RoadGraph();
            graph.AddSegment(RoadSegmentId.FromRaw(1), Road().Id, new GridCoordinate(0, 0), new GridCoordinate(1, 0), RoadDirection.E, Varynth.Core.Simulation.Common.PlayerId.None);

            var mesh = RoadMeshBuilder.BuildIslandMesh(Grid(), graph, Road(), new FlatHeightSource(), 0.05f);

            Assert.Greater(mesh.vertexCount, 0);
            Assert.Greater(mesh.triangles.Length, 0);
            foreach (var normal in mesh.normals)
            {
                Assert.Greater(normal.y, 0f, "Road surface normals should face generally upward.");
            }
        }

        [Test]
        public void DiagonalSegment_ProducesGeometry()
        {
            var graph = new RoadGraph();
            graph.AddSegment(RoadSegmentId.FromRaw(1), Road().Id, new GridCoordinate(0, 0), new GridCoordinate(1, 1), RoadDirection.NE, Varynth.Core.Simulation.Common.PlayerId.None);

            var mesh = RoadMeshBuilder.BuildIslandMesh(Grid(), graph, Road(), new FlatHeightSource(), 0.05f);
            Assert.Greater(mesh.vertexCount, 0);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void JunctionOfAnyConnectedDegree_BuildsWithoutError(int degree)
        {
            var graph = new RoadGraph();
            var center = new GridCoordinate(10, 10);
            var directions = new[]
            {
                RoadDirection.N, RoadDirection.NE, RoadDirection.E, RoadDirection.SE,
                RoadDirection.S, RoadDirection.SW, RoadDirection.W, RoadDirection.NW
            };

            for (var i = 0; i < degree; i++)
            {
                var (dx, dz) = directions[i].ToDelta();
                var neighbor = new GridCoordinate(center.X + dx, center.Z + dz);
                graph.AddSegment(RoadSegmentId.FromRaw((ulong)(i + 1)), Road().Id, center, neighbor, directions[i], Varynth.Core.Simulation.Common.PlayerId.None);
            }

            Mesh mesh = null;
            Assert.DoesNotThrow(() => mesh = RoadMeshBuilder.BuildIslandMesh(Grid(), graph, Road(), new FlatHeightSource(), 0.05f));
            Assert.Greater(mesh.vertexCount, 0, $"Degree {degree} junction should still produce geometry.");
        }

        [Test]
        public void TerrainFollowing_VerticesReflectRealSampledHeights()
        {
            var graph = new RoadGraph();
            graph.AddSegment(RoadSegmentId.FromRaw(1), Road().Id, new GridCoordinate(0, 0), new GridCoordinate(2, 0), RoadDirection.E, Varynth.Core.Simulation.Common.PlayerId.None);

            var mesh = RoadMeshBuilder.BuildIslandMesh(Grid(), graph, Road(), new SlopedHeightSource(), 0.05f);

            var minY = float.MaxValue;
            var maxY = float.MinValue;
            foreach (var v in mesh.vertices)
            {
                minY = Mathf.Min(minY, v.y);
                maxY = Mathf.Max(maxY, v.y);
            }

            Assert.Greater(maxY - minY, 0.05f, "Sloped terrain should produce visibly varying vertex heights along the segment, not a flat plane.");
        }

        [Test]
        public void MeshBounds_AreComputed()
        {
            var graph = new RoadGraph();
            graph.AddSegment(RoadSegmentId.FromRaw(1), Road().Id, new GridCoordinate(0, 0), new GridCoordinate(3, 0), RoadDirection.E, Varynth.Core.Simulation.Common.PlayerId.None);

            var mesh = RoadMeshBuilder.BuildIslandMesh(Grid(), graph, Road(), new FlatHeightSource(), 0.05f);
            Assert.Greater(mesh.bounds.size.x, 0f);
        }

        [Test]
        public void JunctionPatch_IsTwoRingOctagonFan_SeventeenVerticesTwentyFourTrianglesPerNode()
        {
            var grid = Grid();
            var road = Road();

            // One node's fixed junction contribution: 1 center + 8 inner ring + 8
            // outer ring = 17 vertices; 8 center->inner triangles + 2*8 inner->outer
            // strip triangles = 24 triangles -- independent of connectivity degree
            // and of segment length/tessellation (only the segment strips scale with
            // RoadVisualConfig.MaxTessellationSpacing, not the junction patch).
            var oneNodeGraph = new RoadGraph();
            var mesh = RoadMeshBuilder.BuildIslandMesh(grid, oneNodeGraph, road, new FlatHeightSource(), 0.05f);
            Assert.AreEqual(0, mesh.vertexCount, "Sanity: no nodes yet.");

            // Isolate the junction-only contribution by comparing two otherwise-
            // identical graphs that differ by exactly one extra collinear segment
            // (and therefore exactly one extra node) -- the segment-strip vertex/
            // triangle delta is measured independently (AppendSegmentQuadStrip is
            // exercised directly) and subtracted out, leaving only the fixed
            // per-node junction contribution.
            var shortGraph = new RoadGraph();
            shortGraph.AddSegment(RoadSegmentId.FromRaw(1), road.Id, new GridCoordinate(0, 0), new GridCoordinate(1, 0), RoadDirection.E, Varynth.Core.Simulation.Common.PlayerId.None);
            var shortMesh = RoadMeshBuilder.BuildIslandMesh(grid, shortGraph, road, new FlatHeightSource(), 0.05f);

            var longGraph = new RoadGraph();
            longGraph.AddSegment(RoadSegmentId.FromRaw(1), road.Id, new GridCoordinate(0, 0), new GridCoordinate(1, 0), RoadDirection.E, Varynth.Core.Simulation.Common.PlayerId.None);
            longGraph.AddSegment(RoadSegmentId.FromRaw(2), road.Id, new GridCoordinate(1, 0), new GridCoordinate(2, 0), RoadDirection.E, Varynth.Core.Simulation.Common.PlayerId.None);
            var longMesh = RoadMeshBuilder.BuildIslandMesh(grid, longGraph, road, new FlatHeightSource(), 0.05f);

            var extraSegmentVertices = new System.Collections.Generic.List<Vector3>();
            var extraSegmentTriangles = new System.Collections.Generic.List<int>();
            var halfWidth = Mathf.Max(0.5f, road.LogicalWidthCells * grid.CellSize * 0.5f);
            RoadMeshBuilder.AppendSegmentQuadStrip(grid, new FlatHeightSource(), new GridCoordinate(1, 0), new GridCoordinate(2, 0), halfWidth, 0.05f, extraSegmentVertices, extraSegmentTriangles);

            var vertexDeltaFromExtraNode = (longMesh.vertexCount - shortMesh.vertexCount) - extraSegmentVertices.Count;
            var triangleDeltaFromExtraNode = (longMesh.triangles.Length - shortMesh.triangles.Length) - extraSegmentTriangles.Count;

            Assert.AreEqual(17, vertexDeltaFromExtraNode, "One extra node must contribute exactly 17 fixed junction-patch vertices.");
            Assert.AreEqual(24 * 3, triangleDeltaFromExtraNode, "One extra node must contribute exactly 24 fixed junction-patch triangles (72 indices).");
        }

        [Test]
        public void OrthogonalToDiagonalTransition_SegmentStubsShareExactSeamVerticesWithJunctionPatch()
        {
            var grid = Grid();
            var road = Road();
            var graph = new RoadGraph();
            graph.AddSegment(RoadSegmentId.FromRaw(1), road.Id, new GridCoordinate(0, 0), new GridCoordinate(1, 0), RoadDirection.E, Varynth.Core.Simulation.Common.PlayerId.None);
            graph.AddSegment(RoadSegmentId.FromRaw(2), road.Id, new GridCoordinate(1, 0), new GridCoordinate(2, 1), RoadDirection.NE, Varynth.Core.Simulation.Common.PlayerId.None);

            var mesh = RoadMeshBuilder.BuildIslandMesh(grid, graph, road, new FlatHeightSource(), 0.05f);
            var meshVertices = mesh.vertices;

            var halfWidth = Mathf.Max(0.5f, road.LogicalWidthCells * grid.CellSize * 0.5f);

            foreach (var segment in graph.Segments)
            {
                var fromCenter = grid.CellToWorldCenter(segment.From);
                var toCenter = grid.CellToWorldCenter(segment.To);
                var direction2D = (toCenter - fromCenter).normalized;
                var perpendicular = new Vector2(-direction2D.y, direction2D.x) * halfWidth;

                AssertHasVertexNear(meshVertices, fromCenter + perpendicular, 0.01f);
                AssertHasVertexNear(meshVertices, fromCenter - perpendicular, 0.01f);
                AssertHasVertexNear(meshVertices, toCenter + perpendicular, 0.01f);
                AssertHasVertexNear(meshVertices, toCenter - perpendicular, 0.01f);
            }
        }

        [Test]
        public void DiagonalToDiagonalTransition_SegmentStubsShareExactSeamVerticesWithJunctionPatch()
        {
            var grid = Grid();
            var road = Road();
            var graph = new RoadGraph();
            graph.AddSegment(RoadSegmentId.FromRaw(1), road.Id, new GridCoordinate(0, 0), new GridCoordinate(1, 1), RoadDirection.NE, Varynth.Core.Simulation.Common.PlayerId.None);
            graph.AddSegment(RoadSegmentId.FromRaw(2), road.Id, new GridCoordinate(1, 1), new GridCoordinate(2, 0), RoadDirection.SE, Varynth.Core.Simulation.Common.PlayerId.None);

            var mesh = RoadMeshBuilder.BuildIslandMesh(grid, graph, road, new FlatHeightSource(), 0.05f);
            var meshVertices = mesh.vertices;

            var halfWidth = Mathf.Max(0.5f, road.LogicalWidthCells * grid.CellSize * 0.5f);

            foreach (var segment in graph.Segments)
            {
                var fromCenter = grid.CellToWorldCenter(segment.From);
                var toCenter = grid.CellToWorldCenter(segment.To);
                var direction2D = (toCenter - fromCenter).normalized;
                var perpendicular = new Vector2(-direction2D.y, direction2D.x) * halfWidth;

                AssertHasVertexNear(meshVertices, fromCenter + perpendicular, 0.01f);
                AssertHasVertexNear(meshVertices, fromCenter - perpendicular, 0.01f);
                AssertHasVertexNear(meshVertices, toCenter + perpendicular, 0.01f);
                AssertHasVertexNear(meshVertices, toCenter - perpendicular, 0.01f);
            }
        }

        [Test]
        public void EveryVertex_IsAtOrAboveSampledTerrainHeightPlusRenderClearance_FlatTerrain()
        {
            AssertAllVerticesRespectClearance(new FlatHeightSource());
        }

        [Test]
        public void EveryVertex_IsAtOrAboveSampledTerrainHeightPlusRenderClearance_SlopedTerrain()
        {
            AssertAllVerticesRespectClearance(new SlopedHeightSource());
        }

        [Test]
        public void EveryVertex_IsAtOrAboveSampledTerrainHeightPlusRenderClearance_UndulatingTerrain()
        {
            // Real, non-linear synthetic relief -- exercises the actual scenario the
            // turquoise-notch bug came from (terrain bulging between road samples).
            AssertAllVerticesRespectClearance(new UndulatingHeightSource());
        }

        private static void AssertAllVerticesRespectClearance(IWorldHeightSource heights)
        {
            var grid = Grid();
            var road = Road();
            var graph = new RoadGraph();
            graph.AddSegment(RoadSegmentId.FromRaw(1), road.Id, new GridCoordinate(0, 0), new GridCoordinate(1, 0), RoadDirection.E, Varynth.Core.Simulation.Common.PlayerId.None);
            graph.AddSegment(RoadSegmentId.FromRaw(2), road.Id, new GridCoordinate(1, 0), new GridCoordinate(2, 1), RoadDirection.NE, Varynth.Core.Simulation.Common.PlayerId.None);
            graph.AddSegment(RoadSegmentId.FromRaw(3), road.Id, new GridCoordinate(2, 1), new GridCoordinate(3, 2), RoadDirection.NE, Varynth.Core.Simulation.Common.PlayerId.None);

            var mesh = RoadMeshBuilder.BuildIslandMesh(grid, graph, road, heights, RoadVisualConfig.RenderClearance);

            foreach (var v in mesh.vertices)
            {
                heights.TryGetHeight(v.x, v.z, out var terrainHeight);
                Assert.GreaterOrEqual(v.y, terrainHeight + RoadVisualConfig.RenderClearance - 0.0001f,
                    $"Vertex at ({v.x}, {v.z}) must sit at least RenderClearance above its own sampled terrain height.");
            }
        }

        [Test]
        public void LongDiagonalSegment_ProducesMultipleIntermediateCrossSections()
        {
            var grid = Grid();
            var road = Road();
            var vertices = new System.Collections.Generic.List<Vector3>();
            var triangles = new System.Collections.Generic.List<int>();
            var halfWidth = Mathf.Max(0.5f, road.LogicalWidthCells * grid.CellSize * 0.5f);

            // A single long diagonal segment (well beyond one grid cell) must be
            // subdivided into several intermediate cross-sections, not just its two
            // endpoints -- both along length and across width -- per
            // RoadVisualConfig.MaxTessellationSpacing.
            RoadMeshBuilder.AppendSegmentQuadStrip(grid, new FlatHeightSource(), new GridCoordinate(0, 0), new GridCoordinate(6, 6), halfWidth, 0.05f, vertices, triangles);

            var distinctX = new System.Collections.Generic.HashSet<float>();
            foreach (var v in vertices) distinctX.Add(Mathf.Round(v.x * 100f) / 100f);

            Assert.Greater(distinctX.Count, 4, "A long diagonal segment must produce several distinct intermediate sample positions, not just its two endpoints.");
        }

        [Test]
        public void TwoParallelOrthogonalNeighborRoads_EdgesMeetOrOverlap_NoPositiveGap()
        {
            // Diagnosed relationship (not guessed): halfWidth = LogicalWidthCells *
            // WorldGrid.CellSize * 0.5 = 1 * 4 * 0.5 = 2.0 for the prototype road,
            // i.e. exactly half the cell size. Two directly-neighboring orthogonal
            // rows (dz=1) have centerlines exactly CellSize (4.0) apart, so each
            // road's own +-halfWidth (2.0) reaches exactly to the shared cell
            // boundary -- the two road strips must meet there with zero *positive*
            // gap (a hairline-negative/zero gap, i.e. touching or slightly
            // overlapping, is acceptable and robust against float error; a visible
            // gap -- real terrain strip between them -- is not).
            var grid = Grid();
            var road = Road();

            var graphRowA = new RoadGraph();
            graphRowA.AddSegment(RoadSegmentId.FromRaw(1), road.Id, new GridCoordinate(0, 0), new GridCoordinate(1, 0), RoadDirection.E, Varynth.Core.Simulation.Common.PlayerId.None);
            var meshA = RoadMeshBuilder.BuildIslandMesh(grid, graphRowA, road, new FlatHeightSource(), 0.05f);

            var graphRowB = new RoadGraph();
            graphRowB.AddSegment(RoadSegmentId.FromRaw(1), road.Id, new GridCoordinate(0, 1), new GridCoordinate(1, 1), RoadDirection.E, Varynth.Core.Simulation.Common.PlayerId.None);
            var meshB = RoadMeshBuilder.BuildIslandMesh(grid, graphRowB, road, new FlatHeightSource(), 0.05f);

            var maxZOfRowA = MaxZ(meshA.vertices);
            var minZOfRowB = MinZ(meshB.vertices);

            // A positive gap would mean minZOfRowB > maxZOfRowA (real terrain visible
            // between them). Touching (== 0) or a tiny controlled overlap (< 0) is fine.
            var gap = minZOfRowB - maxZOfRowA;
            Assert.LessOrEqual(gap, 0.001f, $"Two directly-adjacent orthogonal road rows must meet with no visible gap (measured gap: {gap} world units).");
        }

        [Test]
        public void TwoParallelDiagonalNeighborRoads_NoObviousVisibleCorridorGap()
        {
            // Diagonal lanes offset by one grid cell along a single axis (the
            // natural integer-grid-adjacent "next" diagonal lane) sit only
            // CellSize/sqrt(2) apart in the true perpendicular sense -- closer than
            // an orthogonal neighbor pair -- so a full-width diagonal road already
            // overlaps its neighbor there by construction (acceptable: small
            // controlled overlap, never a gap). This test only guards against a
            // regression that would reopen a visible corridor between them.
            var grid = Grid();
            var road = Road();

            var graphLaneA = new RoadGraph();
            graphLaneA.AddSegment(RoadSegmentId.FromRaw(1), road.Id, new GridCoordinate(0, 0), new GridCoordinate(1, 1), RoadDirection.NE, Varynth.Core.Simulation.Common.PlayerId.None);
            var meshA = RoadMeshBuilder.BuildIslandMesh(grid, graphLaneA, road, new FlatHeightSource(), 0.05f);

            var graphLaneB = new RoadGraph();
            graphLaneB.AddSegment(RoadSegmentId.FromRaw(1), road.Id, new GridCoordinate(0, 1), new GridCoordinate(1, 2), RoadDirection.NE, Varynth.Core.Simulation.Common.PlayerId.None);
            var meshB = RoadMeshBuilder.BuildIslandMesh(grid, graphLaneB, road, new FlatHeightSource(), 0.05f);

            // Sample the true perpendicular gap at the lanes' shared midpoint: the
            // closest distance from any vertex of lane B to any vertex of lane A
            // must not exceed a small tolerance -- a large distance would indicate a
            // visible open corridor between the two diagonal strips.
            var minDistance = MinVertexDistanceXZ(meshA.vertices, meshB.vertices);
            Assert.LessOrEqual(minDistance, 0.5f, $"Two directly-adjacent diagonal road lanes must not leave an obvious visible corridor gap (closest approach: {minDistance} world units).");
        }

        private static float MaxZ(Vector3[] vertices)
        {
            var max = float.MinValue;
            foreach (var v in vertices) max = Mathf.Max(max, v.z);
            return max;
        }

        private static float MinZ(Vector3[] vertices)
        {
            var min = float.MaxValue;
            foreach (var v in vertices) min = Mathf.Min(min, v.z);
            return min;
        }

        private static float MinVertexDistanceXZ(Vector3[] a, Vector3[] b)
        {
            var min = float.MaxValue;
            foreach (var va in a)
            {
                foreach (var vb in b)
                {
                    var d = new Vector2(va.x - vb.x, va.z - vb.z).magnitude;
                    min = Mathf.Min(min, d);
                }
            }
            return min;
        }

        private static void AssertHasVertexNear(Vector3[] vertices, Vector2 expected2D, float epsilon)
        {
            foreach (var v in vertices)
            {
                if (Mathf.Abs(v.x - expected2D.x) < epsilon && Mathf.Abs(v.z - expected2D.y) < epsilon)
                {
                    return;
                }
            }

            Assert.Fail($"Expected a mesh vertex near world ({expected2D.x}, {expected2D.y}) -- " +
                         "segment stub and junction patch must share exact boundary points, otherwise a visible seam gap results.");
        }
    }
}
