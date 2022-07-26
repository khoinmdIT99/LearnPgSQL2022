using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostgresqlEntityDemo.Models
{
    public class Students
    {
        [Key]
       
        public int Id { get; set; }
        [Required]
        
        public string  FirstName { get; set; }
       
        public string  LastName { get; set; }
        public int RollNumber { get; set; }

    }
}
