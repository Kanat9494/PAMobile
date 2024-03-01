namespace PAMobile.Views;

public partial class RegisteringPage : ContentPage
{
	public RegisteringPage()
	{
		InitializeComponent();

        BindingContext = _viewModel = new RegisteringViewModel();

    }

    RegisteringViewModel _viewModel;


    private void OnShowPassword(object sender, TappedEventArgs args)
    {
        if (!_viewModel.ShowPassword)
        {
            codeEntry.IsPassword = false;
            passwordEntry1.IsPassword = false;
            passwordEntry2.IsPassword = false;
            _viewModel.ShowPassword = !_viewModel.ShowPassword;
        }
        else
        {
            codeEntry.IsPassword = true;
            passwordEntry1.IsPassword = true;
            passwordEntry2.IsPassword = true;
            _viewModel.ShowPassword = !_viewModel.ShowPassword;
        }
    }
}