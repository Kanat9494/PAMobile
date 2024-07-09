namespace PAMobile.Views.Graphics;

public partial class LoanGraphicsPage : ContentPage
{
	public LoanGraphicsPage()
	{
		InitializeComponent();
		BindingContext = new LoanGraphicsViewModel();
	}
}