using ERP.Domain.Core.Repositories;
using ERP.Domain.Core.Specifications;
using ERP.Domain.Modules.Projects;
using MediatR;

namespace ERP.Application.Modules.Projects.Queries
{
    public class ProjectQueryHandlers :
        IRequestHandler<GetAllProjectsReq, GetAllProjectsRes>,
        IRequestHandler<GetProjectByIdReq, Project>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProjectQueryHandlers(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetAllProjectsRes> Handle(GetAllProjectsReq request, CancellationToken cancellationToken)
        {
            BaseSpecification<Project> spec;
            if (!string.IsNullOrWhiteSpace(request.SearchKeyword))
            {
                spec = ProjectSpecifications.SearchProjectsSpec(request.SearchKeyword);
            }
            else
            {
                spec = ProjectSpecifications.GetAllProjectsSpec();
            }
            var count = await _unitOfWork.Repository<Project>().CountAsync(spec);

            if (request.PageSize > 0)
            {
                spec.ApplyPaging((request.PageIndex * request.PageSize), request.PageSize);
            }
            var data = await _unitOfWork.Repository<Project>().ListAsync(spec, false);

            return new GetAllProjectsRes
            {
                Result = data,
                Count = count
            };
        }

        public async Task<Project> Handle(GetProjectByIdReq request, CancellationToken cancellationToken)
        {
            var spec = ProjectSpecifications.GetProjectByIdSpec(request.Id);
            return await _unitOfWork.Repository<Project>().SingleAsync(spec, false);
        }
    }
}
