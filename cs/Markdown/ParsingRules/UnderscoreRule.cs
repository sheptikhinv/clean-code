using Markdown.Nodes;
using Markdown.Tokens;

namespace Markdown.ParsingRules;

public class UnderscoreRule : IParsingRule
{
    public bool CanHandle(Token token) => token.Type == TokenType.Underscore;

    public void Handle(ParsingContext context)
    {
        context.Nodes.Add(new TextNode { Content = "_" });
        context.MoveForward();
    }
}