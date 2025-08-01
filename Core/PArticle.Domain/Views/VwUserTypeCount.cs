using Domain.Contracts.Enums;
using Domain.Contracts.Interfaces;

namespace PArticle.Domain.Views
{
	public class VwUserTypeCount : IViewBase
	{
		public UserType UserType { get; set; }
		public bool IsActive { get; set; }
		public int Count { get; set; }
	}
}
