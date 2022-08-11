// A part of the C# Language Syntactic Sugar suite.

using System;
using System.Collections;
using System.Collections.Generic;

namespace CLSS
{
  public static partial class IEnumerableLooping
  {
    /// <summary>
    /// Returns an enumerable of the source collection that would enumerate
    /// infinitely in a forward-moving loop. If created from an empty source
    /// collection, the result of this method is no different from an empty
    /// enumerable.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of
    /// <paramref name="source"/>.</typeparam>
    /// <param name="source">The collection to loop elements on.</param>
    /// <returns>An enumerable that infinitely loops the source collection.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is
    /// null.</exception>
    public static IEnumerable<TSource> Looping<TSource>(
      this IEnumerable<TSource> source)
    {
      if (source == null) throw new ArgumentNullException("source");
      var enumerator = source.GetEnumerator();
      if (!enumerator.MoveNext()) yield break;
      enumerator.Reset();
      while (true) yield return enumerator.LoopNextElement();
    }

    /// <inheritdoc cref="Looping{TSource}(IEnumerable{TSource})"/>
    public static IEnumerable Looping<TSource>(this IEnumerable source)
    {
      if (source == null) throw new ArgumentNullException("source");
      var enumerator = source.GetEnumerator();
      if (!enumerator.MoveNext()) yield break;
      enumerator.Reset();
      while (true) yield return enumerator.LoopNextElement();
    }
  }
}
