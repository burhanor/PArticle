﻿using PArticle.ArticleSubscriber.Enums;

namespace PArticle.ArticleSubscriber.Model
{
	public class TagDto
	{
		public string Name { get; set; }
		public string Slug { get; set; }
		public Status Status { get; set; }
	}
}
