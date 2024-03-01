namespace PAMobile.Views.OnlineLoans;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class OnlineLoanPage : ContentPage
{
	public OnlineLoanPage()
	{
		InitializeComponent();

		BindingContext = _viewModel = new OnlineLoanViewModel();

        clientWaitFrame.BackgroundColor = Color.FromRgba(0, 0, 0, 195);
	}

	OnlineLoanViewModel _viewModel;

    void OnSliderLoanSumValueChanged(object sender, ValueChangedEventArgs args)
    {
        _viewModel.OnSliderLoanSumValueChanged(args);
    }

    void OnSliderLoanTermValueChanged(object sender, ValueChangedEventArgs args)
    {
        _viewModel.OnSliderLoanTermValueChanged(args);
    }
}