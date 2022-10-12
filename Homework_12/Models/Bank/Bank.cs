using Homework_12.Models.Client;
using Homework_12.Models.Department;

namespace Homework_12.Models.Bank
{
    public class Bank
    {
        /// <summary>
        /// Наименование Банка.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// worker
        /// </summary>
        private Worker.Worker _worker;

        public Department.Department MainDepartment { get; set; }        

        private DepartmentRepository departmentRepository;

        public DepartmentRepository DepartmentRepository { get { return departmentRepository; } set { departmentRepository = value; } }
        public Bank(string name, DepartmentRepository departmentRepository, Worker.Worker worker)
        {
            Name = name;
            this.departmentRepository = departmentRepository;
            this._worker = worker;

            MainDepartment = new Department.Department();
            MainDepartment.departments = departmentRepository.Departments;
        }

        public void AddClient(Department.Department department, ClientAccessInfo client)
        {
            DepartmentRepository.InsertClient(department, client);
        }

        public void EditClient(Department.Department department, ClientAccessInfo client)
        {
            DepartmentRepository.UpdateClient(department, client);
        }

        public void DeleteClient(Department.Department department, ClientAccessInfo client)
        {
            DepartmentRepository.DeleteClient(department, client.Id);
        }

        public void AddDepartment(Department.Department parentDepartment, Department.Department childDepartment)
        {            
            DepartmentRepository.InsertDepartment(parentDepartment, childDepartment);                       
        }

        public void DeleteDepartment(Department.Department department)
        {
            DepartmentRepository.DeleteDepartment(MainDepartment, department);
        }
    }
}
