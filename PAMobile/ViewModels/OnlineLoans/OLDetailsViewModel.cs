namespace PAMobile.ViewModels.OnlineLoans;

internal class OLDetailsViewModel : BaseViewModel
{
    internal OLDetailsViewModel()
    {
        OpenCameraForFrontPassport = new AsyncRelayCommand(OnOpenCameraForFrontPassport);
    }

    private byte[] _frontPassportPhotoBytes;

    public ICommand OpenCameraForFrontPassport { get; }

    private decimal? _loanSum;
    public decimal? LoanSum
    {
        get => _loanSum;
        set
        {
            if (value > OnlineLoanConstants.ONLINE_LOAN_MAX_SUM)
                SetProperty(ref _loanSum, OnlineLoanConstants.ONLINE_LOAN_MAX_SUM);
            else
                SetProperty(ref _loanSum, value);
        }
    }
    private decimal? _loanTerm;
    public decimal? LoanTerm
    {
        get => _loanTerm;
        set
        {
            if (value > OnlineLoanConstants.ONLINE_LOAN_MAX_TERM)
                SetProperty(ref _loanTerm, OnlineLoanConstants.ONLINE_LOAN_MAX_TERM);
            else
                SetProperty(ref _loanTerm, value);
        }
    }
    private string _frontPassportPhotoPath;
    public string FrontPassportPhotoPath
    {
        get => _frontPassportPhotoPath;
        set => SetProperty(ref _frontPassportPhotoPath, value);
    }

    internal void OnSliderLoanSumValueChanged(ValueChangedEventArgs args)
    {
        LoanSum = (int)args.NewValue;
    }

    internal void OnSliderLoanTermValueChanged(ValueChangedEventArgs args)
    {
        LoanTerm = (int)args.NewValue;
    }

    private async Task OnOpenCameraForFrontPassport()
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
            if (photo != null)
            {
                string localFilePath = System.IO.Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                FrontPassportPhotoPath = localFilePath;

                using (Stream sourceStream = await photo.OpenReadAsync())
                {
                    using (FileStream localFileStream = File.OpenWrite(localFilePath))
                    {
                        await sourceStream.CopyToAsync(localFileStream);
                        using (FileStream fileStream = File.OpenRead(localFilePath))
                        {
                            _frontPassportPhotoBytes = new byte[fileStream.Length];
                            await fileStream.ReadAsync(_frontPassportPhotoBytes, 0, _frontPassportPhotoBytes.Length);
                        }
                    }
                }

                Console.WriteLine(_frontPassportPhotoBytes);
            }
        }
    }
}
