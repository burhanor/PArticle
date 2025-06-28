using MediatR;
using PArticle.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Features.Article.Commands.InsertArticleView
{
	public class InsertArticleViewCommandRequest(int articleId):IRequest<ResponseContainer<InsertArticleViewCommandResponse>>
	{
		public int ArticleId { get; set; } = articleId;
		public string IpAddress { get; set; }=string.Empty;
		public DateTime ViewDate { get; set; } = DateTime.Now;
	}
}
