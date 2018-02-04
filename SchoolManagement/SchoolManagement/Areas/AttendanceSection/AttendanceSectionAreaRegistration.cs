using System.Web.Mvc;

namespace SchoolManagement.Areas.AttendanceSection
{
    public class AttendanceSectionAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AttendanceSection";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AttendanceSection_default",
                "AttendanceSection/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}