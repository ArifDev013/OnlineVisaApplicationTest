using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Utils;
using Utils.Models;

namespace OnlineVisaApplicationSystem.Pages.Dashboard
{
    public class AppliedVisaListModel : PageModel
    {
        [BindProperty]
        public List<Utils.Models.VisaApplicationModel> VisaApplicationList { get; set; }
        public void OnGet()
        {
            GetVisaList();
        }

        private void GetVisaList()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    //var jsn = JsonConvert.SerializeObject(user);
                    //var content = new StringContent(jsn, Encoding.UTF8, "application/json");
                    var response = client.GetAsync(GlobalData.baseUrl + "api/VisaApplication/GetList").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var json1 = JsonConvert.DeserializeObject<List<Utils.Models.VisaApplicationModel>>(response.Content.ReadAsStringAsync().Result);
                        VisaApplicationList = json1;
                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {


                }
            }
        }
    }
}
