using Chrono.Models;
using Chrono.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chrono.Services
{
    static class ProjectService
    {

        public static Project GetNewProject(ProjectDTO dto)
        {
            Project project = new()
            {
                Code = dto.Code,
                Name = dto.Name,
                Wbs = dto.Wbs,
            };

            return project;
        }
    }
}
