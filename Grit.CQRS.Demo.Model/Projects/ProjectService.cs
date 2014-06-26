﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS.Demo.Model.Projects
{
    public class ProjectService : IProjectService
    {
        private IProjectRepository _repository;
        public ProjectService(IProjectRepository repository)
        {
            _repository = repository;
        }

        public Project Get(int id)
        {
            return _repository.Get(id);
        }
    }
}
