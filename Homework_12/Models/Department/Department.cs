using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12.Models.Department
{
    public class Department
    {
        #region Поля

        /// <summary>
        /// идентификационный номер отдела
        /// </summary>
        Guid id;
        /// <summary>
        /// Наименование отдела
        /// </summary>
        string name;

        #endregion

        /// <summary>
        /// Список клиентов отдела
        /// </summary>
        public List<Client.Client> clients { get; set; }

        /// <summary>
        /// Список отделов, вложенных в текущий отдел
        /// </summary>
        public List<Department> departments { get; set; }

        public Department() { }

        /// <summary>
        /// Конструктор, заполняющий базу данных Department
        /// </summary>
        /// <param name="Count">Количество сотрудников, которых нужно создать</param>
        public Department(string Name)
        {
            this.id = Guid.NewGuid();
            this.name = Name;
            this.clients = new List<Client.Client>();            
            this.departments = new List<Department>();
        }        

        /// <summary>
        /// метод добавления отдела в структуру вышестоящего отдела
        /// </summary>
        /// <param name="dep">структура вышестоящего отдела</param>
        /// <param name="Name">название добавляемого отдела</param>           
        public void AddDepartment(Department dep, string name)
        {
            dep.departments.Add(new Department(name));
        }        

        /// <summary>
        /// Методо добавления клиента
        /// </summary>         
        public void AddClient(Client.Client client)
        {
            clients.Add(client);            
        }
        /// <summary>
        /// метод добавления работника в конкретный департамент
        /// </summary>        
        public void AddClientToDep(Department dep, Client.Client client)
        {
            dep.AddClient(client);
        }

        public void EditClient()
        {

        }
               

        #region Свойства
        /// <summary>
        /// Наименование департамента
        /// </summary>
        public string Name { get { return this.name; } set { this.name = value; } }

        public Guid Id { get { return id; } set { id = value; } }

        #endregion
    }
}
