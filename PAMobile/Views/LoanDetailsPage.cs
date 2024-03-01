namespace PAMobile.Views;

public class LoanDetailsPage : ContentPage
{
	public LoanDetailsPage()
	{
        Shell.SetTabBarIsVisible(this, false);
		Shell.SetNavBarIsVisible(this, false);
        var image = new Image
        {
            HeightRequest = 30,
            WidthRequest = 30,
            Source = "white_back_icon.png"
        }.Start().Margins(5, 20, 0, 20);

        var tapGesture = new TapGestureRecognizer().Bind(TapGestureRecognizer.CommandProperty, static (LoanDetailsViewModel vm) => 
        vm.BackCommand);
        image.GestureRecognizers.Add(tapGesture);

        TapGestureRecognizer gestureRecognizer = new TapGestureRecognizer().Bind(TapGestureRecognizer.CommandProperty, static (LoanDetailsViewModel vm) => 
            vm.OpenMBankCommand);

        var openMBankBorder = new Border
        {
            Background = Colors.White,
            Stroke = Colors.Transparent,
            StrokeShape = new RoundRectangle
            {
                CornerRadius = 10
            },

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                            Children =
                {
                    new ActivityIndicator
                                {
                                    Color = Color.FromArgb("00e600"),
                                }.Height(50).Width(50).Bind(ActivityIndicator.IsRunningProperty, static (LoanDetailsViewModel vm) => vm.IsLoadingSum)
                                .Bind(ActivityIndicator.IsVisibleProperty, static (LoanDetailsViewModel vm) => vm.IsLoadingSum),

                            new Label
                                {
                                    Text = "Оплатить через MBank",
                                    TextColor = Colors.Black,
                                    HorizontalOptions = LayoutOptions.Center,
                                    VerticalOptions = LayoutOptions.Center,
                                }.Bind(Label.IsVisibleProperty, static (LoanDetailsViewModel vm) => !vm.IsLoadingSum).Font(size: 16, bold: true),
                }
                        }
        }.Height(50).Margins(10, 10, 10, 10);
        openMBankBorder.GestureRecognizers.Add(gestureRecognizer);
        var extractBorder = new Border
        {
            Background = Colors.White,
            Stroke = Colors.Transparent,
            StrokeShape = new RoundRectangle
            {
                CornerRadius = 10
            },

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    new ActivityIndicator
                                {
                                    Color = Color.FromArgb("00e600"),
                                }.Height(50).Width(50).Bind(ActivityIndicator.IsRunningProperty, static (LoanDetailsViewModel vm) => vm.IsLoadingExtract)
                                .Bind(ActivityIndicator.IsVisibleProperty, static (LoanDetailsViewModel vm) => vm.IsLoadingExtract),

                            new Label
                                {
                                    Text = "Выписка",
                                    TextColor = Colors.Black,
                                    HorizontalOptions = LayoutOptions.Center,
                                    VerticalOptions = LayoutOptions.Center,
                                }.Bind(Label.IsVisibleProperty, static (LoanDetailsViewModel vm) => !vm.IsLoadingExtract).Font(size: 16, bold: true),
                }
            }
        }.Height(50).Margins(10, 10, 10, 10);

        TapGestureRecognizer extractRecognizer = new TapGestureRecognizer().Bind(TapGestureRecognizer.CommandProperty, static (LoanDetailsViewModel vm) =>
            vm.GetExtractCommand);
        extractBorder.GestureRecognizers.Add(extractRecognizer);
        var graphicBorder = new Border
        {
            Background = Colors.White,
            Stroke = Colors.Transparent,
            StrokeShape = new RoundRectangle
            {
                CornerRadius = 10
            },

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    new ActivityIndicator
                                {
                                    Color = Color.FromArgb("00e600"),
                                }.Height(50).Width(50).Bind(ActivityIndicator.IsRunningProperty, static (LoanDetailsViewModel vm) => vm.IsLoadingGraphic)
                                .Bind(ActivityIndicator.IsVisibleProperty, static (LoanDetailsViewModel vm) => vm.IsLoadingGraphic),

                            new Label
                                {
                                    Text = "График погашения",
                                    TextColor = Colors.Black,
                                    HorizontalOptions = LayoutOptions.Center,
                                    VerticalOptions = LayoutOptions.Center,
                                }.Bind(Label.IsVisibleProperty, static (LoanDetailsViewModel vm) => !vm.IsLoadingGraphic).Font(size: 16, bold: true),
                }
            }
        }.Height(50).Margins(10, 10, 10, 10);
        
        TapGestureRecognizer graphicRecognizer = new TapGestureRecognizer().Bind(TapGestureRecognizer.CommandProperty, static (LoanDetailsViewModel vm) =>
            vm.GetLoanGraphicCommand);

        graphicBorder.GestureRecognizers.Add(graphicRecognizer);

        Content = new ScrollView
        {
            VerticalScrollBarVisibility = ScrollBarVisibility.Never,
            Content = new StackLayout
            {
                BackgroundColor = Color.FromArgb("#ff0000"),
                Children =
                {
                    image,
                    new Border
                    {
                        BackgroundColor = Colors.White,
                        StrokeShape = new RoundRectangle
                        {
                            CornerRadius = new CornerRadius(10)
                        },
                        StrokeThickness = 0,
                        VerticalOptions = LayoutOptions.Fill,
                        Content = new StackLayout
                        {
                            new Label(){TextColor = Colors.Black}.Text("Информация по кредиту").Font(size: 17, bold: true, family: "OpenSansRegular")
                                    .Margins(5, 25, 10, 10).Start(),
                            new Label().Text("Код погашения").Font(size: 12).TextColor(Color.FromArgb("#212121")).
                            Margins(5, 15, 10, 5),
                            new Label().Font(size: 15, bold: true, family: "OpenSansRegular").TextColor(Colors.Black).Margins(5, 0, 10, 0).Bind(Label.TextProperty,
                            static (LoanDetailsViewModel vm) => vm.LoanPositionalNumber),
                            new BoxView{Color = Color.FromArgb("#999999"), HeightRequest = 1, HorizontalOptions = LayoutOptions.Fill}.Margins(5, 5, 5, 0),
                            new Label().TextColor(Color.FromArgb("#212121")).Font(size: 12).Margins(5, 8, 10, 5).Text("Сумма выданного кредита"),
                            new Label().TextColor(Colors.Black).Font(size: 15, bold: true, family: "OpenSansRegular").Margins(5, 0, 10, 0)
                            .Bind(Label.TextProperty, static (LoanDetailsViewModel vm) => vm.LoanSum, stringFormat: "{0:N2}"),
                            new BoxView{Color = Color.FromArgb("#999999"), HeightRequest = 1, HorizontalOptions = LayoutOptions.Fill}.Margins(5, 5, 5, 0),
                            new Label().Text("Валюта по кредиту").Font(size: 12).TextColor(Color.FromArgb("#212121"))
                            .Margins(5, 8, 10, 5),
                            new Label().TextColor(Colors.Black).Font(size: 15, bold: true, family: "OpenSansRegular").Margins(5, 0, 10, 0)
                            .Bind(Label.TextProperty, static (LoanDetailsViewModel vm) => vm.LoanCurrency),
                            new BoxView{Color = Color.FromArgb("#999999"), HeightRequest = 1, HorizontalOptions = LayoutOptions.Fill}.Margins(5, 5, 5, 0),
                            new Label().Text("Процентная ставка (за год)").Font(size: 12).TextColor(Color.FromArgb("#212121")).Margins(5, 8, 10, 5),
                            new Label().TextColor(Colors.Black).Font(size: 15, bold: true, family: "OpenSansRegular").Margins(5, 0, 10, 0)
                            .Bind(Label.TextProperty, static (LoanDetailsViewModel vm) => vm.LoanPercent),
                            new BoxView{Color = Color.FromArgb("#999999"), HeightRequest = 1, HorizontalOptions = LayoutOptions.Fill}.Margins(5, 5, 5, 0),
                            new Label().Text("Срок по кредиту").Font(size: 12).TextColor(Color.FromArgb("#212121")).Margins(5, 8, 10, 5),
                            new Label().TextColor(Colors.Black).Font(size: 15, bold: true, family: "OpenSansRegular").Margins(5, 0, 10, 0)
                            .Bind(Label.TextProperty, "LoanTerm", stringFormat: "{0} месяцев"),
                            new BoxView{Color = Color.FromArgb("#999999"), HeightRequest = 1, HorizontalOptions = LayoutOptions.Fill}.Margins(5, 5, 5, 0),

                            new Label().Text("Дата выдачи кредита").Font(size: 12).TextColor(Color.FromArgb("#212121")).Margins(5, 8, 10, 5),
                            new Label().TextColor(Colors.Black).Font(size: 15, bold: true, family: "OpenSansRegular").Margins(5, 0, 10, 0)
                            .Bind(Label.TextProperty, static (LoanDetailsViewModel vm) => vm.LoanStartDate),
                            new BoxView{Color = Color.FromArgb("#999999"), HeightRequest = 1, HorizontalOptions = LayoutOptions.Fill}.Margins(5, 5, 5, 0),

                            new Label().Text("Дата окончания кредита").Font(size: 12).TextColor(Color.FromArgb("#212121")).Margins(5, 8, 10, 5),
                            new Label().TextColor(Colors.Black).Font(size: 15, bold: true, family: "OpenSansRegular").Margins(5, 0, 10, 0)
                            .Bind(Label.TextProperty, static (LoanDetailsViewModel vm) => vm.LoanEndDate),
                            new BoxView{Color = Color.FromArgb("#999999"), HeightRequest = 1, HorizontalOptions = LayoutOptions.Fill}.Margins(5, 5, 5, 0),

                        }
                    }.Margins(14, 10, 14, 10).Paddings(10),

                    extractBorder,

                    new Button
                    {
                        BackgroundColor = Colors.White,
                        TextColor = Colors.Black,
                    }.Text("Остаток кредита").Margins(14, 10, 14, 10).Bind(Button.CommandProperty, static (LoanDetailsViewModel vm) =>
                    vm.GetLoanDebtCommand).Font(size: 16, bold: true),

                    graphicBorder,

                    
                    openMBankBorder,

                }
            }//StackLayout
		};//ScrollView


        LocalNotificationCenter.Current.NotificationActionTapped +=
            Current_NotificationActionTapped;

        BindingContext = new LoanDetailsViewModel();
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