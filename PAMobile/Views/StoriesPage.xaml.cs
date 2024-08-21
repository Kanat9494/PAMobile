namespace PAMobile.Views;

public partial class StoriesPage : ContentPage
{
    public StoriesPage(int storyId)
    {
        InitializeComponent();

        _progressTimer = Application.Current.Dispatcher.CreateTimer();
        _progressTimer.Interval = TimeSpan.FromMilliseconds(50);
        BindingContext = _viewModel = new StoriesViewModel(storyId);
    }

    StoriesViewModel _viewModel;
    IDispatcherTimer _progressTimer;
    List<ProgressBar> _progressBars = new();

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Point? tappedPoint = e.GetPosition(null);
        double centerX = cv.Width / 2;

        if (tappedPoint?.X < centerX)
        {
            _viewModel.CheckPosition(false);
            _progressBars[_viewModel.Index].Progress = 0;
            if (_viewModel.Index < _viewModel.Images.Count - 1)
            {
                for (int i = _viewModel.Index; i < _viewModel.Images.Count; i++)
                {
                    _progressBars[i].Progress = 0;
                }
            }
        }
        else
        {
            var hasNext = _viewModel.CheckPosition(true);
            if (hasNext)
            {
                _progressBars[_viewModel.Index].Progress = 0;
                if (_viewModel.Index > 0)
                {
                    for (int i = 0; i < _viewModel.Index; i++)
                    {
                        _progressBars[i].Progress = 1;
                    }
                }
            }
            else
            {
                Navigation.PopModalAsync();
            }
        }
    }

    private async void BackCommand(object sender, TappedEventArgs e)
    {
        Preferences.Default.Set("has_to_scroll", true);
        Preferences.Default.Set("story_id", _viewModel.Id);
        await Navigation.PopModalAsync();
        if (_progressTimer.IsRunning)
        {
            _progressTimer.Stop();
        }
    }

    private void DrawStoriesIndicators(int count)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            for (int i = 0; i < count; i++)
            {
                storiesGrid.ColumnDefinitions.Add(new ColumnDefinition
                {
                });
            }


            for (int i = 0; i < count; i++)
            { 
                _progressBars.Add(new ProgressBar
                {
                    Progress = 0,
                    ProgressColor = Color.FromArgb("#919191"),
                    VerticalOptions = LayoutOptions.Center,
                    ScaleY = 0.6
                });
            }

            for (int i = 0; i < _progressBars.Count; i++)
            {
                storiesGrid.Add(_progressBars[i], i, 0);
            }

            
        });
        RunStoriesIndicator();
        _progressTimer.Start();

    }

    void RunStoriesIndicator()
    {


        _progressTimer.Tick += (s, e) =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (_viewModel.Index < _viewModel.StoriesCount)
                {
                    if (_progressBars[_viewModel.Index].Progress < 1)
                    {
                        _progressBars[_viewModel.Index].Progress += 0.01;
                    }
                    else
                    {
                        if (_viewModel.Index < _viewModel.StoriesCount - 1)
                        {
                            _viewModel.Index++;
                            _progressBars[_viewModel.Index].Progress = 0;
                        }
                        else
                        {
                            Navigation.PopModalAsync();
                            if (_progressTimer.IsRunning)
                                _progressTimer.Stop();
                            return;
                        }
                    }
                }
                else
                {
                    Navigation.PopModalAsync();
                    if (_progressTimer.IsRunning)
                        _progressTimer.Stop();
                    return;
                }
            });
        };
    }
     
    protected override void OnAppearing()
    {
        base.OnAppearing();

        _viewModel.ImagesReceivedEvent += DrawStoriesIndicators;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        _viewModel.ImagesReceivedEvent -= DrawStoriesIndicators;
    }
}