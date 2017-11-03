using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace Parser
{
    class Program
    {
        public static string GetFile()
        {
            Console.Write("Название файла: ");
            string fileName = Console.ReadLine();
            string path = Directory.GetCurrentDirectory();
            if (File.Exists(path + @"\" + fileName))
            {
                return (path + @"\" + fileName);
            }
            else
            {
                Console.WriteLine("В директории {0} нет такого файла!", path);
                Console.WriteLine();
                GetFile();
            }
            return "";
        }



        static void Main(string[] args)
        {

            //DataTable ReadCSVFile(string pathToCsvFile)
            //{
            //создаём таблицу
            DataTable dt = new DataTable("Lines");
            //создаём колонки
            DataColumn colData;
            colData = new DataColumn("Date", typeof(DateTime));
            DataColumn colMessage;
            colMessage = new DataColumn("Message", typeof(string));
            DataColumn colType;
            colType = new DataColumn("Type", typeof(string));
            DataColumn colSenderName;
            colSenderName = new DataColumn("SenderName", typeof(string));
            DataColumn colElementType;
            colElementType = new DataColumn("ElementType", typeof(string));
            DataColumn colSenderId;
            colSenderId = new DataColumn("SenderId", typeof(uint));
            DataColumn colRecipientId;
            colRecipientId = new DataColumn("RecipientId", typeof(uint));
            DataColumn colPath;
            colPath = new DataColumn("Path", typeof(string));
            DataColumn colOffset;
            colOffset = new DataColumn("Offset", typeof(string));
            DataColumn colLength;
            colLength = new DataColumn("Length", typeof(string));
            //добавляем колонки в таблицу
            dt.Columns.AddRange(new DataColumn[] { colData, colMessage, colType, colSenderName, colElementType, colSenderId, colRecipientId, colPath, colOffset, colLength });
            try
            {
                DataRow dr = null;
                string[] lineValues = null;
                string[] lines = File.ReadAllLines(GetFile());
                for (int i = 0; i < lines.Length; i++)
                {
                    try
                    {
                        if (!String.IsNullOrEmpty(lines[i]))
                        {
                            lineValues = lines[i].Split(';');
                            //создаём новую строку
                            dr = dt.NewRow();

                            dr["Date"] = DateTime.Parse(lineValues[0]);
                            dr["Message"] = lineValues[1];
                            dr["Type"] = lineValues[2];
                            dr["SenderName"] = lineValues[3];
                            dr["ElementType"] = lineValues[4];
                            dr["SenderId"] = uint.Parse(lineValues[5]);
                            dr["RecipientId"] = uint.Parse(lineValues[6]);
                            dr["Path"] = lineValues[7];
                            dr["Offset"] = lineValues[8];
                            dr["Length"] = lineValues[9];

                            //добавляем строку в таблицу
                            dt.Rows.Add(dr);
                        }
                    }
                    catch (FormatException fe)
                    {
                        Console.WriteLine(i + " " + fe.Message);
                        //i++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }





            // Presuming the DataTable has a column named Date.
            string expression;
            expression = "Date > #1/1/00#";
            DataRow[] foundRows;

            // Use the Select method to find all rows matching the filter.
            foundRows = dt.Select(expression);

            // Print column 0 of each returned row.
            for (int i = 0; i < foundRows.Length; i++)
            {
                Console.Write(foundRows[i][0]);
                Console.Write("            ");
                Console.Write(foundRows[i][1]);
                Console.Write("            ");
                Console.Write(foundRows[i][2]);
                Console.Write("            ");
                Console.WriteLine(foundRows[i][3]);
            }


            //          // Presuming the DataTable has a column named Date.
            //string expression = "Date = '1/31/1979' or OrderID = 2";
            //// string expression = "OrderQuantity = 2 and OrderID = 2";

            //// Sort descending by column named CompanyName.
            //string sortOrder = "CompanyName ASC";
            //DataRow[] foundRows;

            //// Use the Select method to find all rows matching the filter.
            //foundRows = table.Select(expression, sortOrder);

            //// Print column 0 of each returned row.
            //for (int i = 0; i < foundRows.Length; i++)
            //    Console.WriteLine(foundRows[i][2]);
            ////    return dt;
            ////}
            Console.WriteLine("Allright!!!");
            Console.ReadKey();
        }
    }
}
