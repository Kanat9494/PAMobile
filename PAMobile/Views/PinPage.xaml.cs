namespace PAMobile.Views;

public partial class PinPage : ContentPage
{
	public PinPage()
	{
		InitializeComponent();

		BindingContext = _viewModel = new PinCodeViewModel();
		indicatorBorder.Background = Color.FromRgba(0, 0, 0, 195);
        _viewModel.PinNumberAddedEvent += HandlePinNumberAdded;
		_viewModel.PinNumberDeletedEvent += HandlePinNumberDeleted;

    }

	PinCodeViewModel _viewModel;

	void HandlePinNumberAdded(object sender, int index)
	{
		switch (index)
		{
			case 1:
				firstNumber.BackgroundColor = Color.FromArgb("#ff0000");
				break;
			case 2:
				secondNumber.BackgroundColor = Color.FromArgb("#ff0000");
				break;
			case 3:
				thirdNumber.BackgroundColor = Color.FromArgb("#ff0000");
				break;
			case 4: 
				fourthNumber.BackgroundColor = Color.FromArgb("#ff0000");
				break;
            default:
				break;
        }
	}

	void HandlePinNumberDeleted(object sender, int index)
	{
		switch (index)
		{
			case 1:
				firstNumber.BackgroundColor = Colors.Transparent;
				break;
			case 2:
				secondNumber.BackgroundColor = Colors.Transparent;
				break;
			case 3:
				thirdNumber.BackgroundColor = Colors.Transparent;
				break;
			case 4:
				fourthNumber.BackgroundColor = Colors.Transparent;
				break;
			default:
				break;
		}
	}

    protected override bool OnBackButtonPressed()
    {
		return true;
    }

	protected override async void OnNavigatedTo(NavigatedToEventArgs e)
	{
		base.OnNavigatedTo(e);

        try
		{
            var isUsingBiometrics = Preferences.Default.Get("isUsingBiometrics", false);

            if (isUsingBiometrics)
                await _viewModel.OnUseFaceId();
        }
		catch (Exception ex)
		{

		}
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        firstNumber.BackgroundColor = Colors.Transparent;
        secondNumber.BackgroundColor = Colors.Transparent;
        thirdNumber.BackgroundColor = Colors.Transparent;
        fourthNumber.BackgroundColor = Colors.Transparent;

        _viewModel.PinCode.Clear();
        _viewModel.pinCode.Clear();
    }

    private void FaceIdTapped(object sender, TappedEventArgs e)
    {

    }
}