using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.src.Utils
{
    public class CustomException : Exception
    {
        public int StatusCode { get; set; }

        public CustomException(int statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }

        //Type of Exception
        //1 Not found
        public static CustomException NotFound(string message = "Item not found")
        {
            return new CustomException(404, message);
        }

        //2 Bad request
        public static CustomException BadRequest(string message = "Bad request")
        {
            return new CustomException(400, message);
        }

        //3 Authorization
        public static CustomException UnAuthorized(string message = "Unauthorized. Please log in")
        {
            return new CustomException(401, message);
        }

        //4 Forbidden
        public static CustomException Forbidden(
            string message = "Forbidden. The user does not have access rights to the content"
        )
        {
            return new CustomException(403, message);
        }

        //5 Internal "Genral Error"
        public static CustomException InternalError(string message = "Internal server error")
        {
            return new CustomException(500, message);
        }
    }
}
