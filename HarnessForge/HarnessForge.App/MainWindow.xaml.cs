using System.Windows;
using System.Windows.Controls;

namespace HarnessForge.App;

public partial class MainWindow : Window
{
    private TreeViewItem? _currentProjectNode;
    private TreeViewItem? _projectDevicesNode;

    private int _projectNumber = 1;
    private int _deviceNumber = 1;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void EngineeringTree_SelectedItemChanged(
        object sender,
        RoutedPropertyChangedEventArgs<object> e)
    {
        if (EngineeringTree.SelectedItem is not TreeViewItem selectedItem)
        {
            return;
        }

        string tag = selectedItem.Tag?.ToString() ?? string.Empty;
        string? selectedHeader = selectedItem.Header?.ToString();

        ShowItemDetails(tag, selectedHeader);
    }

    private void ShowItemDetails(
        string tag,
        string? selectedHeader)
    {
        ResetDetails();

        string SelectedHeaderOrFallback(string fallback)
        {
            return string.IsNullOrWhiteSpace(selectedHeader)
                ? fallback
                : selectedHeader;
        }

        switch (tag)
        {
            case "Library":
                DetailsTitle.Text = "Engineering Library";
                DetailsSubtitle.Text = "Reusable engineering knowledge";
                TypeValue.Text = "Library";
                StatusValue.Text = "Loaded";

                DescriptionValue.Text =
                    "The library contains manufacturers, devices, connectors, " +
                    "contacts, seals and other reusable engineering definitions.";
                break;

            case "Manufacturers":
                DetailsTitle.Text = "Manufacturers";
                DetailsSubtitle.Text = "Component manufacturers";
                TypeValue.Text = "Collection";
                StatusValue.Text = "1 manufacturer";

                DescriptionValue.Text =
                    "Manufacturers organize engineering products and source information.";
                break;

            case "Manufacturer":
                DetailsTitle.Text = "MaxxECU";
                DetailsSubtitle.Text = "Engine management systems";
                TypeValue.Text = "Manufacturer";
                ManufacturerValue.Text = "MaxxECU";
                StatusValue.Text = "Sample data";

                DescriptionValue.Text =
                    "This manufacturer currently contains three sample device definitions.";
                break;

            case "Devices":
                DetailsTitle.Text = "Devices";
                DetailsSubtitle.Text = "Available device definitions";
                TypeValue.Text = "Collection";
                ManufacturerValue.Text = "MaxxECU";
                StatusValue.Text = "3 devices";

                DescriptionValue.Text =
                    "Choose a device to inspect its engineering definition.";
                break;

            case "MaxxECURace":
                ShowDevice(
                    name: "MaxxECU Race",
                    subtitle: "Engine Control Unit",
                    connectorCount: "4",
                    pinCount: "124",
                    status: "Sample definition",
                    description:
                    "A high-capability engine control unit represented using " +
                    "temporary sample engineering data.");
                break;

            case "MaxxECUPro":
                ShowDevice(
                    name: "MaxxECU Pro",
                    subtitle: "Engine Control Unit",
                    connectorCount: "3",
                    pinCount: "96",
                    status: "Sample definition",
                    description:
                    "A sample device included to test navigation and library browsing.");
                break;

            case "MaxxECUMini":
                ShowDevice(
                    name: "MaxxECU Mini",
                    subtitle: "Engine Control Unit",
                    connectorCount: "2",
                    pinCount: "48",
                    status: "Sample definition",
                    description:
                    "A smaller sample ECU definition used for the first Explorer prototype.");
                break;

            case "Connectors":
                DetailsTitle.Text = "Connectors";
                DetailsSubtitle.Text = "Device connector definitions";
                TypeValue.Text = "Collection";
                ManufacturerValue.Text = "MaxxECU";
                ConnectorsValue.Text = "4";
                StatusValue.Text = "Sample data";

                DescriptionValue.Text =
                    "Connector definitions contain cavities, compatible contacts, " +
                    "seals and mating information.";
                break;

            case "ConnectorA":
                ShowConnector(
                    name: "Connector A",
                    cavityCount: "34",
                    colour: "Black",
                    status: "Active");
                break;

            case "ConnectorB":
                ShowConnector(
                    name: "Connector B",
                    cavityCount: "34",
                    colour: "Grey",
                    status: "Active");
                break;

            case "ConnectorC":
                ShowConnector(
                    name: "Connector C",
                    cavityCount: "28",
                    colour: "Black",
                    status: "Active");
                break;

            case "ConnectorD":
                ShowConnector(
                    name: "Connector D",
                    cavityCount: "28",
                    colour: "Grey",
                    status: "Active");
                break;

            case "Contacts":
                DetailsTitle.Text = "Contacts";
                DetailsSubtitle.Text = "Electrical terminal definitions";
                TypeValue.Text = "Collection";
                StatusValue.Text = "Sample data";

                DescriptionValue.Text =
                    "Contacts will eventually include wire-size compatibility, " +
                    "plating, current rating and tooling requirements.";
                break;

            case "Seals":
                DetailsTitle.Text = "Seals";
                DetailsSubtitle.Text = "Environmental sealing components";
                TypeValue.Text = "Collection";
                StatusValue.Text = "Sample data";

                DescriptionValue.Text =
                    "Seals will be selected according to connector cavity and " +
                    "wire diameter compatibility.";
                break;

            case "Projects":
                DetailsTitle.Text = "Projects";
                DetailsSubtitle.Text = "Harness engineering projects";
                TypeValue.Text = "Collection";
                StatusValue.Text =
                    ProjectsNode.Items.Count == 0
                        ? "No projects"
                        : $"{ProjectsNode.Items.Count} project(s)";

                DescriptionValue.Text =
                    "Projects contain instantiated devices, connectors, pins, " +
                    "wires and engineering decisions.";
                break;

            case "DynamicProject":
                DetailsTitle.Text =
                    SelectedHeaderOrFallback("Harness Project");

                DetailsSubtitle.Text = "Active engineering project";
                TypeValue.Text = "Project";
                StatusValue.Text = "Draft";

                DescriptionValue.Text =
                    "This project was created during the current HarnessForge session.";
                break;

            case "ProjectDevices":
                DetailsTitle.Text = "Project Devices";
                DetailsSubtitle.Text = "Devices instantiated in this project";
                TypeValue.Text = "Collection";

                int projectDeviceCount =
                    _projectDevicesNode?.Items.Count ?? 0;

                StatusValue.Text =
                    projectDeviceCount == 0
                        ? "Empty"
                        : $"{projectDeviceCount} device(s)";

                DescriptionValue.Text =
                    "Devices added to the active project appear in this collection.";
                break;

            case "ProjectWires":
                DetailsTitle.Text = "Project Wires";
                DetailsSubtitle.Text = "Electrical connections";
                TypeValue.Text = "Collection";
                StatusValue.Text = "Not implemented";

                DescriptionValue.Text =
                    "Wire creation will be added after device and connector " +
                    "generation are working.";
                break;

            case "ProjectValidation":
                DetailsTitle.Text = "Project Validation";
                DetailsSubtitle.Text = "Engineering checks";
                TypeValue.Text = "Validation";
                StatusValue.Text = "Not run";

                DescriptionValue.Text =
                    "Validation will eventually explain errors, warnings and " +
                    "suggested engineering corrections.";
                break;

            case "GeneratedMaxxECURace":
                ShowDevice(
                    name: SelectedHeaderOrFallback("MaxxECU Race"),
                    subtitle: "Generated Project Device",
                    connectorCount: "4",
                    pinCount: "124",
                    status: "Generated",
                    description:
                    "This project device was instantiated from the sample " +
                    "MaxxECU Race library definition.");
                break;

            case "GeneratedConnectors":
                DetailsTitle.Text = "Generated Connectors";
                DetailsSubtitle.Text = "Project connector instances";
                TypeValue.Text = "Collection";
                ConnectorsValue.Text = "4";
                StatusValue.Text = "Generated";

                DescriptionValue.Text =
                    "These connector instances were generated from the selected " +
                    "device definition.";
                break;

            case "GeneratedConnector":
                DetailsTitle.Text =
                    SelectedHeaderOrFallback("Connector");

                DetailsSubtitle.Text = "Generated Project Connector";
                TypeValue.Text = "Connector";
                ManufacturerValue.Text = "Sample connector family";
                ConnectorsValue.Text = "1";
                StatusValue.Text = "Generated";

                DescriptionValue.Text =
                    "This is a project-specific connector instance generated " +
                    "from library knowledge.";
                break;

            case "GeneratedPins":
                DetailsTitle.Text =
                    SelectedHeaderOrFallback("Pins");

                DetailsSubtitle.Text = "Generated connector cavities";
                TypeValue.Text = "Pin collection";
                StatusValue.Text = "Generated";

                DescriptionValue.Text =
                    "Individual pin objects will be displayed in a later prototype.";
                break;

            default:
                DetailsTitle.Text = "HarnessForge Explorer";
                DetailsSubtitle.Text = "Select an engineering object";
                TypeValue.Text = "-";
                StatusValue.Text = "-";

                DescriptionValue.Text =
                    "Select an item in the engineering tree to inspect its details.";
                break;
        }

        StatusText.Text = $"Selected: {DetailsTitle.Text}";
    }

