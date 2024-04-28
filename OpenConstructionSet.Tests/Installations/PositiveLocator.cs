using OpenConstructionSet.Installations;
using OpenConstructionSet.Installations.Locators;
using System.Diagnostics.CodeAnalysis;

namespace OpenConstructionSet.Tests.Installations;

internal class PositiveLocator(string id, IInstallation result) : IInstallationLocator
{
    public string Id { get; } = id;

    public bool TryLocate([MaybeNullWhen(false)] out IInstallation installation)
    {
        installation = result;
        return true;
    }
}
