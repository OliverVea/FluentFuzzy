# FuzzyLogic

FuzzyLogic is a package for doing fuzzy logic in .NET with a natural fluent syntax.

> FuzzyLogic is very much WIP and should be expected to be updated with breaking changes.

## Contents

`FuzzyLogic` contains the core fuzzy logic functionality.

`FuzzyLogic.Visualization` has visualization for some `FuzzyLogic` classes.

## Using

After importing the Nuget package, using FuzzyLogic is a 2-step process.

### Setting up input and output

`FuzzyInput`s and `FuzzyOutput`s can be set up in the following manner:

```cs
// test/FluentFuzzy.Test/Example.cs#L10-L12

const int healthValue = 35;
const int health = new FuzzyInput(() => healthValue);
const int flee = new FuzzyOutput();
```

`FuzzyInput` requires a `Func<double>` to get the current crisp value of the input.

### Setting up member functions

The next step is adding member functions to each input:

```cs
// test/FluentFuzzy.Test/Example.cs#L14-L20

var low = 0;
var medium = 1;
var high = 2;

health.Set(low, new Trapezoid(0, 0, 25, 50));
health.Set(medium, new Triangle(25, 50, 75));
health.Set(high, new Trapezoid(50, 75, 100, 100));
```

Currently, the following membership functions have been implemented:

* `Triangle` defines a triangle.
* `Trapezoid` defines a trapezoid.
* `Line` defines a line.
* `Value` defines a single value.
* `AtLeast` defines a threshold value.

Custom membership functions can be created by implementing the `IMembershipFunction` interface.

After the input functions, membership functions can be added to the `FuzzyOutput`

```cs
// test/FluentFuzzy.Test/Example.cs#L22-L24

flee.Set(low, new Triangle(-0.5, 0, 0.5));
flee.Set(medium, new Triangle(0, 0.5, 1));
flee.Set(high, new Triangle(0.5, 1, 1.5));
```

The only currently supported defuzzification method is a version of the centroid method. As some of the membership functions, namely `Line`, `Value` and `AtLeast` do not have useful centroids, these cannot be used as output member functions.

Custom output membership functions can be created by implementing the `IHasCentroid` interface.

### Setting up fuzzy rules

After setting up the fuzzy inputs and outputs, rules can be created to connect them.

Rules are created with a readable fluent syntax:

```cs
// test/FluentFuzzy.Test/Example.cs#L26-L28

FuzzyRule.If(health.Is(high)).Then(flee.Is(low));
FuzzyRule.If(health.Is(medium)).Then(flee.Is(medium));
FuzzyRule.If(health.Is(low)).Then(flee.Is(high));
```

### Evaluating an output

After creating the fuzzy ruleset, the output can be evaluated by calling the `Evaluate` method on the output:

```cs
// test/FluentFuzzy.Test/Example.cs#L30-L30

Console.WriteLine($"flee(health: {health.Value}) = {flee.Evaluate()}"); // flee(health: 35) = 0,8
```
