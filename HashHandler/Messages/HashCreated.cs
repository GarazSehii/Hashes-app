using HashHandler.Shared.Models.Domain;

namespace HashHandler.Messages
{
    public class HashCreated
    {
        public HashesData HashesData { get; set; } = null!;
    }
}