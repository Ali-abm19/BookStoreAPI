using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BookStore
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        List<Book> books;

        [HttpGet]
        public ActionResult GetBooks()
        {
            return Ok(books);
        }
    }
}
