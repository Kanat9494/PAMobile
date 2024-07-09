namespace PAMobile.ViewModels.Graphics;

[QueryProperty(nameof(LoanPositionalNumber), "LoanPositionalNumber")]
internal class LoanGraphicsViewModel : BaseViewModel
{
    public LoanGraphicsViewModel()
    {
        LoanGraphics = new ObservableCollection<LoanGraphic>();
        GetPdfCommand = new AsyncRelayCommand(OnGetPdf);
    }

    string _accessToken;
    public ICommand GetPdfCommand { get; }

    public ObservableCollection<LoanGraphic> LoanGraphics { get; set; }

    private int _loanPositionalNumber;
    public int LoanPositionalNumber
    {
        get => _loanPositionalNumber;
        set
        {
            SetProperty(ref _loanPositionalNumber, value);
            Task.Run(async () => await GetLoanGraphics());
        }
    }

    private async Task GetLoanGraphics()
    {
        _accessToken = await SecureStorage.Default.GetAsync("UserAccessToken");
        var response = await ContentService.Instance(_accessToken).GetItemsAsync<LoanGraphic>($"api/Loans/GetLoanGraphics?loanPositionalNumber=" +
            $"{LoanPositionalNumber}");

        if (response != null && response.Count() > 0)
        {
            foreach (var item in response)
            {
                LoanGraphics.Add(item);
            }
        }
    }

    async Task OnGetPdf()
    {
        IsBusy = true;
        _accessToken = await SecureStorage.Default.GetAsync("UserAccessToken");

        var filePath = await LocalNotificationHelper.DownloadPDF($"api/Loans/GetLoanGraphicXlsx?loanPositionalNumber={LoanPositionalNumber}", _accessToken);

        if (!string.IsNullOrEmpty(filePath))
        {
            if (true)
            {
                if (File.Exists(filePath))
                {
                    try
                    {
                        await Shell.Current.GoToAsync($"{nameof(PdfPage)}?{nameof(PdfViewModel.FilePath)}={filePath}");
                        //var filePath = $"file:///android_asset/pdfjs/web/viewer.html?file={filePath}";
                    }
                    catch
                    {

                    }
                }
                else
                    Debug.WriteLine("Файл не найден");
            }
        }

        IsBusy = false;
    }
}
