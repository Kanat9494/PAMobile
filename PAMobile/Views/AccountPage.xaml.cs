namespace PAMobile.Views;

public partial class AccountPage : ContentPage
{
	public AccountPage()
    {
        InitializeComponent();
        GenerateUI();

        BindingContext = _viewModel = new AccountViewModel();
    }

    AccountViewModel _viewModel;

    private void GenerateUI()
    {
        App.Current.Dispatcher.Dispatch(() =>
        {
            //contentSL.Add(new Border
            //{
            //	StrokeShape = new RoundRectangle
            //	{
            //		CornerRadius = new CornerRadius(10, 10, 10, 10)
            //	},
            //	StrokeThickness = 0,
            //	Content = new StackLayout
            //	{
            //		Orientation = StackOrientation.Horizontal,
            //		Children =
            //		{
            //			new Label
            //			{
            //				VerticalOptions = LayoutOptions.Center,

            //			}.Text("Настройки").Font(size: 20, bold: true, family: "RegularFont"),

            //			new Border
            //			{
            //				HorizontalOptions = LayoutOptions.EndAndExpand,
            //				StrokeThickness = 0,
            //				BackgroundColor = Color.FromArgb("#f2f2f2"),
            //				StrokeShape = new RoundRectangle
            //				{
            //					CornerRadius = new CornerRadius(40, 40, 40, 40)
            //				},
            //				Content = new Image().Source("next_icon.png")
            //			}.Height(40).Width(40).Paddings(10, 10, 10, 10),
            //		}
            //	}
            //}.Paddings(25, 25, 25, 25).Margins(10, 20, 10, 0).Bind(Border.IsVisibleProperty, static (AccountViewModel vm) => !vm.IsBusy));

            var border = new Border
            {
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = new CornerRadius(10, 10, 10, 10)
                },
                StrokeThickness = 0,
                Content = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children =
                    {
                        new Label
                        {
                            VerticalOptions = LayoutOptions.Center,

                        }.Text("Курсы валют").Font(size: 20, bold: true, family: "RegularFont"),

                        new Border
                        {
                            HorizontalOptions = LayoutOptions.EndAndExpand,
                            StrokeThickness = 0,
                            BackgroundColor = Color.FromArgb("#f2f2f2"),
                            StrokeShape = new RoundRectangle
                            {
                                CornerRadius = new CornerRadius(40, 40, 40, 40)
                            },
                            Content = new Image().Source("next_icon.png")
                        }.Height(40).Width(40).Paddings(10, 10, 10, 10),
                    }
                }
            }.Paddings(25, 25, 25, 25).Margins(10, 20, 10, 0).Bind(Border.IsVisibleProperty, static (AccountViewModel vm) => !vm.IsBusy);
            var gestureRecognizer = new TapGestureRecognizer();
            gestureRecognizer.SetBinding(TapGestureRecognizer.CommandProperty, new Binding("ExchangeRatesCommand"));
            border.GestureRecognizers.Add(gestureRecognizer);
            contentSL.Add(border);

            //var exitBorder2 = new Border
            //{
            //    StrokeShape = new RoundRectangle
            //    {
            //        CornerRadius = new CornerRadius(10, 10, 10, 10)
            //    },
            //    StrokeThickness = 0,
            //    Content = new StackLayout
            //    {
            //        Orientation = StackOrientation.Horizontal,
            //        Children =
            //        {
            //            new Label
            //            {
            //                VerticalOptions = LayoutOptions.Center,

            //            }.Text("Справочник").Font(size: 20, bold: true, family: "RegularFont"),

            //            new Border
            //            {
            //                HorizontalOptions = LayoutOptions.EndAndExpand,
            //                StrokeThickness = 0,
            //                BackgroundColor = Color.FromArgb("#f2f2f2"),
            //                StrokeShape = new RoundRectangle
            //                {
            //                    CornerRadius = new CornerRadius(40, 40, 40, 40)
            //                },
            //                Content = new Image().Source("next_icon.png")
            //            }.Height(40).Width(40).Paddings(10, 10, 10, 10),
            //        }
            //    }
            //}.Paddings(25, 25, 25, 25).Margins(10, 20, 10, 0).Bind(Border.IsVisibleProperty, static (AccountViewModel vm) => !vm.IsBusy);

            //var gestureRecognizer3 = new TapGestureRecognizer();
            //gestureRecognizer3.SetBinding(TapGestureRecognizer.CommandProperty, new Binding("GuideCommand", source: _viewModel));
            //exitBorder2.GestureRecognizers.Add(gestureRecognizer3);

            //contentSL.Add(exitBorder2);

            var exitBorder = new Border
            {
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = new CornerRadius(10, 10, 10, 10)
                },
                StrokeThickness = 0,
                Content = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children =
                    {
                        new Label
                        {
                            VerticalOptions = LayoutOptions.Center,

                        }.Text("Выйти").Font(size: 20, bold: true, family: "RegularFont"),

                        new Border
                        {
                            HorizontalOptions = LayoutOptions.EndAndExpand,
                            StrokeThickness = 0,
                            BackgroundColor = Color.FromArgb("#f2f2f2"),
                            StrokeShape = new RoundRectangle
                            {
                                CornerRadius = new CornerRadius(40, 40, 40, 40)
                            },
                            Content = new Image().Source("next_icon.png")
                        }.Height(40).Width(40).Paddings(10, 10, 10, 10),
                    }
                }
            }.Paddings(25, 25, 25, 25).Margins(10, 20, 10, 0).Bind(Border.IsVisibleProperty, static (AccountViewModel vm) => !vm.IsBusy);

            var gestureRecognizer2 = new TapGestureRecognizer();
            gestureRecognizer2.SetBinding(TapGestureRecognizer.CommandProperty, new Binding("ExitCommand", source: _viewModel));
            exitBorder.GestureRecognizers.Add(gestureRecognizer2);

            contentSL.Add(exitBorder);






            contentSL.Add(new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.FromArgb("#999999"),
                Text = $"Версия {AppInfo.VersionString}"
            }.Margins(0, 20, 0, 0).Font(family: "RegularFont", size: 16, bold: true)); //Версия до 100
        });
    }
}