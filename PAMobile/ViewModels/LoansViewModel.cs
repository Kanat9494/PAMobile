namespace PAMobile.ViewModels;

internal class LoansViewModel : BaseViewModel
{
    public LoansViewModel()
    {
        IsBusy = true;
        Loans = new ObservableCollection<LoanResponse>();
        LoanCommand = new AsyncRelayCommand<int>(OnLoan);
        TestCommand = new AsyncRelayCommand(OnTest);

        Task.Run(async () =>
        {
            _accessToken = await SecureStorage.Default.GetAsync("UserAccessToken");
            _clientCode = await SecureStorage.Default.GetAsync("ClientCode");
            await LoadLoans();
        }).GetAwaiter().OnCompleted(() =>
        {
            IsBusy = false;
        });
    }

    string _accessToken;
    string _clientCode;
    public ObservableCollection<LoanResponse> Loans { get; set; }

    public ICommand LoanCommand { get; }
    public ICommand TestCommand { get; }


    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    async Task LoadLoans()
    {
        try
        {
            var response = await ContentService.Instance(_accessToken).GetItemsAsync<LoanResponse>($"api/Loans/GetUserLoans2?clientCode={_clientCode}");

            if (response != null)
            {
                foreach (var item in response)
                    Loans.Add(item);
            }
        }
        catch
        {

        }
    }

    private async Task OnLoan(int loanPositionalNumber)
    {
        //await Task.Delay(1500);
        await Shell.Current.GoToAsync($"{nameof(LoanDetailsPage)}?{nameof(LoanDetailsViewModel.LoanPositionalNumber)}={loanPositionalNumber}");
    }

    private async Task OnTest()
        => await Shell.Current.DisplayAlert("", "", "");
}
