using ExcelDataReader;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace KeywordDrivenTest.Utils
{
    public class ExcelUtils
    {
        public static List<Dictionary<string, string>> ReadTestCases(string filePath)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var testSteps = new List<Dictionary<string, string>>();

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var dataSet = reader.AsDataSet();
                    var dataTable = dataSet.Tables[0]; // First sheet

                    var columns = new List<string>();
                    foreach (DataColumn col in dataTable.Columns)
                    {
                        Console.WriteLine($"Column Name: {col.ColumnName}"); // Debug log
                        columns.Add(col.ColumnName);
                    }

                    if (!columns.Contains("Keyword"))
                        throw new Exception("The 'Keyword' column is missing from the Excel file.");

                    for (int i = 1; i < dataTable.Rows.Count; i++)
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
