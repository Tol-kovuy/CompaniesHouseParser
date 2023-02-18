using static System.Net.Mime.MediaTypeNames;

namespace CompaniesHouseParser.Storage;

public class ApplicationStorageCompanyIds : IApplicationStorageCompanyIds
{
    private string pathToExistingCompanyIds = @"ExistingCompanyNumbers.txt";

    private IList<string> _allIds;

    public IList<string> GetAll()
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

    public void AddRange(IList<string> ids)
    {
        if (!File.Exists(pathToExistingCompanyIds))
        {
            File.AppendAllLines(pathToExistingCompanyIds, ids);
            _allIds = GetAll();
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
        string[] existCompanyIds = GetAll().ToArray();
        foreach (var existId in existCompanyIds)
            if (existId == id)
                return true;
        return false;
    }
}
