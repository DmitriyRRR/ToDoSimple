using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoSimple.Models;

namespace ToDoSimple.Domain.Repositories

{
    public class ToDoRepository : IToDoRepository
    {
        private bool disposedValue;
        private readonly ToDoContext _context;

        public ToDoRepository(ToDoContext context)
        {
            _context = context;
        }

        public Task CreateItem(Note note)
        {
            throw new NotImplementedException();
        }

        public async Task<Note> GetItem(int id)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(n => n.Id == id);
            return note;
        }

        public Task<IEnumerable<Note>> GetItems()
        {
            throw new NotImplementedException();
        }

        public Task RemoveItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Note note)
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ToDoRepository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
