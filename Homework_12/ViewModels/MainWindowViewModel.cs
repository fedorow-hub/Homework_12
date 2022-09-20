using Homework_12.Infrastructure.Comands;
using Homework_12.Models.Bank;
using Homework_12.Models.Client;
using Homework_12.Models.Department;
using Homework_12.Models.Worker;
using Homework_12.ViewModels.Base;
using Homework_12.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.IO;
using System.Windows.Controls;

namespace Homework_12.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Window title

        private string title;
        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Title
        {
            get => title;
            set => Set(ref title, value);
        }
        #endregion

        public Action UpdateClientsList;

        public Action UpdateDepartmentList;

        public Bank Bank { get; private set; }

        public Worker Worker { get; private set; }

        //public Action UpdateClientsList;

        private ObservableCollection<Client> clients;

        public ObservableCollection<Client> Clients
        {
            get => clients;
            set => Set(ref clients, value);
        }

        private ObservableCollection<Department> departments;

        public ObservableCollection<Department> Departments
        {
            get => departments;
            set => Set(ref departments, value);
        }

        public MainWindowViewModel()
        {

        }

        public MainWindowViewModel(Worker worker)
        {
            Bank = new Bank("Банк А", new DepartmentRepository("departments.json"), worker);
            this.title = $"{Bank.Name}. Работа с клиентами";
            Worker = worker;

            Clients = new ObservableCollection<Client>();

            Departments = new ObservableCollection<Department>();

            //Departments = (List<Department>)Bank.DepartmentRepository.Departments;

            //#region commands
            //DeleteClientCommand = new LambdaCommand(OnDeleteClientCommandExecute, CanDeleteClientCommandExecute);
            OutLoggingCommand = new LambdaCommand(OnOutLoggingCommandExecute, CanOutLoggingCommandExecute);
            AddClientCommand = new LambdaCommand(OnAddClientCommandExecute, CanAddClientCommandExecute);
            AddDepartmentCommand = new LambdaCommand(OnAddDepartmentCommandExecute, CanAddDepartmentCommandExecute);
            //EditClientCommand = new LambdaCommand(OnEditClientCommandExecute, CanEditClientCommandExecute);
            //#endregion

            _enableAddClient = Worker.DataAccess.Commands.AddClient;
            //_enableDelClient = Worker.DataAccess.Commands.DelClient;
            //_enableEditClient = Worker.DataAccess.Commands.EditClient && Clients.Count > 0;

            //UpdateClientsList += UpdateClients;
            //UpdateClientsList.Invoke();

            UpdateDepartmentList += UpdateDeparments;
            UpdateDepartmentList.Invoke();
        }

        /// <summary>
        /// Обновление списка отделов
        /// </summary>
        private void UpdateDeparments()
        {
            Departments.Clear();
            foreach (var department in Bank.DepartmentRepository.Departments)
            {
                Departments.Add(department);
            }
        }



        #region Команды
        #region OutLoggingCommand
        public ICommand OutLoggingCommand { get; }

        private bool CanOutLoggingCommandExecute(object p) => true;

        private void OnOutLoggingCommandExecute(object p)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();

            if (p is Window window)
            {
                window.Close();
            }
        }
        #endregion

        #region AddClientCommand

        public ICommand AddClientCommand { get; }

        private bool CanAddClientCommandExecute(object p)
        {
            if (_enableAddClient == true)
                return true;
            else return false;
        }
        private void OnAddClientCommandExecute(object p)
        {
            ClientInfoWindow infoWindow = new ClientInfoWindow();
            ClientInfoViewModel viewModel = new ClientInfoViewModel(new Client(), Bank, this, Worker.DataAccess, Worker);
            infoWindow.DataContext = viewModel;
            infoWindow.Show();
        }

        #endregion

        #region AddDepartmentCommand

        public ICommand AddDepartmentCommand { get; }

        private bool CanAddDepartmentCommandExecute(object p)
        {
            if (_enableAddClient == true)
                return true;
            else return false;
        }
        private void OnAddDepartmentCommandExecute(object p)
        {
            DeparimentInfoWindow infoWindow = new DeparimentInfoWindow();
            if (p is TreeView treeView)
            {                
                DepartmentInfoViewModel viewModel = new DepartmentInfoViewModel(new Department(), 
                    (Department)treeView.SelectedItem, this, Bank);
                infoWindow.DataContext = viewModel;
                infoWindow.Show();
            }   
        }
        #endregion

        #endregion

        #region SelectedClient

        private Client _SelectedClient;
        /// <summary>
        /// Выбранный клиент
        /// </summary>
        public Client SelectedClient
        {
            get { return _SelectedClient; }
            set => Set(ref _SelectedClient, value);
        }
        #endregion

        #region SelectedDepartment

        private Department _SelectedDepartment;
        /// <summary>
        /// Выбранный отдел
        /// </summary>
        public Department SelectedDepartment
        {
            get { return _SelectedDepartment; }
            set => Set(ref _SelectedDepartment, value);            
        }
        #endregion


        #region EnableAddClient
        private bool _enableAddClient;
        public bool EnableAddClient
        {
            get => _enableAddClient;
            set => Set(ref _enableAddClient, value);
        }
        #endregion

        #region EnableDelClient
        private bool _enableDelClient;
        public bool EnableDelClient
        {
            get => _enableDelClient;
            set => Set(ref _enableDelClient, value);
        }
        #endregion

        #region EnableEditClient
        private bool _enableEditClient;
        public bool EnableEditClient
        {
            get => _enableEditClient;
            set => Set(ref _enableEditClient, value);
        }
        #endregion
        
    }
}
