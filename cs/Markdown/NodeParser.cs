using Markdown.Nodes;
using Markdown.Tokens;

namespace Markdown;

public class NodeParser
{
    private List<Token> _tokens;
    private int index;

    private Token CurrentToken => _tokens[index];

    private Token PeekToken(int offset = 1)
        => index + offset < _tokens.Count ? _tokens[index + offset] : null;

    private void MoveForward() => index++;

    private bool IsEndOfTokens => index >= _tokens.Count;

    public MarkdownNode ParseLine(List<Token> tokens)
    {
        _tokens = tokens;
        index = 0;

        MarkdownNode? result = null;

        if (CurrentToken?.Type == TokenType.Hashtag)
        {
            result = ParseHeader();
        }

        return result ?? ParseParagraph();
    }

    private HeaderNode? ParseHeader()
    {
        var level = 0;

        while (CurrentToken?.Type == TokenType.Hashtag)
        {
            level++;
            MoveForward();
        }

        if (CurrentToken?.Type == TokenType.WhiteSpace)
            MoveForward();
        else
        {
            index = 0;
            return null;
        }

        var header = new HeaderNode
        {
            Level = Math.Min(level, 6),
            Children = ParseInlineContent()
        };

        return header;
    }

    private ParagraphNode ParseParagraph()
    {
        var paragraph = new ParagraphNode
        {
            Children = ParseInlineContent()
        };
        return paragraph;
    }

    private List<MarkdownNode> ParseInlineContent()
    {
        var nodes = new List<MarkdownNode>();
        while (!IsEndOfTokens)
        {
            var token = CurrentToken;

            switch (token.Type)
            {
                case TokenType.EscapeCharacter:
                    MoveForward();
                    if (!IsEndOfTokens)
                    {
                        var nextToken = CurrentToken;
                        var escapedChar = nextToken.Type switch
                        {
                            TokenType.Underscore => "_",
                            TokenType.Hashtag => "#",
                            TokenType.EscapeCharacter => "\\",
                            TokenType.Text => nextToken.Value?.FirstOrDefault().ToString() ?? "",
                            TokenType.WhiteSpace => " ",
                            _ => ""
                        };
                        if (!string.IsNullOrEmpty(escapedChar))
                            nodes.Add(new TextNode { Content = escapedChar });
                        MoveForward();
                    }

                    break;

                case TokenType.Hashtag:
                    nodes.Add(new TextNode { Content = "#" });
                    MoveForward();
                    break;

                case TokenType.WhiteSpace:
                    nodes.Add(new TextNode { Content = " " });
                    MoveForward();
                    break;

                case TokenType.Text:
                    nodes.Add(new TextNode { Content = token.Value! });
                    MoveForward();
                    break;

                default:
                    MoveForward();
                    break;
            }
        }

        return nodes;
    }
}