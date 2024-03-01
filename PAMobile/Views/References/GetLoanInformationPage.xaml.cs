namespace PAMobile.Views.References;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class GetLoanInformationPage : ContentPage
{
	public GetLoanInformationPage()
	{
		InitializeComponent();

		BindingContext = new GetLoanInformationViewModel();
	}
}