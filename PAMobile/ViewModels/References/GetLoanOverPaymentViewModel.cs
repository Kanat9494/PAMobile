namespace PAMobile.ViewModels.References;

internal class GetLoanOverPaymentViewModel : BaseLoanApplicationViewModel
{
    public GetLoanOverPaymentViewModel()
    {
        IsWallet = false;
        NextLoanOverPaymentCommand = new RelayCommand(OnNextOverPayment);
        Wallets = new List<string>()
        {
            "Элкарт",
            "О! Деньги",
            "Balance"
        };
        CustomSendReferenceCommand = new AsyncRelayCommand(OnSendReferenceCustom);
    }

    public ICommand NextLoanOverPaymentCommand { get; }
    public ICommand CustomSendReferenceCommand { get; }

    public List<string> Wallets { get; set; }

    private bool _isWallet;
    public bool IsWallet
    {
        get => _isWallet;
        set => SetProperty(ref _isWallet, value);
    }
    private string _selectedWallet;
    public string SelectedWallet
    {
        get => _selectedWallet;
        set => SetProperty(ref _selectedWallet, value);
    }
    private bool _isSum;
    public bool IsSum
    {
        get => _isSum;
        set => SetProperty(ref _isSum, value);
    }
    private string _walletNumber;
    public string WalletNumber
    {
        get => _walletNumber;
        set => SetProperty(ref _walletNumber, value);
    }


    private void OnNextOverPayment()
    {
        if (SelectedLoan == null)
            return;

        IsWallet = true;

        if (SelectedWallet == null)
            return;

        IsSum = true;

        if (string.IsNullOrWhiteSpace(WalletNumber))
            return;

        IsReference = true;

        ReferenceText = $"Прошу вас произвести возврат излишне переплаченную сумму по кредиту № {SelectedLoan.DG_NOM} от " +
            $"{Convert.ToDateTime(SelectedLoan.DG_DATE1).ToString("d")} года.";
    }

    private async Task OnSendReferenceCustom()
    {
        if (SelectedLoan == null || string.IsNullOrWhiteSpace(SelectedWallet) || string.IsNullOrWhiteSpace(WalletNumber))
            return;

        var clientCode = await SecureStorage.Default.GetAsync("ClientCode");
        
        var loanApplicationRequest = new LoanApplicationRequestForGettingOverPayment
        {
            LoanPositionalNumber = SelectedLoan.DG_POZN,
            ApplicationText = "На выплату переплаты кредита",
            ClientCode = int.Parse(clientCode ?? "0"),
            ClientPhoneNumber = "",
            NumberOfCard = WalletNumber,
            TypeOfCard = SelectedWallet
        };
        await ContentService.Instance(_accessToken).PostItemAsync<LoanApplicationRequestForGettingOverPayment>(loanApplicationRequest, "api/Loans/SendLoanApplicationFGOP");
        await Shell.Current.DisplayAlert("Заявление отправлено", "", "Ок");
        await App.Current.MainPage.Navigation.PopModalAsync();
    }
}
