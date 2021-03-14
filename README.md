# AuraUtilities
A Cross-Platform Utilities Collection for .Net Apps

Now available in Nuget!

```cmd
dotnet add package AuraUtilities --version 0.1.0
```

## Basic Utilities for .Net Apps availables:

### Locator
```c#
//on startup
Locator.Instance.RegisterService<SystemInfoModel>(new SystemInfoModel());

//in another context
SystemInfoModel? sys_info = Locator.Instance.GetService<SystemInfoModel>();
```
### SettingsProvider

```c#
//on app startup
var provider = new SettingsProvider();
App.Current.Settings = provider.Load<AppSettings>();

//important: the objects in properties must be serializables, or the lib will throw an error, I recommend store the object's string representation(Color = "#FFFFFF")
[Serializable]
public class AppSettings : Settings { }

//on app shutdown
var provider = new SettingsProvider();
provider.Save(App.Current.Settings);
```
### LogsService
```c#
//on app startup, if you use "null" the Logger stores the logs in the default temp directory, or you can custom another directory too.
Logger.Start(null);

//in another context
Logger.WriteLine("message", MessageType.Warning); //the MessageType param is optional, the default value is "MessageType.Info"
Logger.Assert(condition == true,"message", MessageType.Warning); //the same but with a condition

//Also support Async
await Logger.WriteLineAsync("message", MessageType.Warning);
```