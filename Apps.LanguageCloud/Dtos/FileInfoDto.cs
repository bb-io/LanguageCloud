namespace Apps.LanguageCloud.Dtos
{
    public class FileInfoDto
    {
        public string Name { get; set; }

        public string Id { get; set; }

        public string Role { get; set; }

        public VersionDto LatestVersion { get; set; }
    }

    public class VersionDto
    {
        public string Id { get; set; }

        public string Type { get; set; }
    }
}
