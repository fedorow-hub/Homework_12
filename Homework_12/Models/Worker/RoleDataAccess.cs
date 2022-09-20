using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12.Models.Worker
{
    public class RoleDataAccess
    {
        public CommandsAccess Commands;
        public EditFieldsAccess EditFields;

        public RoleDataAccess(CommandsAccess commands, EditFieldsAccess editFields)
        {
            Commands = commands;
            EditFields = editFields;
        }
    }

    public struct CommandsAccess
    {
        public bool AddClient;
        public bool DelClient;
        public bool EditClient;
    }

    public struct EditFieldsAccess
    {
        public bool FirstName;
        public bool LastName;
        public bool MidleName;
        public bool PhoneNumber;
        public bool PassortData;
    }
}
