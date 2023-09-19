using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework_5_2
{
    public partial class Main : Form
    {
        private string _filePath = Path.Combine(Environment.CurrentDirectory, "employees.txt");
        public Main()
        {
            InitializeComponent();

            //var employees = new List<Employee>();
            //employees.Add(new Employee { FirstName = "Adam" });
            //employees.Add(new Employee() { FirstName = "Tadam" });
            //employees.Add(new Employee { FirstName = "Madam" });
            //SerializeToFile(employees);

            var employees = DeserializeFromFile();
            foreach (var employee in employees)
            {
                MessageBox.Show(employee.FirstName);
            }
        }

        public void SerializeToFile(List<Employee> employees)
        {
            var serializer = new JsonSerializer();
            using (StreamWriter streamWriter = new StreamWriter(_filePath))
            {
                serializer.Serialize(streamWriter, employees);
                streamWriter.Close();
            }
        }

        public List<Employee> DeserializeFromFile()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Employee>();
            }

            var serializer = new JsonSerializer();

            using (StreamReader streamReader = new StreamReader(_filePath))
            using (JsonTextReader jsonTextReader = new JsonTextReader(streamReader))
            {
                var employees = serializer.Deserialize<List<Employee>>(jsonTextReader);
                jsonTextReader.Close();
                streamReader.Close();
                return employees;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        private void btnDismiss_Click(object sender, EventArgs e)
        {

        }
    }
}
