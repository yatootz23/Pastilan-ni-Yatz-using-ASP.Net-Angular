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
    public class ItemService(ApplicationDBContext _applicationDBContext) : IItemService
    {
        private readonly ApplicationDBContext applicationDBContext = _applicationDBContext;

        public async Task<Item> CreateItem(Item item)
        {
            await applicationDBContext.Items.AddAsync(item);
            await applicationDBContext.SaveChangesAsync();
            return item;
        }

        public async Task<Item?> DeleteItem(int Id)
        {
            var item = await applicationDBContext.Items.FirstOrDefaultAsync(x => x.Id == Id);
            if(item == null){
                return null;
            }
            applicationDBContext.Items.Remove(item);
            return item;
        }

        public async Task<List<Item>> GetAllItems()
        {
            return await applicationDBContext.Items.ToListAsync();
        }

        public async Task<Item?> GetItem(int Id)
        {
            return await applicationDBContext.Items.FindAsync(Id);
        }

        public Task<bool> ItemExists(int Id)
        {
            return applicationDBContext.Items.AnyAsync(s => s.Id == Id);
        }

        public async Task<Item?> UpdateItem(int Id, UpdateItemDto updateItemDto)
        {
            var item = await applicationDBContext.Items.FirstOrDefaultAsync(x => x.Id == Id);
            if(item == null){
                return null;
            }
            if(updateItemDto.Name != null){
                item.Name = updateItemDto.Name;
            }
            if(updateItemDto.Description != null){
                item.Description = updateItemDto.Description;
            }
            if(updateItemDto.Price != item.Price && updateItemDto.Price != 0){
                item.Price = updateItemDto.Price;
            }
            await applicationDBContext.SaveChangesAsync();
            return item;
        }

        public async Task<Item?> UpdateItemQuantity(int Id, int Quantity)
        {
            var item = await applicationDBContext.Items.FirstOrDefaultAsync(x => x.Id == Id);
            if(item == null){
                return null;
            }
            if(item.Quantity - Quantity < 0){
                return null;
            }
            item.Quantity -= Quantity;
            
            await applicationDBContext.SaveChangesAsync();
            return item;
        }

        public async Task<Item?> UpdateMultiItemQuantity(int OldItemId, int NewItemId, int OldQuantity, int NewQuantity)
        {
            if(OldItemId != NewItemId){
                var oldItem = await applicationDBContext.Items.FirstOrDefaultAsync(x => x.Id == OldItemId);
                if(oldItem == null){
                    return null;
                }
                var newItem = await applicationDBContext.Items.FirstOrDefaultAsync(x => x.Id == NewItemId);
                if(newItem == null){
                    return null;
                }
                if(newItem.Quantity - NewQuantity < 0){
                    return null;
                }
                newItem.Quantity -= NewQuantity;
                oldItem.Quantity += OldQuantity;
                await applicationDBContext.SaveChangesAsync();
                return newItem;
            }
            else{
                var updateItem = await applicationDBContext.Items.FirstOrDefaultAsync(x => x.Id == OldItemId);
                if(updateItem == null){
                    return null;
                }
                if(updateItem.Quantity + OldQuantity - NewQuantity < 0){
                    return null;
                }
                updateItem.Quantity = updateItem.Quantity + OldQuantity - NewQuantity; 
                await applicationDBContext.SaveChangesAsync();
                return updateItem;           
            }
        }
    }
}