using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;

namespace FinalProject.Pages
{
    public class DeleteEmailModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync(string emailId)
        {
            try
            {
                String connectionString = "Server=tcp:jsp-buemail.database.windows.net,1433;Initial Catalog=jsp-buemail;Persist Security Info=False;User ID=nawapat;Password=Aa123456;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    String sql = "DELETE FROM emails WHERE emailid=@EmailId";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@EmailId", emailId);
                        await command.ExecuteNonQueryAsync();
                    }
                }

                // Optionally, you can redirect to another page after deletion.
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return RedirectToPage("/Error"); // You might want to handle errors more gracefully
            }
        }
    }
}
