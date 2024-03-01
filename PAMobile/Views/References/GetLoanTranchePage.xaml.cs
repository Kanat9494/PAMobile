namespace PAMobile.Views.References;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class GetLoanTranchePage : ContentPage
{
	public GetLoanTranchePage()
	{
		InitializeComponent();

		BindingContext = new GetLoanTrancheViewModel();
	}
}