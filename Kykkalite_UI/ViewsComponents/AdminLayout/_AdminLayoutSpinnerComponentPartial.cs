using Microsoft.AspNetCore.Mvc;

namespace Kykkalite_UI.ViewsComponents.AdminLayout
{
    public class _AdminLayoutSpinnerComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
