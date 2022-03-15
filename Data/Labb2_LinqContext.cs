public partial class Labb2_LinqContext : DbContext
{
#pragma warning disable CS8618
    public Labb2_LinqContext()
    {
    }

    public Labb2_LinqContext(DbContextOptions<Labb2_LinqContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Teacher> Teacher { get; set; }
    public virtual DbSet<Student> Student { get; set; }
    public virtual DbSet<Education> Education { get; set; }
    public virtual DbSet<Subject> Subject { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        //if (!optionsBuilder.IsConfigured)
        //{
        //    optionsBuilder. UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB; Database=Labb2_Linq;" +
        //        " Trusted_Connection=True; MultipleActiveResultSets=True");
        //}
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
    }
}