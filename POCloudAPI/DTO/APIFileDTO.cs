namespace POCloudAPI.DTO
{
    public class APIFileDTO
    {
        public int Id { get; set; }
        public string? FullNameOfFile { get; set; }

        public DateTime created { get; set; }
        public DateTime LastModified { get; set; }
        public bool isMain { get; set; }
    }
}