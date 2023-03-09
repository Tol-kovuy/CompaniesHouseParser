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
        return Deserialize();
    }

    public TClass Deserialize()
    {
        var settings = File.ReadAllText(_path);

        TClass? jsonToObj = null;
        try
        {
            jsonToObj = JsonConvert.DeserializeObject<TClass>(settings);
            if (jsonToObj == null)
            {
                throw new Exception("Sorry, but json file can not be deserialize to object");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Message : {ex}");
            throw;
        }
        return jsonToObj;
    }
}