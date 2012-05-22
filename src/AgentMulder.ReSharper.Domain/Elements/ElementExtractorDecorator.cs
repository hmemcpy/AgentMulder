namespace AgentMulder.ReSharper.Domain.Elements
{
    public abstract class ElementExtractorDecorator<TResult> : IElementExtractor<TResult>
    {
        private readonly IElementExtractor<TResult> baseExtractor;

        protected ElementExtractorDecorator(IElementExtractor<TResult> baseExtractor)
        {
            this.baseExtractor = baseExtractor;
        }

        public virtual TResult ExtractElement<T>(T element)
        {
            return baseExtractor.ExtractElement(element);
        }
    }
}