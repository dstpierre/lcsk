using System;
using System.Collections.Generic;
using System.Linq;

namespace LCSK.Core
{

	/// <summary>
	/// Adapted from http://blog.wekeroad.com/2007/12/10/aspnet-mvc-pagedlistt/
	/// </summary>
	/// <typeparam name="T">The type of item this list holds</typeparam>
	public class PagedList<T> : List<T>, IPagedList
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PagedList&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="source">The source list of elements containing all elements to be paged over.</param>
		/// <param name="currentPage">The current page number (1 based).</param>
		/// <param name="pageSize">Size of a page (number of items per page).</param>
		public PagedList(IEnumerable<T> source, int currentPage, int itemsPerPage)
		{
			this.TotalItems = source.Count();
			this.ItemsPerPage = itemsPerPage;
			this.CurrentPage = Math.Min(Math.Max(1, currentPage), TotalPages);
			this.AddRange(source.Skip((this.CurrentPage - 1) * itemsPerPage).Take(itemsPerPage).ToList());
		}


		public int CurrentPage { get; private set; }

		public int ItemsPerPage { get; private set; }

		public bool HasPreviousPage { get { return (CurrentPage > 1); } }

		public bool HasNextPage { get { return (CurrentPage * ItemsPerPage) < TotalItems; } }

		public int TotalPages { get { return (int)Math.Ceiling((double)TotalItems / ItemsPerPage); } }

		public int TotalItems { get; private set; }
	}
}