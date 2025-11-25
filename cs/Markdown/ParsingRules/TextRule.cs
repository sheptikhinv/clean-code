using Markdown.Nodes;
using Markdown.Tokens;

namespace Markdown.ParsingRules;

public class TextRule : IParsingRule
{
    public bool CanHandle(Token token) => token.Type == TokenType.Text;

    public void Handle(ParsingContext context)
    {
        context.Nodes.Add(new TextNode { Content = context.CurrentToken.Value });
        context.MoveForward();
    }
}