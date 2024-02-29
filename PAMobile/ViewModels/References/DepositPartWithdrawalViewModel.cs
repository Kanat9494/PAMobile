namespace PAMobile.ViewModels.References;

internal class DepositPartWithdrawalViewModel : BaseDepositApplicationViewModel
{
    public DepositPartWithdrawalViewModel()
    {
        NextCommand = new AsyncRelayCommand(OnNext);
        SendReferenceCommand = new AsyncRelayCommand(OnSendReference);

    }

    public ICommand NextCommand { get; }
    public ICommand SendReferenceCommand { get; }


    private string _depositSum;
    public string DepositSum
    {
        get => _depositSum;
        set => SetProperty(ref _depositSum, value);
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
        IsNext = true;
        if (!IsBICAccountsLoaded)
        {
            await LoadBICAccounts();
            return;
        }
        IsReferenceText = true;
        ReferenceText = $"Прошу Вас, выдать часть денежных средств на сумму {DepositSum} по договору срочного вклада № {SelectedDeposit.DV_NOM}" +
            $" от {SelectedDeposit.DV_DATE1.ToString("d")} года.";
    }

    private async Task OnSendReference()
    {
        if (string.IsNullOrWhiteSpace(DepositSum) || string.IsNullOrWhiteSpace(PaymentAccount) || string.IsNullOrEmpty(CardNumber)
            || SelectedBICAccount == null)
        {
            await Shell.Current.DisplayAlert("", "Перед тем как продолжить, заполните все поля", "Ок");
            return;
        }

        var depositApplication = new DepositApplication
        {
            DepositPostionalNumber = SelectedDeposit.DV_POZN,
            ApplicationText = DepositSum,
            ClientCode = int.Parse(_clientCode),
            BICAccount = SelectedBICAccount.bmfo,
            PaymentAccount = this.PaymentAccount,
            CardNumber = this.CardNumber,
        };

        await ContentService.Instance(_accessToken).PostItemAsync<DepositApplication>(depositApplication, "api/Deposits/SendDepositApplication");
        await Shell.Current.DisplayAlert("Заявление отправлено", "", "Ок");
        await App.Current.MainPage.Navigation.PopModalAsync();
    }
}
