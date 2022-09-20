using Homework_12.Infrastructure;
using Homework_12.Infrastructure.Comands;
using Homework_12.Models.Bank;
using Homework_12.Models.Client;
using Homework_12.Models.Department;
using Homework_12.Models.Worker;
using Homework_12.ViewModels.Base;
using Homework_12.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Homework_12.ViewModels
{
    internal class DepartmentInfoViewModel : ViewModel
    {
        public Department parentDepartment { get; set; }

        private MainWindowViewModel MainWindowViewModel { get; set; }

        private Bank bank { get; set; }

        public DepartmentInfoViewModel()
        {
            
        }
        public DepartmentInfoViewModel(Department departmentInfo, Department parentDepartment, MainWindowViewModel mainWindowViewModel, Bank bank)
        {
            this.parentDepartment = parentDepartment;
            FillField(departmentInfo);
            this.bank = bank;

            this.MainWindowViewModel=mainWindowViewModel;

            OutDepartmentCommand = new LambdaCommand(OnOutDepartmentCommandExecuted, CanOutDepartmentCommandExecute);
            SaveDepartmentCommand = new LambdaCommand(OnSaveDepartmentCommandExecuted, CanSaveDepartmentCommandExecute);
            
        }

        /// <summary>
        /// Заполнение данных
        /// </summary>
        /// <param name="clientInfo"></param>
        private void FillField(Department departmentInfo)
        {
            if (departmentInfo is null)
                return;
            _nameDepartment = departmentInfo.Name ?? String.Empty;
            
        }

        /// <summary>
        /// метод установки модификаторов валидности
        /// </summary>
        /// <param name="isEnable"></param>
        /// <param name="isValid"></param>
        /// <returns></returns>
        private InputValueValidationEnum InputHighlighting(bool isValid)
        {
            if (!isValid) return InputValueValidationEnum.Error;            

            return InputValueValidationEnum.Default;
        }

        /// <summary>
        /// метод для блокирования кнопки сохранения, если введенные данные не валидны
        /// </summary>
        /// <param name="dataAccess"></param>
        private void CheckSaveClient()
        {
            EnableSaveDepartment = _borderNameDepartment != InputValueValidationEnum.Error;
        }

        #region NameDepartment
        private string _nameDepartment;
        public string NameDepartment
        {
            get => _nameDepartment;
            set
            {
                Set(ref _nameDepartment, value);
                BorderNameDepartment =
                InputHighlighting(_nameDepartment.Length > 2);
            }
        }

        private bool _enableFirstName;
        public bool EnableFirstName
        {
            get => _enableFirstName;
            set => Set(ref _enableFirstName, value);
        }

        private InputValueValidationEnum _borderNameDepartment;
        public InputValueValidationEnum BorderNameDepartment
        {
            get => _borderNameDepartment;
            set
            {
                Set(ref _borderNameDepartment, value);
                CheckSaveClient();
            }
        }

        #endregion

        #region OutDepartment
        public ICommand OutDepartmentCommand { get; }
        private void OnOutDepartmentCommandExecuted(object p)
        {
            if (p is Window window)
            {
                window.Close();
            }
        }
        private bool CanOutDepartmentCommandExecute(object p) => true;

        #endregion

        #region SaveDepartmentCommand
        public ICommand SaveDepartmentCommand { get; }
        private void OnSaveDepartmentCommandExecuted(object p)
        {
            var department = new Department(_nameDepartment);
            bank.AddDepartment(parentDepartment, department);

            //if (currentDepartment.Id == 0) // новый отдел
            //{


            //    //bank.AddDepartment(department);
            //}
            //else
            //{
            //    department.Id = currentDepartment.Id;
            //    bank.UpdateDepartment(department);
            //}
            MainWindowViewModel.UpdateDepartmentList.Invoke();
            if (p is Window window)
            {
                window.Close();
            }
        }
        private bool CanSaveDepartmentCommandExecute(object p) => true;

        #endregion

        #region EnableSaveDepartment
        private bool _enableSaveDepartment;
        public bool EnableSaveDepartment
        {
            get => _enableSaveDepartment;
            set => Set(ref _enableSaveDepartment, value);
        }
        #endregion    
    }
}
