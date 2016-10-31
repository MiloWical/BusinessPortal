namespace RedisProvider.Constants
{ /// <summary>
    /// Contains values for allowed operations.
    /// </summary>
    public enum CommandOperation : byte
    {
        None = 0,
        Error = 1,
        ClearCache = 2,
        ClearRegion = 3,
        Help = 4
    }
}
