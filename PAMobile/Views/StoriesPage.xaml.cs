namespace PAMobile.Views;

public partial class StoriesPage : ContentPage
{
	public StoriesPage(int storyId)
	{
		InitializeComponent();

		RunStoriesIndicator();

        _cancelTokenSource = new CancellationTokenSource();
        _cancelToken = _cancelTokenSource.Token;
        _tapped = false;
        _duration = 5000;

        BindingContext = _viewModel = new StoriesViewModel(storyId);

        //_viewModel.ResetIndicatorEvent += OnResetIndicator;
    }

	StoriesViewModel _viewModel;
    private CancellationTokenSource _cancelTokenSource;
    CancellationToken _cancelToken;
    Task runStoriesTask;
    bool _tapped;
    uint _duration;

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
		Point? tappedPoint = e.GetPosition(null);
		double centerX = cv.Width / 2;

		if (tappedPoint?.X < centerX)
		{
           

            _viewModel.CheckPosition(false);
            storiesIndicator.Progress = 0.2;
            await storiesIndicator.ProgressTo(1, _duration, Easing.Linear);
        }
        else
        {
            _tapped = true;
            var hasNext = _viewModel.CheckPosition(true);
            if (hasNext)
            {
                storiesIndicator.Progress = 0.2;
                await storiesIndicator.ProgressTo(1, _duration, Easing.Linear);
                _tapped = false;
            }
            else
            {

                if (!_cancelToken.IsCancellationRequested)
                {
                    storiesIndicator.CancelAnimations();

                    _cancelTokenSource.Cancel();
                    _cancelTokenSource.Dispose();
                    await Navigation.PopModalAsync();
                    _viewModel.Index = 0;

                }

                _tapped = false;

            }
        }
    }

	private void RunStoriesIndicator()
	{
		storiesIndicator.Progress = 0.2;
		runStoriesTask = Task.Run(async () =>
		{
			await storiesIndicator.ProgressTo(1, _duration, Easing.Linear);

            while (_viewModel.Index < _viewModel.Images.Count)
            {
                if (_cancelToken.IsCancellationRequested)
                    break;
                if (_viewModel.Index < _viewModel.Images.Count - 1)
                {
                    _viewModel.Index++;
                    storiesIndicator.Progress = 0.2;
                    await storiesIndicator.ProgressTo(1, _duration, Easing.Linear);
                    
                    //RaiseResetEventAsync();
                }
                else
                {
                    if (_tapped)
                        await storiesIndicator.ProgressTo(1, _duration, Easing.Linear);

                    //await Navigation.PopModalAsync();

                    if (!_cancelToken.IsCancellationRequested)
                    {
                        _cancelTokenSource.Cancel();
                        _cancelTokenSource.Dispose();
                    }
                    _viewModel.Index = 0;

                    break;
                }
            }
        }, _cancelToken);
    }


	private void OnResetIndicator(EventArgs e)
	{
		RunStoriesIndicator();
	}

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
    }

    private async void BackCommand(object sender, TappedEventArgs e)
    {
        storiesIndicator.CancelAnimations();
        if (!_cancelToken.IsCancellationRequested)
        {
            _cancelTokenSource.Cancel();
            _cancelTokenSource.Dispose();
        }

        await Navigation.PopModalAsync();
        _viewModel.Index = 0;
    }
}