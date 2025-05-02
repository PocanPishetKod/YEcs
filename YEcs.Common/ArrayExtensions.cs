using System.Runtime.CompilerServices;

namespace YEcs.Common;

public static class ArrayExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ResizeIfNeeded<T>(this T[] array, int count, int expand)
    {
        if (array.Length == count)
            Array.Resize(ref array, array.Length + expand);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ResizeIfNeeded<T>(this T[] array, int setIndex)
    {
        if (array.Length >= setIndex + 1)
            return;
        
        Array.Resize(ref array, setIndex + 1);
    }
}