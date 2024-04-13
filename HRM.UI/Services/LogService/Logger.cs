using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terp.Core.Entities;
using Terp.Core.Repositories;
using Terp.Core.UnitOfWorks;
using Terp.Domain.Models;
using Terp.UI.Defines;
using Terp.UI.States.Users;

namespace Terp.UI.Services.LogService
{
    public class Logger : ILogger
    {
        private readonly IRepository<LogInformation> _logRepository;
        private readonly IUserStore _userStore;
        private readonly IUnitOfWork _unitOfWork;

        public Logger(IRepository<LogInformation> logRepository,IUserStore userStore,IUnitOfWork unitOfWork) 
        {
            _logRepository = logRepository;
            _userStore = userStore;
            _unitOfWork = unitOfWork;
        }
        public async void LogWrite<TEntity>(TEntity entity,EModifyTypes type) where TEntity : class, IEntity
        {
            LogInformation newLog = new LogInformation();
            switch (type) 
            {
                case EModifyTypes.Added:
                    newLog.Content = $"{entity.ToString()} {entity.GetType().GetProperty("Id")?.GetValue(entity,null)} is added";
                    newLog.DateTime = DateTime.Now;
                    newLog.UserId = _userStore.CurrentUser.Id;
                    await _unitOfWork.BeginTransactionAsync();
                    try
                    {
                        await _logRepository.AddAsync(newLog);
                        await _unitOfWork.CommitAsync();
                    }
                    catch (Exception ex) 
                    {
                        await _unitOfWork.RollbackAsync();
                    }
                    break;
                case EModifyTypes.Updated:
                    newLog.Content = $"{entity.ToString()} {entity.GetType().GetProperty("Id")?.GetValue(entity, null)} is updated";
                    newLog.DateTime = DateTime.Now;
                    newLog.UserId = _userStore.CurrentUser.Id;
                    await _unitOfWork.BeginTransactionAsync();
                    try
                    {
                        await _logRepository.AddAsync(newLog);
                        await _unitOfWork.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await _unitOfWork.RollbackAsync();
                    }
                    break;
                case EModifyTypes.Deleted:
                    newLog.Content = $"{entity.ToString()} {entity.GetType().GetProperty("Id")?.GetValue(entity, null)} is deleted";
                    newLog.DateTime = DateTime.Now;
                    newLog.UserId = _userStore.CurrentUser.Id;
                    await _unitOfWork.BeginTransactionAsync();
                    try
                    {
                        await _logRepository.AddAsync(newLog);
                        await _unitOfWork.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await _unitOfWork.RollbackAsync();
                    }
                    break;
            }
        }
    }
}
