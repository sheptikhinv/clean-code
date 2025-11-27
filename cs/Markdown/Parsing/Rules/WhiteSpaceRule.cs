using Markdown.Parsing.Nodes;
using Markdown.Tokens;

namespace Markdown.ParsingRules;

public class WhiteSpaceRule : IParsingRule
{
    public bool CanHandle(Token token) => token.Type == TokenType.WhiteSpace;

    public void Handle(ParsingContext context)
    {
        context.Nodes.Add(new TextNode { Content = " " });
        context.MoveForward();
    }
}