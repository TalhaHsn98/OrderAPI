using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllAsync();
        Task<List<Order>> GetByCustomerIdAsync(string customerId);
        Task<Order?> GetAsync(int id);
        Task<Order> CreateAsync(Order order);
        Task UpdateAsync(int id, Order order);
        Task DeleteAsync(int id);
    }
}
