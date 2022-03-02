public class Student
{
    #pragma warning disable CS8618
    [Key]
    public int Id { get; set; }
    [StringLength(50)]
    public string FirstName { get; set; } = null!;
    [StringLength(50)]
    public string LastName { get; set; } = null!;
    [Column("SSN")]
    [StringLength(10)]
    [Unicode(false)]
    public Int64 SSN { get; set; }
    [StringLength(320)]
    [Unicode(false)]
    public string? Email { get; set; }
    public int EducationId { get; set; }
    [StringLength(50)]
    [Unicode(false)]
    public Education Education { get; set; }
}
