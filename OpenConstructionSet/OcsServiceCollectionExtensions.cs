﻿using GameFinder.RegistryUtils;
using GameFinder.StoreHandlers.GOG;
using GameFinder.StoreHandlers.Steam;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NexusMods.Paths;
using OpenConstructionSet;
using OpenConstructionSet.Installations;
using OpenConstructionSet.Installations.Locators;

namespace Microsoft.Extensions.DependencyInjection;

public static class OcsServiceCollectionExtensions
{
    public static IServiceCollection AddOpenConstructionSet(this IServiceCollection services)
    {
        // Game finder
        services.AddSingleton(FileSystem.Shared);
        services.AddSingleton(WindowsRegistry.Shared);
        services.AddSingleton<SteamHandler>();
        services.AddSingleton<GOGHandler>();

        services.TryAddEnumerable(new[]
        {
                ServiceDescriptor.Singleton<IInstallationLocator, SteamLocator>(),
                ServiceDescriptor.Singleton<IInstallationLocator, GogLocator>(),
                ServiceDescriptor.Singleton<IInstallationLocator, LocalLocator>(),
                ServiceDescriptor.Singleton<IInstallationLocator, ManualLocator>()
        });

        return services.AddSingleton<IInstallationService, InstallationService>()
                       .AddSingleton<IContextBuilder, ContextBuilder>();
    }
}