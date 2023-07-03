using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTest
{
    public abstract class TestBuider : IDisposable
    {
        protected HttpClient Test;
        private bool Disposed;
        protected TestBuider()
        {
            BootstrapTestingSuite();
        }
        protected void BootstrapTestingSuite()
        {
            Disposed = false;
            var appFactory = new WebApplicationFactory<Program>();
            Test = appFactory.CreateClient();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (Disposed)
                return;
            if (disposing)
            {
                Test.Dispose();
            }
            Disposed = true;
        }

    }
}
