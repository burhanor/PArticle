﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Subscribers.Core.Models
{
	public class RedisModel
	{
		public string Host { get; set; }
		public int Port { get; set; }
		public string Password { get; set; }
	}
}
