using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace desafio.Services.IServices
{
    public interface ISubscriptionService
    {
        Task SendingMessage<T>(T message);
    }
}