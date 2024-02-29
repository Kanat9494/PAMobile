namespace PAMobile.ViewModels.References;

internal class GetLoanTrancheViewModel : BaseLoanApplicationViewModel
{
    public GetLoanTrancheViewModel()
    {
        NextTrancheCommand = new AsyncRelayCommand(OnNextTranche);
        CustomSendReferenceCommand = new AsyncRelayCommand(OnSendReferenceCustom);
    }

    public ICommand NextTrancheCommand { get; }
    public ICommand CustomSendReferenceCommand { get; }

    private string _loanTrancheSum;
    public string LoanTrancheSum
    {
        get => _loanTrancheSum;
        set => SetProperty(ref _loanTrancheSum, value);
    }

    private async Task OnNextTranche()
    {
        if (string.IsNullOrWhiteSpace(LoanTrancheSum))
        {
            IsNext = false;
            await Shell.Current.DisplayAlert("Сумма не введена", "Сумма транша не может быть пустой", "Ок");
            return;
        }
        IsNext = true;
        ReferenceText = $"В соответствии с Кредитным договором № {SelectedLoan.DG_NOM} от {Convert.ToDateTime(SelectedLoan.DG_DATE1).ToString("d")} года, прошу выдать часть кредита" +
            $" в размере {LoanTrancheSum}.";
    }

    private async Task OnSendReferenceCustom()
    {
        var clientCode = await SecureStorage.Default.GetAsync("ClientCode");
        var loanApplicationRequest = new LoanApplicationRequest
        {
            LoanPositionalNumber = SelectedLoan.DG_POZN,
            ApplicationText = $"Сумма транша {LoanTrancheSum}",
            ClientCode = int.Parse(clientCode ?? "0"),
            ClientPhoneNumber = ""
        };
        await ContentService.Instance(_accessToken).PostItemAsync<LoanApplicationRequest>(loanApplicationRequest, "api/Loans/SendLoanApplicationForTranche");
        await Shell.Current.DisplayAlert("Заявление отправлено", "", "Ок");
        await App.Current.MainPage.Navigation.PopModalAsync();
    }
}
