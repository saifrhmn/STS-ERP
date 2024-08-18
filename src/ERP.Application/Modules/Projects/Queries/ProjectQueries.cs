using ERP.Application.Core.Models;
using ERP.Domain.Modules.Projects;
using MediatR;

namespace ERP.Application.Modules.Projects.Queries
{
    public class GetAllProjectsReq : PagedListReq, IRequest<GetAllProjectsRes>
    { }

    public class GetAllProjectsRes : PagedListRes<Project>
    { }

    public class GetProjectByIdReq : IRequest<Project>
    {
        public Guid Id { get; set; }
    }
}
