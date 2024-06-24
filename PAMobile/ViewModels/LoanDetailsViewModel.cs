namespace PAMobile.ViewModels;

//[QueryProperty(nameof(LoanPositionalNumber), "Loan")]
internal class LoanDetailsViewModel : BaseViewModel, IQueryAttributable
{
    public LoanDetailsViewModel()
    {
        ExtractLabel = true;
        GraphicLabel = true;
        MbankLabel = true;
        GetLoanGraphicCommand = new AsyncRelayCommand(OnGetLoansGraphic);
        GetLoanDebtCommand = new AsyncRelayCommand(OnGetLoanDebt);
        BackCommand = new AsyncRelayCommand(OnBack);
        GetExtractCommand = new AsyncRelayCommand(OnGetExtract);
        OpenMBankCommand = new AsyncRelayCommand(OnOpenMBank);
        LoanRepaymentChooseCommand = new AsyncRelayCommand(OnLoanRepaymentChoose);

        Task.Run(async () =>
        {
            //await Task.Delay(1000);
            _accessToken = await SecureStorage.Default.GetAsync("UserAccessToken");
        });
    }

    public ICommand GetLoanGraphicCommand { get; }
    public ICommand GetLoanDebtCommand { get; }
    public ICommand BackCommand { get; }
    public ICommand GetExtractCommand { get; }
    public ICommand OpenMBankCommand { get; }
    public ICommand LoanRepaymentChooseCommand { get; }

    string _accessToken;

    private string _loanPositionalNumber;
    public string LoanPositionalNumber
    {
        get => _loanPositionalNumber;
        set => SetProperty(ref _loanPositionalNumber, value);
    }
    private string _loanNumber;
    public string LoanNumber
    {
        get => _loanNumber;
        set => SetProperty(ref _loanNumber, value);
    }
    private bool _isLoadingSum;
    public bool IsLoadingSum
    {
        get => _isLoadingSum;
        set => SetProperty(ref _isLoadingSum, value);
    }
    private double _loanSum;
    public double LoanSum
    {
        get => _loanSum;
        set => SetProperty(ref _loanSum, value);
    }
    private string _loanCurrency;
    public string LoanCurrency
    {
        get => _loanCurrency;
        set => SetProperty(ref _loanCurrency, value);
    }
    private double _loanPercent;
    public double LoanPercent
    {
        get => _loanPercent;
        set => SetProperty(ref _loanPercent, value);
    }
    private double _loanTerm;
    public double LoanTerm
    {
        get => _loanTerm;
        set => SetProperty(ref _loanTerm, value);
    }
    private string _loanStartDate;
    public string LoanStartDate
    {
        get => _loanStartDate;
        set => SetProperty(ref _loanStartDate, value);
    }
    private string _loanEndDate;
    public string LoanEndDate
    {
        get => _loanEndDate;
        set => SetProperty(ref _loanEndDate, value);
    }
    private double _totalDebt;
    public double TotalDebt
    {
        get => _totalDebt;
        set => SetProperty(ref _totalDebt, value);
    }
    private double _totalOverdueDebt;
    public double TotalOverdueDebt
    {
        get => _totalOverdueDebt;
        set => SetProperty(ref _totalOverdueDebt, value);
    }
    private bool _isLoadingExtract;
    public bool IsLoadingExtract
    {
        get => _isLoadingExtract;
        set => SetProperty(ref _isLoadingExtract, value);
    }
    private bool _isLoadingGraphic;
    public bool IsLoadingGraphic
    {
        get => _isLoadingExtract;
        set => SetProperty(ref _isLoadingExtract, value);
    }
    private bool _extractLabel;
    public bool ExtractLabel
    {
        get => _extractLabel;
        set => SetProperty(ref _extractLabel, value);
    }
    private bool _graphicLabel;
    public bool GraphicLabel
    {
        get => _graphicLabel;
        set => SetProperty(ref _graphicLabel, value);
    }
    private bool _mbankLabel;
    public bool MbankLabel
    {
        get => _mbankLabel;
        set => SetProperty(ref _mbankLabel, value);
    }


    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        
        LoanPositionalNumber = HttpUtility.UrlDecode(query["LoanPositionalNumber"].ToString());

