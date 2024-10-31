namespace PAMobile.Views.DigitalDocuments;

public partial class LoanDigitalDocsPage : ContentPage
{
	public LoanDigitalDocsPage()
	{
		InitializeComponent();

		BindingContext = new LoanDigitalDocsViewModel();
	}
}