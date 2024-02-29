namespace PAMobile.Views.References;

public partial class ChangeLoanTermsPage : ContentPage
{
	public ChangeLoanTermsPage()
	{
		InitializeComponent();

        BindingContext = new ChangeLoanTermsViewModel();
    }
}