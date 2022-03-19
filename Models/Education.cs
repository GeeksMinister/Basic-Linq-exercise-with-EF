public class Education
{
    #pragma warning disable CS8618
    public int Id { get; set; }
    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; }
    List<Subject> Subjects { get; set; }
    List<Student> Students { get; set; }
    List<Teacher> Teachers { get; set; }
}