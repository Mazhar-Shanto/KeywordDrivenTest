using ExcelDataReader;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace KeywordDrivenTest.Utils
{
    public class TestCaseReader
    {
        public static List<Dictionary<string, string>> ReadTestCases(string filePath)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var testSteps = new List<Dictionary<string, string>>();

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                var config = new ExcelReaderConfiguration
                {
                    FallbackEncoding = System.Text.Encoding.UTF8
                };

                using (var reader = ExcelReaderFactory.CreateReader(stream, config))
                {
                    // Configure to treat the first row as column names
                    var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    });

                    var dataTable = dataSet.Tables[0]; // First sheet

                    var columns = new List<string>();
                    foreach (DataColumn col in dataTable.Columns)
                    {
                        Console.WriteLine($"Column Name: {col.ColumnName}"); // Debug log
                        columns.Add(col.ColumnName);
                    }

                    if (!columns.Contains("ActionKeyword"))
                        throw new Exception("The 'ActionKeyword' column is missing from the Excel file.");

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        var row = dataTable.Rows[i];
                        var step = new Dictionary<string, string>();
                        for (int j = 0; j < columns.Count; j++)
                            step[columns[j]] = row[j].ToString();
                        testSteps.Add(step);
                    }
                }
            }

            return testSteps;
        }
    }
}
