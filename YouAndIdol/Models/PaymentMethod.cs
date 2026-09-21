// SPDX-FileCopyrightText: 2026 Tayra Sakurai <b4151069@edu.kit.ac.jp>
// SPDX-License-Identifier: AGPL-3.0-or-later

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YouAndIdol.Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<Income> Incomes { get; } = new HashSet<Income>();
        public ICollection<Expense> Expenses { get; } = new HashSet<Expense>();
        public ICollection<Transfer> TransferFroms { get; } = new HashSet<Transfer>();
        public ICollection<Transfer> TransferTos { get; } = new HashSet<Transfer>();
        public double Balance => Incomes.Sum(e => e.Amount) + TransferTos.Sum(e => e.Amount) - Expenses.Sum(e => e.Amount) - TransferFroms.Sum(e => e.Amount);
    }
}
