using ClassManageableBackEnd.Models;
using ManageableBackEnd.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices.ComTypes;
//using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ManageableBackEnd.Services
{
    public class ManageableItemService : IManageableItemService
    {
        // Dependency injection on data access.
        private readonly ApplicationDbContext _context;

        public ManageableItemService(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<ManageableItem[]> GetManageableItems(bool isDone)
        {
            // Return only done items.
            if (isDone)
            {
                return await _context.Items
                    .Where(x => x.IsDone)
                    .ToArrayAsync();
            }
            else
            {
                return await _context.Items
                    .Where(x => x.IsDone == false)
                    .ToArrayAsync();
            }
        }

        // Adds new item to the data base.
        public async Task<bool> AddManageableItem(ManageableItem newItem)
        {
            newItem.Id = Guid.NewGuid();
            newItem.IsDone = false;
            newItem.DueAt = DateTimeOffset.Now.AddDays(1);

            _context.Items.Add(newItem);
            var saveOutcome = await _context.SaveChangesAsync();

            return saveOutcome == 1;
        }

        // Deletes item from data base.
        public async Task<bool> DeleteManageableItem(Guid id) 
        {
            var itemToBeRemoved = new ManageableItem { Id = id };
            _context.Items.Attach(itemToBeRemoved);
            _context.Items.Remove(itemToBeRemoved);
            var saveOutcome = await _context.SaveChangesAsync();
            return saveOutcome == 1;
        }
    }
}
