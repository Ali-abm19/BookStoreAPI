// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text.Json;
// using System.Threading.Tasks;
// using BookStore.src.Entity;
// using BookStore.src.Utils;
// using Microsoft.AspNetCore.HttpLogging;
// using Microsoft.AspNetCore.Mvc;

// namespace BookStore.src.Controllers
// {
//     [ApiController]
//     [Route("api/v1/[Controller]")]
//     public class UsersController : ControllerBase
//     {
//         public static List<User> users = new List<User>
//         {
//             new User
//             {
//                 Id = 1,
//                 Name = "Mohammed",
//                 Address = "Dammam",
//                 Phone = 0593939393,
//                 Email = "xxx@gmail.com",
//                 Password = "azaz",
//             },
//             new User
//             {
//                 Id = 2,
//                 Name = "Aya",
//                 Address = "Jeddah",
//                 Phone = 0574747474,
//                 Email = "yyy@gmail.com",
//                 Password = "trtr",
//             },
//             new User
//             {
//                 Id = 3,
//                 Name = "Sara",
//                 Address = "Riyadh",
//                 Phone = 0565656565,
//                 Email = "zzz@gmail.com",
//                 Password = "jgjh",
//             },
//         };

//         [HttpGet] // Get Method
//         public ActionResult GetUser()
//         {
//             return Ok(users);
//         }

//         [HttpGet("{id}")]
//         public ActionResult GetUserById(int id)
//         {
//             User? foundUser = users.FirstOrDefault(p => p.Id == id);
//             if (foundUser == null)
//             {
//                 return NotFound();
//             }
//             return Ok(foundUser);
//         }

//         [HttpPost] // Post Method
//         public ActionResult CreateUser(User newUser)
//         {
//             users.Add(newUser);
//             return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
//         }

//         [HttpDelete("{id}")] // Delete Method
//         public ActionResult DeleteUser(int id)
//         {
//             User? foundUser = users.FirstOrDefault(u => u.Id == id);
//             if (foundUser == null)
//             {
//                 return NotFound();
//             }
//             users.Remove(foundUser);
//             return NoContent();
//         }

//         [HttpPut("{id}")] // Put Method
//         public ActionResult UpdateUser(int id, User updateUser)
//         {
//             User? foundUser = users.FirstOrDefault(u => u.Id == id);
//             if (foundUser == null)
//             {
//                 return NotFound();
//             }
//             foundUser.Name = updateUser.Name;
//             return Ok(foundUser);
//         }

//         [HttpPatch("{id}")]
//         public ActionResult UpdateUserInfo(int id, JsonElement updatedFields)
//         {
//             User? foundUser = users.FirstOrDefault(u => u.Id == id);
//             if (foundUser == null)
//             {
//                 return NotFound();
//             }
//             if (
//                 updatedFields.TryGetProperty("Address", out var addressUpdated)
//                 && addressUpdated.ValueKind == JsonValueKind.String
//             )
//             {
//                 foundUser.Address = addressUpdated.GetString();
//             }
//             if (
//                 updatedFields.TryGetProperty("Phone", out var phoneUpdated)
//                 && phoneUpdated.ValueKind == JsonValueKind.Number
//             )
//             {
//                 foundUser.Phone = phoneUpdated.GetInt32();
//             }

//             if (
//                 updatedFields.TryGetProperty("Email", out var emailUpdated)
//                 && emailUpdated.ValueKind == JsonValueKind.String
//             )
//             {
//                 foundUser.Email = emailUpdated.GetString();
//             }

//             return Ok(foundUser);
//         }

//         [HttpPost("signup")]
//         public ActionResult SignUpUser(User newUser)
//         {
//             PasswordUtils.Password(newUser.Password, out string hashedPassword, out byte[] salt);
//             newUser.Password = hashedPassword;
//             newUser.Salt = salt;
//             users.Add(newUser);

//             return Created($"/api/users/{newUser.Id}", newUser);
//         }

//         [HttpPost("login")]
//         public ActionResult LogInUser(User user)
//         {
//             User? foundUser = users.FirstOrDefault(e => e.Email == user.Email);

//             if (foundUser == null)
//             {
//                 return NotFound();
//             }

//             bool isMatched = PasswordUtils.VerifyPassword(
//                 user.Password,
//                 foundUser.Salt,
//                 foundUser.Password
//             );

//             if (!isMatched)
//             {
//                 return Unauthorized();
//             }

//             return Ok(foundUser);
//         }
//     }
// }
