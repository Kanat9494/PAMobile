namespace PAMobile.ViewModels.Details;

[QueryProperty(nameof(DepositPositionalNumber), "DepositPositionalNumber")]
internal class DepositBalanceViewModel : BaseViewModel
{
    public DepositBalanceViewModel()
    {
        IsBusy = true;

        Task.Run(async () =>
        {
            _accessToken = await SecureStorage.Default.GetAsync("UserAccessToken");
            _clientCode = await SecureStorage.Default.GetAsync("ClientCode");

            await InitializeDepositBalance();
        }).GetAwaiter().OnCompleted(() =>
        {
            IsBusy = false;
        });
    }

    string _accessToken;
    string _clientCode;

    private string _depositPositionalNumber;
    public string DepositPositionalNumber
    {
        get => _depositPositionalNumber;
        set => SetProperty(ref _depositPositionalNumber, value);
    }
    private DepositBalance _currentDepositBalance;
    public DepositBalance CurrentDepositBalance
    {
        get => _currentDepositBalance;
        set => SetProperty(ref _currentDepositBalance, value);
    }


    private async Task InitializeDepositBalance()
    {
        CurrentDepositBalance = await ContentService.Instance(_accessToken).GetItemAsync<DepositBalance>
            ($"api/Deposits/GetDepositBalance?clientCode={_clientCode}&depositPositionalNumber={DepositPositionalNumber}");
    }
}
