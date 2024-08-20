using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoSimple.Domain;
using ToDoSimple;

namespace ToDoSimple.Tests.Common
{
    public abstract class TestCommandBase : IDisposable
    {
        protected readonly ToDoContext Context;

        public TestCommandBase()
        {
            Context = ToDoSimpleContextFactory.Create();
        }

        public void Dispose()
        {
            ToDoSimpleContextFactory.Destroy(Context);
        }
    }
}
