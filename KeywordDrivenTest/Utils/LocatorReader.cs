using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeywordDrivenTest.Utils
{
    internal class LocatorReader
    {
        public static Dictionary<string, (string ElementType, string LocatorType, string LocatorValue)> ReadLocators(string filePath)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var locators = new Dictionary<string, (string ElementType, string LocatorType, string LocatorValue)>();

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    });

                    var table = dataSet.Tables[0]; // Assuming locators are in the first sheet

                    foreach (DataRow row in table.Rows)
                    {
                        var locatorName = row["LocatorName"].ToString();
                        var elementType = row["ElementType"].ToString();
                        var locatorType = row["LocatorType"].ToString();
                        var locatorValue = row["Locator"].ToString();

                        locators[locatorName] = (elementType, locatorType, locatorValue);
                    }
                }
            }

            return locators;
        }
    }
}
