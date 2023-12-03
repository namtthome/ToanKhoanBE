namespace Pnsuite.Common
{
    public class ConvertHelper
    {
        public static int GetInt(string input)
        {
            if (int.TryParse(input, out var result))
            {
                return result;
            }
            return default;
        }
    }
}
