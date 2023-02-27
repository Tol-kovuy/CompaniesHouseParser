namespace CompaniesHouseParser.Storage;

public class ApplicationStorageCompanyIds : IApplicationStorageCompanyIds
{

    private string pathToExistingCompanyIds = @"ExistingCompanyNumbers.txt";

    private List<string> _allIds;

    public IList<string> GetIds()
    {
        CheckIsFileUploaded();
        return _allIds;
    }

    private void CheckIsFileUploaded()
    {
        if (_allIds != null)
        {
            return;
        }

        var allCompanyIds = File.ReadAllLines(pathToExistingCompanyIds);

        _allIds = new List<string>();
        _allIds.AddRange(allCompanyIds);
    }

    public void AddNewIds(IList<string> ids)
    {
        CheckIsFileUploaded();
        var newIds = GetNewIds(ids);
        File.AppendAllLines(pathToExistingCompanyIds, newIds);
        _allIds.AddRange(newIds);
    }

    private IList<string> GetNewIds(IList<string> ids)
    {
        var newIds = new List<string>();
        foreach (var id in ids)
        {
            if (CompareCompanyIds(id))
            {
                continue;
            }
           newIds.Add(id);
        }
        return newIds;
    }

    private bool CompareCompanyIds(string id)
    {
       return _allIds.Contains(id);
    }
}