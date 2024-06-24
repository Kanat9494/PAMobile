namespace PAMobile.ViewModels.Details;

[QueryProperty(nameof(LoanPositionalNumber), "LoanPositionalNumber")]
internal class LoanRepaymentChooseViewModel : BaseViewModel
{
    public LoanRepaymentChooseViewModel()
    {
        OpenMBankCommand = new AsyncRelayCommand(OnOpenMBank);
    }

    public ICommand OpenMBankCommand { get; }

    string _accessToken;
    string _clientCode;


    private bool _isLoadingSum;
    public bool IsLoadingSum
    {
        get => _isLoadingSum;
        set => SetProperty(ref _isLoadingSum, value);
    }
    private string _loanPositionalNumber;
    public string LoanPositionalNumber
    {
        get => _loanPositionalNumber;
        set => SetProperty(ref _loanPositionalNumber, value);
    }

    private async Task OnOpenMBank()
    {
        IsLoadingSum = true;
        try
        {
            //OpenOtherApp openOtherApp = new OpenOtherApp();
            //openOtherApp.LaunchApp("com.maanavan.mb_kyrgyzstan");
            //await Task.Delay(2000);
            var paymentSum = await ContentService.Instance(_accessToken).GetItemAsync2<decimal>($"api/Loans/GetLoanPaymentSum?positionalNumber={LoanPositionalNumber}");
            var url = "https://app.mbank.kg/deeplink?service=bd679f4d-ec20-4e07-9c8f-7ac84153a69d&account="
                + LoanPositionalNumber + "&amount=" + paymentSum.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
            await Launcher.Default.OpenAsync(url);
            IsLoadingSum = false;
        }
        catch (Exception ex)
        {
            IsLoadingSum = false;
            await Shell.Current.DisplayAlert("Ошибка", "Произошла непредвиденная ошибка", "Ок");
        }
    }
}
