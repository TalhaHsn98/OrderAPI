using ApplicationCore.Contracts;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrdersDbContext _db;
        public OrderRepository(OrdersDbContext db)
        {
            _db = db;
        }

        public Task<List<Order>> GetAllAsync()
        {
            return _db.Orders.Include(o=>o.Order_Details).AsNoTracking().ToListAsync();
        }

        public Task<Order?> GetByIdAsync(int id) {
            return _db.Orders.Include(o => o.Order_Details).FirstOrDefaultAsync(o => o.Id == id);
        }

        public Task<List<Order>> GetByCustomerIdAsync(string customerId)
        {
           return _db.Orders.Include(o => o.Order_Details)
                .Where(o => o.CustomerId == customerId)
                .AsNoTracking().ToListAsync();
        }

        public async Task<Order> AddAsync(Order order)
        {
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
            return order;
        }

        public async Task UpdateAsync(Order order)
        {
            _db.Entry(order).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            Order deleteOrder = await _db.Orders.FindAsync(id);
            if (deleteOrder == null) { return; }
            _db.Orders.Remove(deleteOrder);
            await _db.SaveChangesAsync();
        }


        public async Task<bool> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
