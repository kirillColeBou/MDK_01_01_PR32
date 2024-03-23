using System.Data;
using System.Data.SqlClient;
using VinylRecordsApplication_Тепляков.Pages;

namespace VinylRecordsApplication_Тепляков.Classes
{
    public class DBConnection
    {
        public static DataTable Connection(string SQL)
        {
            if (Pages.ChangeSettings.ConnectIsApply == true)
            {
                DataTable dataTable = new DataTable("Datatable");
                SqlConnection sqlConnection = ChangeSettings.Connect;
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = SQL;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);
                sqlConnection.Close();
                return dataTable;
            }
            return null;
        }
    }
}
