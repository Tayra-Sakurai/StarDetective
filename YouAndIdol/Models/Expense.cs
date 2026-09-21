// SPDX-FileCopyrightText: 2026 Tayra Sakurai <b4151069@edu.kit.ac.jp>
// SPDX-License-Identifier: AGPL-3.0-or-later

using System;
using System.Collections.Generic;
using System.Text;

namespace YouAndIdol.Models
{
    public class Expense : Item
    {
        public int PaymentMethodId { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
    }
}
