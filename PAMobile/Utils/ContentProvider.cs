namespace PAMobile.Utils;

internal class ContentProvider
{
    internal ContentProvider()
    {

    }

    internal static List<PaymentMethod> GeneratePaymentMethods()
    {
        var listOfPaymentMethods = new List<PaymentMethod>
        {
            new PaymentMethod { PaymentId = 174, CardName = "Элкарт"},
            new PaymentMethod { PaymentId = 21, CardName = "Balance"},
            new PaymentMethod { PaymentId = 288, CardName = "О! Деньги"}
        };

        return listOfPaymentMethods;
    }
}
