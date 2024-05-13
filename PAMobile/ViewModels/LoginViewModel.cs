namespace PAMobile.ViewModels;

internal class LoginViewModel : BaseViewModel
{
    public LoginViewModel()
    {
        IsBusy = false;
        IsTechnicalWorks = false;
        LoginCommand = new AsyncRelayCommand(OnLogin);
        ConfirmFingerPrintCommand = new AsyncRelayCommand(OnConfirmFingerPrint);
        ChangePassword = new AsyncRelayCommand(OnChangePassword);
        RegisterCommand = new AsyncRelayCommand(OnRegister);
        InstructionCommand = new AsyncRelayCommand(OnInstruction);


        //Password = "@bishkek2023";

        Task.Run(async () =>
        {
            UserName = await SecureStorage.Default.GetAsync("UserName");
            //Password = await SecureStorage.Default.GetAsync("UserPassword");
        });
    }

    public ICommand LoginCommand { get; }
    private ICommand _fingerPrintCommand;
    public ICommand FingerPrintCommand => _fingerPrintCommand ??= new AsyncRelayCommand(ConfirmFingerPrint);
    public ICommand ConfirmFingerPrintCommand { get; }
    public ICommand ChangePassword { get; }
    public ICommand RegisterCommand { get; }
    public ICommand InstructionCommand { get; }


    private bool _isTechnicalWorks;
    public bool IsTechnicalWorks
    {
        get => _isTechnicalWorks;
        set => SetProperty(ref _isTechnicalWorks, value);
    }
    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }
    private bool _showPassword;
    public bool ShowPassword
    {
        get => _showPassword;
        set => SetProperty(ref _showPassword, value);
    }
    private string _userName;
    public string UserName
    {
        get => _userName;
        set => SetProperty(ref _userName, value);
    }
    private string _password;
    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }
    private bool _isUsingBiometrics;
    public bool IsUsingBiometrics
    {
        get => _isUsingBiometrics;
        set => SetProperty(ref _isUsingBiometrics, value);
    }

    private async Task OnLogin()
    {
        IsBusy = true;
        await Task.Delay(500);
        if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
        {
            await Shell.Current.DisplayAlert("Пустые значения", "Пожалуйста введите логин и пароль для входа!", "Ок");
            IsBusy = false;
        }
        else
        {
            try
            {
                var response = await LoginService.Instance().AuthenticateUser(userName: UserName, password: Password);

                if (response.StatusCode == 200)
                {
                    await SecureStorage.Default.SetAsync("UserAccessToken", response.AccessToken);
                    await SecureStorage.Default.SetAsync("ClientCode", response.KlKod.ToString());
                    await SecureStorage.Default.SetAsync("UserName", response.KlLogin);
                    await SecureStorage.Default.SetAsync("Fio", response.Fio);
                    Preferences.Default.Set("clientPhoneNumber", response.KlPhone);
                    Preferences.Default.Set("user_full_name", response.Fio);
                    await SecureStorage.Default.SetAsync("UserPassword", Password);

                    IsUsingBiometrics = Preferences.Default.Get("isUsingBiometrics", false);
                    if (IsUsingBiometrics)
                        await SecureStorage.Default.SetAsync("UserPassword", Password);
                    await Shell.Current.GoToAsync("SetupPinCodePage");
                    //await Shell.Current.GoToAsync("//HomePage");
                    IsBusy = false;
                }
                else
                {
                    await Shell.Current.DisplayAlert("Не удалось войти",
                        $"{response.ResponseMessage}", "Ок");
                    IsBusy = false;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Непредвиденная ошибка", ex.Message, "Ок");
            }
        }
    }

    async Task OnConfirmFingerPrint()
    {
        IsBusy = true;
        await Task.Delay(500);
        if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
        {
            await Shell.Current.DisplayAlert("Пустые значения", "Пожалуйста введите логин и пароль для входа!", "Ок");
            IsBusy = false;
        }
        else
        {
            var response = await LoginService.Instance().AuthenticateUser(userName: UserName, password: Password);

            if (response.StatusCode == 200)
            {
                await SecureStorage.Default.SetAsync("UserAccessToken", response.AccessToken);
                await SecureStorage.Default.SetAsync("ClientCode", response.KlKod.ToString());
                await SecureStorage.Default.SetAsync("UserName", response.KlLogin);
                await SecureStorage.Default.SetAsync("UserPassword", Password);
                Preferences.Default.Set("user_full_name", response.Fio);


                Preferences.Default.Set("isUsingBiometrics", true);
                await Shell.Current.DisplayAlert("Данные биометрики", "Биометрика теперь настроена, вы можете использовать биометрику " +
                    "для входа", "Ок");

                await Shell.Current.GoToAsync("..");
                IsBusy = false;
            }
            else
            {
                await Shell.Current.DisplayAlert("Не удалось войти в систему",
                    $"{response.ResponseMessage}", "Ок");
                IsBusy = false;
            }
        }
    }

    async Task ConfirmFingerPrint()
    {
        IsUsingBiometrics = Preferences.Default.Get("isUsingBiometrics", false);

        //await Shell.Current.GoToAsync("FingerPrintConfirmPage");
        if (IsUsingBiometrics)
            await FingerPrintLoginAsync();
        else
            await Shell.Current.GoToAsync("FingerPrintConfirmPage");
    }
    internal async Task FingerPrintLoginAsync()
    {
        IsBusy = true;
        await Task.Delay(500);
        
        IsBusy = false;
    }

    private async Task OnChangePassword()
    {
        await Shell.Current.GoToAsync("ChangePasswordPage");
    }

    async Task OnRegister()
        => await Shell.Current.GoToAsync("RegisteringPage");

    private async Task OnInstruction()
    {
        try
        {
            Uri uri = new Uri("https://ib.salymfinance.kg/docs/instruction.pdf");
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
        catch
        {

        }
    }
}
