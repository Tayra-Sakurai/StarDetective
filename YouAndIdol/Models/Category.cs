// SPDX-FileCopyrightText: 2026 Tayra Sakurai <b4151069@edu.kit.ac.jp>
// SPDX-License-Identifier: AGPL-3.0-or-later

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace YouAndIdol.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Bugdet { get; set; } = double.NaN;
        public int? ParentId { get; set; } = null;
        public Category? Parent { get; set; }
        public ObservableCollection<Category> Children { get; } = [];
        public ICollection<Item> Items { get; } = new HashSet<Item>();
    }
}
