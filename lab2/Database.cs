using SQLite;

namespace lab2;

public class Database
{
    readonly SQLiteAsyncConnection _db;

    public Database(string dbPath)
    {
        if (_db is not null)
            return;

        _db = new SQLiteAsyncConnection(dbPath, Constants.Flags);
        _db.CreateTableAsync<Person>().Wait();
    }
    public async Task<List<Person>> GetPersonsAsync()
    {
        return await _db.Table<Person>().ToListAsync();
    }
    public async Task<Person> GetPersonAsync(int id)
    {
        return await _db.Table<Person>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }
    public async Task<int> SavePersonAsync(Person person)
    {
        if (person.Id != 0)
            return await _db.UpdateAsync(person);
        return await _db.InsertAsync(person);
    }
    public async Task<int> DeletePersonAsync(Person person)
    {
        return await _db.DeleteAsync(person);
    }
}