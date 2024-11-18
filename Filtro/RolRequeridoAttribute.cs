using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class RolRequeridoAttribute : ActionFilterAttribute
{
     private readonly Rol _requeridoRol;

    public RolRequeridoAttribute(Rol requeridoRol)
    {
        _requeridoRol = requeridoRol;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var rolUsuario = context.HttpContext.Session.GetString("NivelAcceso");

        if (rolUsuario != _requeridoRol.ToString())
        {
            context.Result = new RedirectToActionResult("Index", "Home", null); // Redirigir si no es Admin
        }

        base.OnActionExecuting(context);
    }
}