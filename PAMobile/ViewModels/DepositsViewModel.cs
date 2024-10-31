namespace PAMobile.ViewModels;

internal class DepositsViewModel : BaseViewModel
{
    public DepositsViewModel()
    {
        IsLoading = true;
        Deposits = new ObservableCollection<DepositResponse>();
        DepositCommand = new AsyncRelayCommand<int>(OnDeposit);
        DeclarationCommand = new AsyncRelayCommand(OnDeclaration);

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
    string _accessToken;
    string _clientCode;

    public ObservableCollection<DepositResponse> Deposits { get; set; }

    public ICommand DepositCommand { get; }
    public ICommand DeclarationCommand { get; }

    private async Task LoadDeposits()
    {
        try
        {
            var response = await ContentService.Instance(_accessToken).GetItemsAsync<DepositResponse>($"api/Deposits/GetUserDeposits2?clientCode={_clientCode}");

            if (response != null)
            {
                foreach (var item in response)
                    Deposits.Add(item);
            }
        }
        catch { }
    }

    private async Task OnDeposit(int depositPositionalNumber)
    {
        //await Task.Delay(1500);
        await Shell.Current.GoToAsync($"{nameof(DepositDetailsPage)}?{nameof(DepositDetailsViewModel.DepositPositionalNumber)}={depositPositionalNumber}");
    }

    async Task OnDeclaration()
        => await Shell.Current.GoToAsync($"{nameof(DeclarationPage)}?{nameof(DeclarationViewModel.Type)}=2");
}
