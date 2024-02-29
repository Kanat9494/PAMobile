namespace PAMobile.ViewModels;

internal class MainViewModel : BaseViewModel
{
    public MainViewModel()
    {
        LoanInfoDefinition = "1";
        ReferenceDefinitionCommand = new AsyncRelayCommand<string>(OnReference);
        LoansCommand = new AsyncRelayCommand(OnLoanTapped);
        DepositsCommand = new AsyncRelayCommand(OnDeposit);
        OnlineLoansCommand = new AsyncRelayCommand(OnOnlineLoans);
        GetDocumentsCommand = new AsyncRelayCommand(OnGetDocuments);
        OnlineDepositCommand = new AsyncRelayCommand(OnOnlineDeposits);
        GuideCommand = new AsyncRelayCommand(OnGuide);


    }

    public ICommand ReferenceDefinitionCommand { get; }
    public ICommand GetDocumentsCommand { get; }
    public ICommand LoansCommand { get; }
    public ICommand DepositsCommand { get; }
    public ICommand OnlineLoansCommand { get; }
    public ICommand OnlineDepositCommand { get; }
    public ICommand GuideCommand { get; }



    private string _loanInfoDefinition;
    public string LoanInfoDefinition
    {
        get => _loanInfoDefinition;
        set => SetProperty(ref _loanInfoDefinition, value); 
    }

    async Task OnLoanTapped()
    {
        //await Task.Delay(1500);
        await Shell.Current.GoToAsync("LoansPage");
    }

    async Task OnDeposit()
    {
        //await Task.Delay(1500);
        await Shell.Current.GoToAsync("DepositsPage");

    }

    async Task OnReference(string referenceDefinition)
    {
        switch (referenceDefinition)
        {
            case "1":
                //await Task.Delay(1000);
                await App.Current.MainPage.Navigation.PushModalAsync(new ChangeLoanTermsPage());
                break;
            case "2":
                //await Task.Delay(1000);

                await App.Current.MainPage.Navigation.PushModalAsync(new GetLoanInformationPage()); 
                break;
            case "3":
                //await Task.Delay(1000);

                await App.Current.MainPage.Navigation.PushModalAsync(new GetLoanTranchePage());
                break;
            case "4":

                //await Task.Delay(1000);

                await App.Current.MainPage.Navigation.PushModalAsync(new GetLoanDocumentsPage());
                break;
            case "5":
                //await Task.Delay(1000);

                await App.Current.MainPage.Navigation.PushModalAsync(new GetLoanOverPaymentPage());
                break;
            case "6":
                //await Task.Delay(1000);

                await App.Current.MainPage.Navigation.PushModalAsync(new DepositPartWithdrawalPage());
                break;
            case "7":
                //await Task.Delay(1000);

                await App.Current.MainPage.Navigation.PushModalAsync(new DepositApplicationFTPage());
                break;
            case "8":
                //await Task.Delay(1000);
                await App.Current.MainPage.Navigation.PushModalAsync(new DepositTerminationPage());
                break;
            case "9":
                //await Task.Delay(1000);
                await App.Current.MainPage.Navigation.PushModalAsync(new DepositOriginOFPage());
                break;
            default:
                break;
        }
            
        //await Shell.Current.GoToAsync("LoansPage");
    }

    async Task OnOnlineLoans()
    {
        //await Task.Delay(1500);
        await App.Current.MainPage.Navigation.PushModalAsync(new OnlineLoanPage());
    }

    async Task OnOnlineDeposits()
    {
        //await Task.Delay(1500);
        await App.Current.MainPage.Navigation.PushModalAsync(new OnlineDepositPage());
    }

    private async Task OnGetDocuments()
    {
        await Shell.Current.GoToAsync("DocumentsPage");
    }

    private async Task OnGuide()
        => await Shell.Current.GoToAsync("GuidePage");
}
