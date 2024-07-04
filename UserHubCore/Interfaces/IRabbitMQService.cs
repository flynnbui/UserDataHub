using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDataHub.Core.DTOs;

namespace UserDataHub.Core.Interfaces
{
    internal interface IRabbitMQService
    {
        Task PublishAsync(UpdateUserInfoDto message);
    }
}
