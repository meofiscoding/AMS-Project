using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects.Context.AmsModels;

[Table("skill")]
public class Skill
{
    public Skill()
    {
        Teachers = new HashSet<Teacher>();
    }

    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; }
    
    // relationships
    public virtual ICollection<Teacher> Teachers { get; set; }
}