namespace PAMobile.ViewModels.Details;

[QueryProperty(nameof(DepositPositionalNumber), "DepositPositionalNumber")]
internal class DepositStatementViewModel : BaseViewModel
{
    public DepositStatementViewModel()
    {
        IsBusy = true;
        DepositStatementsWithSum = new ObservableCollection<DepositStatement>();
        DepositStatementsWithPercent = new ObservableCollection<DepositStatement>();
        PrincipalAmountCommand = new AsyncRelayCommand(OnPrincipalAmount);
        InterestsStatementCommand = new AsyncRelayCommand(OnInterestsStatement);

        TestWord = "How are you, this is a test word?";

        Task.Run(async () =>
        {
            await InitializeDepositStatement();
        }).GetAwaiter().OnCompleted(() =>
        {
            IsBusy = false;
        });
    }

    public ObservableCollection<DepositStatement> DepositStatementsWithSum { get; set; }
    public ObservableCollection<DepositStatement> DepositStatementsWithPercent { get; set; }

    string _accessToken;
    string _clientCode;

    public ICommand PrincipalAmountCommand { get; }
    public ICommand InterestsStatementCommand { get; }


    private string _depositPositionalNumber;
    public string DepositPositionalNumber
    {
        get => _depositPositionalNumber;
        set => SetProperty(ref _depositPositionalNumber, value);
    }
    private string _testWord;
    public string TestWord
    {
        get => _testWord;
        set => SetProperty(ref _testWord, value);
    }


    async Task InitializeDepositStatement()
    {
        var response = await ContentService.Instance(_accessToken).GetItemsAsync<DepositStatement>
            ($"api/Deposits/GetDepositStatementsWithSum?depositPN={DepositPositionalNumber}");

        if (response != null)
        {
            foreach (var item in response)
                DepositStatementsWithSum.Add(item);
        }

        var response2 = await ContentService.Instance(_accessToken).GetItemsAsync<DepositStatement>
            ($"api/Deposits/GetDepositStatementWithPercent?depositPN={DepositPositionalNumber}");

        if (response2 != null)
        {
            foreach (var item in response2)
                DepositStatementsWithPercent.Add(item);
        }
    }

    private async Task OnPrincipalAmount()
    {
        //await Task.Delay(1000);

        var filePath = await LocalNotificationHelper.DownloadPDF($"api/Deposits/GetDepositSOPAXlsx?depositPN={DepositPositionalNumber}", _accessToken);
        if (!string.IsNullOrEmpty(filePath))
        {
            //bool answer = await Shell.Current.DisplayAlert("", "Файл скачан и готов к просмотру, вы хотите его открыть?", "Ок", "Нет");
            if (true)
            {
                if (File.Exists(filePath))
                {
                    try
                    {
                        //await Launcher.OpenAsync(new OpenFileRequest
                        //{
                        //    File = new ReadOnlyFile(filePath)
                        //});
                        await Shell.Current.GoToAsync($"{nameof(PdfPage)}?{nameof(PdfViewModel.FilePath)}={filePath}");

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

    async Task OnInterestsStatement()
    {
        //await Task.Delay(1000);
        var filePath = await LocalNotificationHelper.DownloadPDF($"api/Deposits/GetDepositISXlsx?depositPN={DepositPositionalNumber}", _accessToken);


        if (!string.IsNullOrEmpty(filePath))
        {
            //bool answer = await Shell.Current.DisplayAlert("", "Файл скачан и готов к просмотру, вы хотите его открыть?", "Ок", "Нет");
            if (true)
            {
                if (File.Exists(filePath))
                {
                    try
                    {
                        //await Launcher.OpenAsync(new OpenFileRequest
                        //{
                        //    File = new ReadOnlyFile(filePath)
                        //});
                        await Shell.Current.GoToAsync($"{nameof(PdfPage)}?{nameof(PdfViewModel.FilePath)}={filePath}");
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
}
