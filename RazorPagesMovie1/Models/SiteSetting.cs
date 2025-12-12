namespace RazorPagesMovie1.Models
{
    public class SiteSetting
    {
        public int Id { get; set; }
        public string Key { get; set; }        // e.g., "SiteName", "SupportEmail"
        public string Value { get; set; }      // e.g., "Blockbusters", "support@site.com"
    }
}
