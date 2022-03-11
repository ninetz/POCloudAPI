namespace POCloudAPI.DTO
{
    public class APIDownloadDTO
    {
        public Byte[]? FileAsBase64 { get; set; }
        public string? FileName { get; set; }
        public string? ContentType { get; set; }
    }
}
