using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Configuration;

namespace LCSK.Core
{
    public class Manager
    {
        protected static T Execute<T>(Func<LCSKDb, T> query, string errorMessage)
        {
            try
            {
                using (LCSKDb db = new LCSKDb())
                {
                    return query(db);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex.Message + "\r\n" + ex.StackTrace.ToString());
                throw;
#else
                //TODO: log this ("DataBase exception: " + ex.Message + "\r\n----------------------" + ex.StackTrace);
                return default(T);
#endif
            }
        }
    }
}
