using Homework_12.Models.Department;
using Homework_12.Models.Worker;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Homework_12.Models.Bank
{
    public class Bank
    {
        /// <summary>
        /// Наименование Банка.
        /// </summary>
        public string Name { get; private set; }

        private Worker.Worker _worker;

        private DepartmentRepository departmentRepository;

        public DepartmentRepository DepartmentRepository { get { return departmentRepository; } set { departmentRepository = value; } }
        public Bank(string name, DepartmentRepository departmentRepository, Worker.Worker worker)
        {
            Name = name;
            this.departmentRepository = departmentRepository;
            this._worker = worker;
        }

        public void AddClient(Department.Department department, Client.Client client)
        {
            DepartmentRepository.InsertClient(department, client);
        }

        public void EditClient(Client.Client client)
        {
            DepartmentRepository.UpdateClient(client);
        }

        public void DeleteClient(Client.Client client)
        {
            DepartmentRepository.DeleteClient(client.Id);
        }

        public void AddDepartment(Department.Department parentDepartment, Department.Department childDepartment)
        {            
            DepartmentRepository.InsertDepartment(parentDepartment, childDepartment);
                       
        }

        public void UpdateDepartment(Department.Department department)
        {
            DepartmentRepository.UpdateDepartment(department);
        }

        ///// <summary>
        ///// Получение сведений об отделах        
        ///// </summary>
        ///// <returns></returns>
        //public IEnumerable<Department.Department> GetDepartment()
        //{
        //    var listDepartments = new List<Department.Department>();
        //    foreach (var department in DepartmentRepository.Departments)
        //    {
        //        listDepartments.Add(department);
        //    }
        //    return listDepartments;
        //}

    }
}
