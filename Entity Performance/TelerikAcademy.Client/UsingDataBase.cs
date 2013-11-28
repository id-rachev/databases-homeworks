using System;
using System.Collections.Generic;
using System.Linq;
using TelerikAcademy.Data;


namespace TelerikAcademy.Client
{
    class UsingDataBase
    {
        static void Main()
        {
            using (TelerikAcademyEntities taDataBase = new TelerikAcademyEntities())
            {
                // Task 1
                Console.WriteLine("Without Include:");
                PrintEmployeesByThreeColumns_WithoutInclude(taDataBase);

                Console.WriteLine("With Include:");
                PrintEmployeesByThreeColumns_WithInclude(taDataBase);

                // Task 2
                Console.WriteLine("Without Optimization:");
                ListEmployeesByThreeColumns_NotOptimized(taDataBase);

                Console.WriteLine("With Optimization:");
                ListEmployeesByThreeColumns_Optimized(taDataBase);
            }
        }

        private static void ListEmployeesByThreeColumns_Optimized(TelerikAcademyEntities taDataBase)
        {
            var employees = taDataBase.Employees
                .Select(e => new
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Address = e.Address.AddressText,
                    TownName = e.Address.Town.Name
                })
                .Where(e => e.TownName == "Sofia").ToList();

            foreach (var employee in employees)
            {
                Console.WriteLine("Employee name: {0}, Address: {1}, Town: {2}",
                    employee.FirstName + " " + employee.LastName,
                    employee.Address, employee.TownName);
            }
        }

        private static void ListEmployeesByThreeColumns_NotOptimized(TelerikAcademyEntities taDataBase)
        {
            List<Employee> employees = taDataBase.Employees.ToList();

            var employeesInfo = employees.Select(e => new
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                Address = e.Address.AddressText,
                TownName = e.Address.Town.Name
            }).ToList();

            var employeesInSofia = employeesInfo
                .Where(e => e.TownName == "Sofia").ToList();

            foreach (var employee in employeesInSofia)
            {
                Console.WriteLine("Employee name: {0}, Address: {1}, Town: {2}",
                    employee.FirstName + " " + employee.LastName,
                    employee.Address, employee.TownName);
            }
        }

        private static void PrintEmployeesByThreeColumns_WithInclude(TelerikAcademyEntities taDataBase)
        {
            foreach (var employee in taDataBase.Employees.Include("Department").Include("Address"))
            {
                Console.WriteLine("Employee name: {0}, Department: {1}, Town: {2}",
                    employee.FirstName + " " + employee.LastName,
                    employee.Department.Name, employee.Address.Town.Name);
            }
        }

        private static void PrintEmployeesByThreeColumns_WithoutInclude(TelerikAcademyEntities taDataBase)
        {
            foreach (var employee in taDataBase.Employees)
            {
                Console.WriteLine("Employee name: {0}, Department: {1}, Town: {2}",
                    employee.FirstName + " " + employee.LastName,
                    employee.Department.Name, employee.Address.Town.Name);
            }
        }
    }
}
