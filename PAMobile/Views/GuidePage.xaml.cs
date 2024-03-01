namespace PAMobile.Views;

public partial class GuidePage : ContentPage
{
	public GuidePage()
	{
		InitializeComponent();


		BindingContext = new GuideViewModel();
	}
}