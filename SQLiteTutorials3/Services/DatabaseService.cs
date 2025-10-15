using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteTutorials3.Models;
using Contact = SQLiteTutorials3.Models.Contact;

namespace SQLiteTutorials3.Services
{
    
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;
        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath); // this is the connection to the database.
            _database.CreateTableAsync<Contact>().Wait(); // this creates the table if it does not exist.
        }

        public Task<List<Contact>> GetContactAsync()
        {
            return _database.Table<Contact>().ToListAsync();
        }
        public Task<int> SaveContactAsync(Contact contact)
        {
            return _database.InsertOrReplaceAsync(contact);
        }
        public Task<int> DeleteContactAsync(Contact contact)
        {
            return _database.DeleteAsync(contact);
        }

    }
}
