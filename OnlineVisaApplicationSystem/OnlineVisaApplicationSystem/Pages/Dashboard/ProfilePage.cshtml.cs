using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System;
using Utils;
using Utils.Models;
using System.Text;

namespace OnlineVisaApplicationSystem.Pages.Dashboard
{
    public class ProfilePageModel : PageModel
    {
        [BindProperty]
        public UserModel user { get; set; }
        public void OnGet()
        { 
            user=GlobalData.CurrentUser;
        }
        public IActionResult OnPost()
        {

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    user.UserID = GlobalData.CurrentUser.UserID;
                    user.Password = GlobalData.CurrentUser.Password;
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var jsn = JsonConvert.SerializeObject(user);
                    var content = new StringContent(jsn, Encoding.UTF8, "application/json");
                    var response = client.PutAsync(GlobalData.baseUrl + "api/User/Update", content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        return Page();
                    }
                    else
                    {
                        return Page();
                    }
                }
                catch (Exception ex)
                {
                    return Page();
                }
            }
        }

         
         
        }
}
