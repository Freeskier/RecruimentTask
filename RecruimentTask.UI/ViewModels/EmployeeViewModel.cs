using Microsoft.Win32;
using RecruimentTask.DAL.Database;
using RecruimentTask.DAL.Models;
using RecruimentTask.UI.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Data.Entity;
using RecruimentTask.UI.Views;

namespace RecruimentTask.UI.ViewModels
{
    public class EmployeeViewModel : ViewModelBase
    {
        private readonly DataContext _dataContext;
        private RelayCommand _loadDataCommand;
        private RelayCommand _editEmployeeCommand;
        private RelayCommand _deleteDataCommand;
        private ObservableCollection<Employee> _employees;
        private Employee _selectedEmployee;
        private bool _canEdit;

        public EmployeeViewModel(DataContext dataContext)
        {
            _dataContext = dataContext;
            ReloadTableData();
        }


        public bool CanEdit
        {
            get => _canEdit;
            set
            {
                _canEdit = value;
                OnPropertyChanged();
            }
        }
        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                CanEdit = value != null;
                _selectedEmployee = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Employee> Employees
        {
            get => _employees;
            set
            {
                _employees = value;
                OnPropertyChanged();
            }
        }
        public ICommand DeteleDataCommand =>
            _deleteDataCommand ??
            (_deleteDataCommand = new RelayCommand(
                async param => await DeleteData()));
               

        public ICommand EditEmployeeCommand =>
            _editEmployeeCommand ??
            (_editEmployeeCommand = new RelayCommand(
                param => EditEmployee()));

        public ICommand LoadDataCommand =>
            _loadDataCommand ??
            (_loadDataCommand = new RelayCommand(
                async param => await LoadData()));

        private async Task LoadData()
        {
            var employeesFromFile = ReadEmployeesFromFile();
            _dataContext.Employees.AddRange(employeesFromFile);
            await _dataContext.SaveChangesAsync();
            await ReloadTableData();
        }

        private void EditEmployee()
        {
            var editWindow = new EditEmployeeWindow();
            editWindow.DataContext = new EditEmployeeViewModel(_dataContext, SelectedEmployee);
            editWindow.Show();
        }
        private async Task DeleteData()
        {
            try
            {
                await _dataContext.Database.ExecuteSqlCommandAsync("DELETE FROM Employees");
                await _dataContext.SaveChangesAsync();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }

            await ReloadTableData();
        }

        private async Task ReloadTableData()
        {
            Employees = new ObservableCollection<Employee>(await _dataContext.Employees.ToListAsync());
        }

        private List<Employee> ReadEmployeesFromFile()
        {
            var fileDialog = new OpenFileDialog
            {
                DefaultExt = ".csv",
                Filter = "Document CSV (*.csv)|*.csv"
            };

            if (fileDialog.ShowDialog() == true)
            {
                try
                {
                    return File.ReadAllLines(fileDialog.FileName)
                       .Skip(1)
                       .Select(x => GetEmployeeFromCSV(x))
                       .ToList();
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }

            return new List<Employee>();
        }

        public Employee GetEmployeeFromCSV(string csvLine)
        {
            string[] line = csvLine.Split(',');
            if (line.Length != 5)
                throw new Exception("Given CSV does not match with Employee model to convert.");
            return new Employee
            {
                EmployeeID = int.Parse(line[0]),
                Name = line[1],
                Surname = line[2],
                Email = line[3],
                Phone = line[4]
            };
        }
    }
}
