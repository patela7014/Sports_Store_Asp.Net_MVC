using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Concrete
{
	public class NinjectProductRepository : IProductRepository
	{
		public IEnumerable<Product> Products
		{
			get { return products; }
		}

		private List<Product> products = new List<Product>()
		{
			new Product { Name = "Basketball", Category = "Basketball", Description = "A basketball", Price = 15, ProductId = 1},
			new Product { Name = "Baseball", Category = "Baseball", Description = "A baseball", Price = 15, ProductId = 2 },
			new Product { Name = "Football", Category = "Football", Description = "A football", Price = 15, ProductId = 3 },
			new Product { Name = "Soccer Ball", Category = "Soccer", Description = "A soccer ball", Price = 15, ProductId = 4 }
		};
	}
}
