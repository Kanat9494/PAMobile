namespace PAMobile.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class FingerPrintConfirmPage : ContentPage
{
    public FingerPrintConfirmPage(IFingerprint fingerPrint)
    {
        InitializeComponent();

        BindingContext = new LoginViewModel(fingerPrint);
    }
}