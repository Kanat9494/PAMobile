namespace PAMobile.ViewModels;

internal class SetupPinCodeViewModel : BaseViewModel
{
    public SetupPinCodeViewModel()
    {
        NextCommand = new AsyncRelayCommand(OnNext);
    }

    public ICommand NextCommand { get; }

    private string? _pin1;
    public string? Pin1
    {
        get => _pin1;
        set => SetProperty(ref _pin1, value);
    }
    private string? _pin2;
    public string? Pin2
    {
        get => _pin2;
        set => SetProperty(ref _pin2, value);
    }

    private async Task OnNext()
    {
        if (string.IsNullOrEmpty(Pin1) || string.IsNullOrEmpty(Pin2) || Pin1.Length > 4 || Pin2.Length > 4)
        {
            return;
        }

        if (Pin1 != Pin2)
            return;

        await SecureStorage.Default.SetAsync("pinCodeState", "1");
        await SecureStorage.Default.SetAsync("pinCode", Pin1);
        await Shell.Current.GoToAsync("PinPage");
    }
}
