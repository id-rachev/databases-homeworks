﻿using System;
using System.Data.Entity;
using System.Linq;
using StudentSystem.Models;

namespace StudentSystem.Data
{
    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
            : base("StudentSystem")
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Homework> Homeworks { get; set; }
    }
}
