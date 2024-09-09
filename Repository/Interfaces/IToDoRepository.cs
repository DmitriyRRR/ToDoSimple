using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoSimple.Models;

namespace Repository.Interfaces
{
    internal interface IToDoRepository: IDisposable
    {
        public Task<IEnumerable<Note>> GetItems();
        public Task CreateItem(Note note);
        public Task RemoveItem(int id);
        public Task<Note> GetItem(int id);
        public Task Update(Note note);
    }
}
