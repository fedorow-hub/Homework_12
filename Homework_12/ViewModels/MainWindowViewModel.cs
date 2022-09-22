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
        /// <summary>
        /// Выбранный отдел
        /// </summary>
        private Department selectedDepartment;
        /// <summary>
        /// Выбранный отдел
        /// </summary>
        public Department SelectedDepartment
        {
            get => selectedDepartment;
            set => Set(ref selectedDepartment, value);
        }

        public MainWindowViewModel(){}

        public MainWindowViewModel(Worker worker)
        {
            Bank = new Bank("Банк А", new DepartmentRepository("departments.json"), worker);
            this.title = $"{Bank.Name}. Работа с клиентами";
            Worker = worker;

            Clients = new ObservableCollection<Client>();

            Departments = new ObservableCollection<Department>();
                       
            //#region commands
            //DeleteClientCommand = new LambdaCommand(OnDeleteClientCommandExecute, CanDeleteClientCommandExecute);
            OutLoggingCommand = new LambdaCommand(OnOutLoggingCommandExecute, CanOutLoggingCommandExecute);
            AddClientCommand = new LambdaCommand(OnAddClientCommandExecute, CanAddClientCommandExecute);
            AddDepartmentCommand = new LambdaCommand(OnAddDepartmentCommandExecute, CanAddDepartmentCommandExecute);
            DeleteDepartmentCommand = new LambdaCommand(OnDeleteDepartmentCommandExecute, CanDeleteDepartmentCommandExecute);            
            //EditClientCommand = new LambdaCommand(OnEditClientCommandExecute, CanEditClientCommandExecute);
            //#endregion

            _enableAddClient = Worker.DataAccess.Commands.AddClient;
            _enableDelClient = Worker.DataAccess.Commands.DelClient;
            _enableEditClient = Worker.DataAccess.Commands.EditClient && Clients.Count > 0;

            UpdateClientsList += UpdateClients;
            UpdateClientsList.Invoke();

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

        /// <summary>
        /// Обновление списка клиентов
        /// </summary>
        private void UpdateClients()
        {
            Clients.Clear();
            foreach (var clientInfo in GetClientsInfo())
            {
                Clients.Add(clientInfo);
            }
        }

        /// <summary>
        /// Получение сведений о клиентах
        /// представление зависит от работника
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ClientAccessInfo> GetClientsInfo()
        {
            var clientsInfo = new List<ClientAccessInfo>();
            foreach (var client in Bank.DepartmentRepository.Clients??new List<Client>())
            {
                clientsInfo.Add(Worker.GetClientInfo(client));
            }
            return clientsInfo;
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
            if(p is TreeView treeView)
            {
                ClientInfoWindow infoWindow = new ClientInfoWindow();
                ClientInfoViewModel viewModel = new ClientInfoViewModel(new ClientAccessInfo(), Bank, this, Worker.DataAccess, Worker, (Department)treeView.SelectedItem);
                infoWindow.DataContext = viewModel;
                infoWindow.Show();
            }
            
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

        #region DeleteDepartmentCommand

        public ICommand DeleteDepartmentCommand { get; }

        private bool CanDeleteDepartmentCommandExecute(object p)
        {
            if(p is TreeView treeView)
            {
                if (treeView.SelectedItem == null)
                    return false;
            }
            if (_enableDelClient == true)
                return true;
            else return false;
        }
        private void OnDeleteDepartmentCommandExecute(object p)
        {
            if (p is null) return;
            if (p is TreeView treeView)
            {
                Bank.DeleteDepartment((Department)treeView.SelectedItem);
                UpdateDepartmentList.Invoke();
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

        /*#region SelectedDepartment

        private Department _SelectedDepartment;
        /// <summary>
        /// Выбранный отдел
        /// </summary>
        public Department SelectedDepartment
        {
            get { return _SelectedDepartment; }
            set => Set(ref _SelectedDepartment, value);            
        }
        #endregion*/


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
