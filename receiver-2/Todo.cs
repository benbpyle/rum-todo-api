using System.ComponentModel.DataAnnotations.Schema;

namespace Receiver.Models
{
    [Table("Todos")]
    public class Todo
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string? Name { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
