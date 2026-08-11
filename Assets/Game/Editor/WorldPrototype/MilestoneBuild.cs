using System.IO;
using UnityEditor;
using UnityEngine;

namespace Varynth.Tooling.Editor.WorldPrototype
{
    /// <summary>
    /// Builds a Linux x86_64 standalone of the current milestone. Fully
    /// version-neutral: BuildCurrentVersion() only *reads* PlayerSettings.bundleVersion
    /// and derives the output path from it -- it never sets/hardcodes a version. This
    /// file is not renamed/duplicated per milestone (was Version0_1_0Build.cs for
    /// 0.1.0, renamed to MilestoneBuild.cs from 0.1.1 onward, .meta moved with it so
    /// the asset GUID stayed stable); the same unmodified class builds 0.1.2, 0.2.0,
    /// 1.0.0, etc. without any code change. Editor-only, batchmode-callable via
    /// -executeMethod. Never commits/pushes -- only writes into the ignored Builds/ folder.
    /// </summary>
    public static class MilestoneBuild
    {
        private const string ExecutableName = "Varynth";

        [MenuItem("Varynth/Build Current Milestone (Linux x86_64)")]
        public static void BuildCurrentVersion()
        {
            var version = PlayerSettings.bundleVersion;
            var outputDirectory = $"Builds/Varynth-{version}-linux-x64";

            Directory.CreateDirectory(outputDirectory);

            var buildOptions = new BuildPlayerOptions
            {
                scenes = new[] { WorldPrototypeSceneBuilder.ScenePath },
                locationPathName = Path.Combine(outputDirectory, ExecutableName),
                target = BuildTarget.StandaloneLinux64,
                options = BuildOptions.None
            };

            var report = BuildPipeline.BuildPlayer(buildOptions);

            if (report.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded)
            {
                throw new BuildFailedException($"Varynth {version} build failed: {report.summary.result} ({report.summary.totalErrors} errors).");
            }

            Debug.Log($"Varynth {version} built successfully at {outputDirectory} ({report.summary.totalSize} bytes).");
        }

        private sealed class BuildFailedException : System.Exception
        {
            public BuildFailedException(string message) : base(message)
            {
            }
        }
    }
}
