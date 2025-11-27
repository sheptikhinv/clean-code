namespace Markdown.Nodes;

public class LinkNode : MarkdownNode
{
    public string Url { get; set; }
    public string Label { get; set; }

    public override string ToHtml() => $"<a href=\"{Url}\">{Label}</a>";
}