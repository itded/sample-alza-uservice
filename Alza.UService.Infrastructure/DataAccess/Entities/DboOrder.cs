namespace Alza.UService.Infrastructure.DataAccess.Entities;

public class DboOrder
{
    public Guid Id { get; set; }

    public int OrderNumber { get; set; }

    public string CustomerName { get; set; } = null!;

    public string OrderStatus { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }

    public ICollection<DboOrderItem> OrderItems { get; set; } = new List<DboOrderItem>();
}
