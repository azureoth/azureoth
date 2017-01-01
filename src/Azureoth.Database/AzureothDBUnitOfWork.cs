﻿using Azureoth.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azureoth.Database
{
    public class AzureothDbUnitOfWork : IDisposable
    {
        private readonly AzureothDbContext _context;

        public AzureothDbUnitOfWork(AzureothDbContext context)
        {
            _context = context;
        }

        #region Private Members
        private UsersRepository usersRepository;
        private ApplicationsRepository applicationsRepository;
        private SchemasRepository schemasRepository;
        #endregion Private Members

        #region Public Members
        public UsersRepository UsersRepository
        {
            get
            {
                if (this.usersRepository == null)
                {
                    this.usersRepository = new UsersRepository(_context);
                }
                return this.usersRepository;
            }
        }

        public ApplicationsRepository ApplicationsRepository
        {
            get
            {
                if (this.applicationsRepository == null)
                {
                    this.applicationsRepository = new ApplicationsRepository(_context);
                }
                return this.applicationsRepository;
            }
        }

        public SchemasRepository SchemasRepository
        {
            get
            {
                if (this.schemasRepository == null)
                {
                    this.schemasRepository = new SchemasRepository(_context);
                }
                return this.schemasRepository;
            }
        }
        #endregion Public Members

        public void Save()
        {
            _context.SaveChanges();
        }

        #region IDisposable
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
