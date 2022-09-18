using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12.Models.Worker
{
    internal abstract class Worker
    {
        public RoleDataAccess DataAccess { get; protected set; }

        
    }
}
