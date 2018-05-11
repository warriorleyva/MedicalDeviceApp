using System;
using System.IO;
using SQLite;
using MedicalPhotonDevice.Droid.Persistence;
using MedicalPhotonDevice.Persistence;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDb))]


namespace MedicalPhotonDevice.Droid.Persistence
{
    public class SQLiteDb :ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "MySQLite2.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}
