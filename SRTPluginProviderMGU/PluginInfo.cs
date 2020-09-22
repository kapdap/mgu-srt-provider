using SRTPluginBase;
using System;

namespace SRTPluginProviderMGU
{
    public class PluginInfo : IPluginInfo
    {
        public string Name => "Game Memory Provider (Martian Gothic: Unification)";

        public string Description => "A game memory provider plugin for Martian Gothic: Unification.";

        public string Author => "Kapdap";

        public Uri MoreInfoURL => new Uri("https://github.com/kapdap/mgu-srt-provider");

        public int VersionMajor => assemblyFileVersion.ProductMajorPart;

        public int VersionMinor => assemblyFileVersion.ProductMinorPart;

        public int VersionBuild => assemblyFileVersion.ProductBuildPart;

        public int VersionRevision => assemblyFileVersion.ProductPrivatePart;

        private System.Diagnostics.FileVersionInfo assemblyFileVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
    }
}