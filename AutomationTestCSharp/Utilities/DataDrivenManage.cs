using ExcelDataReader;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace AutomationTestCSharp.Utilities
{
    public class DataDrivenManage
    {
        private IEnumerable<Dictionary<string, object>> _dataRow;

        public IEnumerable<Dictionary<string, object>> TestCases(string testSourceName)
        {
            var DynamicDataSourceSection = ConfigurationManager.GetSection(DynamicDataSource.sectionName) as DynamicDataSource;
            if (DynamicDataSourceSection != null)
            {
                foreach (ConnectionManagerSourceElement sourceElement in DynamicDataSourceSection.ConnectionManagerSources)
                {
                    if(sourceElement.Name == testSourceName)
                    {
                        _dataRow = ToList(GetData(sourceElement.FilePath, sourceElement.SheetName), sourceElement.RowIndexes);
                    }
                }
            }

            return _dataRow;
        }

        private DataTable GetData(string filePath, string sheetName)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            var reader = ExcelReaderFactory.CreateReader(stream);

            var conf = new ExcelDataSetConfiguration
            {
                ConfigureDataTable = _ => new ExcelDataTableConfiguration
                {
                    UseHeaderRow = true   // si la primera fila son los nombres de columnas
                }
            };

            var dataSet = reader.AsDataSet(conf);

            return dataSet.Tables[sheetName];
        }

        private IEnumerable<Dictionary<string, object>> ToList(DataTable table, IEnumerable<int> rowIndexes)
        {
            foreach (var i in rowIndexes)
            {
                if (i < 0 || i >= table.Rows.Count)
                    continue;

                var row = table.Rows[i];

                yield return table.Columns
                    .Cast<DataColumn>()
                    .ToDictionary(
                        col => col.ColumnName,
                        col => row[col]
                    );
            }

        }

    }
}
