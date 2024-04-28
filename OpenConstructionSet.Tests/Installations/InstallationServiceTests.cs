using AutoFixture;
using AutoFixture.AutoNSubstitute;
using OpenConstructionSet.Installations;
using OpenConstructionSet.Installations.Locators;

namespace OpenConstructionSet.Tests.Installations;

public class InstallationServiceTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void TryLocate_PositiveLocators_LocatesCorrectly(int locatorCount)
    {
        var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());

        var locators = new List<IInstallationLocator>();

        for (int i = 0; i < locatorCount; i++)
        {
            locators.Add(new PositiveLocator(i.ToString(), fixture.Create<IInstallation>()));
        }

        var target = new InstallationService(locators);

        var result = target.TryLocate(out var _);

        Assert.True(result);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void TryLocate_NegativeLocators_FailsToLocateCorrectly(int locatorCount)
    {
        var locators = new List<IInstallationLocator>();

        for (int i = 0; i < locatorCount; i++)
        {
            locators.Add(new NegativeLocator(i.ToString()));
        }

        var target = new InstallationService(locators);

        var result = target.TryLocate(out var _);

        Assert.False(result);
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(1, 1)]
    [InlineData(1, 2)]
    [InlineData(2, 0)]
    [InlineData(2, 1)]
    [InlineData(2, 2)]
    [InlineData(2, 3)]
    public void TryLocate_MixedLocators_LocatesCorrectly(int positiveLocatorCount, int negativeLocatorCount)
    {
        var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());

        var locators = new List<IInstallationLocator>();

        for (int i = 0; i < positiveLocatorCount; i++)
        {
            locators.Add(new PositiveLocator($"+{i}", fixture.Create<IInstallation>()));
        }

        for (int i = 0; i < negativeLocatorCount; i++)
        {
            locators.Add(new NegativeLocator($"-{i}"));
        }

        var target = new InstallationService(locators);

        var result = target.TryLocate(out var _);

        Assert.True(result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(5)]
    public void TryLocate_ById_LocatesCorrectly(int otherLocatorCount)
    {
        var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());

        var id = "id";

        var expected = fixture.Create<IInstallation>();

        var locator = new PositiveLocator(id, expected);

        var otherLocators = new List<IInstallationLocator>();

        for (int i = 0; i < otherLocatorCount; i++)
        {
            otherLocators.Add(new PositiveLocator(i.ToString(), fixture.Create<IInstallation>()));
        }

        var target = new InstallationService([locator]);

        var result = target.TryLocate(id, out var actual);

        Assert.True(result);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TryLocate_InvalidId_ThrowsInvalidOperationException()
    {
        var validId = "id";
        var invalidId = "invalid";

        var locator = new NegativeLocator(validId);

        var target = new InstallationService([locator]);

        Assert.Throws<InvalidOperationException>(() => target.TryLocate(invalidId, out var _));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(3)]
    public void TryLocateAll_Multiple_LocatesAll(int locatorCount)
    {
        var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());

        var locators = new List<IInstallationLocator>();
        var installations = new List<IInstallation>();

        for (int i = 0; i < locatorCount; i++)
        {
            var installation = fixture.Create<IInstallation>();

            var locator = new PositiveLocator(i.ToString(), installation);

            locators.Add(locator);
            installations.Add(installation);
        }

        var target = new InstallationService(locators);

        var results = target.LocateAll();

        foreach (var result in results)
        {
            Assert.Contains(result, installations);
        }
    }
}
