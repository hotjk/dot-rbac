using System;

namespace Grit.CQRS.Demo.Model.Projects
{
    public interface IProjectRepository
    {
        Project Get(int id);
    }
}
