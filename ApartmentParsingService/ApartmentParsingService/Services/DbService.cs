using Microsoft.Data.Sqlite;

namespace ApartmentParsingService.Services
{
    public class DbService
    {
        public SqliteConnection connection { get; }
        public DbService() {
            using (connection = new SqliteConnection("Data Source=SubscriptionsData.db"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = "CREATE TABLE IF NOT EXISTS Subscriptions(_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Email TEXT NOT NULL, link TEXT NOT NULL)";
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM Subscriptions";
                command.ExecuteNonQuery();
                //Добавим тестовые начальные данные
                string sqlExpression = "INSERT INTO Subscriptions(email, link) VALUES " +
                    "('email@my.ru', 'https://prinzip.su/apartments/parkovyj/60034')," +
                    " ('mmmm@com.com', 'https://prinzip.su/apartments/l8/57436')," +
                    "('mymail@ekb.com', 'https://prinzip.su/apartments/newtonpark/62267')";
                command = new SqliteCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
