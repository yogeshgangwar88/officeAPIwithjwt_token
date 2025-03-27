using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using RepoLibrary.DBContext;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace officeapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public usersController(ApplicationDbContext context)
        {
            _context = context;
        }
       
        [HttpGet]
        [Route("Getusers")]
        public async Task< IEnumerable<dynamic>> Getusers()
        {
            ////left join with UserFamilyDetails table /////////////
            var udata =await _context.Users.
                GroupJoin(_context.UserFamilyDetails,
                u=>u.UserID,
                uf=>uf.UserID,
                (u,uf)=>new
                           {
                              u,uf
                           })
                .SelectMany(uf=>uf.uf.DefaultIfEmpty(),
           (u,uf) => new { u = u.u.Name, uf = (uf.FatherName!=null)? uf.FatherName:"not avail" }
           ).ToListAsync();
            return udata;
        }

        // POST api/<usersController>
        [HttpPost]
        public void Post([FromBody] Users Users)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(Users);
                _context.SaveChanges();
            }
        }

    }
}
