using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace LiveChat.DAL
{
    public class Manager
    {

        static O RunCatchAndTrace<O>(Func<LiveChatDataContext, O> q, string em, O valueIfError)
        {
            try
            {
                using (LiveChatDataContext dc = new LiveChatDataContext(Properties.Settings.Default.DBConnectionString)) { return q(dc); }
            }
            catch (Exception e)
            {
                Trace.TraceInformation(em + "\n" + e);
                Debug.WriteLine(em + "\n" + e);
                return valueIfError;
            }
        }

        protected static bool ExecuteNonQuery(Func<LiveChatDataContext, bool> q, string em)
        {
            return RunCatchAndTrace(q, em, false);
        }

        /// <summary>
        /// This is a skeleton method for fetch operations. It's a delegate that will perform
        /// the operation and will trap all exceptions to put an given error message to the
        /// Trace class.
        /// </summary>
        /// <typeparam name="O">Type of the output of the fetch operation.</typeparam>
        /// <param name="q">Any function that fetch some data from the DataContext.</param>
        /// <param name="em">Default error message to send to the Trace class if fetch fail.</param>
        /// <returns>
        /// The result of the fetch operation or default(O) if an exception has been raised.
        /// </returns>
        protected static O ExecuteQuery<O>(Func<LiveChatDataContext, O> q, string em)
        {
            return RunCatchAndTrace(q, em, default(O));
        }

    }
}
