using SQLite;

namespace lab2;

public class Database
{
    readonly SQLiteAsyncConnection _db;

    public Database(string dbPath)
    {
        _db = new SQLiteAsyncConnection(dbPath);
        _db.CreateTableAsync<Person>().Wait();
    }

    public Task<List<Person>> GetPersonsAsync() =>
        _db.Table<Person>().ToListAsync();

    public Task<int> SavePersonAsync(Person person)
    {
        if (person.Id != 0)
            return _db.UpdateAsync(person);
        else
            return _db.InsertAsync(person);
    }

    public Task<int> DeletePersonAsync(Person person) =>
        _db.DeleteAsync(person);
}