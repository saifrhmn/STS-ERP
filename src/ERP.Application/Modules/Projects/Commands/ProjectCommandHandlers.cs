using ERP.Application.Core;
using ERP.Application.Modules.Projects.Commands;
using ERP.Domain.Core.Repositories;
using ERP.Domain.Core.Services;
using ERP.Domain.Modules.Projects;
using MediatR;

namespace ERP.Application.Modules.Projects.Commands
{
    public class ProjectCommandHandlers : BaseCommandHandler,
        IRequestHandler<CreateProjectCommand, Guid>,
        IRequestHandler<UpdateProjectCommand, Guid>,
        IRequestHandler<DeleteProjectCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectCommandHandlers(IMediator mediator, IUnitOfWork unitOfWork, IUserContext _userContext) : base(mediator, _userContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var newProject = Project.CreateProject(request.Name, request.Description, GetCurrentEmployeeId(),
             IsProjectNameExist);

            await _unitOfWork.Repository<Project>().AddAsync(newProject);
            await _unitOfWork.SaveChangesAsync();

            return newProject.Id;
        }

        private async Task<bool> IsProjectNameExist(string name)
        {
            var spec = ProjectSpecifications.GetByProjectNameSpec(name);
            var Projects = await _unitOfWork.Repository<Project>().ListAsync(spec, false);
            return Projects.Any();
        }

        public async Task<Guid> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var byIdSpec = ProjectSpecifications.GetProjectByIdSpec(request.Id);
            var existingProject = await _unitOfWork.Repository<Project>().SingleAsync(byIdSpec, true);

            existingProject.UpdateProject(request.Name, request.Description, GetCurrentEmployeeId(), IsProjectNameExist);

            _unitOfWork.Repository<Project>().Update(existingProject);
            await _unitOfWork.SaveChangesAsync();

            return existingProject.Id;
        }

        private async Task<bool> IsProjectNameExist(Guid id, string name)
        {
            var spec = ProjectSpecifications.GetByProjectNameSpec(name);
            var Projects = await _unitOfWork.Repository<Project>().ListAsync(spec, false);
            return Projects.Any(x => x.Id != id);
        }

        public async Task<Guid> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var byIdSpec = ProjectSpecifications.GetProjectByIdSpec(request.Id);
            var existingProject = await _unitOfWork.Repository<Project>().SingleAsync(byIdSpec, true);

            existingProject.DeleteProject(GetCurrentEmployeeId());

            _unitOfWork.Repository<Project>().Update(existingProject);
            await _unitOfWork.SaveChangesAsync();

            return existingProject.Id;
        }

    }
}
