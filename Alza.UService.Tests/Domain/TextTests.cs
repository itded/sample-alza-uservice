using Alza.UService.Domain.Common;

namespace Alza.UService.Tests.Domain;

public class TextTests
{
    [Theory]
    [InlineData("test", "test")]
    [InlineData("Test", "Test")]
    [InlineData("test Test", "test Test")]
    [InlineData("Český text", "Český text")]
    [InlineData("    test Test \t  ", "test Test")]
    public void Text_is_valid(string inputText, string expectedValue)
    {
        var result = Text.Create(inputText);
        Assert.True(result.IsSuccess);
        Assert.Equal(expectedValue, result.Value.Value);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("\t")]
    [InlineData(" \t ")]
    [InlineData("   ")]
    [InlineData("very very very very very very very very very very very very very very very long text")]
    public void Text_is_not_valid(string inputText)
    {
        var result = Text.Create(inputText);
        Assert.False(result.IsSuccess);
        Assert.Throws<ResultFailureException>(() => result.Value.Value);
    }

    [Theory]
    [InlineData("test", "test")]
    [InlineData("Test", "Test")]
    [InlineData("test Test", "test Test")]
    [InlineData("Český text", "Český text")]
    [InlineData("    test Test \t  ", "test Test")]
    public void Texts_are_equal(string inputText1, string inputText2)
    {
        var result1 = Text.Create(inputText1).Value;
        var result2 = Text.Create(inputText2).Value;
        Assert.Equal(result1.Value, result2.Value);
        Assert.Equal(result1, result2);
    }

    [Fact]
    public void Texts_are_not_equal()
    {
        var result1 = Text.Create(" abc ").Value;
        var result2 = Text.Create(" Abc ").Value;
        Assert.NotEqual(result1, result2);
    }
}
