namespace PAMobile.ViewModels;

internal class DepositDetailsViewModel : BaseViewModel, IQueryAttributable
{
    public DepositDetailsViewModel()
    {
        BackCommand = new AsyncRelayCommand(OnBack);
        GetDepositBalance = new AsyncRelayCommand(OnGetDepositBalance);
        GetDepositStatement = new AsyncRelayCommand(OnGetDepositStatement);
        SendRequestForReference = new AsyncRelayCommand(OnSendRequestForReference);
        Loans = new ObservableCollection<DepositResponse>();
        GetDigitalDocument = new AsyncRelayCommand<DigitalDocument>(OnGetDigitalDocument);
        DigitalDocuments = new ObservableCollection<DigitalDocument>();
        OpenMBankCommand = new AsyncRelayCommand(OnOpenMBank);
        DepositReplenishmentChooseCommand = new AsyncRelayCommand(OnDepositReplenishmentChoose);


        Task.Run(async () =>
        {

            _accessToken = await SecureStorage.Default.GetAsync("UserAccessToken");
            _clientCode = await SecureStorage.Default.GetAsync("ClientCode");
            await LoadLoans();

        }).GetAwaiter().OnCompleted(() =>
        {
            IsLoading = false;
        });
    }

    string _accessToken;
    string _clientCode;

    public ObservableCollection<DigitalDocument> DigitalDocuments { get; set; }
    public ObservableCollection<DepositResponse> Loans { get; set; }



    public ICommand GetDigitalDocument { get; }
    public ICommand OpenMBankCommand { get; }

    public ICommand DepositReplenishmentChooseCommand { get; set; }
    public ICommand GetDepositBalance { get; }
    public ICommand GetDepositStatement { get; }
    public ICommand SendRequestForReference { get; }
    public ICommand BackCommand { get; }

    private string _depositPositionalNumber;
    public string DepositPositionalNumber
    {
        get => _depositPositionalNumber;
        set => SetProperty(ref _depositPositionalNumber, value);
    }
    private DepositDetail _depositInfo;
    public DepositDetail DepositInfo
    {
        get => _depositInfo;
        set => SetProperty(ref _depositInfo, value);
    }
    private DepositResponse _selectedLoan;
    public DepositResponse SelectedLoan
    {
        get => _selectedLoan;
        set
        {

            SetProperty(ref _selectedLoan, value);
            LoadDocuments();
        }
    }
    private bool _hasDocument;
    public bool HasDocument
    {
        get => _hasDocument;
        set => SetProperty(ref _hasDocument, value);
    }
    private bool _isDocument;
    public bool IsDocument
    {
        get => _isDocument;
        set => SetProperty(ref _isDocument, value);
    }
    private string _remainingIntereset;
    public string RemainingInterest
    {
        get => _remainingIntereset;
        set => SetProperty(ref _remainingIntereset, value);
    }
    private string _balanceOTPA;
    public string BalanceOTPA
    {
        get => _balanceOTPA;
        set => SetProperty(ref _balanceOTPA, value);
    }

    private async Task InitializeDeposit()
    {
        _clientCode = await SecureStorage.Default.GetAsync("ClientCode");

        try
        {
            var response = await ContentService.Instance(_accessToken)
                .GetItemAsync2<DepositDetail>($"api/Deposits/GetDepositBasicInfo?depositPositionalNumber={DepositPositionalNumber}");

            if (response != null)
            {
                DepositInfo = response;
            }

            //var depositBalance = await ContentService.Instance(_accessToken)
            //    .GetItemAsync2<DepositBalance>($"api/Deposits/GetDepositBalance?clientCode={_clientCode}&depositPositionalNumber={DepositPositionalNumber}");
            //if (depositBalance != null)
            //{
            //    RemainingInterest = depositBalance.SumPersent.ToString();
            //    BalanceOTPA = depositBalance.OstatokDeposit.ToString();
            //}
        }
        catch
        {

        }
    }

    private async Task OnBack()
        => await Shell.Current.GoToAsync("..");

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        //await Task.Delay(1500);
        DepositPositionalNumber = HttpUtility.UrlDecode(query["DepositPositionalNumber"].ToString());

        await InitializeDeposit();
    }


    async Task OnGetDepositBalance()
    {
        //await Task.Delay(1500);
        await Shell.Current.GoToAsync($"{nameof(DepositBalancePage)}?{nameof(DepositBalanceViewModel.DepositPositionalNumber)}={DepositPositionalNumber}");
    }

    private async Task OnGetDepositStatement()
    {
        //await Task.Delay(1500);
        await Shell.Current.GoToAsync($"{nameof(DepositStatementPage)}?{nameof(DepositStatementViewModel.DepositPositionalNumber)}={DepositPositionalNumber}");
    }

    async Task OnSendRequestForReference()
    {
        await ContentService.Instance(_accessToken).GetItemAsync2<string>($"api/Deposits/AddRequestForReference?depositPN={DepositPositionalNumber}");
        await Shell.Current.DisplayAlert("", "Отправлено на рассмотрение", "Ок");
    }

    async Task LoadLoans()
    {
        try
        {
            var response = await ContentService.Instance(_accessToken).GetItemsAsync<DepositResponse>($"api/Deposits/GetUserDeposits?clientCode={_clientCode}");

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
    private async void LoadDocuments()
    {
        var response = await ContentService.Instance(_accessToken).GetItemsAsync<DigitalDocument>($"api/DigitalDocuments/GetDepositDigitalDocuments?positionalN" +
            $"={SelectedLoan.DV_POZN}");
        if (response != null && response.Count() > 0)
        {
            DigitalDocuments.Clear();
            App.Current.Dispatcher.Dispatch(() =>
            {
                foreach (var item in response)
                    DigitalDocuments.Add(item);

                IsDocument = true;
                HasDocument = false;

            });
        }
        else
        {
            DigitalDocuments.Clear();

            HasDocument = true;
            IsDocument = false;

        }
    }

    async Task OnGetDigitalDocument(DigitalDocument document)
    {
        var filePath = await LocalNotificationHelper.DownloadAndNotify($"api/DigitalDocuments/GetDigitalDocument?id={document.Id}&" +
            $"dgpozn={document.DG_POZN}", _accessToken);


        if (!string.IsNullOrEmpty(filePath))
        {
            bool answer = await Shell.Current.DisplayAlert("", "Файл скачан и готов к просмотру, вы хотите его открыть?", "Ок", "Нет");
            if (answer)
            {
                if (File.Exists(filePath))
                {
                    try
                    {
                        await Launcher.OpenAsync(new OpenFileRequest
                        {
                            File = new ReadOnlyFile(filePath)
                        });
                    }
                    catch
                    {

                    }
                }
                else
                    Debug.WriteLine("Файл не найден");
            }
        }
    }

    private async Task OnOpenMBank()
    {
        try
        {
            //OpenOtherApp openOtherApp = new OpenOtherApp();
            //openOtherApp.LaunchApp("com.maanavan.mb_kyrgyzstan");
            //await Task.Delay(2000);
            var url = "https://app.mbank.kg/deeplink?service=4aeb462f-9cbe-4868-aa99-b0bc51dd5155&account="
                + DepositPositionalNumber + "&amount=100";
            await Launcher.Default.OpenAsync(url);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Произошла непредвиденная ошибка", "Ок");
        }
    }

    async Task OnDepositReplenishmentChoose()
        => await Shell.Current.GoToAsync($"{nameof(DepositReplenishmentChoosePage)}?{nameof(DepositReplenishmentChooseViewModel.DepositPN)}={DepositPositionalNumber}");
}
