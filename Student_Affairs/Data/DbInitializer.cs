using Student_Affairs.Models;
using System;
using System.Linq;

namespace Student_Affairs.Data
{
    public static class DbInitializer
    {
        public static void Initialize(StudentAffairsContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Classes.Any())
            {
                return;   // DB has been seeded
            }
            var classes = new Class[]
            {
            new Class{Name="Alpha"},
            new Class{Name="Beta"},
            new Class{Name="Gamma"},
            new Class{Name="Delta"}
            };
            foreach (Class c in classes)
            {
                context.Classes.Add(c);
            }

            context.SaveChanges();

            var students = new Student[]
            {
            new Student{Name="Carl",Address="Cairo",DateOfBirth=DateTime.Parse("2005-09-01"), Email="carl@gmail.com", ClassID=1},
            new Student{Name="Alex",Address="Alexandria",DateOfBirth=DateTime.Parse("2002-09-01"), Email="alex@gmail.com", ClassID=1},
            new Student{Name="Gina",Address="Giza",DateOfBirth=DateTime.Parse("2003-09-01"), Email="gina@gmail.com", ClassID = 2},
            new Student{Name="Saied",Address="Suez",DateOfBirth=DateTime.Parse("2002-09-01"), Email = "saied@gmail.com", ClassID = 2},
            new Student{Name="Paula",Address="PortSaid",DateOfBirth=DateTime.Parse("2002-09-01"), Email = "paula@gmail.com", ClassID = 3},
            new Student{Name="Carol",Address="Cairo",DateOfBirth=DateTime.Parse("2001-09-01"), Email = "carol@gmail.com", ClassID = 3},
            new Student{Name="Urlich",Address="USA",DateOfBirth=DateTime.Parse("2003-09-01"), Email = "urlich@gmail.com", ClassID = 4},
            new Student{Name="Usef",Address="UK",DateOfBirth=DateTime.Parse("2005-09-01"), Email = "usef@gmail.com", ClassID = 4},
            new Student{Name="Aba",Address="UK",DateOfBirth=DateTime.Parse("2005-09-01"), Email = "aba@gmail.com", ClassID = 4},
            new Student{Name="Amr",Address="UK",DateOfBirth=DateTime.Parse("2005-09-01"), Email = "amr@gmail.com", ClassID = 4},
            new Student{Name="Omar",Address="UK",DateOfBirth=DateTime.Parse("2005-09-01"), Email = "Omar@gmail.com", ClassID = 4}
            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var subjects = new Subject[]
            {
            new Subject{Name="Chemistry"},
            new Subject{Name="Microeconomics"},
            new Subject{Name="Macroeconomics"},
            new Subject{Name="Calculus"},
            new Subject{Name="Trigonometry"},
            new Subject{Name="Composition"},
            new Subject{Name="Literature"},
            new Subject{Name="English"},
            new Subject{Name="Arabic"},
            new Subject{Name="Biology"},
            new Subject{Name="Linear Algerba"},
            };
            foreach (Subject s in subjects)
            {
                context.Subjects.Add(s);
            }
            context.SaveChanges();

            var studentSubject = new StudentSubject[]
            {
                new StudentSubject{StudentID=1,SubjectID=1},
                new StudentSubject{StudentID=2,SubjectID=2},
                new StudentSubject{StudentID=3,SubjectID=3},
                new StudentSubject{StudentID=4,SubjectID=4},
                new StudentSubject{StudentID=5,SubjectID=5},
                new StudentSubject{StudentID=6,SubjectID=6},
                new StudentSubject{StudentID=7,SubjectID=7},
                new StudentSubject{StudentID=8,SubjectID=8},
                new StudentSubject{StudentID=9,SubjectID=9},
                new StudentSubject{StudentID=10,SubjectID=10},
                new StudentSubject{StudentID=11,SubjectID=11},
            };
            foreach(StudentSubject s in studentSubject)
            {
                context.StudentSubjects.Add(s);
            }
            context.SaveChanges();
        }
    }
}