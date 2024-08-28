namespace frontend.UI.Common
{
    public class DisplayFunctions
    {

        public static string DisplayPercentage(int value)
        {
            if (value == 0) return "-";
            else return $"{value}%";
        }

    }
}
