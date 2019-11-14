using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Database
{
    public interface IRepository<T> where T : IEntity
    {
        bool Add(T entity);
    }
}
