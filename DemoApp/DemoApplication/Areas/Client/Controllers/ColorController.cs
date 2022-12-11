using DemoApplication.Areas.Client.ViewModels.Colors;
using DemoApplication.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DemoApplication.Areas.Client.Controllers
{
    [Area("client")]
    [Route("color")]
    public class ColorController : Controller
    {
        private readonly DataContext _datacontext;

        public ColorController(DataContext datacontext)
        {
            _datacontext = datacontext;
        }
        [HttpGet("add/{id}", Name = "client-color-add")]
        public async Task<IActionResult> AddColorAsync([FromRoute] int id)
        {
            var color = await _datacontext.Colors.FirstOrDefaultAsync(clr => clr.Id == id);

            var colorCookieValue = HttpContext.Request.Cookies["Color"];

            if (colorCookieValue is null)
            {
                var colorsCookieViewModel = new ColorViewModel(color.Id, color.Name);

                HttpContext.Response.Cookies.Append("Color", JsonSerializer.Serialize(colorsCookieViewModel));
            }
            else
            {
                var colorsCookieViewModel = JsonSerializer.Deserialize<ColorViewModel>(colorCookieValue);

                colorsCookieViewModel = new ColorViewModel(color.Id, color.Name);

                HttpContext.Response.Cookies.Append("Color", JsonSerializer.Serialize(colorsCookieViewModel));
            }

            return RedirectToRoute("client-home-index");
        }
    } 

}

