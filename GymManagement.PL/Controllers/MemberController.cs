using GymManagment.BLL.Services.Interfaces;
using GymManagment.BLL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymSystem.PL.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        //GET: Member/Index
        public async Task<IActionResult> Index(CancellationToken ct = default)
        {
            var members = await _memberService.GetAllMembersAsync(ct);

            return View(members);
        }
        [HttpGet]
        public IActionResult Create() => View();

        public async  Task<IActionResult >CreateMember(CreateMemberViewModel model,CancellationToken ct = default)
        {
            if (!ModelState.IsValid) return View(nameof(Create), model);

            var result =await  _memberService.CreateMemberAsync(model, ct);

            if (result)
                TempData["SuccessMessage"] = "Member Created Successfully";
            else
                TempData["ErrorMessage"] = "Member Failed To Create";


            return RedirectToAction(nameof(Index)); 
        }
    }
}
