using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.data;
using backend.dtos;
using backend.interfaces;
using backend.models;
using Microsoft.EntityFrameworkCore;

namespace backend.services
{
    public class OrderService(ApplicationDBContext _applicationDBContext) : IOrderService
    {
        private readonly ApplicationDBContext applicationDBContext = _applicationDBContext;
        public async Task<Order> CreateOrder(Order order)
        {
            await applicationDBContext.Orders.AddAsync(order);
            await applicationDBContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> DeleteOrder(int Id)
        {
            var order = await applicationDBContext.Orders.FirstOrDefaultAsync(x => x.Id == Id);
            if(order == null){
                return null;
            }
            applicationDBContext.Orders.Remove(order);
            return order;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await applicationDBContext.Orders.ToListAsync();
        }

        public async Task<Order?> GetOrder(int Id)
        {
            return await applicationDBContext.Orders.FindAsync(Id);
        }

        public async Task<Order?> UpdateOrder(int Id, UpdateOrderDto updateOrderDto)
        {
            var order = await applicationDBContext.Orders.FirstOrDefaultAsync(x => x.Id == Id);
            if(order == null){
                return null;
            }
            if(updateOrderDto.ItemId != order.ItemId && updateOrderDto.ItemId != 0){
                order.ItemId = updateOrderDto.ItemId;
            }
            if(updateOrderDto.Quantity != order.Quantity && updateOrderDto.Quantity != 0){
                order.Quantity = updateOrderDto.Quantity;
            }
            order.Subtotal = updateOrderDto.Subtotal;
            await applicationDBContext.SaveChangesAsync();
            return order;
        }
    }
}