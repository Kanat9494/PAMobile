namespace PAMobile.ViewModels.Details;

internal class LoanExtractViewModel : BaseViewModel, IQueryAttributable
{
    public LoanExtractViewModel()
    {
        IsBusy = true;
        LoanStatements = new ObservableCollection<LoanStatement>();

        //Task.Run(async () =>
        //{
        //    _accessToken = await SecureStorage.Default.GetAsync("UserAccessToken");

        //    await InitializeLoanStatement();
        //}).GetAwaiter().OnCompleted(() =>
        //{
        //    IsBusy = false;
        //});
    }

    string _accessToken;

    public ObservableCollection<LoanStatement> LoanStatements { get; set; }



    private string _loanPositionalNumber;
    public string LoanPositionalNumber
    {
        get => _loanPositionalNumber;
        set => SetProperty(ref _loanPositionalNumber, value);
    }

    private async Task InitializeLoanStatement()
    {
        try
        {
            var response = await ContentService.Instance(_accessToken).GetItemsAsync<LoanStatement>
                ($"api/Loans/GetLoanStatement?loanPositionalNumber={LoanPositionalNumber}");

            if (response != null)
            {
                foreach (var item in response)
                    LoanStatements.Add(item);
            }
        }
        catch { }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        LoanPositionalNumber = HttpUtility.UrlDecode(query["LoanPositionalNumber"].ToString());

        Task.Run(async () =>
        {
            _accessToken = await SecureStorage.Default.GetAsync("UserAccessToken");

            await InitializeLoanStatement();
        }).GetAwaiter().OnCompleted(() =>
        {

        });
    }
}
