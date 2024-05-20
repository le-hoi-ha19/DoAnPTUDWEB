using DoAn_PTUDWEB.Constains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace DoAn_PTUDWEB.Filters
{
    public class AdminRequired : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext filterContext) {
            var role = filterContext.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            if (role == null || role != Roles.Admin)
                filterContext.Result = new ForbidResult();
        }
    }
}
