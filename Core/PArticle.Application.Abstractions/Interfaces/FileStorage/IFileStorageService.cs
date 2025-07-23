using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Abstractions.Interfaces.FileStorage
{
	public interface IFileStorageService
	{
		Task<string> SaveFileAsync(Stream content, string fileName);

	}
}
