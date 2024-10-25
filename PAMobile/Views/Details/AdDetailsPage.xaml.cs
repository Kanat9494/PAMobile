namespace PAMobile.Views.Details;

public partial class AdDetailsPage : ContentPage
{
	public AdDetailsPage()
	{
		InitializeComponent();

		BindingContext = new AdDetailsViewModel();
	}
}