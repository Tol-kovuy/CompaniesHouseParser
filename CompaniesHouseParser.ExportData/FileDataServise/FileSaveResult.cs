namespace CompaniesHouseParser.ExportData.FileDataServise
{
    public class FileSaveResult
    {
        public string AbsoluteFilePath { get; set; }
        public string FileName => Path.GetFileName(AbsoluteFilePath);
        public string FileNameWithOutExtension => Path.GetFileNameWithoutExtension(AbsoluteFilePath);
        public string FileDirectoryPath => Path.GetDirectoryName(AbsoluteFilePath);
        public string FileExtension => Path.GetExtension(AbsoluteFilePath);

        public FileSaveResult(string absoluteFilePath)
        {
            AbsoluteFilePath = absoluteFilePath;
        }
    }
}
