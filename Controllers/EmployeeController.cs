using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InMemoryDemo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InMemoryDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

        /// <summary>
        /// Constructor with Employee DbContext Model
        /// </summary>
        /// <param name="context"></param>
        public EmployeeController(EmployeeDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Get Employee Record
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<EmployeeModel> GetEmployeeModels()
        {
            return _context.Employees.ToList();
        }
        /// <summary>
        /// Get Employee Record by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public EmployeeModel GetEmployeeById(int Id)
        {
            return _context.Employees.SingleOrDefault(e => e.Id == Id);
        }
        /// <summary>
        /// Deleteing Employee Record
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var Emp = _context.Employees.SingleOrDefault(x => x.Id == Id);
            if (Emp == null)
            {
                return NotFound("Employee with the id " + Id + " not exits");
            }
            _context.Employees.Remove(Emp);
            _context.SaveChanges();
            return Ok("Employee with the id " + Id + " Deleted Sucessfully");
        }
        /// <summary>
        /// Creating Employee Record
        /// </summary>
        /// <param name="Employee"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddEmployee(EmployeeModel Employee)
        {
            _context.Employees.Add(Employee);
            _context.SaveChanges();
            return Created("/api/Employee/" + Employee.Id, Employee);
        }
        /// <summary>
        /// Update Operation
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Employee"></param>
        /// <returns></returns>
        [HttpPut("{Id}")]
        public IActionResult Update(int Id,EmployeeModel Employee)
        {
            var Emp = _context.Employees.SingleOrDefault(x => x.Id == Id);
            if(Emp==null)
            {
                return NotFound("Employee with the id " + Id + " not exits");
            }
                if(Employee.Name != null)
                {
                    Emp.Name = Employee.Name;
                }

                if(Employee.Gender != null)
                {
                    Emp.Gender = Employee.Gender;
                }

                if (Employee.Age != 0)
                {
                    Emp.Age = Employee.Age;
                }

                if (Employee.Salary != 0)
                {
                    Emp.Salary = Employee.Salary;
                }
            _context.Update(Emp);
            _context.SaveChanges();
            return Ok("Employee with the id" + Id + "updated Sucessfully");
        }
    }
}
