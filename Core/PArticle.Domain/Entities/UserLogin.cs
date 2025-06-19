using PArticle.Domain.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Domain.Entities
{
	public class UserLogin:EntityBase
	{
		public DateTime LoginDate { get; set; }
		public int UserId { get; set; }
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }
		public string IpAddress { get; set; }

		public virtual User User { get; set; }
	}
}
