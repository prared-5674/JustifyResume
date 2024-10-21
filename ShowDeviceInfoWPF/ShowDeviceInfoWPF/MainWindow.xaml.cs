using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace ShowDeviceInfoWPF
{
    public partial class MainWindow : Window
    {
        private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("Add your Address") };

        public MainWindow()
        {
            InitializeComponent();
        }

        // Register Device Button Click
        private async void RegisterDevice_Click(object sender, RoutedEventArgs e)
        {
            var device = new
            {
                DeviceName = DeviceNameTextBox.Text,
                DeviceId = DeviceIdTextBox.Text
            };

            _httpClient.DefaultRequestHeaders.Add("tenantId", "tenant1");
            var response = await _httpClient.PostAsJsonAsync("/api/device/register", device);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Device Registered Successfully!");
            }
        }

        // Get Devices Button Click
        private async void GetDevices_Click(object sender, RoutedEventArgs e)
        {           
            _httpClient.DefaultRequestHeaders.Add(WithDeviceIdTextBox.Text, "tenant1");
            var devices = await _httpClient.GetFromJsonAsync<List<string>>("/api/device/data/tenant1");

            foreach (var device in devices)
            {
                MessageBox.Show(device);
            }
        }
    }
}
