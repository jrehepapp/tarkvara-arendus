using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication1.Pages
{
    public class PostModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {

        }
        public void OnPost()
        {
            var title = Request.Form["title"];
            var content = Request.Form["content"];
            var topic = Request.Form["topic"];
            GetAllPostTitles(title, content, topic);
        }
        public void GetAllPostTitles(string title, string content, string topic)
        {
            if (title == "" || content == "" || topic == "") {
                ViewData["nodata"] = "Kõik alad peavad olema täidetud!";
            } else {
                ViewData["nodata"] = "";
                string connectionString = "Server=vhk-12r.database.windows.net;User Id=User3;pwd=Fix7ZSuy;Database=Kuubik;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = $"insert into posts (title, content, topic) values ('{title}', '{content}', '{topic}');";
                    SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                    cmd.ExecuteReader();
                    connection.Close();
                }
            }
        }
    }
}
