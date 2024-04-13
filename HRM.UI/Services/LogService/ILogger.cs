using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terp.Core.Entities;
using Terp.UI.Defines;

namespace Terp.UI.Services.LogService
{
    public interface ILogger
    {
        void LogWrite<TEntity>(TEntity entity,EModifyTypes type) where TEntity :class, IEntity;
    }
}
