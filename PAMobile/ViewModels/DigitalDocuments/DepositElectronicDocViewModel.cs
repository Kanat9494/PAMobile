namespace PAMobile.ViewModels.DigitalDocuments;

internal class DepositElectronicDocViewModel : BaseViewModel
{
    public DepositElectronicDocViewModel()
    {
        Loans = new ObservableCollection<DepositResponse>();
        GetDigitalDocument = new AsyncRelayCommand<DigitalDocument>(OnGetDigitalDocument);
        DigitalDocuments = new ObservableCollection<DigitalDocument>();

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
}

