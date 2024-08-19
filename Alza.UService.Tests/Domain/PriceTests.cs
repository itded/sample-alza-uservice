using Alza.UService.Domain.Common;

namespace Alza.UService.Tests.Domain;

public class PriceTests
{
    [Theory]
    [MemberData(nameof(ValidData))]
    public void Price_is_valid(Decimal inputPrice)
    {
        var result = Price.Create(inputPrice);
        Assert.True(result.IsSuccess);
        Assert.Equal(inputPrice, result.Value.Value);
    }

    [Theory]
    [MemberData(nameof(InvalidData))]
    public void Price_is_not_valid(decimal inputPrice)
    {
        var result = Price.Create(inputPrice);
        Assert.False(result.IsSuccess);
        Assert.Throws<CSharpFunctionalExtensions.ResultFailureException>(() => result.Value.Value);
    }

    [Theory]
    [MemberData(nameof(ValidData))]
    public void Prices_are_equal(decimal inputPrice)
    {
        var result1 = Price.Create(inputPrice).Value;
        var result2 = Price.Create(inputPrice).Value;
        Assert.Equal(result1.Value, result2.Value);
        Assert.Equal(result1, result2);
    }

    [Fact]
    public void Prices_are_not_equal()
    {
        var result1 = Price.Create(200.01m).Value;
        var result2 = Price.Create(200m).Value;
        Assert.NotEqual(result1, result2);
    }

    public static TheoryData<decimal> ValidData =>
        new()
        {
            0m,
            0.01m,
            4,
            500.34m,
            int.MaxValue,
        };

    public static TheoryData<decimal> InvalidData =>
        new()
        {
            -0.01m,
            -1,
            -500.34m,
        };
}
