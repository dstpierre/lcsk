using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveChat.DAL
{
    public partial class Operators : Manager
    {
        public static IList<Operator> Fetch()
        {
            return ExecuteQuery((dc) =>
                {
                    var list = from o in dc.Operators
                               orderby o.Name
                               select o;
                    return list.ToList();
                }, "Fetch: Unable to fetch the operators.");
        }

        public static IList<Operator> Fetch(bool isOnline)
        {
            return ExecuteQuery((dc) =>
            {
                return dc.Operators.Where(o => o.IsOnline == isOnline).ToList();
            }, "Fetch: Unable to fetch the operators.");
        }

        public static Operator Fetch(int operatorId)
        {
            return ExecuteQuery((dc) =>
                {
                    return dc.Operators.FirstOrDefault(o => o.OperatorId == operatorId);
                }, "Fetch by id: unable to fetch the operator.");
        }        


        public static int Create(string name, string password, string email, bool isAdmin)
        {
            return ExecuteQuery((dc) =>
                {
                    var op = new Operator();
                    op.Name = name;
                    op.Password = password;
                    op.Email = email;
                    op.IsAdmin = isAdmin;
                    op.IsOnline = false;
                    dc.Operators.InsertOnSubmit(op);
                    dc.SubmitChanges();
                    return op.OperatorId;
                }, "Create: Unable to create an operator.");
        }

        public static bool Save(int operatorId, string name, string password, string email, bool isAdmin)
        {
            return ExecuteNonQuery((dc) =>
                {
                    var current = dc.Operators.FirstOrDefault(op => op.OperatorId == operatorId);
                    if( current != null )
                    {
                        current.Name = name;
                        current.Password = password;
                        current.Email = email;
                        current.IsAdmin = isAdmin;
                        current.IsOnline = false;
                        dc.SubmitChanges();
                        return true;
                    }
                    return false;
                }, "Save: Unable to save the operator.");
        }

        public static Operator LogIn(string name, string password)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(password))
            {
                return ExecuteQuery((dc) =>
                {
                    return dc.Operators.FirstOrDefault(o => o.Name == name && o.Password == password);
                }, "LogIn: Unable to perform the log in.");
            }
            else
                return null;
        }

        public static bool IsOperatorAvailable()
        {
            return ExecuteNonQuery((dc) =>
                {
                    return dc.Operators.Count(o => o.IsOnline) > 0;
                }, "IsChatAvailable: Unable to determine if chat is available.");
        }

        public static bool ChangeStatus(int operatorId, bool isOnline)
        {
            return ExecuteNonQuery((dc) =>
                {
                    var o = dc.Operators.FirstOrDefault(op => op.OperatorId == operatorId);
                    if (o != null)
                    {
                        o.IsOnline = isOnline;
                        dc.SubmitChanges();
                        return true;
                    }
                    else
                        return false;
                }, "Unable to change operator status.");
        }

        public static bool HasNewRequests(int operatorId)
        {
            return ExecuteQuery((dc) =>
                {
                    return dc.Channels.Count(c => c.OperatorId == operatorId && c.AcceptDate == null) > 0;
                    
                }, "HasNewRequests: Failed to get new chat channel.");
        }

        public static bool Remove(int operatorId)
        {
            return ExecuteNonQuery((dc) =>
                {
                    var op = dc.Operators.FirstOrDefault(o => o.OperatorId == operatorId);
                    if (op != null)
                        dc.Operators.DeleteOnSubmit(op);
                    return true;
                }, "Unable to remove the operator.");
        }
    }
}
