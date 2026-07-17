namespace HarnessForge.App.Models;

public class DeviceDefinition
{
    public string Manufacturer { get; set; } = "";
    public string Name { get; set; } = "";

    public List<ConnectorDefinition> Connectors { get; } = new();
}