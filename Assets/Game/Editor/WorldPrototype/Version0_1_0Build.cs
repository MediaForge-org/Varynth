using System.IO;
using UnityEditor;
using UnityEngine;

namespace Varynth.Tooling.Editor.WorldPrototype
{
    /// <summary>
    /// Sets the Varynth 0.1.0 player version and produces the Linux x86_64 standalone
    /// build for the first visual milestone. Editor-only, batchmode-callable via
    /// -executeMethod. Never commits/pushes -- only writes into the ignored Builds/ folder.
    /// </summary>
    public static class Version0_1_0Build
    {
        private const string Version = "0.1.0";
        private const string OutputDirectory = "Builds/Varynth-0.1.0-linux-x64";
        private const string ExecutableName = "Varynth";

        [MenuItem("Varynth/Build Varynth 0.1.0 (Linux x86_64)")]
        public static void Build()
        {
            PlayerSettings.bundleVersion = Version;

            Directory.CreateDirectory(OutputDirectory);

            var buildOptions = new BuildPlayerOptions
            {
                scenes = new[] { WorldPrototypeSceneBuilder.ScenePath },
                locationPathName = Path.Combine(OutputDirectory, ExecutableName),
                target = BuildTarget.StandaloneLinux64,
                options = BuildOptions.None
            };

            var report = BuildPipeline.BuildPlayer(buildOptions);

            if (report.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded)
            {
                throw new BuildFailedException($"Varynth 0.1.0 build failed: {report.summary.result} ({report.summary.totalErrors} errors).");
            }

            Debug.Log($"Varynth 0.1.0 built successfully at {OutputDirectory} ({report.summary.totalSize} bytes).");
        }

        private sealed class BuildFailedException : System.Exception
        {
            public BuildFailedException(string message) : base(message)
            {
            }
        }
    }
}
