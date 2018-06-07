using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Task_Serialization
{
    public class BookList
    {
        public static List<Book> books;

        public BookList()
        {
            books = new List<Book>();
        }

        public void AddBookToList(string name, string authorName, string genre, double price)
        {
            books.Add(new Book
            {
                Name = name,
                AuthorName = authorName,
                Genre = genre,
                Price = price
            });
        }

        public void RemoveBookFromList(Book book)
        {
            books.Remove(book);
        }
    }
}
