using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System;
using Utils.Models;
using Utils;
using Newtonsoft.Json;
using System.Text;

namespace OnlineVisaApplicationSystem.Pages.Register
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public UserModel user { get; set; }
        [BindProperty]
        public string ConfirmPassword { get; set; }
        public void OnGet()
        {
        }
        private bool Validate()
        {
            if (ConfirmPassword != user.Password) return false;
            else if (user.Username != null) return false
                    ;
            else return true;
        }
        public IActionResult OnPost()
        {
            if (Validate())
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
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var jsn = JsonConvert.SerializeObject(user);
                        var content = new StringContent(jsn, Encoding.UTF8, "application/json");
                        var response = client.PostAsync(GlobalData.baseUrl + "api/User/Add", content).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToPage("/Register/Login");
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
            }else return Page();
        }
        public IActionResult OnPostClear()
        {
            user = new UserModel();
            return Page();
        }
    }
}
