namespace PAMobile.ViewModels.Graphics;

[QueryProperty(nameof(FilePath), "FilePath")]
internal class PdfViewModel : BaseViewModel
{
    public PdfViewModel()
    {
        DownloadCommand = new AsyncRelayCommand(OnDownload);
    }

    string _accessToken;

    public ICommand DownloadCommand { get; }

    private string _pdfUrl;
    public string PdfUrl
    {
        get => _pdfUrl;
        set => SetProperty(ref _pdfUrl, value);
    }
    private int _loanPN;
    public int LoanPN
    {
        get => _loanPN;
        set
        {
            SetProperty(ref _loanPN, value);
            //Task.Run(GetPdf);
        }
    }
    private string _filePath;
    public string FilePath
    {
        get => _filePath;
        set
        {
            SetProperty(ref _filePath, value);
            PdfUrl = FilePath;
        }
    }

    private async Task OnDownload()
    {
        try 
        {
            await Launcher.OpenAsync(new OpenFileRequest
            {
                File = new ReadOnlyFile(FilePath)
            });
        }
        catch
        {

        }
    }
}
