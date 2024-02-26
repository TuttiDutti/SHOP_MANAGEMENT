using ShopManagement.DAL;
using ShopManagement.Migrations;
using ShopManagement.Models;

namespace ShopManagement.Services
{
    public class OrderItemService
    {
        private readonly OrderItemRepository _orderItemRepository;
        public OrderItemService(OrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        public OrderItem? GetById(int id)
        {
            OrderItem? orderItem = _orderItemRepository.GetById(id);
            return orderItem;
        }

        public List<OrderItem> GetAll()
        {
            List<OrderItem> orderItem = _orderItemRepository.GetAll().ToList();
            return orderItem;
        }

        public OrderItem Add(OrderItem orderItem)
        {
            OrderItem? newOrderItem = _orderItemRepository.AddAndSaveChanges(orderItem);

            return newOrderItem;
        }

        public void Update(OrderItem orderItem)
        {
            _orderItemRepository.UpdateAndSaveChanges(orderItem);
        }

        public void Delete(int id)
        {
            _orderItemRepository.RemoveByIdAndSaveChanges(id);
        }
    }
}
