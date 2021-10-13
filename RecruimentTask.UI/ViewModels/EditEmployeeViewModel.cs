using RecruimentTask.DAL.Database;
using RecruimentTask.DAL.Models;
using RecruimentTask.UI.Commands;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RecruimentTask.UI.ViewModels
{
    public class EditEmployeeViewModel : ViewModelBase
    {
        private readonly DataContext _context;
        private Employee _employee;
        private RelayCommand _editEmployeeCommand;

        public EditEmployeeViewModel(DataContext context, Employee employee)
        {
            _context = context;
            _employee = employee;
        }
        public Employee Employee
        {
            get => _employee;
            set
            {
                _employee = value;
                OnPropertyChanged();
            }
        }

        public ICommand EditEmployeeCommand =>
            _editEmployeeCommand ??
            (_editEmployeeCommand = new RelayCommand(
                async param => await EditEmployee(param)));

        private async Task EditEmployee(object param)
        {
            var entity = await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeID == _employee.EmployeeID);
            if (entity == null)
                throw new Exception("Selected entity is null.");
            entity.Name = _employee.Name;
            entity.Surname = _employee.Surname;
            entity.Email = _employee.Email;
            entity.Phone = _employee.Phone;
            await _context.SaveChangesAsync();
            var window = (Window)param;
            window.Close();
        }
    }
}
