// SPDX-FileCopyrightText: 2026 Tayra Sakurai <b4151069@edu.kit.ac.jp>
// SPDX-License-Identifier: AGPL-3.0-or-later

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using YouAndIdol.Contexts;

namespace ExpandingSky.Fakes
{
    public class FakeDbContextFactory : IDbContextFactory<YouAndIdolContext>
    {
        public YouAndIdolContext CreateDbContext()
        {
            DbContextOptionsBuilder<YouAndIdolContext> optionsBuilder = new();

            optionsBuilder.UseInMemoryDatabase("Database");

            return new(optionsBuilder.Options);
        }
    }
}
