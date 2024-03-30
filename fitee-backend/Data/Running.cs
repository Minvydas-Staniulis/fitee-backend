using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fitee_backend.Data
{
    [Table("running")]
    public class Running
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string name { get; set; }
        public float distance { get; set; }
        public TimeSpan running_time { get; set; }
    }
}