    private void ShowDevice(
        string name,
        string subtitle,
        string connectorCount,
        string pinCount,
        string status,
        string description)
    {
        DetailsTitle.Text = name;
        DetailsSubtitle.Text = subtitle;
        TypeValue.Text = "Device";
        ManufacturerValue.Text = "MaxxECU";
        ConnectorsValue.Text = connectorCount;
        PinsValue.Text = pinCount;
        StatusValue.Text = status;
        DescriptionValue.Text = description;
    }

    private void ShowConnector(
        string name,
        string cavityCount,
        string colour,
        string status)
    {
        DetailsTitle.Text = name;
        DetailsSubtitle.Text = "Device Connector";
        TypeValue.Text = "Connector";
        ManufacturerValue.Text = "Sample connector family";
        ConnectorsValue.Text = "1";
        PinsValue.Text = cavityCount;
        StatusValue.Text = status;

        DescriptionValue.Text =
            $"{name} is a {colour.ToLowerInvariant()} connector " +
            $"with {cavityCount} sample cavities.";
    }

    private void ResetDetails()
    {
        DetailsTitle.Text = "HarnessForge Explorer";
        DetailsSubtitle.Text = "Select an item from the engineering tree.";

        TypeValue.Text = "-";
        ManufacturerValue.Text = "-";
        ConnectorsValue.Text = "-";
        PinsValue.Text = "-";
        StatusValue.Text = "-";

        DescriptionValue.Text =
            "This is the first interactive HarnessForge prototype.";
    }

