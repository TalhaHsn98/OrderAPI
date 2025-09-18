using ApplicationCore.Contracts;
using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class OrderService: IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository repo)
        {
            _orderRepository = repo;
        }

        public Task<List<Order>> GetAllAsync()
        {
            return _orderRepository.GetAllAsync();
        }

        public Task<List<Order>> GetByCustomerIdAsync(string customerId)
        {
            return _orderRepository.GetByCustomerIdAsync(customerId);
        }

        public Task<Order?> GetAsync(int id)
        {
            return _orderRepository.GetByIdAsync(id);
        }

        public Task<Order> CreateAsync(Order order) 
        {
            return _orderRepository.AddAsync(order);
        }

        public async Task UpdateAsync(int id, Order order)
        {
            if (id != order.Id) throw new ArgumentException("Id mismatch");
            await _orderRepository.UpdateAsync(order);
        }

        public Task DeleteAsync(int id) {
            return _orderRepository.DeleteAsync(id);
        }
    }
}
