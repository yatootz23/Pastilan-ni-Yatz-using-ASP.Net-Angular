using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.dtos;
using backend.models;

namespace backend.mappers
{
    public static class ItemMapper
    {
        public static ItemDto ToItemDto(this Item itemModel){
            return new ItemDto{
                Name = itemModel.Name,
                Description = itemModel.Description,
                Price = itemModel.Price
            };
        }

        public static Item CreateItemFromDto(this CreateItemDto createItemDto){
            return new Item{
                Name = createItemDto.Name,
                Description = createItemDto.Description,
                Price = createItemDto.Price,
                Quantity = createItemDto.Quantity
            };
        }
    }
}