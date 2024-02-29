namespace PAMobile.ViewModels;

internal class AccountViewModel : BaseViewModel
{
    public AccountViewModel()
    {
        IsBusy = true;
        RefreshAccountInfo = new AsyncRelayCommand(async () =>
        {
            await InitializeUser();
            IsRefreshing = false;
        });
        ExitCommand = new RelayCommand(OnExit);
        ExchangeRatesCommand = new AsyncRelayCommand(OnExchangeRates);
        GuideCommand = new AsyncRelayCommand(OnGuide);

        Task.Run(async () =>
        {
            //await Task.Delay(1000);
            _accesToken = await SecureStorage.Default.GetAsync("UserAccessToken");
            _userName = await SecureStorage.Default.GetAsync("UserName");

            await InitializeUser();
        });
    }

    string _accesToken;
    string _userName;

    public ICommand RefreshAccountInfo { get; }
    public ICommand ExitCommand { get; }
    public ICommand ExchangeRatesCommand { get; }
    public ICommand GuideCommand { get; }

    private bool _isRefreshing;
    public bool IsRefreshing
    {
        get => _isRefreshing;
        set => SetProperty(ref _isRefreshing, value);
    }
    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }
    private string _userFullName;
    public string UserFullName
    {
        get => _userFullName;
        set => SetProperty(ref _userFullName, value);
    }
    private string _profileImg;
    public string ProfileImg
    {
        get => _profileImg;
        set => SetProperty(ref _profileImg, value);
    }
    private double _userBalance;
    public double UserBalance
    {
        get => _userBalance;
        set => SetProperty(ref _userBalance, value);
    }
    private int _userId;
    public int UserId
    {
        get => _userId;
        set => SetProperty(ref _userId, value);
    }

    private async Task InitializeUser()
    {
        try
        {
            var response = await ContentService.Instance(_accesToken).GetItemAsync<ILoginResponse>($"api/ILogin/GetUserInfo?userName={_userName}");

            if (response.StatusCode == 200)
            {
                Random randomNumber = new Random();
                UserFullName = response.Fio;
                UserBalance = randomNumber.NextDouble();
                UserId = randomNumber.Next(1, 1000);
                ProfileImg = "https://picsum.photos/id/2/200/300";
            }

            IsBusy = false;
        }
        catch { IsBusy = false; }
    }

    void OnExit()
        => System.Diagnostics.Process.GetCurrentProcess().Kill();


    private async Task OnGuide()
        => await Shell.Current.GoToAsync("GuidePage");

    private async Task OnExchangeRates()
    {
        await Shell.Current.GoToAsync("ExchangeRatesPage");
    }
}
