using ClassManageableBackEnd.Models;
using ManageableBackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageableBackEnd.Controllers
{
    [ApiController]
    [Router("api/[controller]")]

    public class ManageableController : ControllerBase

    { // DEPENDENCY INJECTION
        private readonly IManageableItemService _manageableItemService;

        public ManageableController(IManageableItemService manageableItemService) 
        {
            _manageableItemService = manageableItemService;
        }

        // TAKES COMPLETED TASK LIST.
        [HttpGet]
        public async Task<IActionResult> GetManageables([FromQuery(Name ="isdone)] bool isDone)
            {
                 return Ok(await _manageableItemService.GetManageableItems(isDone)); 
            }

        // CREATES A BRAND NEW TASK LIST.
        [HttpPost]
        public async Task<IActionResult> CreateNewManageable([FromBody] ManageableItem manageableItem) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            await _manageableItemService.AddManageableItem(manageableItem);

            return Ok();
        }
           
        // DELETE THE TASK.
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteManageable([FromRoute] Guid id) 
        {
            try 
            {
                await _manageableItemService.DeleteManageableItem(id);
            }
            catch (Exception ex) 
            {
                return StatusCode(500);
            }
            return Ok();
        }
    }
}
