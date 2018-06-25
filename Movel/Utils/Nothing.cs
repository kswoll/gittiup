namespace Movel.Utils
{
    /// <summary>
    /// Represents a return type that has no value.
    /// </summary>
    public sealed class Nothing
    {
        public static Nothing Value { get; } = new Nothing();

        private Nothing()
        {
        }
    }
}