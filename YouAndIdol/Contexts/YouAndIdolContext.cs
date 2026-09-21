// SPDX-FileCopyrightText: 2026 Tayra Sakurai <b4151069@edu.kit.ac.jp>
// SPDX-License-Identifier: AGPL-3.0-or-later

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using YouAndIdol.Models;

namespace YouAndIdol.Contexts
{
    public class YouAndIdolContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Transfer> Transfer { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        public YouAndIdolContext(DbContextOptions<YouAndIdolContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Income>(
                t =>
                {
                    t.HasBaseType<Item>();

                    t.Property(e => e.PaymentMethodId)
                    .HasColumnName("PaymentMethodId");

                    t.Property(e => e.AccountId)
                    .HasColumnName("AccountId");

                    t.ToTable(
                        b => b.HasCheckConstraint(
                            "CK_EXCLUSIVE",
                            "(([PaymentMethodId] IS NOT NULL) + ([AccountId] IS NOT NULL) = 1) OR (([PaymentMethodId] IS NOT NULL) + ([AccountId] IS NOT NULL) = 0)"));
                });

            modelBuilder.Entity<Expense>(
                t =>
                {
                    t.HasBaseType<Item>();

                    t.Property(e => e.PaymentMethodId)
                    .HasColumnName("PaymentrMethodId_Expense");
                });

            modelBuilder.Entity<Transfer>(
                t =>
                {
                    t.HasBaseType<Item>();

                    t.HasOne(e => e.FromAccount)
                    .WithMany(e => e.TransferFroms);

                    t.HasOne(e => e.FromPaymentMethod)
                    .WithMany(e => e.TransferFroms);

                    t.HasOne(e => e.ToAccount)
                    .WithMany(e => e.TransferTos);

                    t.HasOne(e => e.ToPaymentMethod)
                    .WithMany(e => e.TransferTos);

                    t.ToTable(
                        b => b.HasCheckConstraint(
                            "CK_TRANSFER",
                            "((([FromPaymentMethodId] IS NOT NULL) + ([FromAccountId] IS NOT NULL) = 1) AND (([ToPaymentMethodId] IS NOT NULL) + ([ToAccountId] IS NOT NULL) = 1)) OR (([FromPaymentMethodId] IS NOT NULL) + ([FromAccountId] IS NOT NULL) + ([ToPaymentMethodId] IS NOT NULL) + ([ToAccountId] IS NOT NULL) = 0)"));
                });

            modelBuilder.Entity<Category>();
            modelBuilder.Entity<Account>();
            modelBuilder.Entity<PaymentMethod>();
        }
    }
}
