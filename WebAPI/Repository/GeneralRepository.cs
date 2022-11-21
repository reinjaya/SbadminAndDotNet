using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebAPI.Context;
using WebAPI.IRepository;

namespace WebAPI.Repository
{
    public class GeneralRepository<Entity> : IRepository<Entity> where Entity : class
    {
        MyContext _context;
        public GeneralRepository(MyContext myContext)
        {
            this._context = myContext;
        }

        public int Create(Entity entity)
        {
            _context.Set<Entity>().Add(entity);
            var result = _context.SaveChanges();
            return result;
        }

        public int Delete(int id)
        {
            var data = _context.Set<Entity>().Find(id);
            if (data != null)
            {
                _context.Remove(data);
                var result = _context.SaveChanges();
                return result;
            }
            return 0;
        }

        public Entity GetById(int id)
        {
            return _context.Set<Entity>().Find(id);
        }

        public int Update(Entity entity)
        {
            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            var result = _context.SaveChanges();
            return result;
        }

        IEnumerable<Entity> IRepository<Entity>.GetAll()
        {
            return _context.Set<Entity>().ToList();
        }
    }
}
