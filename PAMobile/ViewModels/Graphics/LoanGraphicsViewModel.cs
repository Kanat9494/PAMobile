namespace PAMobile.ViewModels.Graphics;

[QueryProperty(nameof(FilePath), "FilePath")]
internal class LoanGraphicsViewModel : BaseViewModel
{
    public LoanGraphicsViewModel()
    {

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
    private string _pdfUrl;
    public string PdfUrl
    {
        get => _pdfUrl;
        set => SetProperty(ref _pdfUrl, value);
    }
}
