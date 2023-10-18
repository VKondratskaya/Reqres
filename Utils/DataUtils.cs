using Newtonsoft.Json.Linq;
public class DataUtils
{
    public static JObject ReadJsonFromFile(string filePath)
    {
        string jsonText = File.ReadAllText(filePath);
        return JObject.Parse(jsonText);
    }

    public static string GetTestDataFilePath()
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        return Path.Combine(currentDirectory, "Resources", "testdata.json");
    }
}
