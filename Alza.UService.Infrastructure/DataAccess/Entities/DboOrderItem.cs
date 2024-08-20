namespace Alza.UService.Infrastructure.DataAccess.Entities;

public class DboOrderItem
{
    public Guid Id { get; set; }

    public required string ProductName { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public virtual required DboOrder Order { get; set; }
}
