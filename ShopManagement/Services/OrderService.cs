using ShopManagement.DAL;
using ShopManagement.Models;

namespace ShopManagement.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        public OrderService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Order? GetById(int id)
        {
            Order? order = _orderRepository.GetById(id);
            return order;
        }

        public List<Order> GetAll()
        {
            List<Order> order = _orderRepository.GetAll().ToList();
            return order;
        }

        public Order Add(Order order)
        {
            Order? newOrder = _orderRepository.AddAndSaveChanges(order);

            return newOrder;
        }

        public void Update(Order order)
        {
            _orderRepository.UpdateAndSaveChanges(order);
        }

        public void Delete(int id)
        {
            _orderRepository.RemoveByIdAndSaveChanges(id);
        }

    }
}
