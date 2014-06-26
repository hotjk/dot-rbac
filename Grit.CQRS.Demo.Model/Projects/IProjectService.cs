using System;

namespace Grit.CQRS.Demo.Model.Projects
{
    public interface IProjectService
    {
        Project Get(int id);
    }
}
