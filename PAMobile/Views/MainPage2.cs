namespace PAMobile.Views;

public class MainPage2 : ContentPage
{
    public MainPage2()
    {
        var statusBarBehavior = new StatusBarBehavior()
        {
            StatusBarColor = Color.FromArgb("#ffad33"),
            StatusBarStyle = StatusBarStyle.DarkContent
        };

        Label loanInfoLbl = new Label().Text("Информация по кредитам").Font(size: 20, bold: true, family: "OpenSansRegular")
            .Margins(10, 20, 10, 0);
        loanInfoLbl.GestureRecognizers.Add(new TapGestureRecognizer()
            .Bind(TapGestureRecognizer.CommandProperty, static (MainViewModel vm) => vm.ReferenceDefinitionCommand)
            .Bind(TapGestureRecognizer.CommandParameterProperty, static (MainViewModel vm) => vm.LoanInfoDefinition));

        Behaviors.Add(statusBarBehavior);

        Content = new ScrollView
        {
            Content = new StackLayout
            {
                Background = Color.FromArgb("#f2f2f2"),
                Children =
                {
                    loanInfoLbl,
                    new Label().Text("Информация по депозитам").Font(size: 20, bold: true, family: "OpenSansRegular")
                    .Margins(10, 10, 10, 0),
                    new Expander
                    {
						//Header = new Label().Text("Заявления").Font(bold: true, size: 18),
						Header = new Border
                        {
                            StrokeThickness = 0,
                            StrokeShape = new RoundRectangle
                            {
                                CornerRadius = new CornerRadius(10),
                                HorizontalOptions = LayoutOptions.Fill
                            },

                            Content = new StackLayout
                            {
                                Orientation = StackOrientation.Horizontal,

                                Children =
                                {
                                    new Label()
                                    {
                                        VerticalOptions = LayoutOptions.Center,
                                        HorizontalOptions = LayoutOptions.Start,
                                    }.Text("Заявления").Font(size: 20, bold: true, family: "OpenSansRegular"),
                                    new Border()
                                    {
                                        HorizontalOptions = LayoutOptions.EndAndExpand,
                                        StrokeThickness = 0,
                                        StrokeShape = new RoundRectangle
                                        {
                                            CornerRadius = new CornerRadius(35),
                                        },
                                        Background = Color.FromArgb("#f2f2f2"),
                                        Content = new Image().Source("next_icon.png").Aspect(Aspect.AspectFill),
                                    }.Height(35).Width(35).Padding(10),
                                }
                            }
                        }.Padding(15).Margins(10, 20, 10, 0),

                        Content = new VerticalStackLayout
                        {
                            new Expander
                            {
                                Header = new Border
                                {
                                    StrokeThickness = 0,
                                    StrokeShape = new RoundRectangle
                                    {
                                        CornerRadius = new CornerRadius(10),
                                        HorizontalOptions = LayoutOptions.Fill
                                    },
                                    Content = new Label()
                                    {
                                        VerticalOptions = LayoutOptions.Center,
                                        HorizontalOptions = LayoutOptions.Start,
                                    }.Text("Кредит").Font(size: 20, bold: true, family: "OpenSansRegular"),
                                }.Padding(15).Margins(10, 5, 10, 10),
                                Content = new VerticalStackLayout
                                {
                                    new Label().Text("На изменение условий кредитного договора")
                                    .Font(bold: true, size: 17).Margins(10, 0, 10, 10),
                                    new Label().Text("Получение информации по кредиту")
                                    .Font(bold: true, size: 17).Margins(10, 0, 10, 10),
                                    new Label().Text("На выдачу транша")
                                    .Font(bold: true, size: 17).Margins(10, 0, 10, 10),
                                    new Label().Text("На выдачу документа по залогу")
                                    .Font(bold: true, size: 17).Margins(10, 0, 10, 10),
                                    new Label().Text("На выплату переплаты")
                                    .Font(bold: true, size: 17).Margins(10, 0, 10, 10),
                                }.Margins(10)
                            }.Margins(0, 0, 10, 0),
                            new Expander
                            {
                                Header = new Border
                                {
                                    StrokeThickness = 0,
                                    StrokeShape = new RoundRectangle
                                    {
                                        CornerRadius = new CornerRadius(10),
                                        HorizontalOptions = LayoutOptions.Fill
                                    },
                                    Content = new Label()
                                    {
                                        VerticalOptions = LayoutOptions.Center,
                                        HorizontalOptions = LayoutOptions.Start,
                                    }.Text("Депозит").Font(size: 20, bold: true, family: "OpenSansRegular"),
                                }.Padding(15).Margins(10, 5, 10, 10),
                                Content = new VerticalStackLayout
                                {
                                    new Label().Text("На выдачу части депозита")
                                    .Font(bold: true, size: 17).Margins(10, 0, 10, 10),
                                    new Label().Text("На перечисление депозита и %")
                                    .Font(bold: true, size: 17).Margins(10, 0, 10, 10),
                                    new Label().Text("На расторжение договора")
                                    .Font(bold: true, size: 17).Margins(10, 0, 10, 10),
                                    new Label().Text("О происхождении денежных средств")
                                    .Font(bold: true, size: 17).Margins(10, 0, 10, 10)
                                }.Margins(10) //VerticalStackLayout
                            }.Margins(0, 0, 10, 0),//Expander
                        }.Margins(10),//VerticalStackLayout
					},//Expander
				}
            }
        };

        BindingContext = new MainViewModel();
    }
}