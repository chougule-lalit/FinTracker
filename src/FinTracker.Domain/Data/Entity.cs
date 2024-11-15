using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Domain.Data
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; }
    }
}
