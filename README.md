# Testable Unit-Of-Work

This project shows how to develop a `UnitOfWork` pattern and a `Repository` pattern, and how to cover this code with tests. For the example, these patterns have two different implementations using `EntityFramework` and `Dapper`.

# Tests

The goal of this project is to demonstrate the development of the `UnitOfWork` pattern and the `Repository` pattern. In order to minimize the dependency between these patterns, they are linked together by interfaces. This level of abstraction not only reduces dependency, but also allows us to extend the code with new implementations and test each level separately. 

Tests project contains a class `PaymentServiceTests`, which tests `PaymentService`. This test does not use a specific implementation of the `IUnitOfWork` and `IPaymentRepository` interfaces, but uses a surrogate object that is created using `Mock`.

# Benchmarks

|                       Method |       Mean |     Error |    StdDev |  Gen 0 | Allocated |
|----------------------------- |-----------:|----------:|----------:|-------:|----------:|
|              InsertViaDapper |   381.5 us |  22.57 us |  66.19 us | 0.9766 |      5 KB |
|                  InsertViaEf | 1,849.5 us | 109.95 us | 313.69 us |      - |     50 KB |
| InsertAndGetPaymentViaDapper |   615.3 us |  30.54 us |  85.63 us | 1.9531 |      8 KB |
|     InsertAndGetPaymentViaEf | 2,283.8 us | 150.08 us | 433.01 us |      - |     61 KB |

Benchmarks show a significant advantage of the `Dapper` implementation of the `UnitOfWork` pattern and the `Repository` pattern over the `EntityFramework` implementation. However, we must keep in mind that EF is a powerful framework, while `Dapper` is a simple object mapper.