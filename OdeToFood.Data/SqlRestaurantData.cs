using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
	public class SqlRestaurantData : IRestaurantData
	{
		private readonly OdeToFoodDbContext db;
		public SqlRestaurantData(OdeToFoodDbContext db)
		{
			this.db = db;
		}
		public Restaurant Add(Restaurant newRestaurant)
		{
			this.db.Add(newRestaurant);
			return newRestaurant;
		}

		public int Commit()
		{
			return this.db.SaveChanges();
		}

		public Restaurant Delete(int id)
		{
			var restaurant = GetbyId(id);
			if (restaurant != null)
			{
				this.db.Remove(restaurant);
			}
			return restaurant;
		}

		public Restaurant GetbyId(int id)
		{
			return this.db.Restaurants.Find(id);
		}

		public IEnumerable<Restaurant> GetRestaurantsByName(string name)
		{
			var query = from r in db.Restaurants
						where r.Name.StartsWith(name) || string.IsNullOrEmpty(name)
						orderby r.Name
						select r;
			return query;
		}

		public Restaurant Update(Restaurant updatedRestaurant)
		{
			var entity = this.db.Restaurants.Attach(updatedRestaurant);
			entity.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			return updatedRestaurant;
		}
	}
}
