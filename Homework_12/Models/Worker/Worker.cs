using Homework_12.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12.Models.Worker
{
    public abstract class Worker
    {
        public RoleDataAccess DataAccess { get; protected set; }

        /// <summary>
        /// Получение информации о клиенте
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public abstract ClientAccessInfo GetClientInfo(Client.Client client);
    }
}
