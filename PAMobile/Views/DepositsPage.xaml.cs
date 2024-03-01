namespace PAMobile.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DepositsPage : ContentPage
{
	public DepositsPage()
	{
		InitializeComponent();

		BindingContext = new DepositsViewModel();
	}
}