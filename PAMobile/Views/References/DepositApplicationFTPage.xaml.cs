namespace PAMobile.Views.References;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DepositApplicationFTPage : ContentPage
{
	public DepositApplicationFTPage()
	{
		InitializeComponent();

		BindingContext = new DepositApplicationFTViewModel();
	}
}