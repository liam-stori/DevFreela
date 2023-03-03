using DevFreela.Application.ViewModels;
using MediatR;
using System.Collections.Generic;

namespace DevFreela.Application.Queries.GetAllProject
{
    public class GetAllProjectQuery : IRequest<List<ProjectViewModel>> 
    {
        public GetAllProjectQuery(string query)
        {
            Query = query;
        }

        public string? Query { get; private set; }
    }
}
