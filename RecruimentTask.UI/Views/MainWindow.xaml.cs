using RecruimentTask.DAL.Database;
using RecruimentTask.UI.ViewModels;
using System.Windows;


namespace RecruimentTask.UI.Views
{
    public partial class MainWindow : Window
    {
        private DataContext context;
        public MainWindow()
        {
            context = new DataContext();
            DataContext = new EmployeeViewModel(context);
            InitializeComponent();
        }
    }
}
