using Alza.UService.Domain.Orders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Alza.UService.Infrastructure.DataAccess.Conventions;

internal class OrderStatusConvention : ValueConverter<string, OrderStatus>
{
        public OrderStatusConvention()
            : base(
                v => (OrderStatus)Enum.Parse(typeof(OrderStatus), v),
                v => v.ToString())
    {
    }
}
