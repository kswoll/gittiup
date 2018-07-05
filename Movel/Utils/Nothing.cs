namespace Movel.Utils
{
    /// <summary>
    /// Represents a return type that has no value.
    /// </summary>
    public struct Nothing
    {
        public static Nothing Value { get; } = new Nothing();
    }
}