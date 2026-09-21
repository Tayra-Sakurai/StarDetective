// SPDX-FileCopyrightText: 2026 Tayra Sakurai <b4151069@edu.kit.ac.jp>
// SPDX-License-Identifier: AGPL-3.0-or-later

using System;
using System.Collections.Generic;
using System.Text;

namespace YouAndIdol.Models
{
    public class Transfer : Item
    {
        public int? FromAccountId { get; set; }
        public int? ToAccountId { get; set; }
        public int? FromPaymentMethodId { get; set; }
        public int? ToPaymentMethodId { get; set; }
        public Account? FromAccount { get; set; }
        public Account? ToAccount { get; set; }
        public PaymentMethod? FromPaymentMethod { get; set; }
        public PaymentMethod? ToPaymentMethod { get; set; }
    }
}
