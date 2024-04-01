using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.dtos;
using backend.models;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace backend.interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrders();
        Task<Order?> GetOrder(int Id);
        Task<Order> CreateOrder(Order order);

        Task<Order?> UpdateOrder(int Id, UpdateOrderDto updateOrderDto);

        Task<Order?> DeleteOrder(int Id);
    }
}