// SPDX-FileCopyrightText: 2026 Tayra Sakurai <b4151069@edu.kit.ac.jp>
// SPDX-License-Identifier: AGPL-3.0-or-later

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using YouAndIdol.Contexts;
using YouAndIdol.Models;

namespace YouAndIdol.ViewModels
{
    [ObservableRecipient]
    public partial class CategoryViewModel : ObservableValidator
    {
        private readonly IDbContextFactory<YouAndIdolContext> factory;

        public CategoryViewModel(IDbContextFactory<YouAndIdolContext> factory)
        {
            Messenger = WeakReferenceMessenger.Default;
            this.factory = factory;
        }
    }
}
