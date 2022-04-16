# Testable Unit-Of-Work
Unit Of Work pattern implemented via dapper and ef.

# Benchmarks

|                       Method |       Mean |     Error |    StdDev |  Gen 0 | Allocated |
|----------------------------- |-----------:|----------:|----------:|-------:|----------:|
|              InsertViaDapper |   381.5 us |  22.57 us |  66.19 us | 0.9766 |      5 KB |
|                  InsertViaEf | 1,849.5 us | 109.95 us | 313.69 us |      - |     50 KB |
| InsertAndGetPaymentViaDapper |   615.3 us |  30.54 us |  85.63 us | 1.9531 |      8 KB |
|     InsertAndGetPaymentViaEf | 2,283.8 us | 150.08 us | 433.01 us |      - |     61 KB |
