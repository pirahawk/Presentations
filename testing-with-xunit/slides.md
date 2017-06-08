# Writing unit tests for your code

## Disclaimer

<3 testing. Do not trust myself to write code without unit tests.

Everything in this presentation is a quick outlook of how I like to setup and write unit tests.

*False myth*: 

"We will start cutting code and add unit tests later...."

* Good unit testing deserves the same attention and discipline as platform code
* If you find it hard to write a unit test -> your code is probably really badly designed  


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





## 1. Writing a unit testing with XUnit - Fact

Xunit - Fact: Always true for some invariant condition

**[DEMO]**

Limitations of our test code:

* Lot of noise in test:
  * Creating the timestamps
  * Creating market data values
* Fairly static test case. How do I test other market situations and outcomes?






## 2. Mocking domain objects

* PRO TIP: Do not new() up types directly in your test!!
* Ideally, shouldnt require any mocking libraries
* Use POCO fixture to build concrete objects.
* Always assign default values to save time and reduce unnecessary error


**[DEMO]**

What have we done?
* Reduces constructor coupling, at most two places to change when constructor changes
* Facilitate reuse
* Use method chaining to introduce specific mocking calls to augment parts of the builder in some context (Makes writing unit tests/cases more legible & easier to understand) 




## 3. Using the Xunit Theory attribute to inject test cases

 Xunit - Theory: Always true for a given data set

 Xunit offers three options to inject test data: 
* InlineData: Attribute used to add inline data values for a theory
* MemberData: Data injected using a c# property. 
  * Should be static property with a `get` that returns `IEnumerable<object[]>`. Each `object[]` represents the parameters to your unit test method. Take care to ensure they match up. (Note: this attribute was formely known as `PropertyData`)
* ClassData: Use an explicit class that inherits from `IEnumerable<object[]>` and similarly allows iterating through the test cases. Never really had much use for it tbh.

**[DEMO]**

Benefits of approach
* Use/reuse fixtures to mock test cases 
* Can further isolate test case generation (sort of) using naming conventions that make sense
* Reduce noise in actual unit test. Test case generation is isolated and injected into the actual unit test. Great for maintainability




## 4. Always Inject behaviour

* My recommendation: use `Constructor based injection`, easier to mock IMO. 
  * Can also try `Property based injection` if you like
* Depend on abstractions rather than concrete types. Interfaces remove coupling.
* Can verify method calls & correctness via libraries like moq

**[DEMO]**






## 5. Mocking dependencies using Moq

* We will use Moq to inject market calculator behaviour into our prediction algorithm.
* Can verify calls on mocked object to test correctness
* Can either use concrete instances as method arguments or the `It.IsAny<>` construct (when function arguments are unkown)
* Can verify how many times it was called

**[DEMO]**




Thank you!

You can follow my daily ranting on twitter `@peetat004`
