using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Utils;
using Utils.Models;

namespace OnlineVisaApplicationSystem.Pages.Dashboard
{
    public class VisaApplicationModel : PageModel
    {
        [BindProperty]
        public List<VisaTypeModel> AvailableVisaTypes { get; set; }
        [BindProperty]
        public Utils.Models.VisaApplicationModel VisaApplication { get; set; }
        public void OnGet()
        {
            GetVisaTypes();
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
                    VisaApplication.UserID = GlobalData.CurrentUser.UserID;
                    VisaApplication.Applicant.Country = "";
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var jsn = JsonConvert.SerializeObject(VisaApplication);
                    var content = new StringContent(jsn, Encoding.UTF8, "application/json");
                    var response = client.PostAsync(GlobalData.baseUrl + "/api/VisaApplication/Add", content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var json1 = JsonConvert.DeserializeObject<ResponseModel>(response.Content.ReadAsStringAsync().Result);
                        TempData["ApplicationID"] = 1;
                        return RedirectToPage("/Dashboard/DocumentUpload");
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
        private void GetVisaTypes()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    //var jsn = JsonConvert.SerializeObject(user);
                    //var content = new StringContent(jsn, Encoding.UTF8, "application/json");
                    var response = client.GetAsync(GlobalData.baseUrl + "api/VisaType/GetList").Result;

                    if (response.IsSuccessStatusCode)
                    { 
                        var json1 = JsonConvert.DeserializeObject<List<VisaTypeModel>>(response.Content.ReadAsStringAsync().Result);
                        AvailableVisaTypes=json1;
                    }
                    else
                    {
                        TempData["AlertMessage"] = response.ReasonPhrase;
                        TempData["AlertType"] = "danger"; // You can use different types like "success", "info", "warning", "danger"
                         
                    }
                }
                catch (Exception ex)
                {
                   
                     
                }
            }
        }
    }
}
