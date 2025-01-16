using Microsoft.AspNetCore.Mvc;

public class BaseController : Controller
{
    protected IActionResult SetSessionData()
    {
        var username = HttpContext.Session.GetString("Username");
        var userid = HttpContext.Session.GetInt32("Userid");
        var rolename = HttpContext.Session.GetString("Rolename");
        var roleid = HttpContext.Session.GetInt32("Roleid");

        if (string.IsNullOrEmpty(username) || userid == null)
        {
            RedirectToAction("Login", "Login");
        }

        ViewBag.Username = username;
        ViewBag.Rolename = rolename;
        ViewBag.userid = userid;
        return null; // Return null if the session is valid



        //var result = SetSessionData();
        //if (result != null) return result; // Redirect if the session is invalid

    }
}
