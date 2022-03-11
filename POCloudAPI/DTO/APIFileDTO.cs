namespace POCloudAPI.DTO
{
    public class APIFileDTO
    {
        public string? FileName { get; set; }
        public string? FileAsBase64 { get; set; }
        public string? ContentType { get; set; }
        public string? ContentDisposition { get; set; }
        public string? Username { get; set; }

        public string? Token { get; set; } 
    }
}