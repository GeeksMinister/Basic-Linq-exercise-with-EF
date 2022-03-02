public class Subject
{
    #pragma warning disable CS8618
    [Key]
    public int Id { get; set; }
    [StringLength(50)]
    public string Name { get; set; }
    public int EducationId { get; set; }
    [StringLength(50)]
    [Unicode(false)]
    public Education Education { get; set; }
    public int? TeacherId { get; set; }
    [StringLength(50)]
    [Unicode(false)]
    public Teacher Teacher { get; set; }
}
