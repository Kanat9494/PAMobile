namespace PAMobile.ViewModels.References;

internal class BaseDepositApplicationViewModel : BaseViewModel
{
    public BaseDepositApplicationViewModel()
    {
        IsLoading = true;
        Deposits = new ObservableCollection<DepositResponse>();
        BICAccounts = new ObservableCollection<BICAccount>();
        IsBICAccountsLoaded = false;

        Task.Run(async () =>
        {
            //await Task.Delay(1000);
            _accessToken = await SecureStorage.Default.GetAsync("UserAccessToken");
            _clientCode = await SecureStorage.Default.GetAsync("ClientCode");

            await LoadDeposits();
        }).GetAwaiter().OnCompleted(() =>
        {
            IsLoading = false;
        });
    }

    protected string _accessToken;
    protected string _clientCode;

    public ObservableCollection<DepositResponse> Deposits { get; set; }
    public ObservableCollection<BICAccount> BICAccounts { get; set; }


    private DepositResponse _selectedDeposit;
    public DepositResponse SelectedDeposit
    {
        get => _selectedDeposit;
        set => SetProperty(ref _selectedDeposit, value);
    }
    private bool _isNext;
    public bool IsNext
    {
        get => _isNext;
        set => SetProperty(ref _isNext, value);
    }
    private BICAccount _selectedBICAccount;
    public BICAccount SelectedBICAccount
    {
        get => _selectedBICAccount;
        set => SetProperty(ref _selectedBICAccount, value);
    }
    
    private string _paymentAccount;
    public string PaymentAccount
    {
        get => _paymentAccount;
        set => SetProperty(ref _paymentAccount, value);
    }
    private string _cardNumber;
    public string CardNumber
    {
        get => _cardNumber;
        set => SetProperty(ref _cardNumber, value);
    }
    private bool _isBICAccountsLoaded;
    public bool IsBICAccountsLoaded
    {
        get => _isBICAccountsLoaded;
        set => SetProperty(ref _isBICAccountsLoaded, value);
    }
    private bool _isReferenceText;
    public bool IsReferenceText
    {
        get => _isReferenceText;
        set => SetProperty(ref _isReferenceText, value);
    }
    private string _referenceText;
    public string ReferenceText
    {
        get => _referenceText;
        set => SetProperty(ref _referenceText, value);
    }


    protected async Task LoadDeposits()
    {
        try
        {
            var response = await ContentService.Instance(_accessToken).GetItemsAsync<DepositResponse>($"api/Deposits/GetUserDeposits?clientCode={_clientCode}");

            if (response != null)
            {
                App.Current.Dispatcher.Dispatch(() =>
                {
                    foreach (var item in response)
                        Deposits.Add(item);
                });
            }
        }
        catch { }
    }

    

    protected async Task LoadBICAccounts()
    {
        IsLoading = true;
        try
        {
            var response = await ContentService.Instance(_accessToken).GetItemsAsync<BICAccount>($"api/BICAccount/GetBICAccounts");
            if (response != null)
            {
                App.Current.Dispatcher.Dispatch(() =>
                {
                    foreach (var item in response)
                        BICAccounts.Add(item);
                });
            }
            IsLoading = false;
            IsBICAccountsLoaded = true;
        }
        catch 
        {
            IsLoading = false;
        }
    }
}
