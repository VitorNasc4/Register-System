using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectName.Application.ViewModels;
using MediatR;

namespace ProjectName.Application.Queries.GetUser
{
    public class GetUserQuery : IRequest<UserViewModel>
    {
        public GetUserQuery(int id)
        {
            Id = id;
        }
        public int Id { get; private set; }
    }
}