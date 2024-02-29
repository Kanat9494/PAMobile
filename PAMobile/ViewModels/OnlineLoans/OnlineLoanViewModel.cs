namespace PAMobile.ViewModels.OnlineLoans;

internal class OnlineLoanViewModel : BaseViewModel
{
    internal OnlineLoanViewModel()
    {
        OpenCameraForFrontPassport = new AsyncRelayCommand(OnOpenCameraForFrontPassport);
        OpenCameraForBackPassport = new AsyncRelayCommand(OnOpenCameraForBackPassport);
        OpenCameraForSelfie = new AsyncRelayCommand(OnOpenCameraForSelfie);
        SendOnlineLoanDatas = new AsyncRelayCommand(OnSendOnlineLoanDatas);
        CurrentOnlineLoan = new OnlineLoan();

        Task.Run(async () =>
        {
            _accessToken = await SecureStorage.Default.GetAsync("UserAccessToken");
            _clientCode = await SecureStorage.Default.GetAsync("ClientCode");
        });

        PaymentMethods = ContentProvider.GeneratePaymentMethods();
    }

    private string _accessToken;
    string _clientCode;

    public List<PaymentMethod> PaymentMethods { get; set; }



    public ICommand OpenCameraForFrontPassport { get; }
    public ICommand OpenCameraForBackPassport { get; }
    public ICommand OpenCameraForSelfie { get; }
    public ICommand SendOnlineLoanDatas { get; }



    private PaymentMethod _selectedPaymentMethod;
    public PaymentMethod SelectedPaymentMethod
    {
        get => _selectedPaymentMethod;
        set => SetProperty(ref _selectedPaymentMethod, value);
    }
    private OnlineLoan _currentOnlineLoan;
    public OnlineLoan CurrentOnlineLoan
    {
        get => _currentOnlineLoan;
        set => SetProperty(ref _currentOnlineLoan, value);
    }

    private decimal? _loanSum;
    public decimal? LoanSum
    {
        get => _loanSum;
        set
        {
            if (value > OnlineLoanConstants.ONLINE_LOAN_MAX_SUM)
                SetProperty(ref _loanSum, OnlineLoanConstants.ONLINE_LOAN_MAX_SUM);
            //else if (value < OnlineLoanConstants.ONLINE_LOAN_MIN_SUM)
            //    SetProperty(ref _loanSum, OnlineLoanConstants.ONLINE_LOAN_MIN_SUM);
            else
                SetProperty(ref _loanSum, value);
        }
    }
    private int? _loanTerm;
    public int? LoanTerm
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
    private string _backPassportPhotoPath;
    public string BackPassportPhotoPath
    {
        get => _backPassportPhotoPath;
        set => SetProperty(ref _backPassportPhotoPath, value);
    }
    private string _selfiePhotoPath;
    public string SelfiePhotoPath
    {
        get => _selfiePhotoPath;
        set => SetProperty(ref _selfiePhotoPath, value);
    }

