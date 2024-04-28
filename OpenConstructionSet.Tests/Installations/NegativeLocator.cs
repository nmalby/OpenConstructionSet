using OpenConstructionSet.Installations;
using OpenConstructionSet.Installations.Locators;
using System.Diagnostics.CodeAnalysis;

namespace OpenConstructionSet.Tests.Installations;

internal class NegativeLocator(string id) : IInstallationLocator
{
    public string Id { get; } = id;

    public bool TryLocate([MaybeNullWhen(false)] out IInstallation installation)
    {
        installation = null;
        return false;
    }
}
