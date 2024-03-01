namespace PAMobile.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class LoanGraphicPage : ContentPage
{
	public LoanGraphicPage()
	{
		InitializeComponent();

		BindingContext = new LoanGraphicViewModel();
	}
}