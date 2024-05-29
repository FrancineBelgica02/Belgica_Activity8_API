using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Api_Activity
{
    public partial class AddUser : Form
    {
        public AddUser()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
            this.Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string email = email_field.Text;
            string password = password_field.Text;
            string confirmPassword = confirm_password_field.Text;
            string json;
            // Validate user input (optional)
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please enter email and password.");
                return;
            }
            if (password != confirmPassword)
            {
                password_field.Clear();
                confirm_password_field.Clear();
                MessageBox.Show("Password and confirm password do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create JSON string for user data
            json = $"{{ \"email\": \"{email}\", \"password\": \"{password}\" }}";

            // Send POST request to the API
            await PostUserDataAsync(json);
        }

        private async Task PostUserDataAsync(string json)
        {
            // API endpoint URL
            string url = "http://localhost/api-activity/api.php/users";

            //ADD USER ENDPOINT POST METHOD

            // Create HttpClient instance
            using (HttpClient client = new HttpClient())
            {
                // Set headers
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("User-Agent", "YourApp");

                // Create HttpContent for JSON data
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                // Send POST request
                try
                {
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Show response message
                    MessageBox.Show(responseBody, "API Response", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show($"Request error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
