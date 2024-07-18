using CompaniesHouseParser.Shared;

namespace CompaniesHouseParser.Storage;

public class ApplicationStorageCompanyIds : IApplicationStorageCompanyIds
{
    private string pathToExistingCompanyIds;

    private void EnsureActiveCompaniesFileCreated()
    {
        pathToExistingCompanyIds = Path.Combine(FilePaths.ExistingActiveCompaniesDirectoryName,
            FilePaths.ExistingCompaniesFileName);

        if (!Directory.Exists(FilePaths.ExistingActiveCompaniesDirectoryName))
        {
            Directory.CreateDirectory(FilePaths.ExistingActiveCompaniesDirectoryName);
        }

        if (!File.Exists(pathToExistingCompanyIds))
        {
            File.Create(pathToExistingCompanyIds).Close();
        }
    }

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
            EnsureActiveCompaniesFileCreated();
            return;
        }

        var allCompanyIds = File
            .ReadAllLines(pathToExistingCompanyIds)
            .Distinct()
            .ToArray(); ;
        _allIds.AddRange(allCompanyIds);
    }

    public void AddNewIds(IList<string> ids)
    {
        EnsureFileLoaded();
        var newIds = GetNewIds(ids)
            .Distinct()
            .ToArray(); ;
        File.AppendAllLines(pathToExistingCompanyIds, newIds);
        _allIds.AddRange(newIds);
    }

    private IList<string> GetNewIds(IList<string> ids)
    {
        var newIds = ids.Where(id => !_allIds.Contains(id)).ToList();
        return newIds;
    }
}