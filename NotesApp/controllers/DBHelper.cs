using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace NotesApp.controllers
{
    class DBHelper
    {
        SqlConnection sqlConnection = new SqlConnection();

        public DBHelper()
        {
            sqlConnection.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + System.Environment.CurrentDirectory + @"\db\Notes.mdf;Integrated Security=True";
        }

        public DataTable ExecuteSQLQuery(string query)
        {
            sqlConnection.Open();
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(query, sqlConnection);
            adapter.Fill(table);
            sqlConnection.Close();
            return table;
        }

        public void ExecuteSQLQueryWithoutReturn(string query)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand(query, sqlConnection);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public DataTable GetAllTable()
        {
            return ExecuteSQLQuery("SELECT * FROM [dbo].[Table] ORDER BY CREATION_TIME DESC");
        }

        public DataTable GetRowById(int id)
        {
            return ExecuteSQLQuery("SELECT * FROM [dbo].[Table] WHERE id = " + id);
        }

        public int Insert(string color, string note)
        {
            int id = new Random().Next();
            ExecuteSQLQueryWithoutReturn(String.Format("INSERT INTO [dbo].[Table] ([ID], [CREATION_TIME], [COLOR], [NOTE]) VALUES ({0}, N'{1}', N'{2}', N'{3}')", id, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), color, note));
            return id;
        }

        public void Update(int id, string date, string color, string note)
        {
            ExecuteSQLQueryWithoutReturn(String.Format("UPDATE [dbo].[Table] SET CREATION_TIME = N'{1}', COLOR = '{2}', NOTE = '{3}' WHERE ID = {0}", id, date, color, note));
        }

        public void Delete(int id)
        {
            ExecuteSQLQueryWithoutReturn(String.Format("DELETE FROM [dbo].[Table] WHERE ID = {0}", id));
        }
    }
}
