using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Task_Serialization
{
    [DataContract]
    [Serializable]
    public class Book
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string AuthorName { get; set; }
        [DataMember]
        public string Genre { get; set; }
        [DataMember]
        public double Price { get; set; }
    }
}
