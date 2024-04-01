using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.dtos;
using backend.models;

namespace backend.mappers
{
    public static class OrderMapper
    {
        public static OrderDto ToOrderDto(this Order orderModel){
            return new OrderDto{
                ItemId = orderModel.ItemId,
                Quantity = orderModel.Quantity,
                Subtotal = orderModel.Subtotal
            };
        }

        public static Order CreateOrderFromDto(this CreateOrderDto createOrderDto){
            return new Order{
                ItemId = createOrderDto.ItemId,
                Quantity = createOrderDto.Quantity
            };
        }
    }
}