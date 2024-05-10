namespace PAMobile.ViewModels;

internal class StoriesViewModel : BaseViewModel
{
    public StoriesViewModel()
    {
        Images = new ObservableCollection<string>();
        Index = 0;
        _cancelTokenSource = new CancellationTokenSource();
        _cancelToken = _cancelTokenSource.Token;
        for (int i = 1; i < 11; i++)
        {
            Images.Add($"https://picsum.photos/id/{i}/200/300");
        }
        CloseCommand = new AsyncRelayCommand(OnClose);

        StartTimer();
    }

    private CancellationTokenSource _cancelTokenSource;
    CancellationToken _cancelToken;

    public ICommand CloseCommand { get; }

    public ObservableCollection<string> Images { get; set; }

    private int _index;
    public int Index
    {
        get => _index;
        set => SetProperty(ref _index, value);
    }

    private void StartTimer()
    {
        Task changePositionTask = new Task(async () =>
        {
            if (Images.Count > 0)
            {
                while (Index < Images.Count)
                {
                    if (_cancelToken.IsCancellationRequested)
                        break;
                    if (Index < Images.Count - 1)
                    {
                        Index++;
                    }
                    else
                    {
                        App.Current.MainPage.Navigation.PopModalAsync();
                        Index = 0;
                        _cancelTokenSource.Cancel();
                        _cancelTokenSource.Dispose();
                        break;
                    }
                    await Task.Delay(5000);
                }
            }
        }, _cancelToken);

        changePositionTask.Start();
    }

    async Task OnClose()
    {
        _cancelTokenSource.Cancel();
        _cancelTokenSource.Dispose();
        await App.Current.MainPage.Navigation.PopModalAsync();
    }

    internal void CheckPosition(bool toRight)
    {
        if (Images.Count > 0)
        {
            if (Index > 0 && Index < Images.Count - 1)
            {
                if (toRight)
                {
                    Index++;
                }
                else
                {
                    Index--;
                }
            }
        }
    }
}
