﻿namespace PAMobile.ViewModels;

internal class MainViewModel : BaseViewModel
{
    public MainViewModel()
    {
        LoanInfoDefinition = "1";
        ReferenceDefinitionCommand = new AsyncRelayCommand<string>(OnReference);
        LoansCommand = new AsyncRelayCommand(OnLoanTapped);
        DepositsCommand = new AsyncRelayCommand(OnDeposit);
        //OnlineLoansCommand = new AsyncRelayCommand(OnOnlineLoans);
        GetDocumentsCommand = new AsyncRelayCommand(OnGetDocuments);
        NotificationsCommand = new AsyncRelayCommand(OnNotifications);
        //OnlineDepositCommand = new AsyncRelayCommand(OnOnlineDeposits);
        GuideCommand = new AsyncRelayCommand(OnGuide);
        StoriesCommand = new AsyncRelayCommand<int>(OnStories);
        Stories = new ObservableCollection<Story>();
        AdCommand = new AsyncRelayCommand<int>(OnAd);
        DeclarationCommand = new AsyncRelayCommand(OnDeclaration);

        Task.Run(async () =>
        {
            _accessToken = await SecureStorage.Default.GetAsync("UserAccessToken");
            await GetStories();
        });
        Ads = new ObservableCollection<Ad>();
    }

    string _accessToken;

    public ICommand ReferenceDefinitionCommand { get; }
    public ICommand GetDocumentsCommand { get; }
    public ICommand NotificationsCommand { get; }
    public ICommand LoansCommand { get; }
    public ICommand DepositsCommand { get; }
    public ICommand OnlineLoansCommand { get; }
    public ICommand OnlineDepositCommand { get; }
    public ICommand GuideCommand { get; }
    public ICommand StoriesCommand { get; }
    public ICommand AdCommand { get; }
    public ICommand DeclarationCommand { get; }

    public ObservableCollection<Story> Stories { get; set; }
    public ObservableCollection<Ad> Ads { get; set; }



    private string _loanInfoDefinition;
    public string LoanInfoDefinition
    {
        get => _loanInfoDefinition;
        set => SetProperty(ref _loanInfoDefinition, value); 
    }
    private bool _storiesExist;
    public bool StoriesExist
    {
        get => _storiesExist;
        set => SetProperty(ref _storiesExist, value);
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
                await App.Current.MainPage.Navigation.PushAsync(new ChangeLoanTermsPage());
                break;
            case "2":
                //await Task.Delay(1000);

                await App.Current.MainPage.Navigation.PushAsync(new GetLoanInformationPage()); 
                break;
            case "3":
                //await Task.Delay(1000);

                await App.Current.MainPage.Navigation.PushAsync(new GetLoanTranchePage());
                break;
            case "4":

                //await Task.Delay(1000);

                await App.Current.MainPage.Navigation.PushAsync(new GetLoanDocumentsPage());
                break;
            case "5":
                //await Task.Delay(1000);

                await App.Current.MainPage.Navigation.PushAsync(new GetLoanOverPaymentPage());
                break;
            case "6":
                //await Task.Delay(1000);

                await App.Current.MainPage.Navigation.PushAsync(new DepositPartWithdrawalPage());
                break;
            case "7":
                //await Task.Delay(1000);

                await App.Current.MainPage.Navigation.PushAsync(new DepositApplicationFTPage());
                break;
            case "8":
                //await Task.Delay(1000);
                await App.Current.MainPage.Navigation.PushAsync(new DepositTerminationPage());
                break;
            case "9":
                //await Task.Delay(1000);
                await App.Current.MainPage.Navigation.PushAsync(new DepositOriginOFPage());
                break;
            default:
                break;
        }
            
        //await Shell.Current.GoToAsync("LoansPage");
    }

    async Task OnOnlineLoans()
    {
        //await Task.Delay(1500);
        await App.Current.MainPage.Navigation.PushAsync(new OnlineLoanPage());
    }

    async Task OnOnlineDeposits()
    {
        //await Task.Delay(1500);
        await App.Current.MainPage.Navigation.PushAsync(new OnlineDepositPage());
    }

    private async Task OnGetDocuments()
    {
        await Shell.Current.GoToAsync("DocumentsPage");
    }

    private async Task OnGuide()
        => await Shell.Current.GoToAsync("GuidePage");

    async Task OnStories(int storyId)
    {
        Preferences.Default.Set("story_id", storyId);
        await App.Current.MainPage.Navigation.PushModalAsync(new StoriesPage(storyId));
    }

    private async Task GetStories()
    {
        var response = await ContentService.Instance(_accessToken).GetItemsAsync<Story>("api/Stories/GetStories");

        if (response != null && response.Count() > 0)
        {
            StoriesExist = true;
            foreach (var item in response)
            {
                Preferences.Default.Set($"{item.StoryId}", item.DownloadLink);

                Stories.Add(item);
            }
        }

        var ads = await ContentService.Instance(_accessToken).GetItemsAsync<Ad>("api/Ads/GetAds");

        if (ads != null && ads.Count() > 0)
        {
            foreach (var item in ads)
            {
                Ads.Add(item);
            }
        }
    }

    private async Task OnNotifications()
    {
        await Shell.Current.GoToAsync(nameof(NotificationsPage));
    }

    private async Task OnAd(int id)
    {
        if (id == 1)
        {
            await Shell.Current.GoToAsync(nameof(MortgageDetailsPage));
        }
        else if (id == 3)
        {
            await Shell.Current.GoToAsync(nameof(EducationDetailsPage));
        }
        else if (id == 4)
        {
            await Shell.Current.GoToAsync(nameof(ComfortDetailsPage));
        }
        else if (id == 8)
        {
            await Shell.Current.GoToAsync(nameof(BusinessDetailsPage));
        }
        else if (id == 10)
        {
            await Shell.Current.GoToAsync(nameof(DepositProductDetailsPage));
        }
        //await Shell.Current.GoToAsync($"{nameof(AdDetailsPage)}?{nameof(AdDetailsViewModel.AdId)}={id}");
    }

    async Task OnDeclaration()
        => await Shell.Current.GoToAsync($"{nameof(DeclarationPage)}?{nameof(DeclarationViewModel.Type)}=3");
}
