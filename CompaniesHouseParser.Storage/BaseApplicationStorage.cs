using System.Globalization;

namespace CompaniesHouseParser.Storage
{
    public class BaseApplicationStorage<T>
    {
        private string pathTo;
        public BaseApplicationStorage(string pathTo)
        {
            this.pathTo = pathTo;
        }

        private IList<T> _companyValues;

        public IList<T> GetValuesExistCompanies()
        {
            if (_companyValues != null)
                return _companyValues;

            var allCompanyValue = File.ReadAllLines(pathTo);
            IList<T> castingValue = allCompanyValue.Cast<T>().ToList();
            //DateTime dateTime = DateTime.ParseExact(castingValue, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            _companyValues = new List<T>();
            foreach (var value in castingValue)
            {
                _companyValues.Add(value);
            }

            return _companyValues;
        }

        public void AddRange(IList<T> values)
        {
            if (!File.Exists(pathTo))
            {
                using (StreamWriter writer = new StreamWriter(pathTo))
                {
                    foreach (var value in values)
                    {
                        writer.WriteLine(value);
                    }
                }
                _companyValues = GetValuesExistCompanies();
            }
            else
            {
                foreach (var value in values)
                {
                    if (!CompareCompanyValue(value))
                    {
                        using var file = File.AppendText(pathTo);
                        file.WriteLine(value);
                        _companyValues.Add(value);
                    }
                }
            }
        }

        private bool CompareCompanyValue(T value)
        {
            T[] existCompanyIds = GetValuesExistCompanies().ToArray();
            foreach (var existId in existCompanyIds)
                if (existId.Equals(value))
                    return true;
            return false;
        }
    }
}
