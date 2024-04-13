using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terp.Domain.Models;

namespace Terp.UI.States.Users
{
    public interface IUserStore
    {
        User CurrentUser { get; set; }

        event Action StateChanged;

    }
}
