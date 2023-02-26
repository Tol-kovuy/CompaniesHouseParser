namespace CompaniesHouseParser.Storage;

public class ApplicationStorageCompanyIds : IApplicationStorageCompanyIds
{

    private string pathToExistingCompanyIds = @"ExistingCompanyNumbers.txt";

    private IList<string> _allIds;

    public IList<string> GetIds()
    {
        if (_allIds != null)
            return _allIds;

        var allCompanyIds = File.ReadAllLines(pathToExistingCompanyIds);

        _allIds = new List<string>();
        foreach (var id in allCompanyIds)
        {
            _allIds.Add(id);
        }

        return _allIds;
    }

    public void AddNewIds(IList<string> ids)
    {
        if (!File.Exists(pathToExistingCompanyIds))
        {
            File.AppendAllLines(pathToExistingCompanyIds, ids);
            _allIds = GetIds();
        }
        else
        {
            foreach (var id in ids)
            {
                if (!CompareCompanyIds(id))
                {
                    using var file = File.AppendText(pathToExistingCompanyIds);
                    file.WriteLine(id);
                    _allIds.Add(id);
                }
            }
        }
    }

    private bool CompareCompanyIds(string id)
    {
        string[] existCompanyIds = GetIds().ToArray();
        foreach (var existId in existCompanyIds)
            if (existId == id)
            {
                return true;
            }

        return false;
    }
}