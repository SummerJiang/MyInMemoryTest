using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MyInMemoryTest.Production;
using MyInMemoryTest.Production.Entities;
using NUnit.Framework;

namespace MyInMemoryTest
{
    public class EmployeeRepositoryTests
    {
        private IEmployeeRepository _repository;
        [SetUp]
        public void Setup()
        {
            _repository = GetInMemoryRepositor();
        }

        private IEmployeeRepository GetInMemoryRepositor()
        {
            var options = new DbContextOptionsBuilder<EmployeeContext>().UseInMemoryDatabase(databaseName: "MockDB").Options;
            var initContext = new EmployeeContext(options);
            initContext.Database.EnsureDeleted();
            Populate(initContext);
            var testContext = new EmployeeContext(options);

            var repository = new EmployeeRepository(testContext);

            return repository;
        }

        private void Populate(EmployeeContext initContext)
        {
            var Employees = new Employee
            {
                        Id = 1,
                        Name = "Summer",
                        DepartmentId=1
            };
          

            initContext.Add(Employees);
            initContext.SaveChanges();
        }

        [Test]
        public void add_a_employee()
        {
            var newemployee = new EmployeeDto
            {
                FirstName = "Pei",
                LastName = "Jiang",
                Department = DepartmentEnum.海外企業開發組
            };
            _repository.AddAsync(newemployee);
            var data = _repository.FindByDepartmentId(DepartmentEnum.海外企業開發組);
            var result = data.Result.Where(x => x.Name == "Pei Jiang");
            result.First().As<Employee>().Name.Should().Be("Pei Jiang");
        }

        [Test]
        public void find_海外企業開發組()
        {
            var data = _repository.FindByDepartmentId(DepartmentEnum.海外企業開發組);
            Assert.AreEqual(1, data.Result.Count());
        }
    }
}