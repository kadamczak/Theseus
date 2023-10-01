﻿using Microsoft.EntityFrameworkCore;

namespace Theseus.Infrastructure.DbContexts
{
    public class TheseusDbContextFactory
    {
        private readonly DbContextOptions _options;

        public TheseusDbContextFactory(DbContextOptions options)
        {
            _options = options;
        }

        public TheseusDbContext CreateDbContext()
        {
            return new TheseusDbContext(_options);
        }
    }
}