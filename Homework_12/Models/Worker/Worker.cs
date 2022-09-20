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

        
    }
}
