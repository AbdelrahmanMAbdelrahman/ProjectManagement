
 

namespace ProjectManagement.Common.Problem
{
    public static class ProblemExtn
    {
        public static ObjectResult Problem(this Result res)
        {
            if (res.IsSuccess)
            {
                throw new InvalidOperationException();
            }
            var problem = Results.Problem(statusCode: res.error.StatusCode);
            ProblemDetails? problemDetials = problem.GetType().GetProperty(nameof(ProblemDetails))!.GetValue(problem) as ProblemDetails;
            problemDetials!.Extensions["Errors"] = new
            {
                res.error.Code,
                res.error.Description
            };
            return new ObjectResult(problemDetials);
        }
    }
}
