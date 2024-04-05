namespace PAMobile.ViewModels;

internal class ChangePasswordViewModel : BaseViewModel
{
    public ChangePasswordViewModel()
    {
        Types = new ObservableCollection<string>
        {
            "Кредит",
            "Депозит"
        };

        Task.Run(async () =>
        {
            _accessToken = await SecureStorage.Default.GetAsync("UserAccessToken");
            _clientCode = "0";
        });

        GetCode = new AsyncRelayCommand(OnGetCode);
        CheckCode = new AsyncRelayCommand(OnCheckCode);
        ChangePassword = new AsyncRelayCommand(OnChangePassword);
    }

    string _accessToken;
    string _clientCode;
    int oneTimeCode;

    public ObservableCollection<string> Types { get; set; }


    public ICommand GetCode { get; }
    public ICommand CheckCode { get; }
    public ICommand ChangePassword { get; }


    private bool _showPassword;
    public bool ShowPassword
    {
        get => _showPassword;
        set => SetProperty(ref _showPassword, value);
    }
    private string _selectedType;
    public string SelectedType
    {
        get => _selectedType;
        set
        {
            IsCode = true;
            SetProperty(ref _selectedType, value);
        }
    }
    private bool _isCode;
    public bool IsCode
    {
        get => _isCode;
        set => SetProperty(ref _isCode, value);
    }
    private string _login;
    public string Login
    {
        get => _login;
        set => SetProperty(ref _login, value);
    }
    private int? _code;
    public int? Code
    {
        get => _code;
        set => SetProperty(ref _code, value);
    }
    private int? _positionalNumber;
    public int? PositionalNumber
    {
        get => _positionalNumber;
        set => SetProperty(ref _positionalNumber, value);
    }
    private bool _isNext;
    public bool IsNext
    {
        get => _isNext;
        set => SetProperty(ref _isNext, value);
    }
    private bool _isPassword;
    public bool IsPassword
    {
        get => _isPassword;
        set => SetProperty(ref _isPassword, value);
    }
    private string _password1;
    public string Password1
    {
        get => _password1;
        set => SetProperty(ref _password1, value);
    }
    private string _password2;
    public string Password2
    {
        get => _password2;
        set => SetProperty(ref _password2, value);
    }


    private async Task OnGetCode()
    {
        if (PositionalNumber == null || SelectedType == null || Login == null || Login.Length == 0 || PositionalNumber == 0 || SelectedType.Length == 0)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Нужно заполнить все поля", "Ок");
            return;
        }

        await Shell.Current.DisplayAlert("", "На ваш номер телефона будет отправлен смс для замены пароля", "Ок");
        var response = await ContentService.Instance(_accessToken).GetItemAsync2<ConfirmationCode>($"api/ILogin/GetConfirmationCode?clientITIN={Login}&" +
            $"type={SelectedType}&positionalNumber={PositionalNumber}");

        if (response.Code == null || response.Code == 0)
        {
            await Shell.Current.DisplayAlert("Ошибка", response.Message, "Ок");
            return;
        }

        await Shell.Current.DisplayAlert("", response.Message, "Ок");
        oneTimeCode = (int)response.Code;
        IsNext = true;
    }

    async Task OnCheckCode()
    {
        if (Code == null || Code == 0 || Code != oneTimeCode)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Некорректный код для замены пароля!!", "Ок");
        }
        else
        {
            IsPassword = true;
        }
    }

    private async Task OnChangePassword()
    {
        if (Password1 == null || Password2 == null || (Password1 != Password2))
        {
            await Shell.Current.DisplayAlert("Ошибка", "Пароли должны совпадать", "Ок");
            return;
        }

        var response = await ContentService.Instance(_accessToken).GetItemAsync2<string>($"api/ILogin/ChangePassword?clientITIN={Login}&" +
            $"clientCode={_clientCode}&newPassword={Password1}");
        await Shell.Current.DisplayAlert("", response, "Ок");
        await Shell.Current.GoToAsync("..");
    }
}
