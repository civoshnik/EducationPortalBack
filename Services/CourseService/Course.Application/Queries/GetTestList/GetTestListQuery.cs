using Course.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Queries.GetTestList
{
    public record GetTestListQuery : IRequest<List<TestEntity>>
    {
    }
}
