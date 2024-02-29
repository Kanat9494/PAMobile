namespace PAMobile.ViewModels.References;

internal class DepositOriginOFViewModel : BaseDepositApplicationViewModel
{
    public DepositOriginOFViewModel()
    {
        NextCommand = new AsyncRelayCommand(OnNext);
        SendReferenceCommand = new AsyncRelayCommand(OnSendReference);

        OriginsOfFunds = new ObservableCollection<string>
        {
            "от продажи недвижимости",
            "от продажи автомашины",
            "от продажи техники",
            "от наследства",
            "от доходов по деятельности",
            "другое"
        };
    }


    public ICommand NextCommand { get; }
    public ICommand SendReferenceCommand { get; }

    public ObservableCollection<string> OriginsOfFunds { get; set; }


    private string _depositSum;
    public string DepositSum
    {
        get => _depositSum;
        set => SetProperty(ref _depositSum, value);
    }
    private string _selectedOriginOfFunds;
    public string SelectedOriginOfFunds
    {
        get => _selectedOriginOfFunds;
        set
        {
            if (value == "другое")
            {
                IsOtherOrigin = true;
                SetProperty(ref _selectedOriginOfFunds, value);
                OriginOfFunds = "";
            }
            else
            {
                SetProperty(ref _selectedOriginOfFunds, value);
                OriginOfFunds = value;
            }

        }
    }
    private string _originOfFunds;
    public string OriginOfFunds
    {
        get => _originOfFunds;
        set => SetProperty(ref _originOfFunds, value);
    }
    private bool _isOtherOrigin;
    public bool IsOtherOrigin
    {
        get => _isOtherOrigin;
        set => SetProperty(ref _isOtherOrigin, value);
    }


    private async Task OnNext()
    {
        await Task.Delay(1000);
        if (SelectedDeposit == null)
        {
            IsNext = false;
            await Shell.Current.DisplayAlert("Не выбран депозит", "Выберите депозит, прежде чем продолжить дальше", "Ок");
            return;
        }
        if (!IsNext)
        {
            IsNext = true;
            return;
        }
        if (OriginOfFunds == null || OriginOfFunds.Length == 0)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Выберите происхождение денежных средств", "Ок");
            return;
        }
        //if (!IsBICAccountsLoaded)
        //{
        //    await LoadBICAccounts();
        //    return;
        //}
        IsReferenceText = true;
        var fio = Preferences.Default.Get("user_full_name", "");

        ReferenceText = $"Я {fio}, настоящим подтверждаю собственность денежных средств в сумме {DepositSum}, " +
            $" которые вношу в ОАО МФК Салым Финанс на депозитный счет. \nДенежные средства являются собственными сбережениями, " +
            $"накполены: {OriginOfFunds}";
    }

    private async Task OnSendReference()
    {

        await ContentService.Instance(_accessToken).GetItemAsync2<string>($"api/Deposits/SendOriginOfFunds?depositPN={SelectedDeposit.DV_POZN}&" +
            $"text=Информация о происхождении денежных средств&originOfFunds={OriginOfFunds}");
        await Shell.Current.DisplayAlert("Заявление отправлено", "", "Ок");
        await App.Current.MainPage.Navigation.PopModalAsync();
    }
}
