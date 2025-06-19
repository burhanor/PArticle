using PArticle.Domain.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Domain.Entities
{
	public class ArticleCategory:EntityBase
	{
		public int CategoryId { get; set; }
		public int ArticleId { get; set; }
		public virtual Category Category { get; set; }
		public virtual Article Article { get; set; }
	}
}
