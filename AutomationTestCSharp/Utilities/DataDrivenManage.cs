using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;

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
                        _dataRow = ToList(GetData(sourceElement.ConnectionString, sourceElement.Query), sourceElement.RowIndexes);
                    }
                }
            }

            return _dataRow;
        }

        private DataTable GetData(string connectionString, string queryCommand = null)
        {
            var dt = new DataTable();

            using (var connection = new OleDbConnection())
            {
                connection.ConnectionString = connectionString;

                connection.Open();

                if (string.IsNullOrEmpty(queryCommand))
                {
                    var dtsheet = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    var excelSheetName = dtsheet?.Rows[0]["Table_Name"].ToString();
                    if (!string.IsNullOrEmpty(excelSheetName))
                    {
                        queryCommand = $"select * from [{excelSheetName}]";
                    }
                }

                var command = new OleDbCommand(queryCommand, connection);
                using (var da = new OleDbDataAdapter())
                {
                    da.SelectCommand = command;
                    da.Fill(dt);
                }
            }

            return dt;
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
