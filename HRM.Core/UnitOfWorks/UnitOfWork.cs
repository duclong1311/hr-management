﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terp.Core.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(DbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public Task CommitAsync()
        {
            try
            {
                _context.SaveChanges();
                return _transaction?.CommitAsync();
            }
            catch
            {
                _transaction?.RollbackAsync();
                throw;
            }
            finally
            {
                _transaction?.DisposeAsync();
                _transaction = null;
            }
        }

        public Task RollbackAsync()
        {
            try
            {
                return _transaction?.RollbackAsync();
            }
            catch
            {
                throw;
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }
    }
}
