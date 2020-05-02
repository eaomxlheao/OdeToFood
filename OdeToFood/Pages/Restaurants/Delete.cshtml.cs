using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
	public class DeleteModel : PageModel
	{
		private readonly IRestaurantData restaurantData;
		public Restaurant Restaurant { get; set; }

		public DeleteModel(IRestaurantData restaurantData)
		{
			this.restaurantData = restaurantData;
		}

		public ActionResult OnGet(int id)
		{
			Restaurant = restaurantData.GetbyId(id);
			if (Restaurant == null)
			{
				return RedirectToPage("./NotFound");
			}
			return Page();
		}

		public ActionResult OnPost(int id)
		{
			Restaurant = restaurantData.Delete(id);
			restaurantData.Commit();
			if (Restaurant == null)
			{
				return RedirectToPage("./NotFound");
			}

			TempData["Message"] = $"{Restaurant.Name} deleted!";
			return RedirectToPage("./List");
		}
	}
}
