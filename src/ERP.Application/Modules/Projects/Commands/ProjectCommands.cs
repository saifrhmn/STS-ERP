using MediatR;

namespace ERP.Application.Modules.Projects.Commands
{
    public class CreateProjectCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateProjectCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }

    public class DeleteProjectCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
    
}
