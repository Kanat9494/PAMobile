namespace PAMobile.Views;

public class TechnicalWorksPage : ContentPage
{
	public TechnicalWorksPage()
	{
		Shell.SetNavBarIsVisible(this, false);
		Content = new VerticalStackLayout
		{
			Children = {
				new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "������ �������� �� ��������"
				}.Font(size: 18, bold: true).Margins(10, 10, 10, 10),

				new Label
				{
                    HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center
                }.Text("��������� �������, �� �������� ����������� ������ � ����� ��������� ���������������� ����� ��������. ����������" +
				" �� ��������� � �������� ���������� �� ��������� ����������. ������ ����� �������� � ��������� �����").Margins(10, 10, 10, 10),
			}
		};
	}

    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}