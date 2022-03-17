using Gifter.Models;
using Gifter.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gifter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileRepository _userProfileRepository;
        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }
        // GET: api/<UserProfileController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userProfileRepository.GetAll());
        }

        // GET api/<UserProfileController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _userProfileRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpGet("/GetUserProfileWithPosts/{id}")]
        public IActionResult GetPostIdWithComments(int id)
        {
            var user = _userProfileRepository.GetUserProfileIdWithPostsAndComments(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        // POST api/<UserProfileController>
        [HttpGet("GetByEmail")]
        public IActionResult GetByEmail(string email)
        {
            var user = _userProfileRepository.GetByEmail(email);
            if (email == null || user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post(UserProfile user)
        {
            _userProfileRepository.Add(user);
            return CreatedAtAction("GetByEmail", new { email = user.Email }, user);
        }

        // PUT api/<UserProfileController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, UserProfile userProfile)
        {
            if (id != userProfile.Id)
            {
                return BadRequest();
            }

            _userProfileRepository.Update(userProfile);
            return NoContent();
        }

        // DELETE api/<UserProfileController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userProfileRepository.Delete(id);
            return NoContent();
        }
    }
}
