using ERP.Domain.Core.Specifications;

namespace ERP.Domain.Modules.Projects
{
    public class ProjectSpecifications
    {
        public static BaseSpecification<Project> GetProjectByIdSpec(Guid id)
        {
            return new BaseSpecification<Project>(x => x.Id == id);
        }

        public static BaseSpecification<Project> GetAllProjectsSpec()
        {
            var spec = new BaseSpecification<Project>();
            spec.ApplyOrderByDescending(x => x.CreatedOn);
            return spec;
        }

        public static BaseSpecification<Project> SearchProjectsSpec(string searchKeyword)
        {
            var spec = new BaseSpecification<Project>(x => x.Name.Contains(searchKeyword)
                    || x.Description.Contains(searchKeyword));
            spec.ApplyOrderByDescending(x => x.CreatedOn);
            return spec;
        }

        public static BaseSpecification<Project> GetByProjectNameSpec(string name)
        {
            return new BaseSpecification<Project>(x => x.Name == name);
        }
    }
}
