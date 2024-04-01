using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.dtos;
using backend.models;

namespace backend.interfaces
{
    public interface IItemService
    {
        Task<List<Item>> GetAllItems();
        Task<Item?> GetItem(int Id);
        Task<Item> CreateItem(Item item);
        Task<Item?> UpdateItem(int Id, UpdateItemDto updateItemDto);
        Task<Item?> DeleteItem(int Id);
        Task<bool> ItemExists(int Id);
        Task<Item?> UpdateItemQuantity(int Id, int Quantity);
        Task<Item?> UpdateMultiItemQuantity(int OldItemId, int NewItemId, int OldQuantity, int NewQuantity);
    }
}