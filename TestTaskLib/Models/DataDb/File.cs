using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskLib.Models.DataDb
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; } 
        public FileType FileType { get; set; }
        public virtual RandomUser RandomUser { get; set; }
    }

    public enum FileType
    {
        Large,
        Medium,
        Thumbnail
    }
}
