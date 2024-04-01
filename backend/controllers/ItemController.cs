using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.dtos;
using backend.interfaces;
using backend.mappers;
using Microsoft.AspNetCore.Mvc;

namespace backend.controllers
{
    [Route("items")]
    [ApiController]
    public class ItemController(IItemService _itemService) : ControllerBase
    {
        private readonly IItemService itemService = _itemService;

        [HttpGet]
        public async Task<IActionResult> GetAllItems(){
            var items = await itemService.GetAllItems();
            var itemDto = items.Select(s => s.ToItemDto());
            return Ok(itemDto.ToList());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetItem([FromRoute] int id){
            if(!ModelState.IsValid){
                return BadRequest();
            }
            var item = await itemService.GetItem(id);
            if (item == null){
                return NotFound();
            }            
            return Ok(item.ToItemDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] CreateItemDto newItem){
            if(!ModelState.IsValid){
                return BadRequest();
            }
            var itemModel = newItem.CreateItemFromDto();
            await itemService.CreateItem(itemModel);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateItem([FromRoute] int id, [FromBody] UpdateItemDto updateItem){
            if(!ModelState.IsValid){
                return BadRequest();
            }
            if(await itemService.UpdateItem(id, updateItem) == null){
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteItem([FromRoute] int id){
            if(!ModelState.IsValid){
                return BadRequest();
            }
            if(await itemService.DeleteItem(id) == null){
                return NotFound();
            }
            return Ok();
        }
    }
}