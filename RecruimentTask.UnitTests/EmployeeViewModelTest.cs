using NUnit.Framework;
using RecruimentTask.DAL.Database;
using RecruimentTask.DAL.Models;
using RecruimentTask.UI.ViewModels;
using System;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruimentTask.UnitTests
{
    [TestFixture]
    public class EmployeeViewModelTest
    {
        EmployeeViewModel vm;

        [SetUp]
        public void SetUp()
        {
            DataContext dataContext = new DataContext();
            vm = new EmployeeViewModel(dataContext);
        }

        [Test]
        public void GetEmployeeFromCSV_CorrectData()
        {
            string data = "123,Tim,Corey,timasd@wp.com,23134556";

            var output = vm.GetEmployeeFromCSV(data);
            var expected = new Employee
            {
                EmployeeID = 123,
                Name = "Tim",
                Surname = "Corey",
                Email = "timasd@wp.com",
                Phone = "23134556"
            };

            output.Should().BeEquivalentTo(expected);
        }

        [TestCase("123, Tim, Corey, timasd@wp.com, 23134556, asd")]
        [TestCase("123, Tim, Corey, timasd@wp.com")]
        public void GetEmployeeFromCSV_ThrowException(string lineOfCode)
        {
            Assert.Throws<Exception>(() => vm.GetEmployeeFromCSV(lineOfCode));
        }

        [Test]
        public void LoadDataCommand_IsWorkingProperly()
        {
            Assert.IsTrue(vm.LoadDataCommand.CanExecute(null));
        } 
        
        [Test]
        public void EditEmployeeCommand_IsWorkingProperly()
        {
            Assert.IsTrue(vm.EditEmployeeCommand.CanExecute(null));
        }  
        
        [Test]
        public void DeteleDataCommand_IsWorkingProperly()
        {
            Assert.IsTrue(vm.DeteleDataCommand.CanExecute(null));
        }
    }
}
