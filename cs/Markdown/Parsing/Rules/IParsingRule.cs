using Markdown.Tokens;

namespace Markdown.ParsingRules;

public interface IParsingRule
{
    bool CanHandle(Token token);
    void Handle(ParsingContext context);
}