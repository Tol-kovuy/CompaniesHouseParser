namespace CompaniesHouseParser.Storage
{
    public class ApplicationStorageCreatedDateCompany
    {
        private string pathToCreatedDates = @"CreatedDate.txt";

        private IList<DateTime> _dateCreation;

        public IList<DateTime> GetDates()
        {
            if (_dateCreation != null)
                return _dateCreation;

            var allCreatedDate = File.ReadAllLines(pathToCreatedDates);
            
            _dateCreation = new List<DateTime>();
            foreach (var date in allCreatedDate)
            {
                DateTime dateTime = DateTime.Parse(date);
                _dateCreation.Add(dateTime);
            }

            return _dateCreation;
        }

        public void AddRange(IList<DateTime> dates)
        {
            if (!File.Exists(pathToCreatedDates))
            {
                using (StreamWriter writer = new StreamWriter(pathToCreatedDates))
                {
                    foreach (var date in dates)
                    {
                        writer.WriteLine(date);
                    }
                }
                _dateCreation = GetDates();
            }
            else
            {
                foreach (var date in dates)
                {
                    if (!CompareCompanyIds(date))
                    {
                        using var file = File.AppendText(pathToCreatedDates);
                        file.WriteLine(date);
                        _dateCreation.Add(date);
                    }
                }
            }
        }

        private bool CompareCompanyIds(DateTime newDate)
        {
            IList<DateTime> dates = GetDates();
            foreach (var date in dates)
                if (date == newDate)
                    return true;
            return false;
        }
    }
}
