namespace PAMobile.ViewModels.Details;

[QueryProperty(nameof(LoanPositionalNumber), "LoanPositionalNumber")]
internal class LoanDebtViewModel : BaseViewModel
{
    public LoanDebtViewModel()
    {
        IsBusy = true;

        Task.Run(async () =>
        {
            _accessToken = await SecureStorage.Default.GetAsync("UserAccessToken");
            await InitializeLoanDebt();
        }).GetAwaiter().OnCompleted(() =>
        {
            IsBusy = false;
        });

    }

    string _accessToken;

    private string _loanPositionalNumber;
    public string LoanPositionalNumber
    {
        get => _loanPositionalNumber;
        set => SetProperty(ref _loanPositionalNumber, value);
    }
    private LoanTotalDebt _currentLoanTotalDebt;
    public LoanTotalDebt CurrentLoanTotalDebt
    {
        get => _currentLoanTotalDebt;
        set => SetProperty(ref _currentLoanTotalDebt, value);
    }
    private LoanOverdueDebt _currentLoanOverdueDebt;
    public LoanOverdueDebt CurrentLoanOverdueDebt
    {
        get => _currentLoanOverdueDebt;
        set => SetProperty(ref _currentLoanOverdueDebt, value);
    }
    private string _tester;
    public string Tester
    {
        get => _tester;
        set => SetProperty(ref _tester, value);
    }

    async Task InitializeLoanDebt()
    {
        CurrentLoanTotalDebt = await ContentService.Instance(_accessToken).GetItemAsync2<LoanTotalDebt>($"api/Loans/GetLoanTotalDebt?" +
            $"loanPositionalNumber={LoanPositionalNumber}");

        CurrentLoanOverdueDebt = await ContentService.Instance(_accessToken).GetItemAsync2<LoanOverdueDebt>($"api/Loans/" +
            $"GetLoanOverdueDebt?loanPositionalNumber={LoanPositionalNumber}");

    }
}
