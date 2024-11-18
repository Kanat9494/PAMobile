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
        Routing.RegisterRoute(nameof(ChangeLoanTermsPage), typeof(ChangeLoanTermsPage));
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
        Routing.RegisterRoute(nameof(NotificationsPage), typeof(NotificationsPage));
        Routing.RegisterRoute(nameof(AdDetailsPage), typeof(AdDetailsPage));
        Routing.RegisterRoute(nameof(DeclarationPage), typeof(DeclarationPage));
        Routing.RegisterRoute(nameof(GetLoanInformationPage), typeof(GetLoanInformationPage));
        Routing.RegisterRoute(nameof(GetLoanTranchePage), typeof(GetLoanTranchePage));
        Routing.RegisterRoute(nameof(GetLoanDocumentsPage), typeof(GetLoanDocumentsPage));
        Routing.RegisterRoute(nameof(GetLoanOverPaymentPage), typeof(GetLoanOverPaymentPage));
        Routing.RegisterRoute(nameof(DepositPartWithdrawalPage), typeof(DepositPartWithdrawalPage));
        Routing.RegisterRoute(nameof(DepositApplicationFTPage), typeof(DepositApplicationFTPage));
        Routing.RegisterRoute(nameof(DepositTerminationPage), typeof(DepositTerminationPage));
        Routing.RegisterRoute(nameof(DepositOriginOFPage), typeof(DepositOriginOFPage));
        Routing.RegisterRoute(nameof(LoanDigitalDocsPage), typeof(LoanDigitalDocsPage));
        Routing.RegisterRoute(nameof(DepositElectronicDocumentsPage), typeof(DepositElectronicDocumentsPage));
        Routing.RegisterRoute(nameof(MortgageDetailsPage), typeof(MortgageDetailsPage));
        Routing.RegisterRoute(nameof(EducationDetailsPage), typeof(EducationDetailsPage));
        Routing.RegisterRoute(nameof(ComfortDetailsPage), typeof(ComfortDetailsPage));
        Routing.RegisterRoute(nameof(BusinessDetailsPage), typeof(BusinessDetailsPage));
        Routing.RegisterRoute(nameof(DepositProductDetailsPage), typeof(DepositProductDetailsPage));
    }
}
