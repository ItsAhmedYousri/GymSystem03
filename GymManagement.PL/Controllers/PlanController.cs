using GymManagement.DAL.Data.Models;
using GymManagement.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymSystem01.Controllers
{
    public class PlanController : Controller
    {
        private readonly IGenericRepository<Plan> planRepository;
        public PlanController(IGenericRepository<Plan> repository)
        {
            planRepository = repository;
        }
        public async Task<IActionResult> Index(CancellationToken ct=default)
        {
            var plans = await planRepository.GetAllAsync(ct:ct);
            return View(plans);
        }
        public async Task<IActionResult> Details(int Id,CancellationToken ct=default)
        {
            var plan =await  planRepository.GetByIDAsync(Id,ct);
            if(plan == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(plan);

            }




        }
    }
}
