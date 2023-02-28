namespace CompaniesHouseParser.Storage;

public class ApplicationStorageCompanyIds : IApplicationStorageCompanyIds
{

    private string pathToExistingCompanyIds = @"ExistingCompanyNumbers.txt";

    private List<string> _allIds;
    public IList<string> GetIds()
    {
        EnsureFileLoaded();
        return _allIds;
    }

    private void EnsureFileLoaded()
    {
        if (_allIds != null)
        {
            return;
        }

        _allIds = new List<string>();
        if (!File.Exists(pathToExistingCompanyIds))
        {
            return;
        }

        var allCompanyIds = File.ReadAllLines(pathToExistingCompanyIds);
        _allIds.AddRange(allCompanyIds);
    }

    public void AddNewIds(IList<string> ids)
    {
        EnsureFileLoaded();
        var newIds = GetNewIds(ids);
        File.AppendAllLines(pathToExistingCompanyIds, newIds);
        _allIds.AddRange(newIds);
    }

    private IList<string> GetNewIds(IList<string> ids)
    {
        var newIds = new List<string>();
        foreach (var id in ids)
        {
            if (IsExistingCompanyId(id))
            {
                continue;
            }
            newIds.Add(id);
        }
        var newIdss = ids.Where(id => !_allIds.Contains(id)).ToList();
        return newIds;
    }

    private bool IsExistingCompanyId(string id)
    {
       return _allIds.Contains(id);
    }
}