namespace PAMobile.ViewModels;

internal class StoriesViewModel : BaseViewModel
{
    public StoriesViewModel(int storyId)
    {
        Images = new ObservableCollection<string>();
        Index = 0;
        
        Task.Run(async () =>
        {
            _accessToken = await SecureStorage.Default.GetAsync("UserAccessToken");
            await GetStoryImages(storyId);
        });
        CloseCommand = new AsyncRelayCommand(OnClose);
        DownloadInstruction = new AsyncRelayCommand(OnDownload);
        _storyId = storyId;

        //StartTimer();
    }

    string _accessToken;
    int _storyId;


    public ICommand CloseCommand { get; }
    public ICommand DownloadInstruction { get; }

    public ObservableCollection<string> Images { get; set; }

    private int _index;
    public int Index
    {
        get => _index;
        set => SetProperty(ref _index, value);
    }

    //private void StartTimer()
    //{
    //    Task changePositionTask = new Task(async () =>
    //    {
    //        if (Images.Count > 0)
    //        {
    //            while (Index < Images.Count)
    //            {
    //                if (_cancelToken.IsCancellationRequested)
    //                    break;
    //                if (Index < Images.Count - 1)
    //                {
    //                    Index++;
    //                }
    //                else
    //                {
    //                    App.Current.MainPage.Navigation.PopModalAsync();
    //                    Index = 0;
    //                    _cancelTokenSource.Cancel();
    //                    _cancelTokenSource.Dispose();
    //                    break;
    //                }
    //                await Task.Delay(7000);
    //            }
    //        }
    //    }, _cancelToken);

    //    changePositionTask.Start();
    //}

    async Task OnClose()
    {
        await App.Current.MainPage.Navigation.PopModalAsync();
    }

    internal bool CheckPosition(bool toRight)
    {
        if (Images.Count > 0)
        {
            if (toRight)
            {
                if (Index < Images.Count - 1)
                {
                    Index++;
                    return true;
                }
                return false;
            }
            else
            {
                if (Index > 0 && Index < Images.Count - 1)
                {
                    if (Index > 0)
                    {
                        Index--;
                    }
                }
            }
        }
        return false;
    }

    private async Task GetStoryImages(int storyId)
    {
        var response = await ContentService.Instance(_accessToken).GetItemsAsync<string>($"api/Stories/GetStoryImages?storyType={storyId}");
        if (response != null && response.Count() > 0)
        {
            foreach (var item in response)
            {
                Images.Add(item);
            }
        }
    }

    async Task OnDownload()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var link = Preferences.Default.Get<string>($"{_storyId}", "");

            if (!string.IsNullOrEmpty(link))
            {
                try
                {
                    Uri uri = new Uri(link);
                    await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
                }
                catch
                {

                }
            }
        });
    }
}

