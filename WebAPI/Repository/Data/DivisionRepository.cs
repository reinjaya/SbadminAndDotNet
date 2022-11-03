using Microsoft.AspNetCore.Mvc;
using WebAPI.Context;
using WebAPI.Interface;
using WebAPI.Modes;

namespace WebAPI.Repository.Data
{
    public class DivisionRepository : IRepository<Division, int>
    {
        private readonly MyContext _context;
        public DivisionRepository(MyContext context)
        {
            _context = context;
        }

        //Get All
        [HttpGet]
        public IEnumerable<Division> GetAll()
        {
            return _context.Divisions.ToList();
        }

        //GetByID
        public Division GetById(int id)
        {
            return _context.Divisions.Find(id);
        }

        //Create
        public int Create(Division division)
        {
            _context.Divisions.Add(division);
            var result = _context.SaveChanges();
            return result;
        }

        //Update
        public int Update(Division division)
        {
            _context.Entry(division).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            var result = _context.SaveChanges();
            return result;
        }

        //Delete
        public int Delete(int id)
        {
            var data = _context.Divisions.Find(id);
            if(data != null) 
            {
                _context.Remove(data);
                var result = _context.SaveChanges();
                return result;
            }
            return 0;
        }
    }
}
