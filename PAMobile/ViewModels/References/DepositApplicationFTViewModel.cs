namespace PAMobile.ViewModels.References;

internal class DepositApplicationFTViewModel : BaseDepositApplicationViewModel
{
    public DepositApplicationFTViewModel()
    {
        NextCommand = new AsyncRelayCommand(OnNext);
        SendReferenceCommand = new AsyncRelayCommand(OnSendReference);
    }

    public ICommand NextCommand { get; }
    public ICommand SendReferenceCommand { get; }


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
        string clientFullName = Preferences.Default.Get("user_full_name", "");
        ReferenceText = $"Я {clientFullName}, являюсь вкладчиком депозита в ОАО МФК Салым Финанс, № {SelectedDeposit.DV_NOM} договора" +
            $" от {SelectedDeposit.DV_DATE1.ToString("d")}, заявляю о своем намерении получать основную сумму и начисленные проценты по депозиту";
    }

    private async Task OnSendReference()
    {
        if (string.IsNullOrWhiteSpace(PaymentAccount) || string.IsNullOrEmpty(CardNumber)
            || SelectedBICAccount == null)
        {
            await Shell.Current.DisplayAlert("", "Перед тем как продолжить, заполните все поля", "Ок");
            return;
        }

        var depositApplication = new DepositApplication
        {
            DepositPostionalNumber = SelectedDeposit.DV_POZN,
            ApplicationText = "",
            ClientCode = int.Parse(_clientCode),
            BICAccount = SelectedBICAccount.bmfo,
            PaymentAccount = this.PaymentAccount,
            CardNumber = this.CardNumber,
        };

        await ContentService.Instance(_accessToken).PostItemAsync<DepositApplication>(depositApplication, "api/Deposits/SendDepositApplicationFT");
        await Shell.Current.DisplayAlert("Заявление отправлено", "", "Ок");
        await App.Current.MainPage.Navigation.PopModalAsync();
    }
}
