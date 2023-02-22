using Newtonsoft.Json;

namespace CompaniesHouseParser.Common;

public class AccessorBase<TClass, TInterface>
where TClass : class, TInterface
{
    private readonly string _path;
    public AccessorBase(string path)
    {
        _path = path;
    }

    public TInterface Get()
    {
        string settings;
        using (StreamReader read = new StreamReader(_path))
        {
            settings = read.ReadToEnd();
        }

        TClass? jsonToObj = null;
        try
        {
            jsonToObj = JsonConvert.DeserializeObject<TClass>(settings);
            if (jsonToObj == null)
                throw new Exception("Sorry, but json file can not be deserialize to object");
        }
        catch (Exception ex)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine($"Message : {ex}");
        }

        return jsonToObj;
    }
}