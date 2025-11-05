namespace Markdown;

public class Md
{
    /// <summary>
    /// Возвращает HTML-разметку
    /// </summary>
    /// <param name="markdown">Разметка Markdown для преобразования в HTML</param>
    /// <returns></returns>
    public string Render(string markdown)
    {
        throw new NotImplementedException();
        // Общая идея: взять изначальный markdown-текст,
        // разбить его на строчки, в каждой строке выделить токены,
        // после чего в Renderer их перевести в html теги, собрать обратно
        // в полноценный текст и вернуть
    }
}