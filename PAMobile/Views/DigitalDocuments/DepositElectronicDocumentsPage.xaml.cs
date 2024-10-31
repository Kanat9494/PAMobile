namespace PAMobile.Views.DigitalDocuments;

public partial class DepositElectronicDocumentsPage : ContentPage
{
	public DepositElectronicDocumentsPage()
	{
		InitializeComponent();

		BindingContext = new DepositElectronicDocViewModel();

    }
}