using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Api_Activity
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string url = "http://localhost/api-activity/api.php/users";
            //Get Users

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Remove the "users" prefix and parse the JSON array
                    string jsonArrayString = responseBody.Substring(responseBody.IndexOf('['));
                    List<User> users = JsonConvert.DeserializeObject<List<User>>(jsonArrayString);

                    dataGridView1.DataSource = users;
                    dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                    // Auto size rows
                    dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                    // Set the DataGridView to resize its height to fit all rows
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // Optional: Enable horizontal scrolling if needed
                    dataGridView1.ScrollBars = ScrollBars.Both;
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show($"Request error: {ex.Message}");
                }
                catch (JsonException ex)
                {
                    MessageBox.Show($"JSON error: {ex.Message}");
                }
            }
        }



        private async void button3_Click(object sender, EventArgs e)
        {
            string url = "http://localhost/api-activity/api.php/teachers";
            //Get Teachers
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Remove the "users" prefix and parse the JSON array
                    string jsonArrayString = responseBody.Substring(responseBody.IndexOf('['));
                    List<Teacher> teachers = JsonConvert.DeserializeObject<List<Teacher>>(jsonArrayString);

                    dataGridView1.DataSource = teachers;
                    dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                    // Auto size rows
                    dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                    // Set the DataGridView to resize its height to fit all rows
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // Optional: Enable horizontal scrolling if needed
                    dataGridView1.ScrollBars = ScrollBars.Both;
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show($"Request error: {ex.Message}");
                }
                catch (JsonException ex)
                {
                    MessageBox.Show($"JSON error: {ex.Message}");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddUser addStudent = new AddUser();
            this.Hide();
            addStudent.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddTeacher addTeacher = new AddTeacher();
            this.Hide();
            addTeacher.ShowDialog();
            this.Close();
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
    }

    public class Teacher
    {
        public int teacher_id { get; set; }
        public string teacher_fname { get; set; }
        public string teacher_lname { get; set; }
        public string teacher_salary { get; set; }

    }
}