namespace CompaniesHouseParser.ExportData.FileDataServise
{
    public class FileSaveRequest
    {
        public object Data { get; set; }
        public string FileName { get; set; }

        public FileSaveRequest(string fileName, object data)
        {
            Data = data;
            FileName = fileName;
        }
    }
}
