// SPDX-FileCopyrightText: 2026 Tayra Sakurai <b4151069@edu.kit.ac.jp>
// SPDX-License-Identifier: AGPL-3.0-or-later

using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using YouAndIdol.Models;

namespace YouAndIdol.Messages
{
    public class CategoryUpdatedMessage : ValueChangedMessage<Category>
    {
        public CategoryUpdatedMessage(Category category) : base(category)
        {
        }
    }
}
