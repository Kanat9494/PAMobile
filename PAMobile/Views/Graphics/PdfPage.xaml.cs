namespace PAMobile.Views.Graphics;

public partial class PdfPage : ContentPage
{
	public PdfPage()
	{
		InitializeComponent();
        BindingContext = new PdfViewModel();
    }
}