using System;
using System.Collections.Generic;
using System.Linq;
using StudentSystem.Data;
using StudentSystem.Data.Migrations;
using StudentSystem.Models;
using System.Data.Entity;

namespace StudentSystem.Client
{
    class UsingStudentSystem
    {
        static void Main()
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<StudentSystemContext, Configuration>());

            using (var studentsDataBase = new StudentSystemContext())
            {
                studentsDataBase.Students.Add(new Student()
                    {
                        Name = "Pesho Peshev",
                        Number = 9081231,
                    });

                var goshoHomework = studentsDataBase.Homeworks.Find(1);
                studentsDataBase.Students.Find(1).Homeworks.Add(goshoHomework);

                studentsDataBase.SaveChanges();
            }

        }
    }
}
