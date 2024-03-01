namespace PAMobile.Views.References;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ChangeLoanTermsPage : ContentPage
{
	public ChangeLoanTermsPage()
	{
		InitializeComponent();

		BindingContext = new ChangeLoanTermsViewModel();
	}
}