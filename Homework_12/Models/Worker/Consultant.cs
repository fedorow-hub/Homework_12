using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12.Models.Worker
{
    internal class Consultant : Worker
    {
        public Consultant()
        {
            DataAccess = new RoleDataAccess(
                new CommandsAccess
                {
                    AddClient = false,
                    EditClient = true,
                    DelClient = false
                },
                new EditFieldsAccess()
                {
                    FirstName = false,
                    LastName = false,
                    MidleName = false,
                    PassortData = false,
                    PhoneNumber = true
                });
        }
        public override string ToString()
        {
            return "Консультант";
        }
    }
}
