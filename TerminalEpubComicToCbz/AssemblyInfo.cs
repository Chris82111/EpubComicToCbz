using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Chris82111.TerminalEpubComicToCbz
{
    public class AssemblyInfo
    {
        // The following must be added to the *.csproj file.
#if false
    <Target Name="SetHash" AfterTargets="InitializeSourceControlInformation">

    <Exec Command="git describe --long --always --dirty --exclude=* --abbrev=8" ConsoleToMSBuild="True" IgnoreExitCode="False">
        <Output PropertyName="GetGitHash" TaskParameter="ConsoleOutput" />
    </Exec>
    <Exec Command="git show --pretty=%25%25H" ConsoleToMSBuild="True" IgnoreExitCode="False">
        <Output PropertyName="GetGitCommit" TaskParameter="ConsoleOutput" />
    </Exec>
    <Exec Command="git diff --quiet || echo Dirty &amp; exit 0" ConsoleToMSBuild="True" IgnoreExitCode="False">
        <Output PropertyName="GetDirty" TaskParameter="ConsoleOutput" />
    </Exec>
    <Exec Command="git config --get remote.origin.url" ConsoleToMSBuild="True" IgnoreExitCode="False">
        <Output PropertyName="GetGitUrl" TaskParameter="ConsoleOutput" />
    </Exec>

    <ItemGroup>
        <AssemblyAttribute Include="System.Reflection.AssemblyMetadataAttribute">
        <_Parameter1>GitHash</_Parameter1>
        <_Parameter2>$(GetGitHash)</_Parameter2>
        </AssemblyAttribute>
        <AssemblyAttribute Include="System.Reflection.AssemblyMetadataAttribute">
        <_Parameter1>GitCommit</_Parameter1>
        <_Parameter2>$(GetGitCommit)</_Parameter2>
        </AssemblyAttribute>
        <AssemblyAttribute Include="System.Reflection.AssemblyMetadataAttribute">
        <_Parameter1>GitDirty</_Parameter1>
        <_Parameter2>$(GetDirty)</_Parameter2>
        </AssemblyAttribute>
        <AssemblyAttribute Include="System.Reflection.AssemblyMetadataAttribute">
        <_Parameter1>GitUrl</_Parameter1>
        <_Parameter2>$(GetGitUrl)</_Parameter2>
        </AssemblyAttribute>
    </ItemGroup>
    </Target>
#endif
        /// <summary>
        ///         Gets the git hash value from the assembly or null if it cannot be found.
        /// <br/>   <see href="https://stackoverflow.com/questions/15141338/embed-git-commit-hash-in-a-net-dll"/>
        /// </summary>
        public static string? GitHash => _GitHash;

        // https://learn.microsoft.com/de-de/previous-versions/visualstudio/visual-studio-2015/msbuild/msbuild-special-characters?view=vs-2015&redirectedfrom=MSDN
        public static string? GitCommit => _GitCommit;

        public static bool GitDirty => _GitDirty;

        public static string? GitUrl => _GitUrl;

        public static string? Version => _Version;

        private static string? _GitHash = typeof(AssemblyInfo).Assembly
            .GetCustomAttributes<AssemblyMetadataAttribute>()
            .FirstOrDefault(attr => attr.Key == "GitHash")?.Value;

        private static string? _GitCommit = typeof(AssemblyInfo).Assembly
            .GetCustomAttributes<AssemblyMetadataAttribute>()
            .FirstOrDefault(attr => attr.Key == "GitCommit")?.Value;

        private static bool _GitDirty = (typeof(AssemblyInfo).Assembly
            .GetCustomAttributes<AssemblyMetadataAttribute>()
            .FirstOrDefault(attr => attr.Key == "GitDirty")?.Value?.ToLower() == "dirty") ? true : false;

        private static string? _GitUrl = typeof(AssemblyInfo).Assembly
            .GetCustomAttributes<AssemblyMetadataAttribute>()
            .FirstOrDefault(attr => attr.Key == "GitUrl")?.Value;

        private static string? _Version = System.Diagnostics.FileVersionInfo.GetVersionInfo(
            System.Reflection.Assembly.GetExecutingAssembly().Location)
            .FileVersion;
    }   
}
