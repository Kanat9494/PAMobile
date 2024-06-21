namespace PAMobile.Constants;

internal class PAConstants
{
    //расскомментировать для телефона на работе
    //internal const string SERVER_ROOT_URL = "http://localhost:45455";

    //работает для эмулятора
    //internal const string SERVER_ROOT_URL = "http://192.168.2.33:45457/";

    //боевой сервис
    //internal const string SERVER_ROOT_URL = "http://192.168.2.11:91/";
    internal const string SERVER_ROOT_URL = "https://mobile.salymfinance.kg/";
    internal static HttpClient MSHC = new HttpClient();

    //для дома
    //internal const string SERVER_ROOT_URL = "http://192.168.1.51:45455";

    internal const string CONFIRM_MESSAGE = "Kana-LetoCoOwner";

    internal const string REJECT_MESSAGE = "RejectingTheMessage!";

    //для работы
    internal const string FILE_BASE_PATH = "http://192.168.2.33:40";

    //для дома
    //internal const string FILE_BASE_PATH = "http://192.168.1.51:80";
}
