# ServiceLocatorForUnity
Service Locator implementation for Unity

## Creating custom services

It's recommended to add a static class in your gameplay code, containing a shortcuts for getting services like this:

```c#
 public static class GameplayServices
 {
     public static IInputService InputService => ServiceLocator.Get<IInputService>();
 }
```
