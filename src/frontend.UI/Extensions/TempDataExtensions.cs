using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json;

namespace frontend.UI.Extensions
{
    public static class TempDataExtensions
    {

        public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonSerializer.Serialize(value);
        }

        public static T? Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            tempData.TryGetValue(key, out var value);
            return value == null ? null : JsonSerializer.Deserialize<T>((string)value);
        }


    }
}
