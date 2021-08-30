namespace Datadog.Trace
{
    public abstract class Scope : IScope
    {
        protected Scope(ISpan span)
        {
            Span = span;
        }

        public ISpan Span { get; }

        public abstract void Dispose();
    }
}
