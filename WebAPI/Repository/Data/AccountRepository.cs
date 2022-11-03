using Microsoft.EntityFrameworkCore;
using WebAPI.Context;
using WebAPI.Handlers;
using WebAPI.Modes;
using WebAPI.ViewModel;

namespace WebAPI.Repository.Data
{
    public class AccountRepository
    {
        private MyContext _context;
        public AccountRepository(MyContext context)
        {
            _context = context;
        }

        public UserEmployeeRoleModel GetDataLogin(string email, string password)
        {
            var data = _context.Users
                    .Include(x => x.Employee)
                    .Include(x => x.Role)
                    .SingleOrDefault(x => x.Employee.Email.Equals(email));

            bool pass = true; //Hashing.ValidatePassword(password, data.Password);

            if (pass)
            {
                UserEmployeeRoleModel result = new UserEmployeeRoleModel()
                {
                    Id = data.Id,
                    FullName = data.Employee.FullName,
                    Email = data.Employee.Email,
                    RoleName = data.Role.Name
                };
                return result;
            }
            return null;
        }

        public bool CheckEmail(string email)
        {
            int EmailId = _context.Employees.Where(e => e.Email == email).Count();

            if (EmailId > 0)
            {
                return false;
            }

            return true;
        }

        public int CreateEmployee(string fullName, string email, DateTime birthDate)
        {
            Employee employee = new Employee()
            {
                FullName = fullName,
                Email = email,
                BirthDate = birthDate,
            };

            _context.Employees.Add(employee);
            var result =_context.SaveChanges();
            return result;
        }

        public int CreateUser(string email, string password)
        {
            var id = _context.Employees.SingleOrDefault(x => x.Email.Equals(email)).Id;
            User user = new User()
            {
                Id = id,
                Password = Hashing.HashPassword(password),
                RoleId = 1
            };
            _context.Users.Add(user);
            var result = _context.SaveChanges();

            return result;
        }

        public int UpdatePasswordUser(string email, string oldPassword, string newPassword)
        {
            var id = _context.Employees.SingleOrDefault(x => x.Email.Equals(email)).Id;
            var data = _context.Users.Find(id);
            if (data != null && Hashing.ValidatePassword(oldPassword, data.Password))
            {
                data.Password = Hashing.HashPassword(newPassword);
                _context.Entry(data).State = EntityState.Modified;
                var result = _context.SaveChanges();
                return result;
            }
            return 0;
        }

        public User FindUserFromEmployee(string fullName, string email, DateTime birthDate)
        {
            var id = _context.Employees.SingleOrDefault(x => x.Email.Equals(email)).Id;
            var data = _context.Employees.Find(id);
            if ((data.BirthDate == birthDate) && (data.FullName == fullName))
            {
                var users = _context.Users.Find(id);
                return users;
            }
            return null;
        }

        public int ResetPassword(User user, string newPassword)
        {
            if (user != null)
            {
                user.Password = Hashing.HashPassword(newPassword);
                _context.Entry(user).State = EntityState.Modified;
                var result = _context.SaveChanges();
                return result;
            }
            return 0;
        }
    }
}
