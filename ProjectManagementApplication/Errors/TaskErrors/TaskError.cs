

namespace ProjectManagementApplication.Errors.TaskErrors
{
    public class TaskError
    {
        public static Error AlreadyExist => new Error("Task.AreadyExist", "Task Already Exist", 409);
        public static Error Notfound => new Error("Task.NotFound", "no Task Found", 400);
        public static Error InternalServerError => new Error("Task.InternalServerError", "Internal Server Error", 501);

        public static Error ProjectNotFound => new Error("Task.ProjectNotfound", "no Project Found For This Id", 404);
    }
}
