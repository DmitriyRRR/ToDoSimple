using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoSimple.Domain;
using ToDoSimple;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
using Microsoft.EntityFrameworkCore;
using ToDoSimple.Models;
using Microsoft.AspNetCore.CookiePolicy;

namespace ToDoSimple.Tests.Common
{
    public class ToDoSimpleContextFactory

    {
        public static int NoteIdA = 9999;
        public static int NoteIdB = 8888;

        public static ToDoContext Create()
        {
            var options = new DbContextOptionsBuilder<ToDoContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var context = new ToDoContext(options); ;
            context.Database.EnsureCreated();
            context.Notes.AddRange(
                new Note
                {
                    Id = NoteIdA,
                    Name = "NoteA",
                    Description = "DescriptionA",
                    CreatedTimestamp = DateTime.UtcNow,
                    ExpireDate = DateTime.UtcNow,
                    UserId = 1,
                }    ,      
                new Note
                {
                    Id = NoteIdB,
                    Name = "NoteB",
                    Description = "DescriptionA",
                    CreatedTimestamp = DateTime.UtcNow,
                    ExpireDate = DateTime.UtcNow,
                    UserId = 1,
                }
                );
            context.SaveChanges();
            return context;
        }

        public static void Destroy(ToDoContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
