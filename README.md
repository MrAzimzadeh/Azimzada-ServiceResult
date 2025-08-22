# Azimzada.ServiceResult

A simple generic and non-generic ServiceResult implementation for ASP.NET Core APIs.

## Features
- Generic and non-generic result types
- Implements IActionResult for easy API responses
- Static factory methods for success and failure

## Usage
```csharp
// Generic usage
return ServiceResult<string>.Ok("Success!");

// Non-generic usage
return ServiceResult.Ok();

// Failure
return ServiceResult.Fail(400, "BadRequest");
```

## Installation
Add the NuGet package:
```
dotnet add package Azimzada.ServiceResult
```

## License
MIT
# Azimzada-ServiceResult
