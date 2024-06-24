namespace PAMobile.ViewModels.Details;

[QueryProperty(nameof(DepositPN), "DepositPN")]
internal class DepositReplenishmentChooseViewModel : BaseViewModel
{
    public DepositReplenishmentChooseViewModel()
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
    private int _depositPN;
    public int DepositPN
    {
        get => _depositPN;
        set => SetProperty(ref _depositPN, value);
    }

    private async Task OnOpenMBank()
    {
        try
        {
            //OpenOtherApp openOtherApp = new OpenOtherApp();
            //openOtherApp.LaunchApp("com.maanavan.mb_kyrgyzstan");
            //await Task.Delay(2000);
            var url = "https://app.mbank.kg/deeplink?service=4aeb462f-9cbe-4868-aa99-b0bc51dd5155&account="
                + DepositPN + "&amount=100";
            await Launcher.Default.OpenAsync(url);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Произошла непредвиденная ошибка", "Ок");
        }
    }
}
