﻿using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Terp.Core.Repositories;
using Terp.Core.UnitOfWorks;
using Terp.Domain.Models;
using Terp.UI.Services.LogService;
using Terp.UI.ViewModels.Abstract;

namespace Terp.UI.ViewModels
{
    public class UnitViewModel : BaseViewModel
    {
        private ObservableCollection<Unit> _list = new ObservableCollection<Unit>();
        public ObservableCollection<Unit> List { get => _list; set { _list = value; OnPropertyChanged(); } }
        private Unit _selectedItem;
        public Unit SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                if (_selectedItem != null)
                {
                    DisplayName = _selectedItem.Name;
                }
                OnPropertyChanged();
            }
        }
        private string _displayName;
        public string DisplayName { get => _displayName; set { _displayName = value; OnPropertyChanged(); } }
        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        private IRepository<Unit> _repository;
        private IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public UnitViewModel(IRepository<Unit> repository, IUnitOfWork unitOfWork,ILogger logger)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            LoadData();
            AddCommand = new Abstract.RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName))
                    return false;
                if (_repository.AsQueryable().Any(x => x.Name == DisplayName))
                    return false;
                return true;
            }, async (p) =>
            {
                var unit = new Unit() { Name = DisplayName };
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    unit = await _repository.AddAsync(unit);
                    await _unitOfWork.CommitAsync();
                    LoadData();
                    _logger.LogWrite(unit, Defines.EModifyTypes.Added);

                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                }
            });

            UpdateCommand = new Abstract.RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                if (!_repository.AsQueryable().Any(x => x.Id == SelectedItem.Id))
                    return false;

                return true;

            }, async (p) =>
            {
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    var unit = await _repository.AsQueryable().FirstOrDefaultAsync(x => x.Id == SelectedItem.Id);
                    unit.Name = DisplayName;
                    await _repository.UpdateAsync(unit);
                    await _unitOfWork.CommitAsync();

                    _logger.LogWrite(unit, Defines.EModifyTypes.Updated);
                    LoadData();
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                }
            });

            DeleteCommand = new Abstract.RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            }, async (p) =>
            {
                if (!_repository.AsQueryable().Any(x => x.Id == SelectedItem.Id))
                    return;
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    var unit = await _repository.AsQueryable().FirstOrDefaultAsync(x => x.Id == SelectedItem.Id);
                    unit.IsDeleted = true;
                    await _repository.UpdateAsync(unit);
                    await _unitOfWork.CommitAsync();

                    _logger.LogWrite(unit, Defines.EModifyTypes.Deleted);
                    LoadData();
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                }
            });

        }
        private void LoadData()
        {
            List = new ObservableCollection<Unit>(_repository.AsQueryable().Where(x => !x.IsDeleted).ToList());
        }
    }
}
