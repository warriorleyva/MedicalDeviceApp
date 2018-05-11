using System;
using SQLite;

namespace MedicalPhotonDevice.Persistence
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
