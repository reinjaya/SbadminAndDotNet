using WebAPI.Context;
using WebAPI.IRepository;
using WebAPI.Modes;

namespace WebAPI.Repository.Data
{
    public class DepartementRepository : GeneralRepository<Departement>
    {
        private MyContext _context;
        public DepartementRepository(MyContext context) : base(context)
        {
            _context = context;
        }

        //public int Create(Departement departement)
        //{
        //    _context.Departements.Add(departement);
        //    var result = _context.SaveChanges();
        //    return result;
        //}

        //public int Delete(int id)
        //{
        //    var data = _context.Departements.Find(id);
        //    if (data != null)
        //    {
        //        _context.Remove(data);
        //        var result = _context.SaveChanges();
        //        return result;
        //    }
        //    return 0;
        //}

        //public IEnumerable<Departement> GetAll()
        //{
        //    return _context.Departements.ToList();
        //}

        //public Departement GetById(int id)
        //{
        //    return _context.Departements.Find(id);
        //}

        //public int Update(Departement departement)
        //{
        //    _context.Entry(departement).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //    var result = _context.SaveChanges();
        //    return result;
        //}
    }
}
