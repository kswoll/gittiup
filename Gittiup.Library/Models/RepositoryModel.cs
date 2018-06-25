namespace Gittiup.Library.Models
{
    public class RepositoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Path { get; set; }
        public AccountModel Account { get; set; }
    }
}