## WRLD Tech Test
For this test I decided to implement a 2D Binary Tree to store the results, I then implemented a nearest neighbour algorithm with some pruning of subtrees for performance.
I developed the solution using TDD. 

### Installation / Setup
**.NET SDK**
- [Windows](https://dotnet.microsoft.com/learn/dotnet/hello-world-tutorial/install?initial-os=windows)
- [Linux](https://dotnet.microsoft.com/learn/dotnet/hello-world-tutorial/install?initial-os=linux)
- [macOS](https://dotnet.microsoft.com/learn/dotnet/hello-world-tutorial/install?initial-os=macos)

### Running the program
```
 dotnet run --project wrldtechtest
```

### Running the unit tests for the project
There are 7 unit tests within this project. To run all of the tests and to receive a summary of output and results you can use the following CLI command:
```
 dotnet test
```

### Performance / Complexity
* To build the tree the runtime is **O(n log n)**

* For the nearest neighbour search, we might have to search close to the whole tree in the worst case **O(n)**
* In practice, runtime is closer to **O(2d + log n)** where **d** is always **2** in our case.

