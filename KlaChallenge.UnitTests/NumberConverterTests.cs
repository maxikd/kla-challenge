public class NumberConverterTests
{
    private readonly NumberConverter _converter;

    public NumberConverterTests()
    {
        _converter = new NumberConverter();
    }

    [Theory]
    [InlineData(0, "zero dollars")]
    [InlineData(1, "one dollar")]
    [InlineData(25.1, "twenty-five dollars and ten cents")]
    [InlineData(0.01, "zero dollars and one cent")]
    [InlineData(1_001, "one thousand one dollars")]
    [InlineData(1_111, "one thousand one hundred eleven dollars")]
    [InlineData(13_000, "thirteen thousand dollars")]
    [InlineData(45_100, "forty-five thousand one hundred dollars")]
    [InlineData(450_100, "four hundred fifty thousand one hundred dollars")]
    [InlineData(700_001, "seven hundred thousand one dollars")]
    [InlineData(701_001, "seven hundred one thousand one dollars")]
    [InlineData(15_045_100, "fifteen million forty-five thousand one hundred dollars")]
    [InlineData(16_000_001, "sixteen million one dollars")]
    [InlineData(16_045_030, "sixteen million forty-five thousand thirty dollars")]
    [InlineData(800_000_001, "eight hundred million one dollars")]
    [InlineData(880_000_001, "eight hundred eighty million one dollars")]
    [InlineData(880_000_001.01, "eight hundred eighty million one dollars and one cent")]
    [InlineData(880_000_001.1, "eight hundred eighty million one dollars and ten cents")]
    [InlineData(999_999_999.99, "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents")]
    public void Convert_WhenValidNumber_ShouldGiveExpectedResult(
        decimal number,
        string expected)
    {
        var result = _converter.Convert(number);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-0.85)]
    public void Convert_WhenLessThanZero_ShoudlThrowArgumentOutOfRangeException(
        decimal number)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => _converter.Convert(number));
    }

    [Theory]
    [InlineData(1_000_000_000)]
    [InlineData(1_000_000_000.01)]
    public void Convert_WhenGreaterThanOrEqualToOneBillion_ShoudlThrowArgumentOutOfRangeException(
        decimal number)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => _converter.Convert(number));
    }
}