namespace PlanetoR.Utility;

public static class ExtractDataFromJson
{
    public static string extractString(string json, string field)
    {
        var indexOfField = json.IndexOf(field);
        string result = json.Substring(indexOfField);
        result = result.Substring(result.IndexOf(":") + 2);
        var y = result.IndexOf("\"");
        result = result.Substring(0, y);

        return result;
    }
}