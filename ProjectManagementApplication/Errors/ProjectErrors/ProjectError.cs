using ProjectManagementApplication.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApplication.Errors.ProjectErrors
{
    internal class ProjectError
    {
        public static Error AlreadyExist => new Error("Project.AlreadyExist", "Project Alraedy Exist", 409);
        public static Error Notfound => new Error("Project.NotFound", "No Project Found", 404);
        public static Error BadRequest => new Error("Project.BadRequest", "InCorrect Project Data", 400);
        public static Error InternalServerError => new Error("Project.InternalServerError", "Internal Server Error", 501);
    }
}
