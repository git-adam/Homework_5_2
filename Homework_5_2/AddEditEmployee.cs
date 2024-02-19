using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Homework_5_2
{
    public partial class AddEditEmployee : Form
    {
        private FileHelper<List<Employee>> _fileHelper = new FileHelper<List<Employee>>(Program.FilePath);
        private int _employeeId;
        private Employee _employee;
        private List<Status> _statuses;
        public AddEditEmployee(int id = 0)
        {
            InitializeComponent();
            _statuses = StatusesHelper.GetStatuses("Brak");
            InitStatusesCombobox();
            tbFirstName.Select();
            _employeeId = id;

            GetEmployeeData();

        }

        private void InitStatusesCombobox()
        {
            cmbStatuses.DataSource = _statuses;
            cmbStatuses.DisplayMember = "Name";
            cmbStatuses.ValueMember = "Id";
        }

        private void GetEmployeeData()
        {
            if (_employeeId != 0)
            {
                this.Text = "Edytowanie danych pracownika";
                var employees = _fileHelper.DeserializeFromFile();

                _employee = employees.FirstOrDefault(x => x.Id == _employeeId);
                if (_employee == null)
                    throw new Exception("Brak pracownika o podanym Id!");

                FillTextBoxes();

                if (tbDismissDate.Text != "")
                {
                    dtpDismissDate.Enabled = true;
                    tbDismissDate.ReadOnly = false;
                }
            }
        }

        private void FillTextBoxes()
        {
            tbId.Text = _employee.Id.ToString();
            tbFirstName.Text = _employee.FirstName;
            tbLastName.Text = _employee.LastName;
            tbNumber.Text = _employee.Number.ToString();
            tbSalary.Text = _employee.Salary.ToString();
            tbHireDate.Text = _employee.HireDate.ToShortDateString();
            tbDismissDate.Text = _employee.DismissDate != null ? Convert.ToDateTime(_employee.DismissDate).ToShortDateString() : _employee.DismissDate.ToString();
            tbVacationDaysNumber.Text = _employee.VacationDaysNumber.ToString();
            rtbComments.Text = _employee.Comments;
            cmbStatuses.SelectedItem = _statuses.FirstOrDefault(x => x.Id == _employee.StatusId);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            var employees = _fileHelper.DeserializeFromFile();

            if (_employeeId != 0)
                employees.RemoveAll(x => x.Id == _employeeId);
            else
                AssignIdToNewEmployee(employees);

            AddNewEmployeeToList(employees);

            _fileHelper.SerializeToFile(employees);
            
            Close();
        }

        private void AssignIdToNewEmployee(List<Employee> employees)
        {
            var employeeWithHighestId = employees
                .OrderByDescending(x => x.Id).FirstOrDefault();

            _employeeId = employeeWithHighestId == null ? 1 : employeeWithHighestId.Id + 1;
        }

        private void AddNewEmployeeToList(List<Employee> employees)
        {

            //CO LEPSZE?
            //var employee = new Employee
            //{
            //    Id = _employeeId,
            //    FirstName = tbFirstName.Text,
            //    LastName = tbLastName.Text,
            //    Number = int.TryParse(tbNumber.Text, out int number) && number > 0 ? number : 0,
            //    Salary = decimal.TryParse(tbSalary.Text, out decimal salary) && salary > decimal.Zero ? salary : decimal.Zero,
            //    HireDate = DateTime.TryParse(tbHireDate.Text, out DateTime hireDate) ? hireDate : Convert.ToDateTime(DateTime.Now.ToShortDateString()),
            //    DismissDate = DateTime.TryParse(tbDismissDate.Text, out DateTime dismissDate) && dismissDate >= hireDate ? dismissDate : (DateTime?)null,
            //    VacationDaysNumber = int.TryParse(tbVacationDaysNumber.Text, out int vacationDaysNumber) && vacationDaysNumber >= 0 ? vacationDaysNumber : 0,
            //    Comments = rtbComments.Text,
            //    StatusId = (cmbStatuses.SelectedItem as Status).Id,
            //};

            var employee = new Employee
            {
                Id = _employeeId,
                FirstName = tbFirstName.Text,
                LastName = tbLastName.Text,
                Number = GetNumber(0),
                Salary = GetSalary(decimal.Zero),
                HireDate = GetHireDate(Convert.ToDateTime(DateTime.Now.ToShortDateString())),
                DismissDate = GetDismissDate(),
                VacationDaysNumber = GetVacationDaysNumber(0),
                Comments = rtbComments.Text,
                StatusId = (cmbStatuses.SelectedItem as Status).Id,
            };

            employees.Add(employee);
        }

        private int GetNumber(int defaultValue)
        {
            if (!int.TryParse(tbNumber.Text, out int number) || number < defaultValue)
            {
                MessageBox.Show($"Wpisałeś nieprawidłowe dane w pole {lbNumber.Text}");
                number = defaultValue;
            }
            return number;
        }

        private decimal GetSalary(decimal defaultValue)
        {
            if (!decimal.TryParse(tbSalary.Text, out decimal salary) || salary < defaultValue)
            {
                MessageBox.Show($"Wpisałeś nieprawidłowe dane w pole {lbSalary.Text}");
                salary = defaultValue;
            }
            return salary;
        }

        private DateTime GetHireDate(DateTime defaultValue)
        {
            if (!DateTime.TryParse(tbHireDate.Text, out DateTime hireDate))
            {
                MessageBox.Show($"Wpisałeś nieprawidłowe dane w pole {lbHireDate.Text}");
                hireDate = defaultValue;
            }
            return hireDate;
        }

        private DateTime? GetDismissDate()
        {
            DateTime.TryParse(tbHireDate.Text, out DateTime hireDate);

            if (!DateTime.TryParse(tbDismissDate.Text, out DateTime dismissDate) || dismissDate < hireDate)
            {
                if(tbDismissDate.ReadOnly == false)
                    MessageBox.Show($"Wpisałeś nieprawidłowe dane w pole {lbDismissDate.Text}");
                return (DateTime?)null;
            }
            return dismissDate;            
        }

        private int GetVacationDaysNumber(int defaultValue)
        {
            if (!int.TryParse(tbVacationDaysNumber.Text, out int vacationDaysNumber) || vacationDaysNumber < defaultValue)
            {
                MessageBox.Show($"Wpisałeś nieprawidłowe dane w pole {lbVacationDaysNumber.Text}");
                vacationDaysNumber = defaultValue;
            }
            return vacationDaysNumber;
        }

        private void dtpHireDate_ValueChanged(object sender, EventArgs e)
        {

            tbHireDate.Text = dtpHireDate.Value.ToShortDateString();
        }

        private void dtpDismissDate_ValueChanged(object sender, EventArgs e)
        {
            tbDismissDate.Text = dtpDismissDate.Value.ToShortDateString();
        }
    }
}
