using Microsoft.Data.SqlTypes;
using System.ComponentModel.DataAnnotations.Schema;

namespace POCloudAPI.Entities
{
    [Table("Files")]
    public class APIFile
    {   

        public int Id { get; set; }
        public string? FullNameOfFile { get; set; }
        public string? ContentType { get; set; }
        public string? ContentDisposition { get; set; }
        public Byte[]? FileStreamData { get; set; }

        public APIUser User { get; set; }

        public DateTime created { get; set; }
        public DateTime LastModified { get; set; }

    }
}
