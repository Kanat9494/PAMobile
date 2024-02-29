namespace PAMobile.Views.Details;

public class DepositStatementPage : ContentPage
{
    public DepositStatementPage()
    {
        Shell.SetTabBarIsVisible(this, false);
        Shell.SetNavBarIsVisible(this, false);
        //Title = "Выписка по депозиту";

        contentSL = new StackLayout
        {
            BackgroundColor = Color.FromArgb("#f2f2f2"),
        };

        Content = new ScrollView
        {
            Content = contentSL
        };

        GenerateUIAsync();

        LocalNotificationCenter.Current.NotificationActionTapped +=
            Current_NotificationActionTapped;

        BindingContext = _viewModel = new DepositStatementViewModel();
    }

    DepositStatementViewModel _viewModel;
    StackLayout contentSL;


    private void GenerateUIAsync()
    {
        App.Current.Dispatcher.Dispatch(() =>
        {
            var principalAmountBtn = new Button
            {
                HorizontalOptions = LayoutOptions.Start,
                BackgroundColor = Color.FromArgb("#ff0000"),
            }.Text("Выписка по основной сумме").Margins(10, 10, 10, 10);
            var gesturePAB = new TapGestureRecognizer();
            gesturePAB.SetBinding(TapGestureRecognizer.CommandProperty, new Binding("PrincipalAmountCommand", source: _viewModel));
            principalAmountBtn.GestureRecognizers.Add(gesturePAB);
            contentSL.Add(principalAmountBtn);


            var interestsStatementBtn = new Button
            {
                HorizontalOptions = LayoutOptions.Start,
                BackgroundColor = Color.FromArgb("#ff0000"),
            }.Text("Выписка по процентам").Margins(10, 10, 10, 10);
            var gestureISB = new TapGestureRecognizer();
            gestureISB.SetBinding(TapGestureRecognizer.CommandProperty, new Binding("InterestsStatementCommand", source: _viewModel));
            interestsStatementBtn.GestureRecognizers.Add(gestureISB);

            contentSL.Add(interestsStatementBtn);


        });
    }


    private async void Current_NotificationActionTapped(Plugin.LocalNotification.EventArgs.NotificationActionEventArgs args)
    {
        if (args.IsTapped)
        {
            string filePath = args.Request.ReturningData;

            if (File.Exists(filePath))
            {
                try
                {
                    await Launcher.OpenAsync(new OpenFileRequest
                    {
                        File = new ReadOnlyFile(filePath)
                    });
                }
                catch
                {

                }
            }
            else
                Debug.WriteLine("Файл не найден");
        }
    }
}