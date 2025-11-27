using Markdown.Tokens;

namespace Markdown.ParsingRules;

public class LinkRule : IParsingRule
{
    private static readonly HashSet<TokenType> LinkSymbols =
        [TokenType.LeftSquareBracket, TokenType.RightSquareBracket, TokenType.LeftBracket, TokenType.RightBracket];

    public bool CanHandle(Token token) => LinkSymbols.Contains(token.Type);

    public void Handle(ParsingContext context)
    {
        throw new NotImplementedException();
    }
}