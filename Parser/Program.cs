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
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("В директории {0} нет такого файла!", path);
            Console.ReadKey();
            throw new Exception();
        }

        public static void TestGit()
        {

        }

        

        static void Main(string[] args)
        {
            try
            {
                List<Entry> list = new List<Entry>();
                IEnumerable<string> lines;
                IEnumerable<string> lineValues;
                lines = File.ReadLines(GetFile());
                double numOfLines = lines.Count();
                double n = 0;
                foreach (var item in lines)
                {
                    try
                    {
                        if (!String.IsNullOrEmpty(item))
                        {
                            lineValues = item.Split('\t');
                            string[] myValues = lineValues.ToArray();
                            list.Add(new Entry()
                            {
                                Date = DateTime.Parse(myValues[0]),
                                Message = myValues[1],
                                Type = myValues[2],
                                SenderName = myValues[3],
                                ElementType = myValues[4],
                                SenderId = uint.Parse(myValues[5]),
                                RecipientId = uint.Parse(myValues[6]),
                                //Path = myValues[7],
                                //Offset = myValues[8],
                                //Length = myValues[9]
                            });
                        }
                    }
                    catch (FormatException fe)
                    {
                        //Console.WriteLine(n + " " + fe.Message);
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        //Console.WriteLine(n + " " + ex.Message);
                    }
                    n++;
                    Console.WriteLine("Загружено {0:f}%", (n * 100 / numOfLines));
                }

                uint idSender = list.Find(a => a.Type == "outgoing_privateMessage").SenderId;
                List<uint> ChatID = new List<uint>();
                foreach (var item in list)
                {
                    double x = 0;
                    for (int i = 0; i < ChatID.Count; i++)
                    {
                        if (item.SenderId == ChatID.ElementAt(i))
                        {
                            x++;
                        }
                    }
                    if (x == 0 && item.SenderId != idSender)
                    {
                        ChatID.Add(item.SenderId);
                    }
                }

                List<Entry> noDupList = new List<Entry>();
                noDupList.Add(list.ElementAt(0));
                foreach (var item in list)
                {
                    int temp = 0;
                    foreach (var itemNoDup in noDupList)
                    {
                        if (item.Date == itemNoDup.Date && item.Message == itemNoDup.Message && item.SenderName == itemNoDup.SenderName)
                        {
                            temp++;
                        }
                    }
                    if (temp == 0)
                    {
                        noDupList.Add(item);
                    }
                }
                Console.Clear();
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\" + list.Find(a => a.Type == "outgoing_privateMessage").SenderName);

                var orderedList = noDupList.OrderBy(l => l.Date);
                foreach (var id in ChatID)
                {
                    string fileName = list.Find(a => a.SenderId == id).SenderName + ".txt";
                    string path = (Directory.GetCurrentDirectory() + @"\" + list.Find(a => a.Type == "outgoing_privateMessage").SenderName);
                    Console.WriteLine("Записываю файл {0}", fileName);
                    FileStream fs = new FileStream((path + @"\" + fileName), FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);

                    foreach (var item in orderedList)
                    {
                        if (id == item.SenderId || id == item.RecipientId)
                        {
                            if (item.SenderName == "")
                            {
                                sw.WriteLine(item.Date.ToString() + '\t' + item.Type);
                            }
                            else
                            {
                                sw.WriteLine(item.Date.ToString() + '\t' + item.SenderName + '\t' + item.Message);
                            }
                        }
                    }
                    sw.Close();
                }
                Console.WriteLine("Готово!!!");
                Console.ReadKey();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}