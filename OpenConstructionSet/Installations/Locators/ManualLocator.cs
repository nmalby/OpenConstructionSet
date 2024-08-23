using System.Diagnostics.CodeAnalysis;
using System.Runtime.Versioning;

namespace OpenConstructionSet.Installations.Locators;

/// <summary>
/// Manual/Crossover implementation of a <see cref="IInstallationLocator"/> that looks for the folders in the provided bottle path.
/// </summary>
[SupportedOSPlatform("macOS")]
// TODO: [SupportedOSPlatform("linux")]
public class ManualLocator : IInstallationLocator
{
    /// <inheritdoc/>
    public string Id { get; } = "Manual";

    public bool TryLocate([MaybeNullWhen(false)] out IInstallation installation)
    {
        var bottlePath = "";
        var steamappPath = bottlePath + "/C:/Program Files (x86)/Steam/steamapps";
        var gamePath = steamappPath + "/common/Kenshi";
        var workshopPath = steamappPath + "/workshop/content/233860";
        var savePath = bottlePath + "/C:/Users/Kenshi/AppData/Local/kenshi/save";

        installation = new Installation(Id, gamePath, workshopPath, savePath);
        return true;
    }
}
