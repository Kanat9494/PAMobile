namespace PAMobile.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class FingerPrintConfirmPage : ContentPage
{
    public FingerPrintConfirmPage()
    {
        InitializeComponent();

        BindingContext = new LoginViewModel();
    }
}