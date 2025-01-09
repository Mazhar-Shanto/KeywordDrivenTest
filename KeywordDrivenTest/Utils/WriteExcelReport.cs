using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace KeywordDrivenTest.Utils
{
    internal class WriteExcelReport
    {
        public static void WriteTestResults(string filePath, List<Dictionary<string, string>> testSteps)
        {
           // if (testSteps.Count != results.Count)
               // throw new Exception("Mismatch between the number of test steps and results.");

            // Configure EPPlus to use non-commercial license
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                // Create a new worksheet
                var worksheet = package.Workbook.Worksheets.Add("TestResults");

                // Add headers
                var columns = testSteps[0].Keys.ToList();
                for (int colIndex = 0; colIndex < columns.Count; colIndex++)
                {
                    worksheet.Cells[1, colIndex + 1].Value = columns[colIndex];
                    worksheet.Cells[1, colIndex + 1].Style.Font.Bold = true;
                }

                // Add the "Result" column header
               // worksheet.Cells[1, columns.Count + 1].Value = "Result";

                // Populate rows
                for (int rowIndex = 0; rowIndex < testSteps.Count; rowIndex++)
                {
                    var step = testSteps[rowIndex];

                    // Populate test step data
                    for (int colIndex = 0; colIndex < columns.Count; colIndex++)
                    {
                        if (step[columns[colIndex]] == "Pass")
                        {
                            worksheet.Cells[rowIndex + 2, colIndex + 1].Value = step[columns[colIndex]];
                            worksheet.Cells[rowIndex + 2, colIndex + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[rowIndex + 2, colIndex + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Green);
                            worksheet.Cells[rowIndex + 2, colIndex + 1].Style.Font.Color.SetColor(System.Drawing.Color.White);
                            worksheet.Cells[rowIndex + 2, colIndex + 1].Style.Font.Bold = true;
                        }
                        if (step[columns[colIndex]] == "Fail")
                        {
                            worksheet.Cells[rowIndex + 2, colIndex + 1].Value = step[columns[colIndex]];
                            worksheet.Cells[rowIndex + 2, colIndex + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[rowIndex + 2, colIndex + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
                            worksheet.Cells[rowIndex + 2, colIndex + 1].Style.Font.Color.SetColor(System.Drawing.Color.White);
                            worksheet.Cells[rowIndex + 2, colIndex + 1].Style.Font.Bold = true;
                        }
                        else
                        {
                            worksheet.Cells[rowIndex + 2, colIndex + 1].Value = step[columns[colIndex]];
                        }
                    }

                    // Add the result (Pass/Fail)
                   // worksheet.Cells[rowIndex + 2, columns.Count + 1].Value = results[rowIndex] ? "Pass" : "Fail";
                }

                // Save the Excel file
                var newFilePath = Path.Combine(Path.GetDirectoryName(filePath), "TestResults.xlsx");
                package.SaveAs(new FileInfo(filePath));
                Console.WriteLine($"Test results saved to: {newFilePath}");
            }
        }

        /*
                public static void WriteTestResults(string filePath, List<Dictionary<string, string>> testSteps, List<bool> results)
                {
                    if (testSteps.Count != results.Count)
                        throw new Exception("Mismatch between the number of test steps and results.");

                    // Create a DataTable to store the updated data
                    var dataTable = new DataTable();

                    // Add columns from the testSteps dictionary keys
                    var columns = testSteps[0].Keys.ToList();
                    foreach (var column in columns)
                    {
                        dataTable.Columns.Add(column);
                    }

                    // Add the "Result" column
                    //dataTable.Columns.Add("Result");

                    // Populate rows with data from testSteps and results
                    for (int i = 0; i < testSteps.Count; i++)
                    {
                        var row = dataTable.NewRow();

                        foreach (var column in columns)
                        {
                            row[column] = testSteps[i][column];
                        }

                        // Add Pass/Fail result
                        //row["Result"] = results[i] ? "Pass" : "Fail";
                        //dataTable.Rows.Add(row);
                    }

                    // Save the updated DataTable to an Excel file
                    SaveDataTableToExcel(filePath, dataTable);
                }

                private static void SaveDataTableToExcel(string filePath, DataTable dataTable)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    using (var writer = ExcelWriterFactory.CreateOpenXmlWriter(stream))
                    {
                        // Write the data to the Excel file
                        writer.WriteStartDocument();
                        writer.WriteStartWorksheet("TestResults");

                        // Write headers
                        writer.WriteStartRow();
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            writer.WriteCell(column.ColumnName);
                        }
                        writer.WriteEndRow();

                        // Write rows
                        foreach (DataRow row in dataTable.Rows)
                        {
                            writer.WriteStartRow();
                            foreach (var cell in row.ItemArray)
                            {
                                writer.WriteCell(cell.ToString());
                            }
                            writer.WriteEndRow();
                        }

                        writer.WriteEndWorksheet();
                        writer.WriteEndDocument();
                    }
                }*/
    }
}
