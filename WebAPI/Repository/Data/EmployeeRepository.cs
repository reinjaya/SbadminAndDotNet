using Microsoft.EntityFrameworkCore;
using WebAPI.Context;
using WebAPI.ViewModel;
using WebAPI.Models;
using Microsoft.CodeAnalysis;

namespace WebAPI.Repository.Data
{
    public class EmployeeRepository
    {
        private MyContext _context;

        public EmployeeRepository(MyContext context)
        {
            _context = context;
        }

        public IEnumerable<EmployeeUserRoleClient> GetAll()
        {
            var data = _context.Users.Include(x => x.Employee).Include(x => x.Role).Include(x => x.ClientCompany).ToList();

            List<EmployeeUserRoleClient> result = new List<EmployeeUserRoleClient>();

            foreach (var item in data)
            {
                result.Add(new EmployeeUserRoleClient
                {

                    Id = item.Id,
                    FullName = item.Employee.FullName,
                    Email = item.Employee.Email,
                    RoleName = item.Role.Name,
                    BirthDate = item.Employee.BirthDate,
                    Gender = item.Employee.Gender,
                    Salary = item.Employee.Salary,
                    ClientName = item.ClientCompany.Name,
                    City = item.Employee.City,

                });
            }

            return result;
            
        }
    }
}