        Task.Run(InitializeLoan);
    }

    async Task InitializeLoan()
    {
        //await Task.Delay(1000);
        try
        {
            var response = await ContentService.Instance(_accessToken)
                .GetItemAsync2<LoanDetail>($"api/Loans/GetLoanBasicInfo?loanPositionalNumber={LoanPositionalNumber}");

            if (response != null)
            {
                LoanNumber = response.DG_NOM;
                LoanSum = (double)response.DG_SUM;
                LoanCurrency = response.DG_KODV;
                LoanPercent = (double)response.Percent;
                LoanTerm = (double)response.Period;
                LoanStartDate = response.DATE1.ToString("d");
                LoanEndDate = response.DATE2.ToString("d");
            }

            //var loanTotalDebt = await ContentService.Instance(_accessToken).GetItemAsync2<LoanTotalDebt>($"api/Loans/GetLoanTotalDebt?" +
            //    $"loanPositionalNumber={LoanPositionalNumber}");
            //if (loanTotalDebt != null)
            //    TotalDebt = (double)loanTotalDebt.Summa;
            //var loanTotalOverdueDebt = await ContentService.Instance(_accessToken).GetItemAsync2<LoanOverdueDebt>($"api/Loans/" +
            //    $"GetLoanOverdueDebt?loanPositionalNumber={LoanPositionalNumber}");
            //if (loanTotalOverdueDebt != null)
            //{
            //    TotalOverdueDebt = (double)loanTotalOverdueDebt.Summa;
            //}
        }
        catch { }
    }

    async Task OnGetLoansGraphic()
    {
        //await Task.Delay(1500);
        //await Shell.Current.GoToAsync($"{nameof(LoanGraphicPage)}?{nameof(LoanGraphicViewModel.LoanPositionalNumber)}={LoanPositionalNumber}");

        //var excelBytes = await ContentService.Instance(_accessToken).GetItemAsync2<byte[]>($"api/Loans/GetLoanStatementXlsx?loanPositionalNumber={LoanPositionalNumber}");

        //await FileHelper.SaveFileAsync(excelBytes);
        IsLoadingGraphic = true;
        GraphicLabel = false;

        var filePath = await LocalNotificationHelper.DownloadAndNotify($"api/Loans/GetLoanGraphicXlsx?loanPositionalNumber={LoanPositionalNumber}", _accessToken);

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

        IsLoadingGraphic = false;
        GraphicLabel = true;
    }

    private async Task OnGetExtract()
    {
        IsLoadingExtract = true;
        ExtractLabel = false;
        //await Task.Delay(1500);
        //await Shell.Current.GoToAsync($"{nameof(LoanExtractPage)}?{nameof(LoanExtractViewModel.LoanPositionalNumber)}={LoanPositionalNumber}");

        var filePath = await LocalNotificationHelper.DownloadAndNotify($"api/Loans/GetLoanStatementXlsx?loanPositionalNumber={LoanPositionalNumber}", _accessToken);

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

        IsLoadingExtract = false;
        ExtractLabel = true;
    }

    async Task OnGetLoanDebt()
    {
        //await Task.Delay(1500);
        await Shell.Current.GoToAsync($"{nameof(LoanDebtPage)}?{nameof(LoanDebtViewModel.LoanPositionalNumber)}={LoanPositionalNumber}");

    }


    private async Task OnBack()
        => await App.Current.MainPage.Navigation.PopModalAsync();

    private async Task OnOpenMBank()
    {
        IsLoadingSum = true;
        MbankLabel = false;
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
            MbankLabel = true;
        }
        catch (Exception ex)
        {
            IsLoadingSum = false;
            await Shell.Current.DisplayAlert("Ошибка", "Произошла непредвиденная ошибка", "Ок");
        }
    }

    async Task OnLoanRepaymentChoose()
        => await Shell.Current.GoToAsync($"{nameof(LoanRepaymentChoosePage)}?{nameof(LoanRepaymentChooseViewModel.LoanPositionalNumber)}={LoanPositionalNumber}");
}
