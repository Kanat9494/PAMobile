namespace PAMobile.Views.OnlineDeposits;

public partial class OnlineDepositPage : ContentPage
{
	public OnlineDepositPage()
	{
		InitializeComponent();

        BindingContext = _viewModel = new OnlineDepositViewModel();

        clientWaitFrame.BackgroundColor = Color.FromRgba(0, 0, 0, 195);
    }

    OnlineDepositViewModel _viewModel;


    void OnSliderLoanSumValueChanged(object sender, ValueChangedEventArgs args)
    {
        _viewModel.OnSliderLoanSumValueChanged(args);
    }

    void OnSliderLoanTermValueChanged(object sender, ValueChangedEventArgs args)
    {
        _viewModel.OnSliderLoanTermValueChanged(args);
    }
}