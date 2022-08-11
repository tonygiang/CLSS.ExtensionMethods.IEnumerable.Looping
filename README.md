# CLSS.ExtensionMethods.IEnumerable.Looping

### Problem

Looping a collection indefinitely is an operation that maps well to many use cases (such as AI behavior phases, sequentially looping slideshow, infinite scrolling background, etc...). And while the CLSS package [`IEnumerator.LoopNext`](https://www.nuget.org/packages/CLSS.ExtensionMethods.IEnumerator.LoopNext/) provided a solution for this problem with wide compatibility, its approach may not suit every situation.

`IEnumerator.LoopNext` works on enumerators which are inherently stateful and mutable. It may be enough when you are not particularly concerned about where the enumerator is at in the collection and just want to fetch the next looping element in line one at a time. But when you know the content of the collection, you know where you want to start the loop and how many times you want to take from it all at once, there is some verbosity that can be cut down.

### Solution

`Looping` is an extension method for all [`IEnumerable`](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable?view=net-6.0) and [`IEnumerable<T>`](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-6.0) types that returns an enumerable that loops infinitely in the forward-moving direction on a collection. The enumerable returned by this method loops infinitely just by calling the standard method `MoveNext`. Therefore, a `foreach` statement of it can also iterate infinitely.

```
using CLSS;

var numbers = new int[] { 0, 1, 2, 3 };
var loopingNumbers = numbers.Looping();
foreach (var number in loopingNumbers)
{
  // this will never stop running until it reaches a break statement
}
```

As a result of a never-ending `MoveNext` and the immutability of LINQ methods, controlling where and how much you want to run over a loop can be done as succint as this:

```
using CLSS;
using System.Linq;

var numbers = new int[] { 0, 1, 2, 3 };
foreach (var number in numbers.Looping().Skip(1).Take(6))
{
  // this will enumerate 1, 2, 3, 0, 1, 2 then conclude the foreach statement
}
```

If a looping enumerable was created out of an empty collection, iterating over it is also no different than iterating an empty collection.

```
var emptyArr = new int[0];
foreach (var number in emptyArr.Looping())
{
  // Unreachable code
}
```

The looping enumerable returned by `Looping` should be handled with care. Methods that would cause a full enumeration such as LINQ `ToArray`, `ToList` or `Count` will freeze up a C# program due to infinite enumeration, unless you have limited the enumeration range first with `Take`. Note that `TakeLast` is also a full-enumeration method.

##### This package is a part of the [C# Language Syntactic Sugar suite](https://github.com/tonygiang/CLSS).