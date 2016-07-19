using DaemonCharacter.Infra.Data.Context;
using DaemonCharacter.Infra.Data.Interfaces;
using System;

namespace DaemonCharacter.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DaemonCharacterContext _context;
        private bool _disposed;

        public UnitOfWork(DaemonCharacterContext context)
        {
            _context = context;
            _disposed = false;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
