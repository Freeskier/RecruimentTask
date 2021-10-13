
namespace RecruimentTask.DAL.Models
{
    public class Employee : ModelBase
    {
        private int _employeeId;
        private string _name;
        private string _surname;
        private string _email;
        private string _phone;

        public int EmployeeID
        {
            get => _employeeId;
            set
            {
                if (value == _employeeId) return;
                _employeeId = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                if (value == _surname) return;
                _surname = value;
                OnPropertyChanged();
            }
        }


        public string Email
        {
            get => _email;
            set
            {
                if (value == _email) return;
                _email = value;
                OnPropertyChanged();
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                if (value == _phone) return;
                _phone = value;
                OnPropertyChanged();
            }
        }
    }
}
