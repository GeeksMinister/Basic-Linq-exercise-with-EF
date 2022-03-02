using System.Data;
using System.Reflection;

public static class Database
{
#pragma warning disable CS8602
    public static void PrintMenu()
    {
        while (true)
        {
            Clear();
            Write($"\n\t\t\t* (( Labb2 - LINQ )) * \n\n\n"+
                "  [1] Hämta alla lärare som undervisar Matte\n\n" +
                "  [2] Hämta alla elever med sina lärare\n\n" +
                "  [3] Visa ämnens tabell och kolla om Contains Programmering 1\n\n" +
                "  [4] Redigera ett ämne från Programmering 2 till OOP\n\n" +
                "  [5] Redigera ett ämne från OOP till Programmering 2\n\n" +
                "  [6] Uppdatera student record om sina lärare är Anas till Reidar\n\n" +
                "  [7] Omplacera Anas tillbaka\n\n" +
                "  [8] Avsluta \n\n\n" +
                "   \t-Var god och välj:  ");
            int.TryParse(ReadLine(), out int option);
            switch (option)
            {
                case 1:
                    Clear();
                    GetMathTeachers();
                    Redirecting();
                    break;
                case 2:
                    Clear();
                    GetStudentsWithTeachers();
                    Redirecting();
                    break;
                case 3:
                    Clear();
                    CheckProgramming1();
                    Redirecting();
                    break;
                case 4:
                    Clear();
                    CheckProgramming2();
                    Redirecting();
                    break;
                case 5:
                    Clear();
                    CheckOOP();
                    Redirecting();
                    break;
                case 6:
                    Clear();
                    CheckTeachersID();
                    Redirecting();
                    break;
                case 7:
                    Clear();
                    ReassignAnas();
                    Redirecting();
                    break;
                case 8:
                    Clear();
                    WriteLine("\n\n\n\n\t\tHa a en bra dag :-)");
                    Thread.Sleep(1800);
                    return;
                default:
                    WriteLine("\t\tOgiltig inmatning!   " +
                    "Var god och välj från 1 - 6\n");
                    ReadKey();
                    break;
            }
        }
    }
    private static void Redirecting()
    {
        Write("\n\n\n\t\tTryck 'Enter' för att återkomma till menyn igen");
        ReadLine();
    }


    private static void GetMathTeachers()
    {
        using Labb2_LinqContext context = new Labb2_LinqContext();
        var mathTeachers = (from teacher in context.Teacher
                            join subject in context.Subject
                            on teacher.Id equals subject.TeacherId
                            where subject.Name == "Matematik"
                            select teacher).ToList();
        PrintRows(mathTeachers);
    }

    private static void PrintRows<T>(T collection)
    {
        Clear();
        if (collection is List<Teacher>)
        {
            List<Teacher>? teachers = collection as List<Teacher>;
            PrintTeachers(teachers);
        }
        else if (collection is IEnumerable<dynamic>)
        {
            IEnumerable<dynamic>? teachers = collection as IEnumerable<dynamic>;
            PrintResultedTable(teachers);
        }

    }

