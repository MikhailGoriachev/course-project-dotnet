using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace HotelClassLibrary.Context.Configuration
{
    public class HotelInitializer : IDatabaseInitializer<HotelDB>
    {
        // инициализация базы данных
        public void InitializeDatabase(HotelDB db)
        {
            // если базы данных не существует
            if (!db.Database.Exists())
                db.Database.Create();
        }
    }
}
