using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectName.Application.ViewModels;
using MediatR;

namespace ProjectName.Application.Queries.GetUser
{
    public class GetAllUsersQuery : IRequest<List<UserViewModel>>
    {
        public GetAllUsersQuery(string query)
        {
            Query = query;
        }
        public string Query { get; private set; }
    }
}