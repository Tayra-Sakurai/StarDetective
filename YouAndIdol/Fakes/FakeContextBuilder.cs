// SPDX-FileCopyrightText: 2026 Tayra Sakurai <b4151069@edu.kit.ac.jp>
// SPDX-License-Identifier: AGPL-3.0-or-later

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;
using YouAndIdol.Contexts;

namespace YouAndIdol.Fakes
{
    public class FakeContextBuilder : IDesignTimeDbContextFactory<YouAndIdolContext>
    {
        public YouAndIdolContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<YouAndIdolContext> dbContextOptionsBuilder = new();

            dbContextOptionsBuilder.UseSqlite("Data Source=Database.db");

            return new(dbContextOptionsBuilder.Options);
        }
    }
}
