using Dodic.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DoDic_pizza.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DoDic_pizza.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationContext _context;

        public ProfileController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Profile()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;
            if (userId == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                                     .FirstOrDefaultAsync(u => u.id == long.Parse(userId));

            if (user == null)
            {
                return NotFound();
            }

            // Создаем модель профиля, используя данные пользователя
            var model = new ProfileViewModel
            {
                Id = user.id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Birthdate = user.Birthdate.ToString() 
            };


            return View(model);
        }

        public async Task<IActionResult> EditProfile()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;
            if (userId == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                                     .FirstOrDefaultAsync(u => u.id == long.Parse(userId));

            if (user == null)
            {
                return NotFound();
            }

            // Создаем модель профиля, используя данные пользователя
            var model = new ProfileViewModel
            {
                Id = user.id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Birthdate = user.Birthdate
            };

            return View(model);
        }
    }
}
