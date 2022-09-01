using Microsoft.EntityFrameworkCore;

namespace MyStore.Models.Repository
{
    public class EFOrderRepository : IOrderRepository
    {
        private readonly MyStoreDataContext _context;

        public EFOrderRepository(MyStoreDataContext context)
        {
            _context = context;
        }

        public IQueryable<Order> Orders => _context.Orders.Include(o => o.Lines).ThenInclude(l => l.Product);

        public void SaveOrder(Order order)
        {
            _context.AttachRange(order.Lines.Select(l => l.Product));
            if(order.OrderId == 0)
            {
                _context.Orders.Add(order);
            }
            _context.SaveChanges();
        }
    }
}
