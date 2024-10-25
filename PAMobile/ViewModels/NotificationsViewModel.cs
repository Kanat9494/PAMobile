namespace PAMobile.ViewModels;

internal class NotificationsViewModel : BaseViewModel
{
    public NotificationsViewModel()
    {
        ClientNotifications = new ObservableCollection<Notification>();
        Task.Run(async () =>
        {
            IsBusy = true;
            _accessToken = await SecureStorage.Default.GetAsync("UserAccessToken");
            await GetNotifications();
            IsBusy = false;
        });
    }

    string _accessToken;

    public ObservableCollection<Notification> ClientNotifications { get; set; }

    private async Task GetNotifications()
    {
        var clientITIN = await SecureStorage.Default.GetAsync("UserName");

        if (!string.IsNullOrWhiteSpace(clientITIN))
        {
            var response = await ContentService.Instance(_accessToken).GetItemsAsync<Notification>($"" +
                $"api/Notifications/GetNotifications?clientITIN={clientITIN}");

            if (response != null)
            {
                foreach (var item in response)
                {
                    ClientNotifications.Add(item);
                }
            }
        }
    }
}
