using ClassManageableBackEnd.Models;
using ManageableBackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace ManageableBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]

    public class ManageableController : ControllerBase

    { // DEPENDENCY INJECTION
        private readonly IManageableItemService _manageableItemService;

        public ManageableController(IManageableItemService manageableItemService) 
        {
            _manageableItemService = manageableItemService;
        }

        // TAKES COMPLETED TASK LIST.
        [HttpGet]
        public async Task<IActionResult> GetManageables([FromQuery(Name ="isdone")] bool isDone)
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> ToggleCompleteOrIncomplete([FromRoute] Guid id)
        {
            try
            {
                await _manageableItemService.ToggleManageableItemDone(id);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            return Ok();
        }

        // UPDATES / MODIFIES an already existing TASK.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateModifyItem([FromRoute] Guid id, [FromBody] ManageableItem manageableItem)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            try 
            {
                await _manageableItemService.CompletelyUpdateManageableItem(manageableItem);
            } catch(Exception)
            {
                return StatusCode(500);
            }

            return Ok();
        }

        // DELETE THE TASK.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManageable([FromRoute] Guid id) 
        {
            try
            {
                await _manageableItemService.DeleteManageableItem(id);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
            return Ok();
        }
    }
}
