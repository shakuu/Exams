namespace SchoolSystem.Framework.Core
{
    public interface ISchoolSystemDataCollection<T>
    {
        void Add(int id, T entity);

        void Remove(int id);

        T GetById(int id);

        bool ContainsKey(int id);
    }
}
