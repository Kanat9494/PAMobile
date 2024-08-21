namespace PAMobile.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();

		BindingContext = new MainViewModel();
        //imgLogo.Source = "salym_logo_l.png";
	}

    private bool _isUpdatedApp;
    string _accessToken;

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if (!_isUpdatedApp)
        {
            _accessToken = await SecureStorage.Default.GetAsync("UserAccessToken");
            var response = await ContentService.Instance(_accessToken).GetItemAsync2<string>("api/AppInfo/GetAppVersion?deviceId=2");

            if (!string.IsNullOrEmpty(response))
            {
                var currentVersion = AppInfo.VersionString;
                if (!response.Equals(currentVersion))
                {
                    var answer = await Shell.Current.DisplayAlert(TextConstants.Title, TextConstants.Description, TextConstants.Confirm, TextConstants.Reject);
                    if (answer)
                    {
                        Uri uri = new Uri("https://apps.apple.com/kg/app/salymfinance/id6479408917");
                        await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
                        _isUpdatedApp = true;
                    }
                }

                _isUpdatedApp = true;

            }
        }

        var hasToScroll = Preferences.Default.Get("has_to_scroll", false);
        if (hasToScroll)
        {
            var storyId = Preferences.Default.Get("story_id", -1);
            if (storyId >= 0)
            {
                storiesCV.ScrollTo(storyId + 1, position: ScrollToPosition.Start);

                Preferences.Default.Set("has_to_scroll", false);
                Preferences.Default.Set("story_id", -1);
            }
        }
    }
}