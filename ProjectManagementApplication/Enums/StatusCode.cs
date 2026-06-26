using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApplication.Enums
{
    public enum StatusCode
    {
        // 2xx Success
        OK = 200,
        Created = 201,
        Accepted = 202,
        NoContent = 204,

        // 3xx Redirection
        MovedPermanently = 301,
        Found = 302,
        NotModified = 304,

        // 4xx Client Errors
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        MethodNotAllowed = 405,
        Conflict = 409,
        UnprocessableEntity = 422,
        TooManyRequests = 429,

        // 5xx Server Errors
        InternalServerError = 500,
        NotImplemented = 501,
        BadGateway = 502,
        ServiceUnavailable = 503,
        GatewayTimeout = 504
    }
}
