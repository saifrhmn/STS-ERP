using ERP.Application.Modules.Projects.Commands;
using ERP.Application.Modules.Projects.Queries;
using ERP.Domain.Enums;
using ERP.Domain.Modules.Projects;
using ERP.WebApi.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class ProjectController : BaseController
    {
        public ProjectController(IMediator _mediator) : base(_mediator)
        { }

        [HttpPost]
        public async Task<CustomActionResult> GetAllProjects(GetAllProjectsReq req)
        {
            var result = await _mediator.Send<GetAllProjectsRes>(req);
            return new CustomActionResult(true, null, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.ProjectView)]
        [HttpPost]
        public async Task<CustomActionResult> GetProjectById(GetProjectByIdReq req)
        {
            var result = await _mediator.Send<Project>(req);
            return new CustomActionResult(true, null, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.ProjectAdd)]
        [HttpPost]
        public async Task<CustomActionResult> CreateProject(CreateProjectCommand req)
        {
            var result = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record created sucessfully." }, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.ProjectEdit)]
        [HttpPost]
        public async Task<CustomActionResult> UpdateProject(UpdateProjectCommand req)
        {
            var result = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record updated sucessfully." }, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.ProjectDelete)]
        [HttpPost]
        public async Task<CustomActionResult> DeleteProject(DeleteProjectCommand req)
        {
            var result = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record removed sucessfully." }, null, result);
        }
    }
}
