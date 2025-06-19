using PArticle.Domain.Concretes;
using PArticle.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Domain.Entities
{
	public class Tag:EntityBase
	{
		public string Name { get; set; }
		public string Slug { get; set; }
		public Status Status { get; set; }

		public virtual ICollection<ArticleTag> ArticleTags { get; set; }
	}
}
