using System;

namespace Datadog.Trace
{
    public abstract class Span : ISpan
    {
        public string ResourceName { get; set; }

        public string Type { get; set; }

        public bool Error { get; set; }

        public ISpan SetTag(string key, string value)
        {
            throw new NotImplementedException();
        }

        public string GetTag(string key)
        {
            throw new NotImplementedException();
        }

        public void SetException(Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}
