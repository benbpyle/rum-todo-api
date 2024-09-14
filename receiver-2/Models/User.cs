using System.ComponentModel.DataAnnotations.Schema;

namespace Receiver2.Models
{
    [Table("Users")]
    public class User
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("username")]
        public string? Username { get; set; }
        [Column("first_name")]
        public string? FirstName { get; set; }
        [Column("last_name")]
        public string? LastName { get; set; }
        [Column("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
