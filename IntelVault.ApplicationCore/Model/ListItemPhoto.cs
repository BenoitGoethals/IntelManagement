using MongoDB.Bson;

namespace IntelVault.ApplicationCore.Model
{
    public class ListItemPhoto
    {
        public ObjectId Id { get; set; }
        public string? FileName { get; set; }
        public string? Text { get; set; }
        public byte[]? Picture { get; set; }
    }
}
