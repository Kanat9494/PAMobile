namespace PAMobile.ViewModels.References;

internal class GetLoanDocumentsViewModel : BaseLoanApplicationViewModel
{
    public GetLoanDocumentsViewModel()
    {
        CustomSendReferenceCommand = new AsyncRelayCommand(OnSendReferenceCustom);
        NextTrancheCommand = new RelayCommand(OnNextTranche);
    }

    public ICommand CustomSendReferenceCommand { get; }
    public ICommand NextTrancheCommand { get; }


    private string _customReferenceText;
    public string CustomReferenceText
    {
        get => _customReferenceText;
        set => SetProperty(ref _customReferenceText, value);
    }

    private async Task OnSendReferenceCustom()
    {
        var clientCode = await SecureStorage.Default.GetAsync("ClientCode");
        var loanApplicationRequest = new LoanApplicationRequest
        {
            LoanPositionalNumber = SelectedLoan.DG_POZN,
            ApplicationText = "Правоустановливающих документов",
            ClientCode = int.Parse(clientCode ?? "0"),
            ClientPhoneNumber = ""
        };
        await ContentService.Instance(_accessToken).PostItemAsync<LoanApplicationRequest>(loanApplicationRequest, "api/Loans/SendLoanApplicationForTranche");
        await Shell.Current.DisplayAlert("Заявление отправлено", "", "Ок");
        await App.Current.MainPage.Navigation.PopModalAsync();
    }

    private void OnNextTranche()
    {
        IsNext = true;
        ReferenceText = $"Прошу предоставить технический паспорт и правоустанавливающие документы на недвижимость, находяющуюся у вас в залоге" +
            $" согласно Кредитному договору № {SelectedLoan.DG_NOM} от {Convert.ToDateTime(SelectedLoan.DG_DATE1).ToString("d")} года.";
    }
}
