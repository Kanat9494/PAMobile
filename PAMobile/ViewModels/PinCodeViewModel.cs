#if IOS
using Foundation;
using LocalAuthentication;
#endif

namespace PAMobile.ViewModels;

internal delegate void PinNumberAddedEventHandler(object sender, int index);
internal delegate void PinNumberDeletedEventHandler(object sender, int index);
internal class PinCodeViewModel : BaseViewModel
{
    public PinCodeViewModel()
    {
        PinCode = new ObservableCollection<string>();

        PinCodeColors = new ObservableCollection<Color>
        {
            Colors.Transparent,
            Colors.Transparent,
            Colors.Transparent,
            Colors.Transparent,
        };

        DialCommand = new AsyncRelayCommand<string>(OnDial);
        UseFingerPrintCommand = new AsyncRelayCommand(OnExit);
        ClearPinCodeCommand = new RelayCommand(OnClearPinCode);
        UseFaceIdCommand = new AsyncRelayCommand(OnUseFaceId);
    }


    internal event PinNumberAddedEventHandler PinNumberAddedEvent;
    internal event PinNumberDeletedEventHandler PinNumberDeletedEvent;
    public ObservableCollection<Color> PinCodeColors { get; set; }

    public ObservableCollection<string> PinCode { get; set; }
    public StringBuilder pinCode = new StringBuilder();


    public ICommand DialCommand { get; }
    public ICommand ClearPinCodeCommand { get; }
    public ICommand UseFingerPrintCommand { get; }
    public ICommand UseFaceIdCommand { get; }

    private async Task OnDial(string number)
    {
        if (PinCode.Count < 4)
        {
            PinCode.Add(number);
            pinCode.Append(number);
            var count = PinCode.Count;
            NotifyPinNumberAddedEvent(count);
        }

        if (PinCode.Count == 4)
        {
            NotifyPinNumberAddedEvent(4);
            IsBusy = true;
            await Task.Delay(500);
            var correctPin = await SecureStorage.Default.GetAsync("pinCode");
            if (correctPin == pinCode.ToString())
            {
                var userName = await SecureStorage.Default.GetAsync("UserName");
                var password = await SecureStorage.Default.GetAsync("UserPassword");
                try
                {
                    var response = await LoginService.Instance().AuthenticateUser(userName: userName, password: password);

                    if (response.StatusCode == 200)
                    {
                        await SecureStorage.Default.SetAsync("UserAccessToken", response.AccessToken);
                        await SecureStorage.Default.SetAsync("ClientCode", response.KlKod.ToString());
                        await SecureStorage.Default.SetAsync("UserName", response.KlLogin);
                        await SecureStorage.Default.SetAsync("Fio", response.Fio);
                        Preferences.Default.Set("clientPhoneNumber", response.KlPhone);
                        Preferences.Default.Set("user_full_name", response.Fio);
                        await SecureStorage.Default.SetAsync("UserPassword", password);

                        await Shell.Current.GoToAsync("//HomePage");

                    }
                }
                catch (Exception ex)
                {

                }
            }

            IsBusy = false;
            return;
        }

    }

    internal void NotifyPinNumberAddedEvent(int index)
    {
        PinNumberAddedEvent?.Invoke(null, index);
    }
    internal void NotifyPinNumberDeleted(int index)
    {
        PinNumberDeletedEvent?.Invoke(null, index);
    }

    internal async Task OnUseFingerPrint()
    {
        IsBusy = true;
        await Task.Delay(500);
        
    }

    void OnClearPinCode()
    {
        if (PinCode.Count > 0)
        {
            pinCode.Clear();
            PinCode.RemoveAt(PinCode.Count - 1);
            for (int i = 0; i < PinCode.Count; i++)
            {
                pinCode.Append(PinCode[i]);
            }

            NotifyPinNumberDeleted(PinCode.Count + 1);
        }
    }

    private async Task OnExit()
    {
        var answer = await Shell.Current.DisplayAlert("Подтвердите", "Вы действительно хотите выйти?",
                        "Да", "Нет");

        if (answer)
        {
            SecureStorage.Default.RemoveAll();
            Preferences.Default.Clear();

            await Shell.Current.GoToAsync("//MainPage");
        }
        
    }

    public async Task OnUseFaceId()
    {

        IsBusy = true;

        try
        {
#if IOS
            var context = new LAContext();
            NSError authError;
            if (context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out authError)) 
            {
                var isUsingBiometrics = Preferences.Default.Get("isUsingBiometrics", false);
                bool answer = false;
                if (!isUsingBiometrics)
                {
                    answer = await Shell.Current.DisplayAlert("Биометрика", "Вы хотите установить вход с помощью Face Id по умолчанию?",
                        "Да", "Нет");
                }

                if (answer) 
                {
                    Preferences.Default.Set("isUsingBiometrics", true);
                    var result = await context.EvaluatePolicyAsync(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, "Войти с помощью Face Id");
                    
                    if (result.Item1) 
                    {
                        var password = await SecureStorage.Default.GetAsync("UserPassword");
                        var userName = await SecureStorage.Default.GetAsync("UserName");

                        var response = await LoginService.Instance().AuthenticateUser(userName: userName, password: password);

                        if (response.StatusCode == 200)
                        {
                            await SecureStorage.Default.SetAsync("UserAccessToken", response.AccessToken);
                            await SecureStorage.Default.SetAsync("ClientCode", response!.KlKod!.ToString()!);
                            await SecureStorage.Default.SetAsync("UserName", response.KlLogin!);
                            Preferences.Default.Set("user_full_name", response!.Fio);
                            await Shell.Current.GoToAsync("//HomePage");
                            IsBusy = false;
                        }
                        else
                        {
                            await Shell.Current.DisplayAlert("Не удалось войти в систему",
                                $"{response.ResponseMessage}", "Ок");
                            IsBusy = false;
                        }

                    }
                    IsBusy = false;
                }
                else
                {
                    var result = await context.EvaluatePolicyAsync(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, "Войти с помощью Face Id");
                    
                    if (result.Item1) 
                    {
                        var password = await SecureStorage.Default.GetAsync("UserPassword");
                        var userName = await SecureStorage.Default.GetAsync("UserName");

                        var response = await LoginService.Instance().AuthenticateUser(userName: userName, password: password);

                        if (response.StatusCode == 200)
                        {
                            await SecureStorage.Default.SetAsync("UserAccessToken", response.AccessToken);
                            await SecureStorage.Default.SetAsync("ClientCode", response.KlKod.ToString());
                            await SecureStorage.Default.SetAsync("UserName", response.KlLogin);
                            Preferences.Default.Set("user_full_name", response.Fio);
                            await Shell.Current.GoToAsync("//HomePage");
                            IsBusy = false;
                        }
                        else
                        {
                            await Shell.Current.DisplayAlert("Не удалось войти в систему",
                                $"{response.ResponseMessage}", "Ок");
                            IsBusy = false;
                        }

                    }
                    IsBusy = false;
                }
            }
#else
            await Shell.Current.DisplayAlert("Ошибка", "Face Id недоступен", "Ок");
#endif
            IsBusy = false;
        }
        catch (Exception ex)
        {
            IsBusy = false;
            await Shell.Current.DisplayAlert("Ошибка", "Face Id недоступен", "Ок");
        }
    }
}
