namespace PAMobile.ViewModels;

internal class DeclarationViewModel : BaseViewModel, IQueryAttributable
{
    public DeclarationViewModel()
    {
        ReferenceDefinitionCommand = new AsyncRelayCommand<string>(OnReference);

        if (Type == 1)
        {
            IsLoan = true;
            IsDeposit = false;
        }
        else if (Type == 2)
        {
            IsLoan = false;
            IsDeposit = true;
        }
        else
        {
            IsLoan = true;
            IsDeposit = true;
        }
    }
    public ICommand ReferenceDefinitionCommand { get; }

    private int _type;
    public int Type
    {
        get => _type;
        set => SetProperty(ref _type, value);
    }
    private bool _isLoan;
    public bool IsLoan
    {
        get => _isLoan;
        set => SetProperty(ref _isLoan, value);
    }
    private bool _isDeposit;
    public bool IsDeposit
    {
        get => _isDeposit;
        set => SetProperty(ref _isDeposit, value);
    }
    async Task OnReference(string parameter)
    {
        if (parameter == "1")
        {
            await AppShell.Current.GoToAsync(nameof(ChangeLoanTermsPage));
        }
        else if (parameter == "2")
        {
            await AppShell.Current.GoToAsync(nameof(GetLoanInformationPage));
        }
        else if (parameter == "3")
        {
            await AppShell.Current.GoToAsync(nameof(GetLoanTranchePage));
        }
        else if (parameter == "4")
        {
            await AppShell.Current.GoToAsync(nameof(GetLoanDocumentsPage));
        }
        else if (parameter == "5")
        {
            await AppShell.Current.GoToAsync(nameof(GetLoanOverPaymentPage));
        }
        else if (parameter == "6")
        {
            await AppShell.Current.GoToAsync(nameof(DepositPartWithdrawalPage));
        }
        else if (parameter == "7")
        {
            await AppShell.Current.GoToAsync(nameof(DepositApplicationFTPage));
        }
        else if (parameter == "8")
        {
            await AppShell.Current.GoToAsync(nameof(DepositTerminationPage));
        }
        else if (parameter == "9")
        {
            await AppShell.Current.GoToAsync(nameof(DepositOriginOFPage));
        }
    }


    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        //ChatId = int.Parse(HttpUtility.UrlDecode(query["ChatId"]?.ToString() ?? "1"));
        Type = int.Parse(query["Type"] as string);
        if (Type == 1)
        {
            IsLoan = true;
            IsDeposit = false;
        }
        else if (Type == 2)
        {
            IsLoan = false;
            IsDeposit = true;
        }
        else
        {
            IsLoan = true;
            IsDeposit = true;
        }
    }
}
