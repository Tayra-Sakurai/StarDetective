// SPDX-FileCopyrightText: 2026 Tayra Sakurai <b4151069@edu.kit.ac.jp>
// SPDX-License-Identifier: AGPL-3.0-or-later

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouAndIdol.Contexts;
using YouAndIdol.Messages;
using YouAndIdol.Models;

namespace YouAndIdol.ViewModels
{
    [ObservableRecipient]
    public partial class CategoryViewModel : ObservableValidator, IRecipient<CategoryAddedMessage>, IRecipient<CategoryInvokedMessage>
    {
        private readonly IDbContextFactory<YouAndIdolContext> factory;

        private Category category;

        [ObservableProperty]
        public partial ObservableCollection<Category> Categories { get; set; }

        public CategoryViewModel(IDbContextFactory<YouAndIdolContext> factory)
        {
            Messenger = WeakReferenceMessenger.Default;
            this.factory = factory;
            category = new();
            Categories = [];
            ErrorsChanged += CategoryViewModel_ErrorsChanged;
        }

        private void CategoryViewModel_ErrorsChanged(object? sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            SaveCommand.NotifyCanExecuteChanged();
        }

        protected virtual partial void OnActivated()
        {
            Messenger.RegisterAll(this);
        }

        protected virtual partial void OnDeactivated()
        {
            Messenger.UnregisterAll(this);
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task LoadAsync()
        {
            using YouAndIdolContext context = await factory.CreateDbContextAsync();
            
            Categories.Clear();

            foreach (
                var category in
                context.Categories
                .OrderBy(e => e.Name)
                .ThenBy(e => e.Id)
                .ToList())
            {
                Categories.Add(category);
            }
        }

        public void InitializeForExistingValue(Category category)
        {
            this.category = category;
            this.category.Parent = null;

            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Description));
            OnPropertyChanged(nameof(SelectedParentIndex));
            OnPropertyChanged(nameof(Budget));
            ValidateAllProperties();
        }

        public int SelectedParentIndex
        {
            get
            {
                foreach ((int index, Category item) in Categories.Index())
                {
                    if (item.Id == category.ParentId)
                        return index;
                }

                return -1;
            }

            set
            {
                if (value < 0)
                    category.ParentId = null;
                else
                    category.ParentId = Categories[value].Id;
                OnPropertyChanged();
            }
        }

        [Required]
        public string Name
        {
            get => category.Name;
            set => SetProperty(category.Name, value, category, (m, v) => m.Name = v, true);
        }

        public string Description
        {
            get => category.Description;
            set => SetProperty(category.Description, value, category, (m, v) => m.Description = v, false);
        }

        public double Budget
        {
            get => category.Bugdet;
            set => SetProperty(category.Bugdet, value, category, (m, v) => m.Bugdet = v, false);
        }

        public async void Receive(CategoryAddedMessage message)
        {
            await LoadAsync();
            InitializeForExistingValue(message.Value);
        }

        public async void Receive(CategoryInvokedMessage message)
        {
            await LoadAsync();
            InitializeForExistingValue(message.Value);
        }

        [RelayCommand(AllowConcurrentExecutions = false, CanExecute = nameof(CanSave))]
        private async Task SaveAsync()
        {
            if (HasErrors)
                return;

            using YouAndIdolContext context = await factory.CreateDbContextAsync();
            context.Update(category);

            await context.SaveChangesAsync();

            WeakReferenceMessenger.Default.Send(new CategoryUpdatedMessage(category));
        }

        private bool CanSave()
        {
            return !HasErrors;
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        private async Task RemoveAsync()
        {
            using YouAndIdolContext context = await factory.CreateDbContextAsync();

            context.Remove(category);
            await context.SaveChangesAsync();

            InitializeForExistingValue(new());

            WeakReferenceMessenger.Default.Send(new CategoryRemovedMessage(category));
        }
    }
}
