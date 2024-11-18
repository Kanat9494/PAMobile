namespace PAMobile.ViewModels;

internal class GuideViewModel : BaseViewModel
{
    public GuideViewModel()
    {
        IsLoanTutor = false;
        IsDepositTutor = false;
        Task.Run(async () =>
        {
            //await Task.Delay(1000);
            _accesToken = await SecureStorage.Default.GetAsync("UserAccessToken");
            _userName = await SecureStorage.Default.GetAsync("UserName");

            await InitializeTutors();
        });
    }

    string _accesToken;
    string _userName;

    private Tutor _currentTutor;
    public Tutor CurrentTutor
    {
        get => _currentTutor;
        set => SetProperty(ref _currentTutor, value);
    }
    private bool _isLoanTutor;
    public bool IsLoanTutor
    {
        get => _isLoanTutor;
        set => SetProperty(ref _isLoanTutor, value);
    }
    private bool _isLoanPodr;
    public bool IsLoanPodr
    {
        get => _isLoanPodr;
        set => SetProperty(ref _isLoanPodr, value);
    }
    private bool _isDepositTutor;
    public bool IsDepositTutor
    {
        get => _isDepositTutor;
        set => SetProperty(ref _isDepositTutor, value);
    }
    private bool _isDepositPodr;
    public bool IsDepositPodr
    {
        get => _isDepositPodr;
        set => SetProperty(ref _isDepositPodr, value);
    }
    private bool _isDepositName;
    public bool IsDepositName
    {
        get => _isDepositName;
        set => SetProperty(ref _isDepositName, value);
    }
    private bool _isLoanName;
    public bool IsLoanName
    {
        get => _isLoanName;
        set => SetProperty(ref _isLoanName, value);
    }

    private async Task InitializeTutors()
    {
        CurrentTutor = await ContentService.Instance(_accesToken).GetItemAsync2<Tutor>($"api/ILogin/GetTutors?clientITIN={_userName}");

        if (CurrentTutor.Fio1 != null && CurrentTutor.Fio2 != null)
            IsLoanTutor = true;
        if (CurrentTutor.Fio3 != null && CurrentTutor.Fio4 != null)
            IsDepositTutor = true;
        if (CurrentTutor.DepositDepartment != null)
        {
            IsDepositPodr = true;
        }
        if (CurrentTutor.LoanDepartment != null)
        {
            IsLoanPodr = true;
        }

        if (IsDepositPodr == true || IsDepositTutor == true)
        {
            IsDepositName = true;
        }
        if (IsLoanPodr == true || IsLoanTutor == true)
        {
            IsLoanName = true;
        }
    }
}
