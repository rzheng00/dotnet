using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace POMExample.Util
{
    class TestDataReader
    {
        public static IEnumerable<TestCaseData> ReadFromExcel(string excelFileName, string excelsheetTabName)
        {
            string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string xslLocation = Path.Combine(executableLocation, "data\\" + excelFileName);
            Console.WriteLine("file patht: " + xslLocation);
            string cmdText = "SELECT * FROM [" + excelsheetTabName + "$]";

            if (!File.Exists(xslLocation))
                throw new Exception(string.Format("File name: {0}", xslLocation), new FileNotFoundException());

            string connectionStr = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0 Xml;HDR=YES;'", xslLocation);

            var testCases = new List<TestCaseData>();
            using (var connection = new OleDbConnection(connectionStr))
            {
                connection.Open();
                var command = new OleDbCommand(cmdText, connection);
                var reader = command.ExecuteReader();
                if (reader == null)
                    throw new Exception(string.Format("No data return from file, file name:{0}", xslLocation));
                while (reader.Read())
                {
                    var row = new List<string>();
                    var feildCnt = reader.FieldCount;
                    for (var i = 0; i < feildCnt; i++)
                        row.Add(reader.GetValue(i).ToString());
                    testCases.Add(new TestCaseData(row.ToArray()));
                }
            }

            if (testCases != null)
                foreach (TestCaseData testCaseData in testCases)
                    yield return testCaseData;
        }

        public static IEnumerable<TestCaseData> ReadFromCSV(string csvFileName)
        {
            string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string csvLocation = Path.Combine(executableLocation, "data\\" + csvFileName);

            if (!File.Exists(csvLocation))
                throw new Exception(string.Format("File name: {0}", csvLocation), new FileNotFoundException());

            var testCases = new List<TestCaseData>();
            using (var fs = File.OpenRead(@csvLocation))
            using (var sr = new StreamReader(fs))
            {
                string line = string.Empty;
                while (line != null)
                {
                    line = sr.ReadLine();
                    if (line != null)
                    {
                        string[] split = line.Split(new char[] { ',' },
                            StringSplitOptions.None);

                        testCases.Add(new TestCaseData(split));
                    }
                }
            }

            if (testCases != null)
                foreach (TestCaseData testCaseData in testCases)
                    yield return testCaseData;
        }
    }
}
