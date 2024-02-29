namespace PAMobile.ViewModels.References;

internal class ChangeLoanTermsViewModel : BaseLoanApplicationViewModel
{
    public ChangeLoanTermsViewModel()
    {
        LoanTerms = new List<string>()
        {
            "дата погашения",
            "график погашения без продления срока действия",
            "процентная ставка",
            "продлить срок кредита",
            "изменить валюту кредита",
            "провести реструктуризацию"
        };
        SelectLoanTermCommand = new AsyncRelayCommand<string>(OnLoanTermSelected);
    }

    public List<string> LoanTerms { get; set; }

    public ICommand SelectLoanTermCommand { get; }

    private async Task OnLoanTermSelected(string referenceText)
    {
        IsReference = true;
        ReferenceText = $"Прошу изменить условия по кредитному договору №{SelectedLoan.DG_NOM} от {Convert.ToDateTime(SelectedLoan.DG_DATE1).ToString("d")} года, а именно:\n" +
            $"{referenceText}";
        _textToSend = referenceText;
        //await Shell.Current.DisplayAlert("Jr", referenceText, "Ок");
        //App.Current.MainPage.ShowPopup(new ReferencePopupPage());
    }
}
