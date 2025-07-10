# API RATE LIMITER DEMO

Implemented on .NET Core Web API.  
Testing: SwaggerGen  
IDE used: Visual Studio 2022.  
The code is explained branch-wise below.

## main branch

This is the simplest rate limiter:  
A static rate limiter with a simple Web API (i.e., no controllers used).

## controllers branch

Here, a controller named `testController` is created and used for a static rate limiter in the controller using attributes.

## sliding window branch

Uses the sliding window approach for rate limiting.  
Exceeding requests are added to a queue; after the window’s assigned time is over, it shifts to the next phase, processing the requests stored in the queue.

## token bucket branch

We want to smooth out the initial burst of requests; for that case, we use the token bucket algorithm.  
For more on how the token bucket works, check out: [YouTube explanation](https://www.youtube.com/watch?v=1GJtQ1IfU_M).

## project structure

Important files and folders in a pre-made ASP.NET simple Web API application:

- `Program.cs`
- `<controllers branch>`: `testController.cs`
- `<sliding window branch>`: `test2Controller.cs`
- `<token bucket branch>`: `test3Controller.cs`
