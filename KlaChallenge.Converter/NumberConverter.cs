using System.Text;

public class NumberConverter
{
    private readonly StringBuilder _word;

    public NumberConverter()
    {
        _word = new StringBuilder();
    }

    public string Convert(
        decimal number)
    {
        decimal dollars = decimal.Truncate(number),
                cents = decimal.ToInt32((number - dollars) * 100);

        if (number < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(number), "The number cannot be negative.");
        }
        else if (number >= 1_000_000_000)
        {
            throw new ArgumentOutOfRangeException(nameof(number), "The number cannot be one billion or greater.");
        }
        else
        {
            ConvertDollars(dollars);
        }

        AppendDollar(dollars);

        if (cents > 0)
        {
            _word.Append(" and ");
            AppendDozens(cents);
            AppendCent(cents);
        }

        return _word.ToString();
    }

    private void ConvertDollars(
        decimal number)
    {
        if (number < 100)
        {
            HandleUpTo99(number);
        }
        else if (number < 1_000)
        {
            HandleUpTo999(number);
        }
        else if (number < 100_000)
        {
            HandleUpTo99_999(number);
        }
        else if (number < 1_000_000)
        {
            HandleUpTo999_999(number);
        }
        else if (number < 100_000_000)
        {
            HandleUpTo99_999_999(number);
        }
        else if (number < 1_000_000_000)
        {
            HandleUpTo999_999_999(number);
        }
    }

    private void HandleUpTo99(
        decimal number)
    {
        AppendDozens(number);
    }
    private void HandleUpTo999(
        decimal number)
    {
        decimal hundreds = decimal.Truncate(number / 100);
        AppendOneness(hundreds);
        AppendHundreds(hundreds);

        decimal dozens = number - hundreds * 100;
        if (dozens != 0)
        {
            _word.Append(' ');
            ConvertDollars(dozens);
        }
    }
    private void HandleUpTo99_999(
        decimal number)
    {
        decimal dozenHundreds = decimal.Truncate(number / 1_000);
        AppendDozens(dozenHundreds);
        AppendThousands(dozenHundreds);

        decimal hundreds = number - dozenHundreds * 1_000;
        if (hundreds != 0)
        {
            _word.Append(' ');
            ConvertDollars(hundreds);
        }
    }
    private void HandleUpTo999_999(
        decimal number)
    {
        decimal thousandHundreds = decimal.Truncate(number / 100_000);
        AppendOneness(thousandHundreds);
        AppendHundreds(thousandHundreds);

        decimal dozenHundreds = number - thousandHundreds * 100_000;
        if (dozenHundreds == 0)
        {
            AppendThousands(thousandHundreds);
        }
        else if (dozenHundreds < 1_000)
        {
            AppendThousands(thousandHundreds);
            _word.Append(' ');
            ConvertDollars(dozenHundreds);
        }
        else
        {
            _word.Append(' ');
            ConvertDollars(dozenHundreds);
        }
    }
    private void HandleUpTo99_999_999(
        decimal number)
    {
        decimal dozenMillions = decimal.Truncate(number / 1_000_000);
        AppendDozens(dozenMillions);
        AppendMillions(dozenMillions);

        decimal thousandHundreds = number - dozenMillions * 1_000_000;
        if (thousandHundreds != 0)
        {
            _word.Append(' ');
            ConvertDollars(thousandHundreds);
        }
    }
    private void HandleUpTo999_999_999(
        decimal number)
    {
        decimal thousandMillions = decimal.Truncate(number / 100_000_000);
        AppendOneness(thousandMillions);
        AppendHundreds(thousandMillions);

        decimal thousandHundreds = number - thousandMillions * 100_000_000;
        if (thousandHundreds == 0)
        {
            AppendMillions(thousandMillions);
        }
        else if (thousandHundreds < 1_000_000)
        {
            AppendMillions(thousandMillions);
            _word.Append(' ');
            ConvertDollars(thousandHundreds);
        }
        else
        {
            _word.Append(' ');
            ConvertDollars(thousandHundreds);
        }
    }

    private void AppendDollar(
        decimal number)
    {
        _word.Append(number != 1 ? " dollars" : " dollar");
    }
    private void AppendCent(
        decimal number)
    {
        _word.Append(number != 1 ? " cents" : " cent");
    }

    private void AppendOneness(
        decimal number)
    {
        _word.Append(number switch
        {
            0 => "zero",
            1 => "one",
            2 => "two",
            3 => "three",
            4 => "four",
            5 => "five",
            6 => "six",
            7 => "seven",
            8 => "eight",
            9 => "nine",
            _ => throw new ArgumentOutOfRangeException(nameof(number), $"Invalid number \"{number}\" found."),
        });
    }

    private void AppendDozens(
        decimal number)
    {
        if (number < 10)
        {
            AppendOneness(number);
        }
        else if (number < 20)
        {
            AppendDozenSpecialCases(number);
        }
        else
        {
            AppendDozenCommonCase(number);
        }
    }

    private void AppendDozenCommonCase(
        decimal number)
    {
        string numberAsString = number.ToString();
        char dozen = numberAsString[0],
            oneness = numberAsString[1];

        _word.Append(dozen switch
        {
            '2' => "twenty",
            '3' => "thirty",
            '4' => "forty",
            '5' => "fifty",
            '6' => "sixty",
            '7' => "seventy",
            '8' => "eighty",
            '9' => "ninety",
            _ => throw new ArgumentOutOfRangeException(nameof(number), $"Invalid number \"{number}\" found."),
        });

        if (oneness != '0')
        {
            decimal d = System.Convert.ToDecimal(oneness.ToString());
            _word.Append('-');
            AppendOneness(d);
        }
    }

    private void AppendDozenSpecialCases(
        decimal number)
    {
        _word.Append(number switch
        {
            10 => "ten",
            11 => "eleven",
            12 => "twelve",
            13 => "thirteen",
            14 => "fourteen",
            15 => "fifteen",
            16 => "sixteen",
            17 => "seventeen",
            18 => "eighteen",
            19 => "nineteen",
            _ => throw new ArgumentOutOfRangeException(nameof(number), $"Invalid number \"{number}\" found."),
        });
    }

    private void AppendHundreds(
        decimal number)
    {
        if (number != 0)
        {
            _word.Append(" hundred");
        }
    }

    private void AppendThousands(
        decimal number)
    {
        if (number != 0)
        {
            _word.Append(" thousand");
        }
    }

    private void AppendMillions(
        decimal number)
    {
        if (number != 0)
        {
            _word.Append(" million");
        }
    }
}