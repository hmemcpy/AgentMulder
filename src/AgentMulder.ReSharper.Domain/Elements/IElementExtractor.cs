namespace AgentMulder.ReSharper.Domain.Elements
{
    public interface IElementExtractor<out TResult>
    {
        TResult ExtractElement<TElement>(TElement element);
    }
}