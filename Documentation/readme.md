# <center>MVVM pattern</center>
## Prerequisites
### NuGet dependencies: 
- Install for the ViewModels project: `Microsoft.Extensions.DependencyInjection`, `NLog`  
- Install for the Views project: `System.Windows.Interactivity.WPF`

## MVVM.Models 
### `LightBaseModel`
Class implementing INotifyPropertyChanged and the UI notification method. It is inherited by all models whose properties need to notify the UI.
### `ProductM`
Simple sample model used to display some products rows in a ListView control

## MVVM.ViewModels
### `BaseModel`
Class inheriting from `MVVM.Models.BaseModel`, contains extra binding properties that are common for all ViewModels (ex: `WindowTitle`).
It also contains an event called `ClosingView`, to which View classes subscribe a handler inside which they close themselves. Calling the 
method `CloseView` from a view model would invoke this event, which, in turn, would execute all subscribed handlers.
### `IErrorHandler`
Interface providing a basic error handling mecanism. Used by [`FireAndForgetSafeAsync`](#FireAndForgetSafeAsync) to handle exceptions raised 
while executing `Tasks` from within `void` methods.
- A reference implementing it can be injected at runtime through a Dependency Injection system
- A direct implementation also works fine.
### `FireAndForgetSafeAsync`
Extension method for `Task`.  
There are times when we are forced to use **`async void`**, which can be pure evil:
- It generates compiler warnings. Not that important, they can be suppressed.
- If an exception is uncaught inside it, *He's dead, Jim!*
- While debugging, most likely you'll have no proper call stack.  

For more info on why **`async void`** should be avoided at all costs, **Stephen Cleary** (an expert in asynchronous programming) blogs about it:  
[Async await - Best Practices in Asynchronous Programming](https://blog.stephencleary.com/2012/02/async-and-await.html)  
[Async and Await](https://docs.microsoft.com/en-us/archive/msdn-magazine/2013/march/async-await-best-practices-in-asynchronous-programming)

Cases when **`async void`** cannot be avoided:
- Event handlers
- Delegates
- Lambda expressions
- Lifecycle methods

In such cases, awaiting the asynchronous calls at the bottom of the `async void` method, without any code following, can help a bit, but not by much.
When no code folows after an awaited call, awaiting the call becomes useless. When no code follows, we are not interested in a result or completion info.
Therefore, the `await` keyword can be removed, which means the `async` keyword can be removed too. The only problem that remains is exception handling:
of course, wrapping the asynchronous call in a `Try...Catch` block works fine, but that leads to a lot of code duplication and unnecessary lines.  
For such purposes, the `Task` extension method `FireAndForgetAsync` wraps a `Task` into a `Try...Catch` block. When an exception is caught, it is
sent to an error handler which implements the [`IErrorHandler`](#IErrorHandler) interface. The error handler **should not** be set to null!  
Usage:  
```
public void SomeEventHandler(object sender, EventArgs e)
{
    IErrorHandler _error_handler = ... // instantiate from somewhere
    SomeAsyncMethodReturningTask().FireAndForgetSafeAsync(_error_handler);
}
```

### `ISyncCommand<T>`
Interface for providing strong typed implementation of the `ICommand` interface. `ICommand` defines `void Execute(object parameter);`, 
`ISyncCommand<T>` adds `void ExecuteSync(T parameter);`
### `SyncCommand`
Wrapper class providing synchronous implementations of the `ICommand` interface. The `Action` to be executed, the `Func` that indicates whether the 
`Action` can be executed and the  [`IErrorHandler`](#IErrorHandler) implementation are passed through the mandatory overload constructor. 
The `ICommand.CanExecute` and `ICommand.Execute` are exposed through the `CanExecute` and `Execute` methods.
Usage:
```
// Binding of a command to an XAML control:
<Button Content="Click me!" Command="{Binding Whatever_Command}"/>
```

```
// In the view model class:

// declare a property for the SyncCommand
public SyncCommand Whatever_Command { get; private set; }

// declare a void method without parameters:
private void Whatever()
{
    // some code to be executed when the command is invoked
}

// instantiate the SyncCommand in the constructor, 
// assigning the above method to the Action parameter
Whatever_Command = new SyncCommand(Whatever);
```

### `SyncCommand<T>`
Generic version of the [`SyncCommand`](#SyncCommand) class. It implements the [`ISyncCommand<T>`](#ISyncCommand<T>) interface.
It provides the same functionality as the non generic version, the only difference being the usage of a strongly typed parameter passed to the command,
instead of the `object` one used by `ICommand`. The parameter is usually a property of a UI control.
Usage:
```
// Binding of a command to an XAML control, with a parameter
<CheckBox Command="{Binding SomeCheckbox_CheckedChanged_Command}" CommandParameter="{Binding IsChecked, RelativeSource={RelativeSource Self}}"/>
```

```
// In the view model class:

// declare a property for the generic SyncCommand
public ISyncCommand<bool> SomeCheckbox_CheckedChanged_Command { get; private set; }

// declare a void method with a bool parameter
private void SomeCheckbox_CheckedChanged(bool _isChecked)
{
    // some code to be executed when the command is invoked
}

// instantiate the generic SyncCommand in the constructor,
// assigning the above method to the Action parameter
SomeCheckbox_CheckedChanged_Command = new SyncCommand<bool>(SomeCheckbox_CheckedChanged);
```

### `IAsyncCommand`
Interface for extending the `ICommand` interface with asynchronous functionality.

### `IAsyncCommand<T>`
Generic version of [`IAsyncCommand`](#IAsyncCommand), providing strong typed implementation of the `ICommand` interface in an asynchronous way. 
`ICommand` defines `void Execute(object parameter);`, `IAsyncCommand<T>` adds `Task ExecuteAsync(T parameter);`

### `AsyncCommand`
Wrapper class providing asynchronous implementations of the `ICommand` interface through the use of [`IAsyncCommand`](#IAsyncCommand). 
The `Func<Task>` to be executed, the `Func` that indicates whether the `Func<Task>` can be executed and the  [`IErrorHandler`](#IErrorHandler) 
implementation are passed through the mandatory overload constructor. The `ICommand.CanExecute` and `ICommand.Execute` are exposed through the 
`CanExecute` and `ExecuteAsync` methods.

Since `ICommand` only defines a prototype of the `Execute` method returning a `void`, whenever we would need to await asynchronous calls inside 
such methods we would basically end up marking the handler method as `async void`, which is a bad idea for reasons explained in the 
[`FireAndForgetSafeAsync`](#FireAndForgetSafeAsync) section. As explained there, one workaround would be to place the awaited calls as the last 
statements inside the method and launch them using [`FireAndForgetSafeAsync`](#FireAndForgetSafeAsync), which does bring improvements. However, 
defining our own implementation of `ICommand` capable of returning a `Task` instead of `void` is a much better solution, since now we can safely await.
This implementation:
- does not allow concurrent execution (passes [monkey testing](https://en.wikipedia.org/wiki/Monkey_testing))
- provides explicit `ICommand` implementations, so it can be binded from XAML
- uses the [`FireAndForgetSafeAsync`](#FireAndForgetSafeAsync) extension method and the [`IErrorHandler`](#IErrorHandler) interface to deal with the `async void` issues when using XAML binding.
- only publicly exposes the `ExecuteAsync` method, providing the `Task` for cases when the command is not invoked through binding.

Usage:
```
// Binding of a command to an XAML control:
<Button Content="Click me!" Command="{Binding WhateverAsync_Command}"/>
```

```
// In the view model class:

// declare a property for the AsyncCommand
public IAsyncCommand WhateverAsync_Command { get; private set; }

// declare an async Task method without parameters:
private async Task WhateverAsync()
{
    // some async code to be awaited when the command is invoked
}

// instantiate the IAsyncCommand in the constructor, 
// assigning the above method to the Func<Task> parameter
WhateverAsync_Command = new AsyncCommand(WhateverAsync);
```

### `AsyncCommand<T>`
Generic version of the [`AsyncCommand`](#AsyncCommand) class. It implements the [`IAsyncCommand<T>`](#IAsyncCommand<T>) interface.
It provides the same functionality as the non generic version, the only difference being the usage of a strongly typed parameter passed to the command,
instead of the `object` one used by `ICommand`. The parameter is usually a property of a UI control.  
Usage:
```
// Binding of a command to an XAML control, with a parameter
<CheckBox Command="{Binding SomeCheckbox_CheckedChangedAsync_Command}" CommandParameter="{Binding IsChecked, RelativeSource={RelativeSource Self}}"/>
```

```
// In the view model class:

// declare a property for the generic SyncCommand
public IAsyncCommand<bool> SomeCheckbox_CheckedChangedAsync_Command { get; private set; }

// declare an async Task method with a bool parameter
private void SomeCheckbox_CheckedChangedAsync(bool _isChecked)
{
    // some async code to be awaited when the command is invoked
}

// instantiate the generic IAsyncCommand in the constructor,
// assigning the above method to the Func<Task> parameter
SomeCheckbox_CheckedChangedAsync_Command = new AsyncCommand<bool>(SomeCheckbox_CheckedChangedAsync);
```

The non generic and generic versions of `AsyncCommand` are pretty similar, and it is tempting to only keep the latter. We could use a 
`AsyncCommand<object>` with a `null` parameter, to replace the first one. While it technically works, it is better to keep the two of them both, 
in the sense that having no parameter is not semantically similar to taking a `null` parameter.

### `IClipboard`
Interface for abstracting the interaction with the System Clipboard memory. Inside the view, [`WindowsClipboard`](#WindowsClipboard) class implements it.

### `WindowsClipboard`
Concrete implementation for the [`IClipboard`](#IClipboard) interface. Sets a given text to the cache memory.
```
public void SetText(string _text)
{
    Clipboard.SetText(_text);
}
```

### `DependencyInjection`
Class for implementing a Dependency Injection container. It contains a `ServiceCollection` instance, which is just a normal list of ServiceDescriptor 
to which we just register services. After registering the services, we assign them to an `IServiceProvider` instance, which defines a mechanism for 
retrieving a service object.  
Usage:
```
// declare instances of IServiceCollection and IServiceProvider
private static IServiceProvider serviceProvider;
private static readonly IServiceCollection services;

// instantiate ServiceCollection in the constructor and add services to it
services = new ServiceCollection();
services.AddTransient<ILogger, MyLoggerClassImplementingILogger>();

// build the services and assign them to the IServiceProvider
// this must be last step, after registering the services
serviceProvider = services.BuildServiceProvider();

// get the ILogger service and use it in any part of the application 
// through the ServiceProvider property, used through the singleton instance:
myLogger = DependencyInjection.ServiceProvider.GetService<IDispatcher>();
```

This class is also preconfigured to register a dialog service [`IMessageBoxService`](#IMessageBoxService), which can be later used to display UI dialogs
or to offer dialog-like functionality without a GUI, during automated testing, and a dispatcher service [`IDispatcher`](#IDispatcher) which can be set to 
use a real application dispatcher from the view project, or a mock one during tests, useful for running code that needs a synchronization context, such as 
binded observable collections updated on a different thread. Since the real application dispatcher is declared in an assembly (WindowsBase) that is referenced 
in the view project and not in the view model project, the registration of this service is done through reflection.

### `IDispatcher`
Interface for registering a dispatcher service in the [`DependencyInjection`](#DependencyInjection) container.

### `MockDispatcher`
Concrete implementation of [`IDispatcher`](#IDispatcher) used to fake an application dispatcher during automated testing or in environments that do not 
require a UI.

### `DialogResultAttachedProperty`
Class for defining an attached property that allows returning a `DialogResult` from a view, in MVVM.

### `IUserInterface`
Interface for interacting with views from view models. Allows opening a view, opening a modal view and closing a view.

### `UIFactory`
Concrete implementation of [`IUserInterface`](#IUserInterface), used for interacting with views from within view models. Since it is a class declared inside 
the views project, it is alowed to store a private variable of type `Window`. In the constructor of this class, we pass a string variable representing the name 
of the type of the window to be used through this instance. This string is assigned in the code behind of [`StartupV`](#StartupV). 
```
/// <param name="_wnd">The name of the window to be instantiated</param>
public UIFactory(string _wnd)
{
    instance = _wnd;
}
```
It contains methods for interacting with a window, such as `CloseUI`, `ShowUI` and `ShowModalUI`. Inside the `ShowUI` and `ShowModalUI` methods, a new 
window of the type passed through the string in the constructor is instantiated, using the static methid `Activator.CreateInstance`, which creates an 
instance of the specified type, using that type's parameterless constructor. The created window is then assigned to the private `Window` field, for later usage.
```
public void ShowUI()
{
    _wnd = Activator.CreateInstance(Type.GetType(instance, true)) as Window;
    _wnd.Show();
}
```
`ShowUI` and `ShowModalUI` also have overloads that permit the assignment of a specified view model as the `DataContext` of the created window.

### `StartupVM`
The View Models class for the `StartupV` view. It contains a static dictionary of type `<string, IUserInterface>`. This dictionary is used to store mappings of
[`UIFactory`](#UIFactory) instances containing the abstract implementations of views, to names of view models. In other words, it keeps a reference of what view 
model belongs to what view. 

### `StartupV`
Application's startup view. Inside its code behind constructor, we map the names of the viewmodels to a new instance of [`UIFactory`](#UIFactory) to which we pass
the type of the coresponding view.
```
// map the name of the messagebox view model class to the UIFactory instance storing the messagebox view
StartupVM.UIDispatcher.Add(nameof(MsgBoxVM), new UIFactory(typeof(MsgBoxV).Namespace + "." + nameof(MsgBoxV)));
```

### `MessageBoxResult`
Custom enumeration for dealing with a message box result. It emulates the one declared inside `PresentationFramework.dll`, so we don't add a reference to this 
windows-specific assembly to our view models project, which should be agnostic of platform.

### `MessageBoxImage`
Custom enumeration for dealing with a message box icon. It emulates the one declared inside `PresentationFramework.dll`, so we don't add a reference to this 
windows-specific assembly to our view models project, which should be agnostic of platform.

### `MessageBoxButton`
Custom enumeration for dealing with the message box buttons. It emulates the one declared inside `PresentationFramework.dll`, so we don't add a reference to this 
windows-specific assembly to our view models project, which should be agnostic of platform.

### `MsgBoxVM`
View Model for the custom message box dialog designed in `MsgBoxV`. It contains overloaded `Show` methods in which a new `MsgBoxV` window is instantiated
with the help of the `UIDispatcher` static dictionary inside [`StartupVM`](#StartupVM). These methods also return a `DialogResult` which is set through 
[`StartupV`](#StartupV) window, using the [`DialogResultAttachedProperty`](#DialogResultAttachedProperty).

### `MsgBoxV`
View for a custom implementation of a message box dialog. It uses an attached property of type [`DialogResultAttachedProperty`](#DialogResultAttachedProperty) 
for setting the `DialogResult` of the window depending on the pressed buttons.
```
common:DialogResultAttachedProperty.DialogResult="{Binding DialogResult, Mode=TwoWay}"
```

### `IMessageBoxService`
Interface for registering a dialog service in the [`DependencyInjection`](#DependencyInjection) container. Contains overload methods for displaying a 
dialog, and one aditional method for injecting a dialog result during autimated tests (providing an external Yes/No/Cancel option).

### `MessageBoxService`
Concrete implementation of [`IMessageBoxService`](#IMessageBoxService) used to provide a UI dialog system. In this particular implementation, 
`ChangeInjectedDialogResult` is not supported, since the dialog result should be returned as a result of a UI interaction. The overloaded `Show` methods
return a [`MessageBoxResult`](#MessageBoxResult) custom enumeration that is retrieved after calling the `Show` method of a [`MsgBoxVM`](#MsgBoxVM) instance.