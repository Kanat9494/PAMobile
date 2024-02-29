namespace PAMobile.ViewModels;

[QueryProperty(nameof(LoanPositionalNumber), "LoanPositionalNumber")]
internal class LoanGraphicViewModel : BaseViewModel
{
    public LoanGraphicViewModel()
    {
        LoanGraphics = new ObservableCollection<LoanGraphic>();
        IsBusy = true;
        BackCommand = new AsyncRelayCommand(OnBack);


        Task.Run(async () =>
        {
            //await Task.Delay(1000);
            _accessToken = await SecureStorage.Default.GetAsync("UserAccessToken");
            await InitializeLoanGraphic();
        }).GetAwaiter().OnCompleted(() =>
        {
            IsBusy = false;
        });
    }

    string _accessToken;
    public ObservableCollection<LoanGraphic> LoanGraphics { get; set; }

    public ICommand BackCommand { get; }


    private string _loanPositionalNumber;
    public string LoanPositionalNumber
    {
        get => _loanPositionalNumber;
        set => SetProperty(ref _loanPositionalNumber, value);
    }
    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    private async Task InitializeLoanGraphic()
    {
        try
        {
            var response = await ContentService.Instance(_accessToken).GetItemsAsync<LoanGraphic>
                ($"api/Loans/GetLoanGraphics?loanPositionalNumber={LoanPositionalNumber}");
            if (response != null)
            {
                foreach (var item in response)
                    LoanGraphics.Add(item);
            }
        }
        catch { }
    }

    private async Task OnBack()
        => await Shell.Current.GoToAsync("..");
}
