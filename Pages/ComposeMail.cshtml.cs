using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System;

namespace FinalProject.Pages
{
    public class ComposeMailModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Sender { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost(string recipient, string subject, string message)
        {
            try
            {
                String connectionString = "Server=tcp:jsp-buemail.database.windows.net,1433;Initial Catalog=jsp-buemail;Persist Security Info=False;User ID=nawapat;Password=Aa123456;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // ใส่ข้อมูลลงในฐานข้อมูล
                    string insertSql = "INSERT INTO emails (emailsubject, emailmessage, emailisread, emailsender, emailreceiver) " +
                                       "VALUES (@Subject, @Message, 0, @Sender, @Recipient)";

                    using (SqlCommand command = new SqlCommand(insertSql, connection))
                    {
                        // เซ็ทค่าเพื่อใส่ข้อมูลลงในฐานข้อมูล
                        command.Parameters.AddWithValue("@Subject", subject);
                        command.Parameters.AddWithValue("@Message", message);
                        command.Parameters.AddWithValue("@Sender", Sender);
                        command.Parameters.AddWithValue("@Recipient", recipient);

                        // รัน SQL Query
                        command.ExecuteNonQuery();
                    }
                }

                // กลับหน้า Index หลังจากบันทึกเสร็จ
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Page();
            }
        }
    }
}
