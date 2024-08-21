namespace PAMobile.ViewModels.References;

internal class DepositOriginOFViewModel : BaseDepositApplicationViewModel
{
    public DepositOriginOFViewModel()
    {
        NextCommand = new AsyncRelayCommand(OnNext);
        SendReferenceCommand = new AsyncRelayCommand(OnSendReference);
        AddDocumentCommand = new AsyncRelayCommand(OnAddDocument);


        OriginsOfFunds = new ObservableCollection<string>
        {
            "от продажи недвижимости",
            "от продажи автомашины",
            "от продажи техники",
            "от наследства",
            "от доходов по деятельности",
            "другое"
        };
    }


    public ICommand NextCommand { get; }
    public ICommand SendReferenceCommand { get; }
    public ICommand AddDocumentCommand { get; }

    public ObservableCollection<string> OriginsOfFunds { get; set; }


    private string _depositSum;
    public string DepositSum
    {
        get => _depositSum;
        set => SetProperty(ref _depositSum, value);
    }
    private string _selectedOriginOfFunds;
    public string SelectedOriginOfFunds
    {
        get => _selectedOriginOfFunds;
        set
        {
            if (value == "другое")
            {
                IsOtherOrigin = true;
                SetProperty(ref _selectedOriginOfFunds, value);
                OriginOfFunds = "";
            }
            else
            {
                SetProperty(ref _selectedOriginOfFunds, value);
                OriginOfFunds = value;
            }

        }
    }
    private string _originOfFunds;
    public string OriginOfFunds
    {
        get => _originOfFunds;
        set => SetProperty(ref _originOfFunds, value);
    }
    private bool _isOtherOrigin;
    public bool IsOtherOrigin
    {
        get => _isOtherOrigin;
        set => SetProperty(ref _isOtherOrigin, value);
    }
    private string _selectedOrigin;
    public string SelectedOrigin
    {
        get => _selectedOrigin;
        set => SetProperty(ref _selectedOrigin, value);
    }
    private string _imagePath;
    public string ImagePath
    {
        get => _imagePath;
        set => SetProperty(ref _imagePath, value);
    }
    private string _documentDownloaded;
    public string DocumentDownloaded
    {
        get => _documentDownloaded;
        set => SetProperty(ref _documentDownloaded, value);
    }


    private async Task OnNext()
    {
        await Task.Delay(1000);
        if (SelectedDeposit == null)
        {
            IsNext = false;
            await Shell.Current.DisplayAlert("Не выбран депозит", "Выберите депозит, прежде чем продолжить дальше", "Ок");
            return;
        }
        if (!IsNext)
        {
            IsNext = true;
            return;
        }
        if (OriginOfFunds == null || OriginOfFunds.Length == 0)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Выберите происхождение денежных средств", "Ок");
            return;
        }
        //if (!IsBICAccountsLoaded)
        //{
        //    await LoadBICAccounts();
        //    return;
        //}
        IsReferenceText = true;
        var fio = Preferences.Default.Get("user_full_name", "");

        ReferenceText = $"Я {fio}, настоящим подтверждаю собственность денежных средств в сумме {DepositSum}, " +
            $" которые вношу в ОАО МФК Салым Финанс на депозитный счет. \nДенежные средства являются собственными сбережениями, " +
            $"накполены: {OriginOfFunds}";
    }

    private async Task OnSendReference()
    {

        await ContentService.Instance(_accessToken).GetItemAsync2<string>($"api/Deposits/SendOriginOfFunds?depositPN={SelectedDeposit.DV_POZN}&" +
            $"text=Информация о происхождении денежных средств&originOfFunds={OriginOfFunds}");

        var fileData = await File.ReadAllBytesAsync(ImagePath);
        var clientITIN = await SecureStorage.Default.GetAsync("UserName");
        var file = new FileToSave
        {
            PathFile = $"{clientITIN}\\Deposits\\{SelectedDeposit.DV_POZN}\\ElectronicDocuments\\{SelectedOrigin}",
            FileData = fileData,
        };
        await ContentService.Instance(_accessToken).PostItemAsync<FileToSave>(file, "api/Files/UploadFile");
        await Shell.Current.DisplayAlert("Заявление отправлено", "", "Ок");
        await App.Current.MainPage.Navigation.PopModalAsync();
    }

    private async Task OnAddDocument()
    {
        try
        {
            SelectedOrigin = OriginOfFunds;
            var result = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Выберите фото для обзора",
                FileTypes = FilePickerFileType.Images
            });

            if (result != null)
            {
                ImagePath = result.FullPath;
                DocumentDownloaded = "Ваш документ добавлен";
                SelectedOrigin = SelectedOrigin + "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + System.IO.Path.GetExtension(result.FullPath);
            }
        }
        catch
        {

        }
    }
}