    private static void PrintTeachers(List<Teacher>? teachers)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var row in teachers)
        {
            sb.Append($"{row.FirstName}       {row.LastName}" +
                $"      {row.EmploymentDate}");
            WriteLine($"\n\n\t[{row.Id}]      {sb} \n" +
                $"     ________________________________________________________");
            sb.Clear();
        }
    }

    private static void GetStudentsWithTeachers()
    {
        using Labb2_LinqContext context = new Labb2_LinqContext();
        var studentAndTeachers = (from teacher in context.Teacher
                                 from student in context.Student
                                 from education in context.Education
                                 where (education.Id == teacher.EducationId
                                 && education.Id == student.EducationId)
                                 select new
                                 {
                                     Education = education.Name,
                                     TeacherId = teacher.Id,
                                     TeacherName= teacher.FirstName + " " + teacher.LastName,
                                     StudentId = student.Id,
                                     StudentName= student.FirstName + " " + student.LastName,
                                 }).ToList();

        PrintRows(studentAndTeachers);
    }

    private static void PrintResultedTable(IEnumerable<dynamic>? collection)
    {
        StringBuilder result = new StringBuilder();
        foreach (var row in collection)
        {
            foreach (PropertyInfo prop in row.GetType().GetProperties())
            {
                if (prop.Name == "TeacherId" || prop.Name == "StudentId" || prop.Name == "SubjectId")
                {
                    result.Append($"[{prop.GetValue(row).ToString()}]  ");
                }
                else
                {
                    result.Append($"{prop.GetValue(row).ToString()}       ");
                }
            }    
                WriteLine($"\n\n\t{result}\n    ________________________________" +
                    $"__________________________________________________________");
                result.Clear();
        }
    }

    private static void CheckProgramming1()
    {
        using Labb2_LinqContext context = new Labb2_LinqContext();
        bool check = (from subject in context.Subject
                      select subject.Name).Contains("Programmering 1");
        if (check)
        {
            WriteLine("\n   Ämnen som är registrerad: \n\n");
            context.Subject.Select(s => s.Name).Distinct().ToList().
                ForEach(s => WriteLine($"\n\t{s}"));
            WriteLine("\n\n   [Programmering 1] finns i ämnens tabell");
        }
        else
        {
            Clear();
            WriteLine("\n\n\t[Programmering 1] är inte registrerat!");
        }
    }

    private static void CheckProgramming2()
    {
        using Labb2_LinqContext context = new Labb2_LinqContext();
        bool check = (from subject in context.Subject
                      select subject.Name).Contains("Programmering 2");
        if (check)
        {
            ChangeProgrammering2ToOOP();
        }
        else
        {
            Clear();
            WriteLine("\n\n\t[Programmering 2] har inte återfunnits!");
        }
    }

    private static void ChangeProgrammering2ToOOP()
    {
        using Labb2_LinqContext context = new Labb2_LinqContext();
        var subjectName = (from subject in context.Subject
                          where subject.Name == "Programmering 2"
                          select subject).First();
        subjectName.Name = "OOP";
        context.SaveChanges();
        WriteLine("\n\n\t[Programmering 2] har ändrats till [OOP] ");
    }

    private static void CheckOOP()
    {
        using Labb2_LinqContext context = new Labb2_LinqContext();
        bool check = (from subject in context.Subject
                      select subject.Name).Contains("OOP");
        if (check)
        {
            ChangeOOPToProgramming2();
        }
        else
        {
            Clear();
            WriteLine("\n\n\t[OOP] har inte återfunnits!");
        }
    }

    private static void ChangeOOPToProgramming2()
    {
        using Labb2_LinqContext context = new Labb2_LinqContext();
        var subjectName = (from subject in context.Subject
                           where subject.Name == "OOP"
                           select subject).First();
        subjectName.Name = "Programmering 2";
        context.SaveChanges();
        WriteLine("\n\n\t[OOP] har ändrats till [Programmering 2] ");
    }

    private static void CheckTeachersID()
    {
        using Labb2_LinqContext context = new Labb2_LinqContext();
        var studentAndTeachers = (from teacher in context.Teacher
                                  from student in context.Student
                                  from education in context.Education
                                  where (education.Id == teacher.EducationId
                                  && education.Id == student.EducationId)
                                  select new
                                  {
                                      Education = education.Name,
                                      TeacherId = teacher.Id,
                                      TeacherName = teacher.FirstName + " " + teacher.LastName,
                                      StudentId = student.Id,
                                      StudentName = student.FirstName + " " + student.LastName,
                                  }).ToList();
        if (CheckAnas(studentAndTeachers))
        {
            ChangeTeacher();
            PrintSubjectsAndTeachers();
        }
        else
        {
            WriteLine("\n\n\t[Anas] undervisar inget ämne i någon utblidning!");
        }
        
    }

    private static bool CheckAnas(IEnumerable<dynamic> collection)
    {
        foreach (var row in collection)
        {
            foreach (PropertyInfo prop in row.GetType().GetProperties())
            {
                if (prop.GetValue(row).ToString() == "Anas Alhusain")
                {

                    return true;
                }
            }
        }
        return false;
    }

    private static void ChangeTeacher()
    {
        using Labb2_LinqContext context = new Labb2_LinqContext();
        int AnasId = context.Teacher.Where(name => name.FirstName == "Anas").
                Select(id => id.Id).First();
        int ReidarId = context.Teacher.Where(name => name.FirstName == "Reidar").
            Select(id => id.Id).First();

        context.Subject.Where(s => s.TeacherId == AnasId)
            .ToList().ForEach(s => s.TeacherId = ReidarId);
        context.SaveChanges();
    }

    private static void PrintSubjectsAndTeachers()
    {
        using Labb2_LinqContext context = new Labb2_LinqContext();
        var SubjectsAndTeachers = (from teacher in context.Teacher
                                  from subject in context.Subject
                                  from education in context.Education
                                  where (education.Id == teacher.EducationId
                                  && teacher.Id == subject.TeacherId)
                                  orderby education.Name
                                  select new
                                  {
                                      Education = education.Name,
                                      TeacherId = teacher.Id,
                                      TeacherName = teacher.FirstName + " " + teacher.LastName,
                                      SubjectId = subject.Id,
                                      SubjectName = subject.Name,
                                  }).ToList();
        PrintRows(SubjectsAndTeachers);
    }

    private static void ReassignAnas()
    {
        using Labb2_LinqContext context = new Labb2_LinqContext();
        context.Subject.Where(s => s.Name == "Utveckling Med C#").ToList().ForEach(s => s.TeacherId = 1);
        context.SaveChanges();
        PrintSubjectsAndTeachers();
    }
}

