using ClassManageableBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageableBackEnd.Services
{
    public interface IManageableItemService
    {
        Task<ManageableItem[]> GetManageableItems(bool isDone);
        Task<bool> AddManageableItem(ManageableItem newItem);
        Task<bool> DeleteManageableItem(Guid id);
    }
}
