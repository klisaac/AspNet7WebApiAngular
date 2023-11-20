
 namespace AspNet7.Core.Logging
{
    public interface IAspNet7Logger<T>
    {
        void Information(string message, params object[] args);
        void Warning(string message, params object[] args);
        void Error(string message, params object[] args);
    }
}
