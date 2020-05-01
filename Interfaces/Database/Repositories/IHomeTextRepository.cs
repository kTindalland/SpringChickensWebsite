using Interfaces.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Database.Repositories
{
    public interface IHomeTextRepository : IRepository<IHomeText>
    {
        string GetTitle();
        string GetBody();
        void UpdateTitle(string newTitle);
        void UpdateBody(string newBody);
    }
}
