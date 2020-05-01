using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Database.Entities
{
    public interface IHomeText : IEntity
    {
        string Title { get; set; }
        string Body { get; set; }
    }
}
