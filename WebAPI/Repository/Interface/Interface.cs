namespace WebAPI.Interface
{
    public interface IRepository<Entity, Key> where Entity : class
    {
        public IEnumerable<Entity> GetAll();
        public Entity GetById(int id);
        public int Create(Entity entity);
        public int Update(Entity entity);
        public int Delete(Key id);
    }
}
