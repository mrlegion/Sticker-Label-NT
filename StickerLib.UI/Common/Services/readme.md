# ![](../../Images/folder32x32.png) Services Directory 
Use this directory for create you services or use service in this directory.

``` text
Your app
    |
    |-- Common
         |-- Behaviors
         |-- Converters
         |-- Services
```

## IFrameNavigationService
This service use for navigation in you app.
#### Interface
``` csharp
public interface IFrameNavigationService : INavigationService, INotifyPropertyChanged
{
    object Parameter { get; } // this parameter sending when you navigate new page
    string FrameName { get; } // name for base frame
    bool CanGoBack { get; }   // check for can we go back to navigate

    void Configure(string key, Uri pageType); // config new page in navigation service
}
``` 

#### How to use?
You need create `Frame` element in your `Window` or `Page` with name:
``` xml
<Frame x:Name="RootFrame" />
```
Then you need create instance of `FrameNavigationService` class and send into him `Frame` name. After that need configuration to navigation pages:
``` csharp
var service = new FrameNavigationService("RootFrame");
service.Configure("name of page", new Uri("\\Views\\Where\\Put\\Xaml\\File.xaml", UriKind.Relative));
```
And use this in code:
``` csharp
// base navigate
service.NavigateTo("name of page");
// or send with parameters
service.NavigateTo("name of page", someoneParams);
```
---
## IDialog or DialogService
This service use to show dialog window in your app.
#### Interface
``` csharp
public interface IDialog
{
    void ShowInfo(string message);      // Show information dialog (blue)
    void ShowSuccess(string message);   // Show success dialog (green)
    void ShowError(string message);     // Show error dialog (red)
    void ShowWarning(string message);   // Show warning dialog (yellow)
    bool ShowRequest(string message);   // Show request dialog with answer
    
    // Show dialog with circle on loading messange
    // Send Action callback to running into other Thread
    void ShowLoading(string message, Action callback);

    // Show custom dialog
    void ShowDialog(ItemsControl content, string title);
}
```
#### How to use?
First you need install NuGet-Package `MaterialDesignThemes` and `MaterialDesignColor`:
```
Install-Package MaterialDesignThemes
```
Read [Getting Started](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit/wiki/Getting-Started) for base setup `MaterialDesignThemes` in your application. After install package in your project adding `xmlns` into XAML:
``` xml
<Window xmlns:md="http://materialdesigninxaml.net/winfx/xaml/themes">
```
Then create `DialogHost` tag in your `Window` or `Page` component and set him unique `Identifier` name.
``` xml
<md:DialogHost Identifier="RootDialogHost" Padding="20 30"/>
```
Create instance of `DialogService` class and you can use dialog window in you application.
``` csharp
IDialog dialogService = new DialogService(identifier: "RootDialogHost");
dialogService.ShowInfo("Information text"); // Show information dialog in you application

bool request = await dialogService.ShowRequest("Questens");
if (request) { // someone code here }
else { // someone code here }
```
#### Custom dialog window
You can create your view for the contents of the dialog box with your logic.
