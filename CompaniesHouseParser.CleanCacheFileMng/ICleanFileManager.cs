using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.CleanCacheFileMng
{
    public interface ICleanFileManager : ITransientDependency
    {
        void CleanFiles(IList<string> filePath);
    }
}
