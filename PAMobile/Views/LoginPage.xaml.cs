namespace PAMobile.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();

        //Task.Run(async () =>
        //{
        //	var isTechnicalWorks = await CheckTechnicalWorks();

        //	if (isTechnicalWorks)
        //		await Shell.Current.GoToAsync("TechnicalWorksPage");
        //}).Wait();
        passwordEntry.IsPassword = true;

        BindingContext = _viewModel = new LoginViewModel();
        _viewModel.ShowPassword = false;
    }

    private LoginViewModel _viewModel;



    protected override async void OnNavigatedTo(NavigatedToEventArgs e)
    {
        base.OnNavigatedTo(e);

        _viewModel.IsTechnicalWorks = true;

        //var isTechnicalWorks = await CheckTechnicalWorks();
        var isTechnicalWorks = false;

        if (!isTechnicalWorks)
        {
            _viewModel.IsTechnicalWorks = true;
            string isPinCodeSetted = "";
            Task.Run(async () =>
            {
                isPinCodeSetted = await SecureStorage.Default.GetAsync("pinCodeState") ?? "0";
            }).Wait();

            if (isPinCodeSetted.Equals("1"))
            {
                await Shell.Current.GoToAsync("PinPage");
            }
            //var isUsingBiometrics = Preferences.Default.Get("isUsingBiometrics", false);
            //if (isUsingBiometrics)
            //    await _viewModel.FingerPrintLoginAsync();
        }
        //else
        //{
        //    await Shell.Current.GoToAsync("TechnicalWorksPage");
        //}

    }


    private async Task<bool> CheckTechnicalWorks()
	{
		using (var httpClient = new HttpClient())
		{
            httpClient.BaseAddress = new Uri(PAConstants.SERVER_ROOT_URL);
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

			try
			{
                var response = await httpClient.GetStringAsync(httpClient.BaseAddress + "api/TechnicalWorks/CheckTechnicalWorks");
                bool result = JsonConvert.DeserializeObject<bool>(response);

                return result;
            }
			catch
			{
				return true;
			}
        }
	}


    private void OnShowPassword(object sender, TappedEventArgs args)
    {
        if (!_viewModel.ShowPassword)
        {
            passwordEntry.IsPassword = false;
            _viewModel.ShowPassword = !_viewModel.ShowPassword;
        }
        else
        {
            passwordEntry.IsPassword = true;
            _viewModel.ShowPassword = !_viewModel.ShowPassword;
        }
    }
}