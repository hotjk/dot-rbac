using System;

namespace Grit.CQRS.Demo.Model.Projects
{
    public interface IProjectWriteRepository
    {
        bool Init(Project project);
        bool DecreaseAmount(int projectId, decimal Amount);
    }
}