    private void NewProjectButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        string projectName = $"Harness Project {_projectNumber}";

        TreeViewItem projectDevicesNode = new()
        {
            Header = "Devices",
            Tag = "ProjectDevices",
            IsExpanded = true
        };

        TreeViewItem wiresNode = new()
        {
            Header = "Wires",
            Tag = "ProjectWires"
        };

        TreeViewItem validationNode = new()
        {
            Header = "Validation",
            Tag = "ProjectValidation"
        };

        TreeViewItem projectNode = new()
        {
            Header = projectName,
            Tag = "DynamicProject",
            IsExpanded = true
        };

        projectNode.Items.Add(projectDevicesNode);
        projectNode.Items.Add(wiresNode);
        projectNode.Items.Add(validationNode);

        ProjectsNode.Items.Add(projectNode);
        ProjectsNode.IsExpanded = true;

        _currentProjectNode = projectNode;
        _projectDevicesNode = projectDevicesNode;

        _projectNumber++;
        _deviceNumber = 1;

        projectNode.IsSelected = true;
        projectNode.BringIntoView();

        StatusText.Text = $"Created project: {projectName}";
    }

    private void AddDeviceButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        if (_currentProjectNode is null ||
            _projectDevicesNode is null)
        {
            MessageBox.Show(
                "Create a project before adding a device.",
                "HarnessForge",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            return;
        }

        string deviceName =
            _deviceNumber == 1
                ? "MaxxECU Race"
                : $"MaxxECU Race {_deviceNumber}";

        TreeViewItem connectorsNode = new()
        {
            Header = "Connectors",
            Tag = "GeneratedConnectors",
            IsExpanded = true
        };

        connectorsNode.Items.Add(
            CreateConnectorNode(
                connectorName: "Connector A",
                cavityCount: 34));

        connectorsNode.Items.Add(
            CreateConnectorNode(
                connectorName: "Connector B",
                cavityCount: 34));

        connectorsNode.Items.Add(
            CreateConnectorNode(
                connectorName: "Connector C",
                cavityCount: 28));

        connectorsNode.Items.Add(
            CreateConnectorNode(
                connectorName: "Connector D",
                cavityCount: 28));

        TreeViewItem deviceNode = new()
        {
            Header = deviceName,
            Tag = "GeneratedMaxxECURace",
            IsExpanded = true
        };

        deviceNode.Items.Add(connectorsNode);

        _projectDevicesNode.Items.Add(deviceNode);
        _projectDevicesNode.IsExpanded = true;

        _deviceNumber++;

        deviceNode.IsSelected = true;
        deviceNode.BringIntoView();

        StatusText.Text =
            $"Added {deviceName}: 4 connectors and 124 pins generated";
    }

    private static TreeViewItem CreateConnectorNode(
        string connectorName,
        int cavityCount)
    {
        TreeViewItem pinsNode = new()
        {
            Header = $"Pins ({cavityCount} cavities)",
            Tag = "GeneratedPins"
        };

        TreeViewItem connectorNode = new()
        {
            Header = connectorName,
            Tag = "GeneratedConnector"
        };

        connectorNode.Items.Add(pinsNode);

        return connectorNode;
    }

    private void ValidateButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        if (_currentProjectNode is null)
        {
            MessageBox.Show(
                "Create a project before running validation.",
                "HarnessForge Validation",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            StatusText.Text =
                "Validation could not run: no active project";

            return;
        }

        int deviceCount =
            _projectDevicesNode?.Items.Count ?? 0;

        if (deviceCount == 0)
        {
            MessageBox.Show(
                "Validation complete.\n\n" +
                "0 errors\n" +
                "1 warning\n\n" +
                "The project does not contain any devices.",
                "HarnessForge Validation",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            StatusText.Text =
                "Validation complete: 0 errors, 1 warning";

            return;
        }

        MessageBox.Show(
            "Validation complete.\n\n" +
            "0 errors\n" +
            "3 sample warnings\n\n" +
            $"{deviceCount} device(s) checked.",
            "HarnessForge Validation",
            MessageBoxButton.OK,
            MessageBoxImage.Information);

        StatusText.Text =
            $"Validation complete: {deviceCount} device(s), " +
            "0 errors, 3 warnings";
    }

    private void ExitMenuItem_Click(
        object sender,
        RoutedEventArgs e)
    {
        Close();
    }
}