using PArticle.Domain.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Domain.Entities
{
	public class ArticleTag:EntityBase
	{
		public int ArticleId { get; set; }
		public int TagId { get; set; }

		public virtual Article Article { get; set; }
		public virtual Tag Tag { get; set; }
	}
}
