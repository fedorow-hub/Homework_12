using Homework_12.Infrastructure;
using Homework_12.Infrastructure.Comands;
using Homework_12.Models.Bank;
using Homework_12.Models.Client;
using Homework_12.Models.Worker;
using Homework_12.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Homework_12.Models.Department;

namespace Homework_12.ViewModels
{
    internal class ClientInfoViewModel : ViewModel
    {
        private ClientAccessInfo currentClient { get; set; }
        private Bank bank { get; set; }

        private Department Department { get; set; }

        private RoleDataAccess _dataAccess;

        private Worker Worker { get; set; }

        private MainWindowViewModel MainWindowViewModel { get; set; }

        public ClientInfoViewModel() { }

        public ClientInfoViewModel(ClientAccessInfo clientInfo, Bank bank,
            MainWindowViewModel mainWindowViewModel, RoleDataAccess dataAccess, Worker worker, Department department)
        {
            this.currentClient = clientInfo;
            this.bank = bank;
            MainWindowViewModel = mainWindowViewModel;
            Department = department;

            _dataAccess = dataAccess;

            Worker = worker;

            FillFields(currentClient);
            EnableFields(dataAccess);
            CheckSaveClient(dataAccess);

            OutCommand = new LambdaCommand(OnOutCommandExecute, CanOutCommandExecute);
            SaveCommand = new LambdaCommand(OnSaveCommandExecute, CanSaveCommandExecute);
        }

        /// <summary>
        /// Заполнение данных
        /// </summary>
        /// <param name="clientInfo"></param>
        private void FillFields(ClientAccessInfo clientInfo)
        {
            if (clientInfo is null)
                return;
            _firstname = clientInfo.Firstname ?? String.Empty;
            _lastname = clientInfo.Lastname ?? String.Empty;
            _patronymic = clientInfo.Patronymic ?? String.Empty;
            _phoneNumber = clientInfo.PhoneNumber?.ToString() ?? String.Empty;
            _passportSerie = clientInfo.PassportSerie ?? String.Empty;
            _passportNumber = clientInfo.PassportNumber ?? String.Empty;
        }

        /// <summary>
        /// Включение/отключения возможности ввода данных
        /// </summary>
        /// <param name="dataAccess"></param>
        private void EnableFields(RoleDataAccess dataAccess)
        {
            _enableFirstName = dataAccess.EditFields.FirstName;
            _enableLastName = dataAccess.EditFields.LastName;
            _enablePatronymic = dataAccess.EditFields.MidleName;
            _enablePassportData = dataAccess.EditFields.PassortData;
            _enablePhoneNumber = dataAccess.EditFields.PhoneNumber;

            _borderFirstName = InputHighlighting(_enableFirstName, _firstname.Length > 0);
            _borderLastName = InputHighlighting(_enableLastName, _lastname.Length > 0);
            _borderPatronymic = InputHighlighting(_enablePatronymic, _patronymic.Length > 0);
            _borderPhoneNumber = InputHighlighting(_enablePhoneNumber, Models.Client.PhoneNumber.IsPhoneNumber(_phoneNumber));           
            _borderPassportSerie = InputHighlighting(_enablePassportData, Passport.IsSeries(_passportSerie));
            _borderPassportNumber = InputHighlighting(_enablePassportData, Passport.IsNumber(_passportNumber));
            
        }

        /// <summary>
        /// метод для блокирования кнопки сохранения, если введенные данные не валидны
        /// </summary>
        /// <param name="dataAccess"></param>
        private void CheckSaveClient(RoleDataAccess dataAccess)
        {
            if (dataAccess.EditFields.PassortData == false)
            {
                EnableSaveClient = _borderFirstName != InputValueValidationEnum.Error
                               && _borderLastName != InputValueValidationEnum.Error
                               && _borderPatronymic != InputValueValidationEnum.Error
                               && _borderPhoneNumber != InputValueValidationEnum.Error;
            }
            else
            {
                EnableSaveClient = _borderFirstName != InputValueValidationEnum.Error
                               && _borderLastName != InputValueValidationEnum.Error
                               && _borderPatronymic != InputValueValidationEnum.Error
                               && _borderPassportSerie != InputValueValidationEnum.Error
                               && _borderPassportNumber != InputValueValidationEnum.Error
                               && _borderPhoneNumber != InputValueValidationEnum.Error;
            }
        }

        /// <summary>
        /// метод установки модификаторов валидности
        /// </summary>
        /// <param name="isEnable"></param>
        /// <param name="isValid"></param>
        /// <returns></returns>
        private InputValueValidationEnum InputHighlighting(bool isEnable, bool isValid)
        {
            if (!isValid) return InputValueValidationEnum.Error;
            if (!isEnable) return InputValueValidationEnum.Disable;

            return InputValueValidationEnum.Default;
        }

        #region ClientInfo

        #region Firstname
        private string _firstname;
        public string Firstname
        {
            get => _firstname;
            set
            {
                Set(ref _firstname, value);
                BorderFirstName =
                InputHighlighting(_enableFirstName, _firstname.Length > 2);
            }
        }

        private bool _enableFirstName;
        public bool EnableFirstName
        {
            get => _enableFirstName;
            set => Set(ref _enableFirstName, value);
        }

