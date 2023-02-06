using Newtonsoft.Json;

namespace CompaniesHouseParser
{
    public class AccessorBase<TClass, TInterface>
        // TODO: tell me class, new,  TClass : TInterface
        where TClass : class, TInterface
    {
        private string _path;
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // todo: jsonToObj can be null?
            return jsonToObj;
        }
    }
}
