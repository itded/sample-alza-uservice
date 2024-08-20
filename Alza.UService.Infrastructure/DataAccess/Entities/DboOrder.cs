namespace Alza.UService.Infrastructure.DataAccess.Entities;

public class DboOrder
{
    public Guid Id { get; set; }

    public int OrderNumber { get; set; }

    public required string CustomerName { get; set; }

    public required string OrderStatus { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public ICollection<DboOrderItem> OrderItems { get; set; } = new List<DboOrderItem>();
}
