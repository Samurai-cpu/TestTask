using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class ChangeEmployeeEnabledStatusCommand : IRequest
    {
        public int Id { get; set; }
        public int EnbaledStatus { get; set; }
    }
}