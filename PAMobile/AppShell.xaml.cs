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
        Routing.RegisterRoute("FingerPrintConfirmPage", typeof(FingerPrintConfirmPage));
        Routing.RegisterRoute(nameof(LoanDetailsPage), typeof(LoanDetailsPage));
        Routing.RegisterRoute("LoansPage", typeof(LoansPage));
        Routing.RegisterRoute("DepositsPage", typeof(DepositsPage));

        Routing.RegisterRoute(nameof(LoanGraphicPage), typeof(LoanGraphicPage));
        Routing.RegisterRoute(nameof(DepositDetailsPage), typeof(DepositDetailsPage));
        Routing.RegisterRoute(nameof(DepositPartWithdrawalPage), typeof(DepositPartWithdrawalPage));
        Routing.RegisterRoute("TechnicalWorksPage", typeof(TechnicalWorksPage));
        Routing.RegisterRoute("LoanDebtPage", typeof(LoanDebtPage));
        Routing.RegisterRoute("LoanExtractPage", typeof(LoanExtractPage));
        Routing.RegisterRoute("DepositBalancePage", typeof(DepositBalancePage));
        Routing.RegisterRoute("DepositStatementPage", typeof(DepositStatementPage));
        Routing.RegisterRoute("ExchangeRatesPage", typeof(ExchangeRatesPage));
        Routing.RegisterRoute("ChangePasswordPage", typeof(ChangePasswordPage));
        Routing.RegisterRoute("DepositTerminationPage", typeof(DepositTerminationPage));
        Routing.RegisterRoute("DepositOriginOFPage", typeof(DepositOriginOFPage));
        Routing.RegisterRoute("DocumentsPage", typeof(DocumentsPage));
        Routing.RegisterRoute("RegisteringPage", typeof(RegisteringPage));
        Routing.RegisterRoute("GuidePage", typeof(GuidePage));
        Routing.RegisterRoute("PinPage", typeof(PinPage));
        Routing.RegisterRoute("SetupPinCodePage", typeof(SetupPinCodePage));
        Routing.RegisterRoute("StoriesPage", typeof(StoriesPage));
        Routing.RegisterRoute(nameof(LoanRepaymentChoosePage), typeof(LoanRepaymentChoosePage));
        Routing.RegisterRoute(nameof(DepositReplenishmentChoosePage), typeof(DepositReplenishmentChoosePage));
        Routing.RegisterRoute(nameof(LoanGraphicsPage), typeof(LoanGraphicsPage));
        Routing.RegisterRoute(nameof(PdfPage), typeof(PdfPage));
    }
}
