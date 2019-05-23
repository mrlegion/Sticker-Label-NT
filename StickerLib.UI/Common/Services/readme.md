# Services Directory

Use this directory for create you services or use service in this directory.

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
Then you need create instance of `FrameNavigationService` class and send into him `Frame` name. After thete need configuration to navigation pages:
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
    void ShowInfo(string message);
    void ShowSuccess(string message);
    void ShowError(string message);
    void ShowWarning(string message);
    bool ShowRequest(string message);
    void ShowLoading(string message, Action callback);

    void ShowDialog(ItemsControl content, string title);
}
```