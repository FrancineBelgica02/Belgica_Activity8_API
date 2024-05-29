using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Api_Activity
{
    public partial class AddTeacher : Form
    {
        public AddTeacher()
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
            string fname = fname_field.Text;
            string lname = lname_field.Text;
            string salary = salary_field.Text;

            string selectedCampus = select_campus_combobox.SelectedItem?.ToString();
            string selectedDepartment = select_department_combobox.SelectedItem?.ToString();

            MessageBox.Show(selectedCampus);
            MessageBox.Show(selectedDepartment);

            string json;
            // Validate user input (optional)
            if (string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(lname) || string.IsNullOrEmpty(salary) || string.IsNullOrEmpty(selectedCampus) || string.IsNullOrEmpty(selectedDepartment))
            {
                MessageBox.Show("Please complete the fields.");
                return;
            }

            // Create JSON string for user data
            json = $"{{\r\n    \"teacher_fname\": \"{fname}\",\r\n    \"teacher_lname\": \"{lname}\",\r\n    \"teacher_salary\": {salary},\r\n    \"campus_id\": 1,\r\n    \"department_id\": 1\r\n}}";

            // Send POST request to the API
            await PostUserDataAsync(json);
        }

        private async Task PostUserDataAsync(string json)
        {
            // API endpoint URL
            string url = "http://localhost/api-activity/api.php/teachers";
            //ADD TEACHER POST METHOD

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
