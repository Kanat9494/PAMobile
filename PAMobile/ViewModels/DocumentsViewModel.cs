namespace PAMobile.ViewModels;

internal class DocumentsViewModel : BaseViewModel
{
    internal DocumentsViewModel()
    {
        Loans = new ObservableCollection<LoanResponse>();
        DigitalDocuments = new ObservableCollection<DigitalDocument>();
        GetDigitalDocument = new AsyncRelayCommand<DigitalDocument>(OnGetDigitalDocument);
        GetPublicOffer = new AsyncRelayCommand(OnGetPublicOffer);
        SignDocumentCommand = new AsyncRelayCommand<int>(OnSignDocument);
        SendSignedDocumentCommand = new AsyncRelayCommand(OnSendSignedDocument);


        Task.Run(async () =>
        {
            //await Task.Delay(1000);
            _accessToken = await SecureStorage.Default.GetAsync("UserAccessToken");
            _clientCode = await SecureStorage.Default.GetAsync("ClientCode");

            await LoadLoans();
        }).GetAwaiter().OnCompleted(() =>
        {
            IsLoading = false;
        });
    }


    protected string _accessToken;
    string _clientCode;
    int _documentId;

    public ObservableCollection<LoanResponse> Loans { get; set; }
    public ObservableCollection<DigitalDocument> DigitalDocuments { get; set; }


    public ICommand GetDigitalDocument { get; }
    public ICommand GetPublicOffer { get; }
    public ICommand SignDocumentCommand { get; }
    public ICommand SendSignedDocumentCommand { get; }


    private LoanResponse _selectedLoan;
    public LoanResponse SelectedLoan
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
    private bool _isSign;
    public bool IsSign
    {
        get => _isSign;
        set => SetProperty(ref _isSign, value);
    }
    private int? _signingCode;
    public int? SigningCode
    {
        get => _signingCode;
        set => SetProperty(ref _signingCode, value);
    }


    async Task LoadLoans()
    {
        try
        {
            var response = await ContentService.Instance(_accessToken).GetItemsAsync<LoanResponse>($"api/Loans/GetUserLoans?clientCode={_clientCode}");

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
        var sign = false;
        if (SelectedLoan.DG_SUM > 200000)
        {
            sign = false;
        }
        else
            sign = true;
        var response = await ContentService.Instance(_accessToken).GetItemsAsync<DigitalDocument>($"api/DigitalDocuments/GetDigitalDocuments?positionalN" +
            $"={SelectedLoan.DG_POZN}");
        if (response != null && response.Count() > 0)
        {
            DigitalDocuments.Clear();
            App.Current.Dispatcher.Dispatch(() =>
            {
                foreach (var item in response)
                {
                    if (sign && item.Status < 2)
                    {
                        item.CanSign = true;
                    }
                    else
                        item.CanSign = false;
                    DigitalDocuments.Add(item);
                }    

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

    async Task OnGetPublicOffer()
    {
        var filePath = await LocalNotificationHelper.DownloadAndNotify($"api/DigitalDocuments/GetPublicOffer?clientCode={_clientCode}", _accessToken);
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

    private async Task OnSignDocument(int documentId)
    {
        try
        {
            bool answer = await Shell.Current.DisplayAlert("Подтвердите действие", "На ваш номер телефона будет отправлен код", "Да", "Нет");

            if (answer)
            {
                _documentId = documentId;
                var userName = await SecureStorage.Default.GetAsync("UserName");
                var response = await ContentService.Instance(_accessToken).GetItemAsync2<ConfirmationCode>($"api/ILogin/SendSms?clientITIN={userName}");
                if (response != null)
                {


                    IsSign = true;

                }
                else
                {
                    await Shell.Current.DisplayAlert("Ошибка", "Произошла непредвиденная ошибка", "Ок");
                }

            }
            else
            {
                IsSign = false;
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Ошибка", $"Произошла непредвиденная ошибка: {ex.Message}", "Ок");
        }
    }

    async Task OnSendSignedDocument()
    {
        if (SigningCode == null || SigningCode == 0)
            return;

        var response = await ContentService.Instance(_accessToken).GetItemAsync2<string>($"api/DigitalDocuments/SignDocumentViaSms?pn={SelectedLoan.DG_POZN}&" +
            $"documentId={_documentId}&clientCode={_clientCode}&smsCode={SigningCode}");
        await Shell.Current.DisplayAlert("", response, "Ок");
        var digitalDoc = DigitalDocuments.FirstOrDefault(d => d.Id == _documentId);
        if (digitalDoc != null)
        {
            digitalDoc.CanSign = false;
            int i = DigitalDocuments.IndexOf(digitalDoc);
            DigitalDocuments[i] = digitalDoc;
        }
        IsSign = false;
    }
}
