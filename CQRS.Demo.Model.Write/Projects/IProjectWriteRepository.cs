using System;

namespace CQRS.Demo.Model.Projects
{
    public interface IProjectWriteRepository
    {
        bool Init(Project project);
        bool ChangeAmount(int projectId, decimal Amount);
    }
}
