using Homework_5_2.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Homework_5_2
{
    public partial class Main : Form
    {
        private FileHelper<List<Employee>> _fileHelper = new FileHelper<List<Employee>>(Program.FilePath);

        private List<Employee> _employees;
        private List<Status> _statuses;

        private List<SortingCategory> _sortingCategory = new List<SortingCategory>()
        {
            new SortingCategory() { Id = 0, Name = "Identyfikator"},
            new SortingCategory() { Id = 1, Name = "Imię"},
            new SortingCategory() { Id = 2, Name = "Nazwisko"},
            new SortingCategory() { Id = 3, Name = "Numer"},
            new SortingCategory() { Id = 4, Name = "Wypłata"},
            new SortingCategory() { Id = 5, Name = "Data Zatrudnienia"},
            new SortingCategory() { Id = 6, Name = "Data Zwolnienia"},
            new SortingCategory() { Id = 7, Name = "Liczba Dni Urlopowych"},
            new SortingCategory() { Id = 8, Name = "Uwagi"},
        };

        public bool IsMaximize
        {
            get
            {
                return Settings.Default.IsMaximize;
            }
            set
            {
                Settings.Default.IsMaximize = value;
            }
        }
        public Main()
        {
            InitializeComponent();
            InitSortingCategoryCombobox();
            _statuses = StatusesHelper.GetStatuses("Brak");
            InitStatusesCombobox();
            RefreshEmployeesList();
            SetColumnsHeader();

            if (IsMaximize)
                WindowState = FormWindowState.Maximized;
        }

        private void InitStatusesCombobox()
        {
            cmbStatuses.DataSource = _statuses;
            cmbStatuses.DisplayMember = "Name";
            cmbStatuses.ValueMember = "Id";
        }

        private void RefreshEmployeesList()
        {
            _employees = _fileHelper.DeserializeFromFile();

            var selectedSortingCategoryId = (cmbSortingCategory.SelectedItem as SortingCategory).Id;

            SortEmployeesListByCategory(selectedSortingCategoryId);

            dgvEmployeesList.DataSource = _employees;
        }

        private void SortEmployeesListByCategory(int categoryId)
        {
            var selectedStatusId = (cmbStatuses.SelectedItem as Status).Id;

            switch (categoryId)
            {
                case 0:
                    _employees = ckbAreDesc.Checked == true ?
                        _employees.OrderByDescending(x => x.Id).ToList() : _employees.OrderBy(x => x.Id).ToList();
                    if (selectedStatusId != 0)
                        _employees = _employees.Where(x => x.StatusId == selectedStatusId).ToList();
                    break;
                case 1:
                    _employees = ckbAreDesc.Checked == true ?
                        _employees.OrderByDescending(x => x.FirstName).ToList() : _employees.OrderBy(x => x.FirstName).ToList();
                    if (selectedStatusId != 0)
                        _employees = _employees.Where(x => x.StatusId == selectedStatusId).ToList();
                    break;
                case 2:
                    _employees = ckbAreDesc.Checked == true ?
                        _employees.OrderByDescending(x => x.LastName).ToList() : _employees.OrderBy(x => x.LastName).ToList();
                    if (selectedStatusId != 0)
                        _employees = _employees.Where(x => x.StatusId == selectedStatusId).ToList();
                    break;
                case 3:
                    _employees = ckbAreDesc.Checked == true ?
                        _employees.OrderByDescending(x => x.Number).ToList() : _employees.OrderBy(x => x.Number).ToList();
                    if (selectedStatusId != 0)
                        _employees = _employees.Where(x => x.StatusId == selectedStatusId).ToList();
                    break;
                case 4:
                    _employees = ckbAreDesc.Checked == true ?
                        _employees.OrderByDescending(x => x.Salary).ToList() : _employees.OrderBy(x => x.Salary).ToList();
                    if (selectedStatusId != 0)
                        _employees = _employees.Where(x => x.StatusId == selectedStatusId).ToList();
                    break;
                case 5:
                    _employees = ckbAreDesc.Checked == true ?
                        _employees.OrderByDescending(x => x.HireDate).ToList() : _employees.OrderBy(x => x.HireDate).ToList();
                    if (selectedStatusId != 0)
                        _employees = _employees.Where(x => x.StatusId == selectedStatusId).ToList();
                    break;
                case 6:
                    _employees = ckbAreDesc.Checked == true ?
                        _employees.OrderByDescending(x => x.DismissDate).ToList() : _employees.OrderBy(x => x.DismissDate).ToList();
                    if (selectedStatusId != 0)
                        _employees = _employees.Where(x => x.StatusId == selectedStatusId).ToList();
                    break;
                case 7:
                    _employees = ckbAreDesc.Checked == true ?
                        _employees.OrderByDescending(x => x.VacationDaysNumber).ToList() : _employees.OrderBy(x => x.VacationDaysNumber).ToList();
                    if (selectedStatusId != 0)
                        _employees = _employees.Where(x => x.StatusId == selectedStatusId).ToList();
                    break;
                case 8:
                    _employees = ckbAreDesc.Checked == true ?
                        _employees.OrderByDescending(x => x.Comments).ToList() : _employees.OrderBy(x => x.Comments).ToList();
                    if (selectedStatusId != 0)
                        _employees = _employees.Where(x => x.StatusId == selectedStatusId).ToList();
                    break;
                default:
                    break;
            }
        }

        private void InitSortingCategoryCombobox()
        {
            cmbSortingCategory.DataSource = _sortingCategory;
            cmbSortingCategory.DisplayMember = "Name";
            cmbSortingCategory.ValueMember = "Id";
        }

        private void SetColumnsHeader()
        {
            dgvEmployeesList.Columns[nameof(Employee.Id)].HeaderText = _sortingCategory[0].Name;// "Identyfikator";
            dgvEmployeesList.Columns[nameof(Employee.FirstName)].HeaderText = _sortingCategory[1].Name;// "Imię";
            dgvEmployeesList.Columns[nameof(Employee.LastName)].HeaderText = _sortingCategory[2].Name;// "Nazwisko";
            dgvEmployeesList.Columns[nameof(Employee.Number)].HeaderText = _sortingCategory[3].Name;// "Numer";
            dgvEmployeesList.Columns[nameof(Employee.Salary)].HeaderText = _sortingCategory[4].Name;// "Wypłata";
            dgvEmployeesList.Columns[nameof(Employee.HireDate)].HeaderText = _sortingCategory[5].Name;// "Data Zatrudnienia";
            dgvEmployeesList.Columns[nameof(Employee.DismissDate)].HeaderText = _sortingCategory[6].Name;// "Data Zwolnienia";
            dgvEmployeesList.Columns[nameof(Employee.VacationDaysNumber)].HeaderText = _sortingCategory[7].Name;// "Liczba Dni Urlopowych";
            dgvEmployeesList.Columns[nameof(Employee.Comments)].HeaderText = _sortingCategory[8].Name;// "Uwagi";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addEditEmployee = new AddEditEmployee();
            addEditEmployee.FormClosing += AddEditEmployee_FormClosing;
            addEditEmployee.ShowDialog();
        }

        private void AddEditEmployee_FormClosing(object sender, FormClosingEventArgs e)
        {
            RefreshEmployeesList();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvEmployeesList.SelectedRows.Count == 0)
            {
                MessageBox.Show("Proszę zaznczyć pracownika, którego chcesz edytować!");
                return;
            }
            else if (dgvEmployeesList.SelectedRows.Count > 1)
            {
                MessageBox.Show("Proszę zaznaczyć tylko jednego pracownika!");
                return;
            }

            var addEditEmployee = new AddEditEmployee(
                Convert.ToInt32(dgvEmployeesList.SelectedRows[0].Cells[0].Value));

            addEditEmployee.FormClosing += AddEditEmployee_FormClosing;

            addEditEmployee.ShowDialog();

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshEmployeesList();
        }

        private void btnDismiss_Click(object sender, EventArgs e)
        {
            if (dgvEmployeesList.SelectedRows.Count == 0)
            {
                MessageBox.Show("Proszę zaznaczyć pracownika, którego chcesz zwolnić!");
                return;
            }

            var selectedEmployee = dgvEmployeesList.SelectedRows[0];

            var confirmDismiss = MessageBox.Show($"Czy na pewno chcesz zwolnić pracownika " +
                $"{(selectedEmployee.Cells[1].Value.ToString() + " " + selectedEmployee.Cells[2].Value.ToString()).Trim()}",
                "Zwalnianie pracownika", MessageBoxButtons.OKCancel);

            if(confirmDismiss == DialogResult.OK)
            {
                DismissStudent(Convert.ToInt32(selectedEmployee.Cells[0].Value));
                RefreshEmployeesList();
            }
        }
        private void DismissStudent(int id)
        {
            _employees = _fileHelper.DeserializeFromFile();
            var employee = _employees.FirstOrDefault(x => x.Id == id);

            if(employee.HireDate < DateTime.Now)
            {
                employee.DismissDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                employee.StatusId = 4;
                MessageBox.Show("Pracownik został zwolniony!");
            }
            else
            {
                MessageBox.Show("Pracownik nie został jeszcze zatrudniony!");
            }
            _fileHelper.SerializeToFile(_employees);
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
                IsMaximize = true;
            else
                IsMaximize = false;

            Settings.Default.Save();
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            RefreshEmployeesList();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            RefreshEmployeesList();
        }
    }
}
