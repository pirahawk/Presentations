# Writing unit tests for your code

## Disclaimer

<3 testing. Do not trust myself to write code without unit tests.

Everything in this presentation is a quick outlook of how I like to setup and write unit tests.


## Domain

We are going to write a simple app that will make stock market decisions to buy/sell on every tick (interval)

the decision making algorithm will take two inputs:
* The current market value
* The previous market value

The algorithm will then simply calculate the slope between the two points to guage the current market trend and use this value to make its decision

```
given two arbitrary points (x1, y1) and (x2, y2)

m = (y2 - y1) / (x2 -x1)
```

## Toolset

* XUnit - unit testing framework
* Moq - mocking library

## Unit testing with XUnit - First pass

Limitations with our test code:
* Lot of noise in test:
** Creating the timestamps
** Creating market data values

* Fairly static. How do I test other market situations? - introducing the Xunit Theory and test parameters

Fact: Always true for some invariant condition
Theory: Always true for a data set

## Mocking domain objects and using the XUnit Theory

* Use POCO fixture to build concrete objects. 
** Reduces constructor coupling, at most two places to change when constructor changes
** Allows easy reuse
** Can use method chaining to introduce specific mocking calls that will augment parts of the builder in some context. Makes unit tests/cases more legible & easier to understand 

## Using the Theory attribute to inject test cases
