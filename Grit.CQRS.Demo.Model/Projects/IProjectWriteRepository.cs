using System;

namespace Grit.CQRS.Demo.Model.Projects
{
    public interface IProjectWriteRepository
    {
        bool Add(Project project);
        bool Update(Project project);
    }
}
