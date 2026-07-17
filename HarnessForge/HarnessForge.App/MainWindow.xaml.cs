using System.Windows;
using System.Windows.Controls;

namespace HarnessForge.App;

public partial class MainWindow : Window
{
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

        ShowItemDetails(tag);
    }

    private void ShowItemDetails(string tag)
    {
        ResetDetails();

        switch (tag)
        {
            case "Library":
                DetailsTitle.Text = "Engineering Library";
                DetailsSubtitle.Text = "Reusable engineering knowledge";
                TypeValue.Text = "Library";
                StatusValue.Text = "Loaded";
                DescriptionValue.Text =
                    "The library contains manufacturers, devices, connectors, contacts, seals and other reusable engineering definitions.";
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
                    "MaxxECU Race",
                    "Engine Control Unit",
                    "4",
                    "124",
                    "Sample definition",
                    "A high-capability engine control unit represented here using temporary sample engineering data.");
                break;

            case "MaxxECUPro":
                ShowDevice(
                    "MaxxECU Pro",
                    "Engine Control Unit",
                    "3",
                    "96",
                    "Sample definition",
                    "A sample device included to test navigation and library browsing.");
                break;

            case "MaxxECUMini":
                ShowDevice(
                    "MaxxECU Mini",
                    "Engine Control Unit",
                    "2",
                    "48",
                    "Sample definition",
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
                    "Connector definitions contain cavities, compatible contacts, seals and mating information.";
                break;

            case "ConnectorA":
                ShowConnector("Connector A", "34", "Black", "Active");
                break;

            case "ConnectorB":
                ShowConnector("Connector B", "34", "Grey", "Active");
                break;

            case "ConnectorC":
                ShowConnector("Connector C", "28", "Black", "Active");
                break;

            case "ConnectorD":
                ShowConnector("Connector D", "28", "Grey", "Active");
                break;

            case "Contacts":
                DetailsTitle.Text = "Contacts";
                DetailsSubtitle.Text = "Electrical terminal definitions";
                TypeValue.Text = "Collection";
                StatusValue.Text = "Sample data";
                DescriptionValue.Text =
                    "Contacts will eventually include wire-size compatibility, plating, current rating and tooling requirements.";
                break;

            case "Seals":
                DetailsTitle.Text = "Seals";
                DetailsSubtitle.Text = "Environmental sealing components";
                TypeValue.Text = "Collection";
                StatusValue.Text = "Sample data";
                DescriptionValue.Text =
                    "Seals will be selected according to connector cavity and wire diameter compatibility.";
                break;

            case "Projects":
                DetailsTitle.Text = "Projects";
                DetailsSubtitle.Text = "Harness engineering projects";
                TypeValue.Text = "Collection";
                StatusValue.Text = "1 project";
                DescriptionValue.Text =
                    "Projects will contain instantiated devices, connectors, pins, wires and engineering decisions.";
                break;

            case "TestProject":
                DetailsTitle.Text = "Test Project";
                DetailsSubtitle.Text = "Prototype harness project";
                TypeValue.Text = "Project";
                StatusValue.Text = "Draft";
                DescriptionValue.Text =
                    "This project is currently only a navigation placeholder. Device generation will be added next.";
                break;

            case "ProjectDevices":
                DetailsTitle.Text = "Project Devices";
                DetailsSubtitle.Text = "Devices instantiated in this project";
                TypeValue.Text = "Collection";
                StatusValue.Text = "Empty";
                DescriptionValue.Text =
                    "The next prototype will allow a library device to be added here.";
                break;

            case "ProjectWires":
                DetailsTitle.Text = "Project Wires";
                DetailsSubtitle.Text = "Electrical connections";
                TypeValue.Text = "Collection";
                StatusValue.Text = "Not implemented";
                DescriptionValue.Text =
                    "Wire creation will come after device and connector generation are working.";
                break;

            case "ProjectValidation":
                DetailsTitle.Text = "Project Validation";
                DetailsSubtitle.Text = "Engineering checks";
                TypeValue.Text = "Validation";
                StatusValue.Text = "Not run";
                DescriptionValue.Text =
                    "Validation will eventually explain errors, warnings and suggested engineering corrections.";
                break;

            default:
                DetailsTitle.Text = "HarnessForge Explorer";
                DetailsSubtitle.Text = "Select an engineering object";
                break;
        }

        StatusText.Text = $"Selected: {DetailsTitle.Text}";
    }

    private void ShowDevice(
        string name,
        string type,
        string connectors,
        string pins,
        string status,
        string description)
    {
        DetailsTitle.Text = name;
        DetailsSubtitle.Text = type;
        TypeValue.Text = "Device";
        ManufacturerValue.Text = "MaxxECU";
        ConnectorsValue.Text = connectors;
        PinsValue.Text = pins;
        StatusValue.Text = status;
        DescriptionValue.Text = description;
    }

    private void ShowConnector(
        string name,
        string cavities,
        string colour,
        string status)
    {
        DetailsTitle.Text = name;
        DetailsSubtitle.Text = "Device Connector";
        TypeValue.Text = "Connector";
        ManufacturerValue.Text = "Sample connector family";
        ConnectorsValue.Text = "1";
        PinsValue.Text = cavities;
        StatusValue.Text = status;
        DescriptionValue.Text =
            $"{name} is a {colour.ToLowerInvariant()} connector with {cavities} sample cavities.";
    }

    private void ResetDetails()
    {
        TypeValue.Text = "-";
        ManufacturerValue.Text = "-";
        ConnectorsValue.Text = "-";
        PinsValue.Text = "-";
        StatusValue.Text = "-";
        DescriptionValue.Text = string.Empty;
    }

    private void NewProjectButton_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show(
            "Project creation will be added in the next prototype.",
            "HarnessForge",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
    }

    private void AddDeviceButton_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show(
            "Device generation is the next feature we will build.",
            "HarnessForge",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
    }

    private void ValidateButton_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show(
            "Validation complete.\n\n0 errors\n3 sample warnings",
            "HarnessForge Validation",
            MessageBoxButton.OK,
            MessageBoxImage.Information);

        StatusText.Text = "Validation complete: 0 errors, 3 warnings";
    }

    private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}