using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Model.Context;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.InitDatas
{
    public static class InitSeed
    {
        public static void Seed()
        {
            var options = new DbContextOptionsBuilder<DirectoryDbContext>().Options;

            using (DirectoryDbContext context = new(options))
            {
                AddSites(context);
                AddDepartments(context);
                AddEmployees(context);
            }
        }

        private static void AddSites(DirectoryDbContext context)
        {
            if (!context.Site.Any())
            {
                context.Site.Add(new Site { City = "Paris" });
                context.Site.Add(new Site { City = "Nantes" });
                context.Site.Add(new Site { City = "Toulouse" });
                context.Site.Add(new Site { City = "Nice" });
                context.Site.Add(new Site { City = "Lille" });

                context.SaveChanges();
            }
        }

        private static void AddDepartments(DirectoryDbContext context)
        {
            if (!context.Department.Any())
            {
                context.Department.Add(new Department { Name = "Comptabilité" });
                context.Department.Add(new Department { Name = "Production" });
                context.Department.Add(new Department { Name = "Accueil" });
                context.Department.Add(new Department { Name = "Informatique" });
                context.Department.Add(new Department { Name = "Commercial" });

                context.SaveChanges();
            }
        }

        private static void AddEmployees(DirectoryDbContext context)
        {
            if (!context.Employee.Any())
            {
                List<Employee> employees =
                [
                    new Employee
                    {
                        LastName = "Dupont",
                        FirstName = "Jean",
                        Phone = "0102030405",
                        MobilePhone = "0601020304",
                        Email = "jean.dupont@example.com",
                        DepartmentId = context.Department.First(d => d.Name == "Comptabilité").Id,
                        SiteId = context.Site.First(s => s.City == "Paris").Id
                    },
                    new Employee
                    {
                        LastName = "Martin",
                        FirstName = "Sophie",
                        Phone = "0102030406",
                        MobilePhone = "0601020305",
                        Email = "sophie.martin@example.com",
                        DepartmentId = context.Department.First(d => d.Name == "Production").Id,
                        SiteId = context.Site.First(s => s.City == "Nantes").Id
                    },
                    new Employee
                    {
                        LastName = "Lefevre",
                        FirstName = "Pierre",
                        Phone = "0102030407",
                        MobilePhone = "0601020306",
                        Email = "pierre.lefevre@example.com",
                        DepartmentId = context.Department.First(d => d.Name == "Accueil").Id,
                        SiteId = context.Site.First(s => s.City == "Toulouse").Id
                    },
                    new Employee
                    {
                        LastName = "Moreau",
                        FirstName = "Marie",
                        Phone = "0102030408",
                        MobilePhone = "0601020307",
                        Email = "marie.moreau@example.com",
                        DepartmentId = context.Department.First(d => d.Name == "Informatique").Id,
                        SiteId = context.Site.First(s => s.City == "Nice").Id
                    },
                    new Employee
                    {
                        LastName = "Girard",
                        FirstName = "Luc",
                        Phone = "0102030409",
                        MobilePhone = "0601020308",
                        Email = "luc.girard@example.com",
                        DepartmentId = context.Department.First(d => d.Name == "Commercial").Id,
                        SiteId = context.Site.First(s => s.City == "Lille").Id
                    },
                    new Employee
                    {
                        LastName = "Andre",
                        FirstName = "Claire",
                        Phone = "0102030410",
                        MobilePhone = "0601020309",
                        Email = "claire.andre@example.com",
                        DepartmentId = context.Department.First(d => d.Name == "Comptabilité").Id,
                        SiteId = context.Site.First(s => s.City == "Paris").Id
                    },
                    new Employee
                    {
                        LastName = "Lambert",
                        FirstName = "Nicolas",
                        Phone = "0102030411",
                        MobilePhone = "0601020310",
                        Email = "nicolas.lambert@example.com",
                        DepartmentId = context.Department.First(d => d.Name == "Production").Id,
                        SiteId = context.Site.First(s => s.City == "Nantes").Id
                    },
                    new Employee
                    {
                        LastName = "Roux",
                        FirstName = "Julie",
                        Phone = "0102030412",
                        MobilePhone = "0601020311",
                        Email = "julie.roux@example.com",
                        DepartmentId = context.Department.First(d => d.Name == "Accueil").Id,
                        SiteId = context.Site.First(s => s.City == "Toulouse").Id
                    },
                    new Employee
                    {
                        LastName = "Blanc",
                        FirstName = "Antoine",
                        Phone = "0102030413",
                        MobilePhone = "0601020312",
                        Email = "antoine.blanc@example.com",
                        DepartmentId = context.Department.First(d => d.Name == "Informatique").Id,
                        SiteId = context.Site.First(s => s.City == "Nice").Id
                    },
                    new Employee
                    {
                        LastName = "Fournier",
                        FirstName = "Emma",
                        Phone = "0102030414",
                        MobilePhone = "0601020313",
                        Email = "emma.fournier@example.com",
                        DepartmentId = context.Department.First(d => d.Name == "Commercial").Id,
                        SiteId = context.Site.First(s => s.City == "Lille").Id
                    }
                ];

                context.Employee.AddRange(employees);
                context.SaveChanges();
            }
        }
    }
}
