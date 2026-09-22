// SPDX-FileCopyrightText: 2026 Tayra Sakurai <b4151069@edu.kit.ac.jp>
// SPDX-License-Identifier: AGPL-3.0-or-later

using System;
using System.Collections.Generic;
using System.Text;

namespace YouAndIdol.Models
{
    public class DatabaseUser
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public Guid UserIdentity { get; set; } = Guid.NewGuid();
        public int Iteration { get; set; } = 1024;
        public byte[] Salt { get; set; } = new byte[32];
    }
}
