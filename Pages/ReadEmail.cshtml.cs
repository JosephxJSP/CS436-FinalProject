using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace FinalProject.Pages
{
    public class ReadEmailModel : PageModel
    {
        public String EmailID { get; set; }
        public String EmailSubject { get; set; }
        public String EmailMessage { get; set; }
        public String EmailDate { get; set; }
        public String EmailSender { get; set; }

        public IActionResult OnGet(string emailId)
        {
            try
            {
                String connectionString = "Server=tcp:jsp-buemail.database.windows.net,1433;Initial Catalog=jsp-buemail;Persist Security Info=False;User ID=nawapat;Password=Aa123456;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // อัพเดทข้อมูลการเปิดอ่าน
                    String updateSql = $"UPDATE emails SET emailisread = 1 WHERE emailid = '{emailId}'";
                    using (SqlCommand updateCommand = new SqlCommand(updateSql, connection))
                    {
                        updateCommand.ExecuteNonQuery();
                    }

                    // ดึงข้อมูลอีเมลจาก emailid
                    String sql = $"SELECT * FROM emails WHERE emailid = '{emailId}'";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                EmailID = reader.GetInt32(0).ToString();
                                EmailSubject = reader.GetString(1);
                                EmailMessage = reader.GetString(2);
                                EmailDate = reader.GetDateTime(3).ToString();
                                EmailSender = reader.GetString(5);

                                return Page();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return RedirectToPage("/Index");
        }
    }
}
