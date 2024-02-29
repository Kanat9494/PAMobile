namespace PAMobile.ViewModels.References;

internal class GetLoanInformationViewModel : BaseLoanApplicationViewModel
{
    public GetLoanInformationViewModel()
    {
        LoanTerms = new List<string>()
        {
            "По залоговому обеспечению",
            "По остатку задолженности",
        };

        SelectLoanTermCommand = new AsyncRelayCommand<string>(OnLoanTermSelected);
        SendReferenceCommandCustom = new AsyncRelayCommand(OnSendReferenceCustom);

    }

    public List<string> LoanTerms { get; set; }

    public ICommand SelectLoanTermCommand { get; }
    public ICommand SendReferenceCommandCustom { get; }


    private async Task OnLoanTermSelected(string referenceText)
    {
        IsReference = true;
        if (referenceText.Equals(LoanTerms[0]))
            ReferenceText = $"Прошу временно предоставить технический паспорт и правоустанавливающие документы на недвижимость," +
                $" находящуюся у вас в залоге согласно Кредитному договору №{SelectedLoan.DG_NOM} от {Convert.ToDateTime(SelectedLoan.DG_DATE1).ToString("d")} года.";
        else
            ReferenceText = $"Прошу предоставить информацию, по кредитному договору №{SelectedLoan.DG_NOM} от {Convert.ToDateTime(SelectedLoan.DG_DATE1).ToString("d")} года, " +
                $"касательно: \n{referenceText}";
        _textToSend = referenceText;
        //await Shell.Current.DisplayAlert("Jr", referenceText, "Ок");
        //App.Current.MainPage.ShowPopup(new ReferencePopupPage());
    }

    private async Task OnSendReferenceCustom()
    {
        var clientPhoneNumber = Preferences.Default.Get("clientPhoneNumber", "0");
        var clientCode = await SecureStorage.Default.GetAsync("ClientCode");

        var loanApplicationRequest = new LoanApplicationRequest
        {
            LoanPositionalNumber = SelectedLoan.DG_POZN,
            ApplicationText = _textToSend,
            ClientCode = int.Parse(clientCode ?? "0"),
            ClientPhoneNumber = clientPhoneNumber
        };
        await ContentService.Instance(_accessToken).PostItemAsync<LoanApplicationRequest>(loanApplicationRequest, "api/Loans/SendLoanApplicationForTranche");
        await Shell.Current.DisplayAlert("Заявление отправлено", "", "Ок");
        await App.Current.MainPage.Navigation.PopModalAsync();
    }
}
