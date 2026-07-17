using HarnessForge.App.Models;

namespace HarnessForge.App.Services;

public static class DeviceFactory
{
    public static DeviceDefinition CreateMaxxEcuRaceDefinition()
    {
        DeviceDefinition definition = new()
        {
            Manufacturer = "MaxxECU",
            Name = "MaxxECU Race"
        };

        definition.Connectors.Add(
            new ConnectorDefinition
            {
                Name = "Connector A",
                PinCount = 34
            });

        definition.Connectors.Add(
            new ConnectorDefinition
            {
                Name = "Connector B",
                PinCount = 34
            });

        definition.Connectors.Add(
            new ConnectorDefinition
            {
                Name = "Connector C",
                PinCount = 28
            });

        definition.Connectors.Add(
            new ConnectorDefinition
            {
                Name = "Connector D",
                PinCount = 28
            });

        return definition;
    }
}