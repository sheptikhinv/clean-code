using Markdown.Nodes;
using Markdown.ParsingRules;
using Markdown.Tokens;

namespace Markdown;

public class NodeParser
{
    private readonly List<IParsingRule> _rules;

    public NodeParser()
    {
        _rules =
        [
            new EscapeRule(),
            new WhiteSpaceRule(),
            new TextRule(),
            new HashtagRule()
        ];
    }

    public MarkdownNode ParseLine(List<Token> tokens)
    {
        MarkdownNode? result = null;
        if (tokens.Count > 0 && tokens[0].Type == TokenType.Hashtag)
        {
            result = ParseHeader(tokens);
        }

        return result ?? ParseParagraph(tokens);
    }

    private HeaderNode? ParseHeader(List<Token> tokens)
    {
        var level = 0;
        var position = 0;

        while (position < tokens.Count && tokens[position].Type == TokenType.Hashtag)
        {
            level++;
            position++;
        }

        if (position >= tokens.Count || tokens[position].Type != TokenType.WhiteSpace) return null;

        position++;
        return new HeaderNode
        {
            Level = level,
            Children = ParseInlineContent(tokens.Skip(position).ToList())
        };
    }

    private ParagraphNode ParseParagraph(List<Token> tokens)
    {
        var paragraph = new ParagraphNode
        {
            Children = ParseInlineContent(tokens)
        };
        return paragraph;
    }

    private List<MarkdownNode> ParseInlineContent(List<Token> tokens)
    {
        var context = new ParsingContext
        {
            Tokens = tokens,
            Index = 0,
            Nodes = new List<MarkdownNode>(),
            OpenTags = new Stack<TagInfo>()
        };

        while (!context.IsEndOfTokens)
        {
            var token = context.CurrentToken;
            var rule = _rules.FirstOrDefault(r => r.CanHandle(token));

            if (rule != null)
            {
                rule.Handle(context);
            }
            else
            {
                context.MoveForward();
            }
        }

        CloseUnclosedTags(context);
        return context.Nodes;
    }

    private void CloseUnclosedTags(ParsingContext context)
    {
        while (context.OpenTags.Count > 0)
        {
            
        }
    }
}