    internal void OnSliderLoanSumValueChanged(ValueChangedEventArgs args)
    {
        LoanSum = (decimal)args.NewValue;
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

                using (Stream sourceStream = await photo.OpenReadAsync())
                {
                    using (FileStream localFileStream = File.OpenWrite(localFilePath))
                    {
                        await sourceStream.CopyToAsync(localFileStream);
                    }
                }

                try
                {
                    using (FileStream fileStream = File.OpenRead(localFilePath))
                    {
                        CurrentOnlineLoan.FrontPassportData = new byte[fileStream.Length];
                        await fileStream.ReadAsync(CurrentOnlineLoan.FrontPassportData, 0, CurrentOnlineLoan.FrontPassportData.Length);
                        CurrentOnlineLoan.FrontPassportPhotoPath = localFilePath;
                    }
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Произошла непредвиденная ошибка", ex.Message, "Ок");
                }

                FrontPassportPhotoPath = localFilePath;
                Debug.WriteLine(CurrentOnlineLoan.FrontPassportData);
            }
        }
    }

    private async Task OnOpenCameraForBackPassport()
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
            if (photo != null)
            {
                string localFilePath = System.IO.Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                using (Stream sourceStream = await photo.OpenReadAsync())
                {
                    using (FileStream localFileStream = File.OpenWrite(localFilePath))
                    {
                        await sourceStream.CopyToAsync(localFileStream);
                    }
                }

                try
                {
                    using (FileStream fileStream = File.OpenRead(localFilePath))
                    {
                        CurrentOnlineLoan.BackPassportData = new byte[fileStream.Length];
                        await fileStream.ReadAsync(CurrentOnlineLoan.BackPassportData, 0, CurrentOnlineLoan.BackPassportData.Length);
                    }
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Произошла непредвиденная ошибка", ex.Message, "Ок");

                }

                BackPassportPhotoPath = localFilePath;
                Debug.WriteLine(CurrentOnlineLoan.BackPassportData);
            }
        }
    }

    private async Task OnOpenCameraForSelfie()
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
            if (photo != null)
            {
                string localFilePath = System.IO.Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                using (Stream sourceStream = await photo.OpenReadAsync())
                {
                    using (FileStream localFileStream = File.OpenWrite(localFilePath))
                    {
                        await sourceStream.CopyToAsync(localFileStream);
                    }
                }

                try
                {
                    using (FileStream fileStream = File.OpenRead(localFilePath))
                    {
                        CurrentOnlineLoan.SelfieData = new byte[fileStream.Length];
                        await fileStream.ReadAsync(CurrentOnlineLoan.SelfieData, 0, CurrentOnlineLoan.SelfieData.Length);
                    }
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Произошла непредвиденная ошибка", ex.Message, "Ок");
                }

                SelfiePhotoPath = localFilePath;
                Debug.WriteLine(CurrentOnlineLoan.SelfieData);
            }
        }
    }

    private async Task OnSendOnlineLoanDatas()
    {
        IsLoading = true;
        await Task.Delay(500);



        CurrentOnlineLoan.ClientCode = int.Parse(_clientCode ?? "0");
        CurrentOnlineLoan.LoanAmount = LoanSum;
        CurrentOnlineLoan.LoanTerm = LoanTerm;

        if ((CurrentOnlineLoan.LoanAmount == 0) || CurrentOnlineLoan.LoanTerm == 0 || CurrentOnlineLoan.FrontPassportData == null
            || CurrentOnlineLoan.BackPassportData == null || CurrentOnlineLoan.SelfieData is null || CurrentOnlineLoan.LoanAmount == null ||
            CurrentOnlineLoan.LoanTerm == null || SelectedPaymentMethod is null || CurrentOnlineLoan.WhatsAppNumber == null ||
            CurrentOnlineLoan.CardNumber == null)
        {
            await Shell.Current.DisplayAlert("", "Заполните все поля", "Ок");
            IsLoading = false;
            return;
        }
        else if (CurrentOnlineLoan.LoanAmount < 1000 || CurrentOnlineLoan.LoanAmount > 200000)
        {
            await Shell.Current.DisplayAlert("", "Минимальная сумма кредита 1 000 сом, максимальная сумма кредита 200 000 сом", "Ок");
            IsLoading = false;
            return;
        }
        //CurrentOnlineLoan.BackPassportData = null;
        //CurrentOnlineLoan.FrontPassportData = null;
        //CurrentOnlineLoan.SelfieData = null;
        CurrentOnlineLoan.PaymentType = SelectedPaymentMethod.CardName;
        CurrentOnlineLoan.PaymentId = SelectedPaymentMethod.PaymentId;

        //var response = await ContentService.Instance(_accessToken).PostItemAsync<OnlineLoan>(CurrentOnlineLoan, "api/Online/AddOnlineLoan");
        var response = await ContentService.Instance(_accessToken).PostStreamAsync(CurrentOnlineLoan, "api/Online/AddOnlineLoan");

        if (response == 0)
        {
            await Shell.Current.DisplayAlert("", "Произошла непредвиденная ошибка, повторите еще раз или отправьте запрос позже", "Ок");
            IsLoading = false;
            return;
        }


        //Debug.WriteLine(CurrentOnlineLoan.SelfieData);
        //Debug.WriteLine(CurrentOnlineLoan.FrontPassportData);
        //Debug.WriteLine(CurrentOnlineLoan.BackPassportData);
        await Shell.Current.DisplayAlert("", "Ваша заявка отправлена на рассмотрение", "Ок");
        await App.Current.MainPage.Navigation.PopModalAsync();
    }
}
