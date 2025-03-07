namespace LookupApi.Application.Lookups.Models;

public class LookupItem
{
    public string Value { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string? AdditionalData { get; set; }

    public Dictionary<string, string>? AdditionalDataDictionary
    {
        get
        {
            if (string.IsNullOrEmpty(AdditionalData))
                return null;

            try
            {
                return System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(AdditionalData);
            }
            catch
            {
                return null;
            }
        }
    }
}
