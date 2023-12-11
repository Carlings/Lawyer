using System.ComponentModel.DataAnnotations;

namespace LawyerDataBase.DAL.Entities
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string Content { get; set; }
        public string Data { get; set; }
        public DateTime DateOfCreation { get; set; }
        public virtual User? User { get; set; }
        public int UserID { get; set; }
    }
}
