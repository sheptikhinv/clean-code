using Markdown.Parsing.Nodes;
using Markdown.Tokens;

namespace Markdown.ParsingRules;

public class HashtagRule : IParsingRule
{
    public bool CanHandle(Token token) => token.Type == TokenType.Hashtag;

    public void Handle(ParsingContext context)
    {
        context.Nodes.Add(new TextNode { Content = "#" });
        context.MoveForward();
    }
}