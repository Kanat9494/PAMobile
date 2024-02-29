namespace PAMobile;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        RegisterRoutingPages();
    }

    private void RegisterRoutingPages()
    {
        Routing.RegisterRoute("PinPage", typeof(PinPage));
        Routing.RegisterRoute("SetupPinCodePage", typeof(SetupPinCodePage));
    }
}
