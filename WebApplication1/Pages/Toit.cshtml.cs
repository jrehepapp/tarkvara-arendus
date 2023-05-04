using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace WebApplication1.Pages
{
    public class ToitModel : PageModel
    {
        public List<string> PostTitles { get; set; }
        public List<string> Contentlist { get; set; }
        public List<string> Datelist { get; set; }
        public int titlecount { get; set; }
        public int contentcount { get; set; }
        public int datecount { get; set; }

        public void OnGet()
        {
            PostTitles = GetAllPostTitles("title");
            Contentlist = GetAllPostTitles("content");
            Datelist = GetAllPostTitles("date");
            titlecount = postcount(PostTitles);
            contentcount = postcount(Contentlist);
            datecount = postcount(Datelist);
        }

        public List<string> GetAllPostTitles(string Column)
        {
            List<string> postTitles = new List<string>();
            string connectionString = "Server=vhk-12r.database.windows.net;User Id=User3;pwd=Fix7ZSuy;Database=Kuubik;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuery = $"SELECT {Column} FROM posts where topic = 'toit' order by date, postid desc;";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                postTitles.Add(dataReader[0].ToString());
                            }
                        }
                        else
                        {
                            postTitles.Add("No data with this Id!");
                        }
                    }
                }
                connection.Close();
            }
            return postTitles;
        }
        public int postcount(List<string> posts) {
            int countt = 0;
            foreach(var i in posts) {
                countt ++;
            }
            return countt;
        }
    }
}
