namespace HashHandler.Shared.Models.Domain
{
    public class HashesData
    {
        public DateTime Date { get; set; }
        public IEnumerable<string> Hashes { get; set; } = null!;
    }
}