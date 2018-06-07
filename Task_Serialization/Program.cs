using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Task_Serialization
{
    class Program
    {
        public static List<Book> bookList = new List<Book>();

        static void Main(string[] args)
        {

            AddBookToList("1984", "Orwell", "Social Science Fiction", 25);
            AddBookToList("Dune", "Herbert", "Science Fiction", 74);
            AddBookToList("Starship Troopers", "Heinlein", "Military Science Fiction", 98);
            AddBookToList("Fahrenheit 451", "Bradbury", "Dystopian", 12);
            AddBookToList("Nightfall", "Asimov", "Science Fiction", 115);
            AddBookToList("Hard to Be a God", "Strugatsky", "Science Fiction", 24);

            try
            {
                SaveXMLFile(AppDomain.CurrentDomain.BaseDirectory + "testxml.xml", bookList);
                Console.WriteLine("//////// XML ////////");
                ReadXMLFile(AppDomain.CurrentDomain.BaseDirectory + "testxml.xml");
                SaveJSONFile(AppDomain.CurrentDomain.BaseDirectory + "testjson.json", bookList);
                Console.WriteLine("//////// JSON ////////");
                ReadJSONFile(AppDomain.CurrentDomain.BaseDirectory + "testjson.json");
                SaveBinaryFile(AppDomain.CurrentDomain.BaseDirectory + "testbin.bin", bookList);
                Console.WriteLine("//////// Binary ////////");
                ReadBinaryFile(AppDomain.CurrentDomain.BaseDirectory + "testbin.bin");

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
                Console.ReadKey();
            }
        }

        public static void AddBookToList(string name, string authorName, string genre, double price)
        {
            bookList.Add(new Book
            {
                Name = name,
                AuthorName = authorName,
                Genre = genre,
                Price = price
            });
        }

        public static void SaveXMLFile(string pathToFile, List<Book> bookList)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Book>));
            string xml;
            using (StringWriter stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, bookList);
                xml = stringWriter.ToString();
            }
            File.WriteAllText(pathToFile, xml);

            //    using (XmlWriter writer = XmlWriter.Create(pathToFile, new XmlWriterSettings() { Indent = true }))
            //    {
            //        writer.WriteStartDocument();
            //        writer.WriteStartElement("BOOKS");
            //        foreach (var x in BookList.books)
            //        {
            //            writer.WriteStartElement("Book");
            //            writer.WriteElementString("name", x.Name);
            //            writer.WriteElementString("author", x.AuthorName);
            //            writer.WriteElementString("genre", x.Genre);
            //            writer.WriteElementString("price", x.Price.ToString());
            //            writer.WriteEndElement();
            //        }
            //        writer.WriteEndElement();
            //        writer.Flush();
            //        writer.WriteEndDocument();
            //    }
        }

        public static void ReadXMLFile(string pathToFile)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Book>));
            string xml = File.ReadAllText(pathToFile);
            using (StringReader stringReader = new StringReader(xml))
            {
                List<Book> list = (List<Book>)xmlSerializer.Deserialize(stringReader);
                foreach (var book in list)
                {
                    Console.WriteLine("{0};{1};{2};{3}", book.Name, book.AuthorName, book.Genre, book.Price.ToString());
                }
            }

            //    XmlDocument xmlDocument = new XmlDocument();
            //    xmlDocument.Load(pathToFile);
            //    if (BookList.books.Count > 0)
            //    {
            //        XmlNodeList xmlNodeList = xmlDocument.GetElementsByTagName("Book");
            //        foreach (XmlNode xmlNode in xmlNodeList)
            //        {
            //            XmlElement xmlElement = (XmlElement)xmlNode;
            //            string Name = xmlElement.GetElementsByTagName("name")[0].ChildNodes[0].InnerText;
            //            string Author = xmlElement.GetElementsByTagName("author")[0].ChildNodes[0].InnerText;
            //            string Genre = xmlElement.GetElementsByTagName("genre")[0].ChildNodes[0].InnerText;
            //            string Price = xmlElement.GetElementsByTagName("price")[0].ChildNodes[0].InnerText;
            //        }
            //    }
            //    else throw new Exception("Списка не существует");
        }

        public static void SaveJSONFile(string pathToFile, List<Book> bookList)
        {
            using (Stream stream = new FileStream(pathToFile, FileMode.Create))
            {
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(List<Book>));
                jsonSerializer.WriteObject(stream, bookList);
            }

            //    string json = JsonConvert.SerializeObject(BookList.books, Newtonsoft.Json.Formatting.Indented);
            //    File.WriteAllText(pathToFile, json);
        }

        public static void ReadJSONFile(string pathToFile)
        {
            using (Stream stream = new FileStream(pathToFile, FileMode.Open))
            {
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(List<Book>));
                List<Book> list = (List<Book>)jsonSerializer.ReadObject(stream);
                foreach (var book in list)
                {
                    Console.WriteLine("{0};{1};{2};{3}", book.Name, book.AuthorName, book.Genre, book.Price.ToString());
                }
            }

            //    string json = File.ReadAllText(pathToFile);
            //    List<Book> jBook = JsonConvert.DeserializeObject<List<Book>>(json);
            //    foreach (var j in jBook)
            //    {
            //        string name = j.Name;
            //        string author = j.AuthorName;
            //        string genre = j.Genre;
            //        string price = j.Price.ToString();
            //    }
        }

        public static void SaveBinaryFile(string pathToFile, List<Book> bookList)
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(pathToFile, FileMode.Create))
            {
                formatter.Serialize(stream, bookList);
            }
        }

        public static void ReadBinaryFile(string pathToFile)
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(pathToFile, FileMode.Open))
            {
                List<Book> list = (List<Book>)formatter.Deserialize(stream);
                foreach (var book in list)
                {
                    Console.WriteLine("{0};{1};{2};{3}", book.Name, book.AuthorName, book.Genre, book.Price.ToString());
                }
            }
        }
    }
}
