﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Models
{
	public class PaginationContainer<T> where T : class
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public int TotalCount { get; set; }
		public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
		public bool HasPreviousPage => PageNumber > 1;
		public bool HasNextPage => PageNumber < TotalPages;
		public List<T> Items { get; set; } = [];

		public PaginationContainer()
		{
			
		}
		public PaginationContainer(int? pageNumber, int? pageSize, int totalCount)
		{
			PageNumber = pageNumber??1;
			PageSize = pageSize??10;
			TotalCount = totalCount;
		}
	}
}
