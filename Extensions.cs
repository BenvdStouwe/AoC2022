namespace AoC2022;

public static class Extensions
{
    public static T WhenDefault<T>(this T value, Func<T> valueFactory) where T : IEquatable<T> =>
        value.Equals(default)
            ? valueFactory()
            : value;
}