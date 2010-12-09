using System;

namespace LCSK.Core
{
	public interface IPagedList
	{
		int CurrentPage { get; }
		bool HasNextPage { get; }
		bool HasPreviousPage { get; }
		int ItemsPerPage { get; }
		int TotalItems { get; }
		int TotalPages { get; }
	}
}