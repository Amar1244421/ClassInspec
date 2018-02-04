using System.Web.Mvc;

namespace SchoolManagement.Areas.School_Registrar
{
    public class School_RegistrarAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "School_Registrar";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "School_Registrar_default",
                "School_Registrar/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}