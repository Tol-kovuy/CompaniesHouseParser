using CompaniesHouseParser.Api;
using CompaniesHouseParser.ExportData.FileDataServise.Base;
using Newtonsoft.Json;
using System.Data;

namespace CompaniesHouseParser.ExportData.FileDataServise.Csv
{
    public class CsvFileDataService : FileDataService, ICsvFileDataService
    {
        public CsvFileDataService(
            ) : base(".csv")
        {
        }

        protected override async Task WriteToAsync(FileSaveRequest request, FileSaveResult result)
        {
            object data = request.Data;
            var json = JsonConvert.SerializeObject(data);

            await Task.FromResult(JsonToExcel(json, result.FileDirectoryPath, result.FileNameWithOutExtension));
        }

        static string JsonToExcel(string json, string path, string fileName)
        {
            json = json.Replace('\u00A0', ' ');

            DataTable dataTable = JsonToDataTable(json, fileName);
            List<string> list = new List<string>();

            list.Add(string.Join(",", dataTable.Columns.Cast<DataColumn>().Select(column => QuoteCsvField(column.ColumnName))));

            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(string.Join(",", row.ItemArray.Select(item => QuoteCsvField(item.ToString()))));
            }

            string text = Path.Combine(path, fileName + ".csv");
            File.AppendAllLines(text, list);
            text.RemoveDuplicatesAndSave();
            return text;
        }

        static string QuoteCsvField(string field)
        {
            if (string.IsNullOrEmpty(field))
                return "\"\"";

            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
                return "\"" + field.Replace("\"", "\"\"") + "\"";
            else
                return field;
        }

        private static DataTable JsonToDataTable(string data, string tableName = "")
        {
            if (!data.Contains("["))
            {
                data = data.Insert(0, "[");
            }

            if (!data.Contains("]"))
            {
                data = data.Insert(data.Length, "]");
            }

            DataTable dataTable = new DataTable(tableName);
            List<Dictionary<string, object>> list;
            try
            {
                list = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(data);
            }
            catch (Exception ex)
            {
                throw new Exception("Json value not valid : Must be List of objects :: " + ex.Message);
            }

            foreach (Dictionary<string, object> item in list)
            {
                DataRow dataRow = dataTable.NewRow();
                foreach (KeyValuePair<string, object> item2 in item)
                {
                    if (!dataTable.Columns.Contains(item2.Key))
                    {
                        dataTable.Columns.Add(item2.Key);
                    }

                    dataRow[item2.Key] = item2.Value;
                }

                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }
    }
}
