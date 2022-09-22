using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Homework_12.Models.Client;
using System.IO;

namespace Homework_12.Models.Department
{    

    public class DepartmentRepository : IEnumerable<Department>
    {
        /// <summary>
        /// ID клиента
        /// </summary>
        private static int Id;
                
        static DepartmentRepository()
        {
            Id = 0;            
        }

        /// <summary>
        /// Получение следующего свободного идентификатора клиента
        /// </summary>
        /// <returns></returns>
        private static int NextId() => ++Id;
                
        private List<Department>? _departments;
        public List<Department>? Departments => _departments;

        private List<Client.Client>? _clients;
        public List<Client.Client>? Clients => _clients;        

        /// <summary>
        /// Файл репозитория
        /// </summary>
        string _path;
        public DepartmentRepository(string path)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }
            _path = path;

            if (File.Exists(_path)) // если файл существует, подгружаем данные
            {
                Load();
                return;
            }
            // если файл не существует, создаем новый пустой репозиторий
            File.Create(_path);
            NoDepartmentsForLoad();
        }

        /// <summary>
        /// Кол-во клиентов
        /// </summary>
        public int Count => Clients.Count();

        /// <summary>
        /// Добавление нового клиента
        /// </summary>
        /// <param name="client">клиент</param>
        public void InsertClient(Department department, Client.Client client)
        {
            if (client is null)
                return;
            client.Id = NextId();
            department.clients.Add(client);             
            Save();
        }

        /// <summary>
        /// Добавление нового отдела
        /// </summary>
        /// <param name="client">клиент</param>
        public void InsertDepartment(Department parentDepartment, Department childDepartment)
        {
            if (childDepartment is null)
                return; 
            if(parentDepartment == null)
            {
                _departments.Add(childDepartment);
            }
            else
            {
                parentDepartment.departments.Add(childDepartment);
            }                      
            Save();
        }

        /// <summary>
        /// Редактирование названия отдела
        /// </summary>
        /// <param name="department"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void UpdateDepartment(Department department)
        {
            if (!_departments.Any(c => c.Id == department.Id))
            {
                throw new ArgumentOutOfRangeException(nameof(department), "Такого объекта нет в списке");
            }

            _departments[_departments.IndexOf(_departments.First(c => c.Id == department.Id))] = department;
            Save();
        }

        /// <summary>
        /// Обновление данных о клиенте
        /// </summary>
        /// <param name="client"></param>
        public void UpdateClient(Client.Client client)
        {
            if (!_clients.Any(c => c.Id == client.Id))
            {
                throw new ArgumentOutOfRangeException(nameof(client), "Такого объекта нет в списке");
            }

            _clients[_clients.IndexOf(_clients.First(c => c.Id == client.Id))] = client;
            Save();
        }

        /// <summary>
        /// Удаление клиента
        /// </summary>
        /// <param name="clientId">ИД клиента</param>
        public void DeleteClient(int clientId)
        {
            if (_clients.Any(c => c.Id == clientId))
            {
                _clients.Remove(_clients.FirstOrDefault(c => c.Id == clientId));
            }
            Save();
        }

        /// <summary>
        /// Удаление отдела
        /// </summary>
        /// <param name="department"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void DeleteDepartment(Department parentDepartment, Department deletingDepartment)
        {
            if (parentDepartment.departments.Count > 0)
            {
                foreach (var item in parentDepartment.departments)
                {
                    if (item.Id == deletingDepartment.Id)
                    {
                        parentDepartment.departments.Remove(item);
                        Save();
                        return;
                    }
                    else
                    {
                        DeleteDepartment(item, deletingDepartment);
                    }
                }
            }

        }

         




        /// <summary>
        /// Сохранение списка отделов с включенными в них клиентами в файл
        /// </summary>
        void Save()
        {
            string? dirPath = Path.GetFileName(Path.GetDirectoryName(_path));
            if (dirPath is null)
                return;
            //if (!Directory.Exists(dirPath))
            //{
            //    Directory.CreateDirectory(dirPath);
            //}
            string json = JsonSerializer.Serialize(_departments);
            File.WriteAllText(_path, json);
        }
               

        /// <summary>
        /// Загрузка списка отделов
        /// </summary>
        void Load()
        {
            string data = File.ReadAllText(_path);
            if (string.IsNullOrEmpty(data))
            {
                NoDepartmentsForLoad();
                return;
            }
            _departments = JsonSerializer.Deserialize<List<Department>>(data, new JsonSerializerOptions()
            {
                //PropertyNameCaseInsensitive = true
            });

            if (_departments is null)
            {
                NoDepartmentsForLoad();
                return;
            }            
        }

        /// <summary>
        /// Обработка ситуации, когда не возможно загрузить отделы
        /// </summary>
        private void NoDepartmentsForLoad()
        {
            _departments = new List<Department>();            
        }

        public IEnumerator<Department> GetEnumerator()
        {
            for (int i = 0; i < Departments.Count(); i++)
            {
                yield return Departments[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
