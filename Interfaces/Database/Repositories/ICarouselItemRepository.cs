using Interfaces.Database.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Interfaces.Database.Repositories
{
    public interface ICarouselItemRepository : IRepository<ICarouselItem>
    {
        void Update(ICarouselItem item);
        void ActivateItem(int id);
        void DeactivateItem(int id);
        void FlipActivation(int id);
        void MoveItemUp(int id);
        void MoveItemDown(int id);
        List<ICarouselItem> GetAllItems();
        List<ICarouselItem> GetAllActiveItems();

    }
}
