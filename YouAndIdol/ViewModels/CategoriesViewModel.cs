// SPDX-FileCopyrightText: 2026 Tayra Sakurai <b4151069@edu.kit.ac.jp>
// SPDX-License-Identifier: AGPL-3.0-or-later

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouAndIdol.Contexts;
using YouAndIdol.Messages;
using YouAndIdol.Models;

namespace YouAndIdol.ViewModels
{
    public partial class CategoriesViewModel : ObservableRecipient, IRecipient<CategoryRemovedMessage>, IRecipient<CategoryUpdatedMessage>
    {
        private IDbContextFactory<YouAndIdolContext> dbContextFactory;

        public CategoriesViewModel(IDbContextFactory<YouAndIdolContext> dbContextFactory)
            : base()
        {
            this.dbContextFactory = dbContextFactory;
            Categories = [];
        }

        protected override void OnActivated()
        {
            Messenger.Register<CategoryRemovedMessage>(this);
            Messenger.Register<CategoryUpdatedMessage>(this);
        }

        protected override void OnDeactivated()
        {
            Messenger.UnregisterAll(this);
        }

        [ObservableProperty]
        public partial ObservableCollection<Category> Categories { get; set; }

        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task LoadAsync()
        {
            using YouAndIdolContext context = await dbContextFactory.CreateDbContextAsync();

            Categories.Clear();
            Stack<Category> stack = new();
            List<Category> categories = [];
            List<Category> flatList = [];

            await foreach (
                Category category in
                context.Categories
                .Where(e => e.ParentId == null)
                .AsAsyncEnumerable())
            {
                stack.Push(category);
                categories.Add(category);
            }

            while (stack.Count > 0)
            {
                Category category = stack.Pop();
                flatList.Add(category);
                EntityEntry<Category> entityEntry = context.Entry(category);

                await entityEntry
                    .Collection(e => e.Children)
                    .LoadAsync();

                foreach (Category child in category.Children)
                   if (!flatList.Contains(child))
                        stack.Push(child);
            }

            foreach (Category category1 in categories)
            {
                Categories.Add(category1);
            }
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        private async Task AddAsync()
        {
            using YouAndIdolContext context = await dbContextFactory.CreateDbContextAsync();

            Category newCategory = new();
            context.Add(newCategory);

            WeakReferenceMessenger.Default.Send(new CategoryAddedMessage(newCategory));
        }

        [RelayCommand(CanExecute = nameof(CanInvoke))]
        private static void Detail(Category? category)
        {
            if (category == null)
                return;

            WeakReferenceMessenger.Default.Send(new CategoryInvokedMessage(category));
        }

        private static bool CanInvoke(Category? category)
        {
            return category != null;
        }

        public async void Receive(CategoryRemovedMessage message)
        {
            await LoadAsync();
        }

        public async void Receive(CategoryUpdatedMessage message)
        {
            await LoadAsync();
        }
    }
}
