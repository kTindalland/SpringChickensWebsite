using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Database.Entities
{
    public interface ICarouselItem : IEntity
    {
        string PhotoFilePath { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        bool IsActive { set; get; }
        int Position { get; set; }
    }
}
