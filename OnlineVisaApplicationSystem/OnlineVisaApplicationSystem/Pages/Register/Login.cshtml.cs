using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System;
using Utils;
using Utils.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.Web;

namespace OnlineVisaApplicationSystem.Pages.Register
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string username { get; set; }
        [BindProperty]
        public string password { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                TempData["AlertMessage"] = "All fields are mandatory";
                TempData["AlertType"] = "warning"; // You can use different types like "success", "info", "warning", "danger"

                return Page();
            }
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    //var jsn = JsonConvert.SerializeObject(user);
                    //var content = new StringContent(jsn, Encoding.UTF8, "application/json");
                    var response = client.GetAsync(GlobalData.baseUrl + $"api/User/Authenticate/{username}/{password}").Result;

                    if (response.IsSuccessStatusCode)
                    { 
                        var json1 = JsonConvert.DeserializeObject<UserModel>(response.Content.ReadAsStringAsync().Result);
                        GlobalData.CurrentUser = json1;
                        return RedirectToPage("/Dashboard/VisaApplication");
                    }
                    else
                    {
                        TempData["AlertMessage"] = response.ReasonPhrase;
                        TempData["AlertType"] = "danger"; // You can use different types like "success", "info", "warning", "danger"

                        return Page();
                    }
                }
                catch (Exception ex)
                {
                    TempData["AlertMessage"] = ex.Message;
                    TempData["AlertType"] = "danger"; // You can use different types like "success", "info", "warning", "danger"

                    return Page();
                }
            }

        }
        //private void SaveTokenInCookie(string jwtToken)
        //{

        //    TempData["Token"] = jwtToken;
        //    // Save the JWT token in a secure cookie
        //    Response.Cookies.Append("JwtToken", jwtToken, new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Secure = true,
        //        SameSite = SameSiteMode.None // Adjust SameSite based on your requirements
        //    });
        //}
    }
}
