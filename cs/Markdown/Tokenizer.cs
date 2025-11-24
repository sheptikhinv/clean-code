using System.Text;
using Markdown.Tokens;

namespace Markdown;

public class Tokenizer
{
    private readonly HashSet<char> SpecialSymbols = ['\n', '\\', '_', '#'];

    private Token ParseDefaultText(Cursor cursor)
    {
        var sb = new StringBuilder();
        while (!cursor.IsEndOfText && !SpecialSymbols.Contains(cursor.CurrentChar))
        {
            sb.Append(cursor.CurrentChar);
            cursor.MoveForward();
        }

        return new Token { Type = TokenType.Text, Value = sb.ToString() };
    }

    /// <summary>
    /// Разбивает текст на набор токенов
    /// </summary>
    /// <param name="text">Текст для разбивания</param>
    /// <returns></returns>
    public List<Token> Tokenize(string text)
    {
        var cursor = new Cursor(text);
        var result = new List<Token>();

        while (!cursor.IsEndOfText)
        {
            var currentChar = cursor.CurrentChar;

            switch (currentChar)
            {
                case '\\':
                    result.Add(new Token { Type = TokenType.EscapeCharacter });
                    cursor.MoveForward();
                    break;
                case '_':
                    if (cursor.IsNextCharSame('_'))
                    {
                        result.Add(new Token { Type = TokenType.Bold });
                        cursor.MoveForward(2);
                    }
                    else
                    {
                        result.Add(new Token { Type = TokenType.Italic });
                        cursor.MoveForward();
                    }

                    break;
                case '#':
                    result.Add(new Token { Type = TokenType.Header });
                    cursor.MoveForward();
                    break;
                default:
                    result.Add(ParseDefaultText(cursor));
                    break;
            }
        }

        return result;
    }
}