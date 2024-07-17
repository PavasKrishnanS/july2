using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace july2
{
    public partial class Weatherapi : Form
    {
        public Weatherapi()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            {
                GetWeatherData();
            }
        }

        private async void GetWeatherData()
        {
            string apiKey = "https://api.example.com/data?api_key=your_api_key_here\r\n"; // Replace with your actual API key
            string city = "New York";
            string apiUrl = $"https://api.example.com/data/endpoint\r\n";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    HttpResponseMessage httpResponseMessage = response.EnsureSuccessStatusCode(); // Throw exception if not successful

                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Parse JSON response (example: using Newtonsoft.Json)
                    dynamic weatherData = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);

                    // Example: Display temperature in a Label control
                    string temperature = $"{weatherData.main.temp} °C";
                    textBox1.Text = $"Current temperature in {city}: {temperature}";
                }
                catch (HttpRequestException ex)
                {
                    textBox1.Text = $"Error fetching weather data: {ex.Message}";
                }
            }
        }
    }
}

