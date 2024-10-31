namespace PAMobile.Views;

public partial class DeclarationPage : ContentPage
{
	public DeclarationPage()
	{
		InitializeComponent();
        BindingContext = new DeclarationViewModel();
    }
}