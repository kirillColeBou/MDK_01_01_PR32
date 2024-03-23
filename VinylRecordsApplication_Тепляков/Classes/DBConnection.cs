using System.Data;
using System.Data.SqlClient;

namespace VinylRecordsApplication_Тепляков.Classes
{
    public class DBConnection
    {
        public static DataTable Connection(string SQL)
        {
            DataTable dataTable = new DataTable("Datatable");
            SqlConnection sqlConnection = new SqlConnection("Server=USER\\SQLEXPRESS;Trusted_Connection=No;DataBase=VinylRecords;user=sa;pwd=Asdfg123");
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = SQL;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
    }
}
