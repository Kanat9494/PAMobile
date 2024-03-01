namespace PAMobile.Views.References;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class GetLoanDocumentsPage : ContentPage
{
	public GetLoanDocumentsPage()
	{
		InitializeComponent();

		BindingContext = new GetLoanDocumentsViewModel();
	}
}