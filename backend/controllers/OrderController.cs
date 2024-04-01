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
    [Route("orders")]
    [ApiController]
    public class OrderController(IOrderService _orderService, IItemService _itemService): ControllerBase
    {
        private readonly IOrderService orderService = _orderService;
        private readonly IItemService itemService = _itemService;

        [HttpGet]
        public async Task<IActionResult> GetAllOrders(){
            var orders = await orderService.GetAllOrders();
            var orderDto = orders.Select(s => s.ToOrderDto());
            return Ok(orderDto.ToList());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrder([FromRoute] int id){
            if(!ModelState.IsValid){
                return BadRequest();
            }
            var order = await orderService.GetOrder(id);
            if (order == null){
                return NotFound();
            }            
            return Ok(order.ToOrderDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto newOrder){
            if(!ModelState.IsValid){
                return BadRequest();
            }
            if(!await itemService.ItemExists(newOrder.ItemId)){
                return BadRequest("Item does not exist");
            }
            var item = await itemService.GetItem(newOrder.ItemId);
            //check if ther is enough stock for the order to push throuh
            if(await itemService.UpdateItemQuantity(item.Id, newOrder.Quantity) == null){
                return BadRequest("Insufficient quantity");
            }
            var orderModel = newOrder.CreateOrderFromDto();
            orderModel.Subtotal = orderModel.Quantity * item.Price;
            await orderService.CreateOrder(orderModel);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOrder([FromRoute] int id, [FromBody] UpdateOrderDto updateOrder){
            if(!ModelState.IsValid){
                return BadRequest();
            }
            var oldOrder = await orderService.GetOrder(id);
            if(oldOrder == null){
                return NotFound();
            }
            //Change the quantity of the updated order item/s
            await itemService.UpdateMultiItemQuantity(oldOrder.ItemId, id, oldOrder.Quantity, updateOrder.Quantity);
            //Change the subtotal of the updated order
            var item = await itemService.GetItem(updateOrder.ItemId);
            updateOrder.Subtotal = updateOrder.Quantity * item.Price;
            await orderService.UpdateOrder(id, updateOrder);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id){
            if(!ModelState.IsValid){
                return BadRequest();
            }
            if(await orderService.DeleteOrder(id) == null){
                return NotFound();
            }
            return Ok();
        }
    }
}