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

* XUnit
* Moq

## Unit testing with XUnit
