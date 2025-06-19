using PArticle.Domain.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Domain.Entities
{
	public class ArticleView:EntityBase
	{
		public int ArticleId { get; set; }
		public DateTime ViewDate { get; set; }
		public string IpAddress { get; set; }
		public virtual Article Article { get; set; }
	}
}
