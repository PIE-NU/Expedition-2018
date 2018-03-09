# Expedition-2018

## Coding Standards

### Whitespace
**Tabs only** for indentation. Do not use whitespace before method invocations, collection accessors, etc...
```csharp
MethodInvocation();
MethodInvocationBad ();
array[goodIndex1, goodIndex2];
array [bad,morebad];
var good = 1 + 3;
var bad=1+3;
var arr[2] = { 1, 2 };
var arrBad[2] = {1,  100  };
```

### Naming
```csharp
private var m_myPrivateVar;
protected var MyProtectedVar;
public var MyPublicVar;

{private|protected|public} var MyFunction(var var1, var var2)
{
  var localVar;
}
```

### Bracing Convention
The following applies to all scopes except for function or class definitions.
```csharp
if (condition)
  OneLineExecution();

if (condition)
{
  FirstLineExecution();
  SecondLineExecution();
}
else if (elsifCondition)
{
  OneLineExecution();
}
else
{
  OneLineExecution();
}
```

## Contributing

### Branch Names
All lowercase, no spaces, use dashes.
```
feature-x-y
bugfix-x-z
refactor-y-z
```

### Commit Names
Brief, past tense, bullet-point summary; capitalize the first word. No periods.
```
Added lighting system
BUGFIX: Fixed lighting issues
Refactored lighting system
```

## General Coding Practices
* If your function is getting long (e.g. >= 10 lines) consider abstracting it into separate functions.
* If you see yourself copying and pasting code, consider abstracting that chunk into a function.
