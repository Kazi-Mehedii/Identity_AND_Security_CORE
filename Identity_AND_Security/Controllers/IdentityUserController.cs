using Identity_AND_Security.Areas.Identity.Data;
using Identity_AND_Security.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity_AND_Security.Controllers
{
    public class IdentityUserController : Controller
    {
        private readonly UserManager<Identity_AND_SecurityUserAcess> _userManager;
        IHostEnvironment _environment;

        public IdentityUserController(UserManager<Identity_AND_SecurityUserAcess> userManager, IHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _environment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View(_userManager.Users.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Profile_ViewModel profile_ViewModel)
        {
            if (User.Identity.IsAuthenticated)
            {
               var uid = User.Identity.Name;
               var user = _userManager.Users.Where(u => u.UserName.Equals(uid)).FirstOrDefault();
                if (user != null)
                {
                    user.FullName = profile_ViewModel.FullName;
                    if (profile_ViewModel.Picture != null)
                    {
                        string ext = Path.GetExtension(profile_ViewModel.Picture.FileName);
                        if (ext == ".jpg"|| ext == ".jpeg" || ext == "png")
                        {
                            string root = this._environment.ContentRootPath;
                            string filetosave = Path.Combine(root,"wwwroot/Pictures", profile_ViewModel.Picture.FileName);
                            using (var fileStream = new FileStream(filetosave, FileMode.Create))
                            {
                                await profile_ViewModel.Picture.CopyToAsync(fileStream);
                            }
                            user.PicPath = "~/Pictures/" + profile_ViewModel.Picture.FileName;                        
                        }
                        var result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index");
                        }
                        if (result.Errors.Count()>0)
                        {
                            string ms = "";
                            foreach (var error in result.Errors)
                            {
                                ms += error + "\t";
                            }
                            ModelState.AddModelError(string.Empty, ms); 
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Please Login");
                }           
            }
            return View(profile_ViewModel);
        }
    }
}
