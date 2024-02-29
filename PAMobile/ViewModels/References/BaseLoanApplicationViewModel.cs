namespace PAMobile.ViewModels.References;

internal class BaseLoanApplicationViewModel : BaseViewModel
{
    public BaseLoanApplicationViewModel()
    {
        IsLoading = true;
        Loans = new ObservableCollection<LoanResponse>();
        TestCommand = new AsyncRelayCommand(OnTest);
        NextCommand = new AsyncRelayCommand(OnNext);
        
        SendReferenceCommand = new AsyncRelayCommand(OnSendReference);

        Task.Run(async () =>
        {
            //await Task.Delay(1000);
            _accessToken = await SecureStorage.Default.GetAsync("UserAccessToken");
            _clientCode = await SecureStorage.Default.GetAsync("ClientCode");

            await LoadLoans();
        }).GetAwaiter().OnCompleted(() =>
        {
            IsLoading = false;
        });
    }

    protected string _accessToken;
    string _clientCode;
    protected private string _textToSend;
    public ObservableCollection<LoanResponse> Loans { get; set; }

    public ICommand TestCommand { get; }
    public ICommand NextCommand { get; }
    public ICommand SendReferenceCommand { get; }

    private LoanResponse _selectedLoan;
    public LoanResponse SelectedLoan
    {
        get => _selectedLoan;
        set => SetProperty(ref _selectedLoan, value);
    }
    private bool _isNext;
    public bool IsNext
    {
        get => _isNext;
        set => SetProperty(ref _isNext, value);
    }
    private bool _isReference;
    public bool IsReference
    {
        get => _isReference;
        set => SetProperty(ref _isReference, value);
    }
    private string _referenceText;
    public string ReferenceText
    {
        get => _referenceText;
        set => SetProperty(ref _referenceText, value);
    }


    async Task LoadLoans()
    {
        try
        {
            var response = await ContentService.Instance(_accessToken).GetItemsAsync<LoanResponse>($"api/Loans/GetUserLoans?clientCode={_clientCode}");

            if (response != null)
            {
                App.Current.Dispatcher.Dispatch(() =>
                {
                    foreach (var item in response)
                        Loans.Add(item);
                });
            }
        }
        catch (Exception ex)
        {

        }
    }

    private async Task OnTest()
        => await Shell.Current.DisplayAlert("Test", "Был нажат тест команд", "Jr");

    private async Task OnNext()
    {
        await Task.Delay(1000);
        if (SelectedLoan == null)
        {
            IsNext = false;
            await Shell.Current.DisplayAlert("Не выбран кредит", "Выберите кредит, прежде чем продолжить дальше", "Ок");
            return;
        }
        IsNext = true;
    }


    private async Task OnSendReference()
    {
        var clientPhoneNumber = Preferences.Default.Get("clientPhoneNumber", "0");
        var loanApplicationRequest = new LoanApplicationRequest
        {
            LoanPositionalNumber = SelectedLoan.DG_POZN,
            ApplicationText = _textToSend,
            ClientCode = int.Parse(_clientCode ?? "0"),
            ClientPhoneNumber = clientPhoneNumber
        };
        await ContentService.Instance(_accessToken).PostItemAsync<LoanApplicationRequest>(loanApplicationRequest, "api/Loans/SendLoanApplication");
        await Shell.Current.DisplayAlert("Заявление отправлено", "", "Ок");
        await App.Current.MainPage.Navigation.PopModalAsync();
    }
}
