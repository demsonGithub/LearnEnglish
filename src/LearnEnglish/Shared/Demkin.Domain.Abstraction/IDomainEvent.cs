using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Demkin.Domain.Abstraction
{
    public interface IDomainEvent : INotification
    {
    }
}