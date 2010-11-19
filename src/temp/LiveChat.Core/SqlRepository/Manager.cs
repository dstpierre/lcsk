using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Configuration;

namespace LiveChat.Core.SqlRepository
{
    public class Manager
    {
        protected static T Execute<T>(Func<Data.LCSKDataContext, T> query, string errorMessage)
        {
            try
            {
                using (Data.LCSKDataContext db = new Data.LCSKDataContext(ConfigurationManager.ConnectionStrings["LCSK"].ToString()))
                {
#if DEBUG
                    //db.Log = new DebuggerWriter();
#endif
                    return query(db);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex.Message + "\r\n" + ex.StackTrace.ToString());
                throw;
#else
                Trace.TraceError("DataBase exception: " + ex.Message + "\r\n----------------------" + ex.StackTrace);
                return default(T);
#endif
            }
        }
    }
}
