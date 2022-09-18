using Homework_12.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12.Models.Worker
{
    internal class Manager : Worker
    {
        public Manager()
        {
            DataAccess = new RoleDataAccess(
                new CommandsAccess
                {
                    AddClient = true,
                    EditClient = true,
                    DelClient = true
                },
                new EditFieldsAccess()
                {
                    FirstName = true,
                    LastName = true,
                    MidleName = true,
                    PassortData = true,
                    PhoneNumber = true
                });
        }
        public override string ToString()
        {
            return "Менеджер";
        }
    }
}
