namespace PAMobile.Views;

public partial class StoriesPage : ContentPage
{
	public StoriesPage()
	{
		InitializeComponent();

		BindingContext = _viewModel = new StoriesViewModel();
	}

	StoriesViewModel _viewModel;

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
		Point? tappedPoint = e.GetPosition(null);
		double centerX = cv.Width / 2;

		if (tappedPoint?.X < centerX)
		{
			_viewModel.CheckPosition(false);
		}
        else
        {
			_viewModel.CheckPosition(true);
        }
    }
}