        private InputValueValidationEnum _borderFirstName;
        public InputValueValidationEnum BorderFirstName
        {
            get => _borderFirstName;
            set
            {
                Set(ref _borderFirstName, value);
                CheckSaveClient(_dataAccess);
            }
        }

        #endregion

        #region Lastname
        private string _lastname;
        public string Lastname
        {
            get => _lastname;
            set
            {
                Set(ref _lastname, value);
                BorderLastName =
                InputHighlighting(_enableLastName, _lastname.Length > 2);
            }
        }

        private bool _enableLastName;
        public bool EnableLastName
        {
            get => _enableLastName;
            set => Set(ref _enableLastName, value);
        }

        private InputValueValidationEnum _borderLastName;
        public InputValueValidationEnum BorderLastName
        {
            get => _borderLastName;
            set
            {
                Set(ref _borderLastName, value);
                CheckSaveClient(_dataAccess);
            }
        }

        #endregion

        #region Patronymic
        private string _patronymic;
        public string Patronymic
        {
            get => _patronymic;
            set
            {
                Set(ref _patronymic, value);
                BorderPatronymic =
                InputHighlighting(_enablePatronymic, _patronymic.Length > 2);
            }
        }

        private bool _enablePatronymic;
        public bool EnablePatronymic
        {
            get => _enablePatronymic;
            set => Set(ref _enablePatronymic, value);
        }

        private InputValueValidationEnum _borderPatronymic;
        public InputValueValidationEnum BorderPatronymic
        {
            get => _borderPatronymic;
            set
            {
                Set(ref _borderPatronymic, value);
                CheckSaveClient(_dataAccess);
            }
        }

        #endregion

        #region PhoneNumber
        private string _phoneNumber;

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                Set(ref _phoneNumber, value);
                BorderPhoneNumber = InputHighlighting(_enablePhoneNumber, Models.Client.PhoneNumber.IsPhoneNumber(_phoneNumber));
            }
        }

        private bool _enablePhoneNumber;
        public bool EnablePhoneNumber
        {
            get => _enablePhoneNumber;
            set => Set(ref _enablePhoneNumber, value);
        }

        private InputValueValidationEnum _borderPhoneNumber;
        public InputValueValidationEnum BorderPhoneNumber
        {
            get => _borderPhoneNumber;
            set
            {
                Set(ref _borderPhoneNumber, value);
                CheckSaveClient(_dataAccess);
            }
        }

        #endregion

        #region PassportData
        private string _passportSerie;
        public string PassportSerie
        {
            get => _passportSerie;
            set
            {
                Set(ref _passportSerie, value);
                BorderPassportSerie = InputHighlighting(_enablePassportData, Passport.IsSeries(_passportSerie));
            }
        }

        private string _passportNumber;
        public string PassportNumber
        {
            get => _passportNumber;
            set
            {
                Set(ref _passportNumber, value);
                BorderPassportNumber = InputHighlighting(_enablePassportData, Passport.IsNumber(_passportNumber));
            }
        }

        private bool _enablePassportData;
        public bool EnablePassportData
        {
            get => _enablePassportData;
            set => Set(ref _enablePassportData, value);
        }

        private InputValueValidationEnum _borderPassportSerie;
        public InputValueValidationEnum BorderPassportSerie
        {
            get => _borderPassportSerie;
            set
            {
                Set(ref _borderPassportSerie, value);
                if (_dataAccess.EditFields.PassortData == true)
                {
                    CheckSaveClient(_dataAccess);
                }
            }
        }

        private InputValueValidationEnum _borderPassportNumber;
        public InputValueValidationEnum BorderPassportNumber
        {
            get => _borderPassportNumber;
            set
            {
                Set(ref _borderPassportNumber, value);
                if (_dataAccess.EditFields.PassortData == true)
                {
                    CheckSaveClient(_dataAccess);
                }
            }
        }

        #endregion

        #endregion      

        #region Commands
        #region OutCommand
        public ICommand OutCommand { get; }

        private bool CanOutCommandExecute(object p) => true;

        private void OnOutCommandExecute(object p)
        {
            if (p is Window window)
            {
                window.Close();
            }
        }
        #endregion

        #region SaveCommand

        public ICommand SaveCommand { get; }

        private bool CanSaveCommandExecute(object p) => true;

        private void OnSaveCommandExecute(object p)
        {
            var client = new Client(_firstname, _lastname, _patronymic,
                new PhoneNumber(_phoneNumber), _enablePassportData ? new Passport(int.Parse(_passportSerie), int.Parse(_passportNumber)) :
                new Passport(currentClient.SeriesAndNumberOfPassport.Serie, currentClient.SeriesAndNumberOfPassport.Number));

            if (currentClient.Id == 0) // новый клиент
            {
                bank.AddClient(Department, client);
            }
            else
            {
                client.Id = currentClient.Id;
                bank.EditClient(client);
            }

            MainWindowViewModel.UpdateClientsList.Invoke();

            if (p is Window window)
            {
                window.Close();
            }
        }
        #endregion
        #endregion

        #region EnableSaveClient
        private bool _enableSaveClient;
        public bool EnableSaveClient
        {
            get => _enableSaveClient;
            set => Set(ref _enableSaveClient, value);
        }
        #endregion  
    }
}
