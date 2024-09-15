using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoSimple.Models;

namespace Domain.Interfaces
{
    public interface IToDoRepository: IDisposable
    {
        public Task<IEnumerable<Note>> GetItems();
        public Task CreateItem(Note note);
        public Task DeleteItem(int id);
        public Task<Note> GetItem(int id);
        public Task Update(Note note);
    }
}
