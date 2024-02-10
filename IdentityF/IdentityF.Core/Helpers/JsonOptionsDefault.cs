using System.Text.Json.Serialization;
using System.Text.Json;

namespace IdentityF.Core.Helpers
{
    public class JsonOptionsDefault
    {
        private static readonly JsonSerializerOptions _defaultOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public static JsonSerializerOptions Default => _defaultOptions;
    }
}
