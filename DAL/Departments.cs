using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveChat.DAL
{
    public partial class Departments : Manager
    {
        public static IList<Department> FetchOnline()
        {
            return ExecuteQuery((dc) =>
                {
                    var dep = from d in dc.Departments
                              where d.DepartmentOperators.Count(o => o.Operator.IsOnline) > 0
                              select d;

                    return dep.OrderBy(d => d.DepartmentName).ToList();
                }, "Unable to fetch for online department");
        }
        public static IList<Department> Fetch()
        {
            return ExecuteQuery((dc) =>
                {
                    return dc.Departments.OrderBy(d => d.DepartmentName).ToList();
                }, "Unable to fetch departments");
        }

        public static int Create(string departmentName)
        {
            return ExecuteQuery((dc) =>
                {
                    Department d = new Department();
                    d.DepartmentName = departmentName;
                    d.IsActive = true;
                    dc.Departments.InsertOnSubmit(d);
                    dc.SubmitChanges();
                    return d.DepartmentId;
                }, "Unable to add the department.");
        }

        public static bool Save(Department entity)
        {
            return ExecuteNonQuery((dc) =>
                {
                    Department original = dc.Departments.FirstOrDefault(d => d.DepartmentId == entity.DepartmentId);
                    if (original != null)
                    {
                        original.DepartmentName = entity.DepartmentName;
                        original.IsActive = entity.IsActive;
                        dc.SubmitChanges();
                    }
                    return true;
                }, "Unable to save the department");
        }

        public static bool Remove(int departmentId)
        {
            return ExecuteNonQuery((dc) =>
                {
                    Department original = dc.Departments.FirstOrDefault(d => d.DepartmentId == departmentId);
                    if (original != null)
                    {
                        dc.Departments.DeleteOnSubmit(original);
                        dc.SubmitChanges();
                    }
                    return true;
                }, "Unable to remove the department.");
        }

        public static bool AddOperator(int departmentId, int operatorId)
        {
            return ExecuteNonQuery((dc) =>
                {
                    DepartmentOperator depOpp = new DepartmentOperator();
                    depOpp.DepartmentId = departmentId;
                    depOpp.OperatorId = operatorId;
                    dc.DepartmentOperators.InsertOnSubmit(depOpp);
                    dc.SubmitChanges();
                    return true;
                }, "Unable to add operator to department.");
        }

        public static bool RemoveOperator(int departmentId, int operatorId)
        {
            return ExecuteNonQuery((dc) =>
                {
                    DepartmentOperator depOpp = dc.DepartmentOperators.FirstOrDefault(d => d.DepartmentId == departmentId && d.OperatorId == operatorId);
                    if (depOpp != null)
                    {
                        dc.DepartmentOperators.DeleteOnSubmit(depOpp);
                        dc.SubmitChanges();
                    }
                    return true;
                }, "Unable to remove the operator from the department.");
        }

        public static List<Operator> FetchOperator(int departmentId)
        {
            return ExecuteQuery((dc) =>
                {
                    var ops = from dep in dc.DepartmentOperators
                              where dep.DepartmentId == departmentId
                              select dep.Operator;

                    return ops.ToList();
                }, "Unable to fetch for operator in department " + departmentId);
        }
    }
}
