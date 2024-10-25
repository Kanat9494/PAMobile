namespace PAMobile.ViewModels.Details;

internal class AdDetailsViewModel : BaseViewModel, IQueryAttributable
{
    public AdDetailsViewModel()
    {
        GoToCommand = new AsyncRelayCommand(OnGoTo);
    }

    string _accessToken;

    public ICommand GoToCommand { get; }


    private Ad _currentAd;
    public Ad CurrentAd
    {
        get => _currentAd;
        set => SetProperty(ref _currentAd, value);
    }
    private int _adId;
    public int AdId
    {
        get => _adId;
        set => SetProperty(ref _adId, value);
    }

    private async Task InitializeAd()
    {
        _accessToken = await SecureStorage.Default.GetAsync("UserAccessToken");

        CurrentAd = await ContentService.Instance(_accessToken).GetItemAsync2<Ad>($"api/Ads/GetAdById?id={AdId}");
    }

    private async Task OnGoTo()
    {
        Uri uri = new Uri(CurrentAd.Url);
        await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
    }

    #region Query params
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        //ChatId = int.Parse(HttpUtility.UrlDecode(query["ChatId"]?.ToString() ?? "1"));
        AdId = int.Parse(query["AdId"] as string);
        Task.Run(async () =>
        {
            await InitializeAd();
        });
    }
    #endregion
}
