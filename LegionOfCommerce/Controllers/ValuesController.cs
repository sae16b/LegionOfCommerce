using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace LegionOfCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
		private IUnitOfWork TheUnitOfWork;

		public ValuesController(IUnitOfWork theUnitOfWork)
		{
			TheUnitOfWork = theUnitOfWork;
			
			Console.WriteLine("Hi");
		}
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {


			List<string> userNames = new List<string>();
			IEnumerable<User> Users = TheUnitOfWork.User.GetAll();
			TheUnitOfWork.Complete();
			if (Users != null)
			{
				Console.WriteLine("USERS", Users);
				foreach (var user in Users)
				{
					userNames.Add(user.Username);
				}
			}
			else
			{
				userNames.Add("NONE");
			}

			return userNames;
		}

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
			//User user = TheUnitOfWork.User.Get(id); 
			//TheUnitOfWork.User.SayHiToUser(user);
			/*
			foreach(User user in TheUnitOfWork.User.GetAll())
			{
				Console.WriteLine(user.Username);
			}
			*/
			

			string userNames = "";
			IEnumerable<User> Users = TheUnitOfWork.User.GetAll();
			if(Users != null)
			{
				Console.WriteLine("USERS", Users);
				foreach (var user in Users)
				{
					userNames += (user.Username + ", ");
				}
			} else
			{
				userNames = "NONE";
			}
			
            return userNames;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
