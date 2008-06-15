using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveChat.Entities;
using LiveChat.Providers;

namespace LiveChat.InMemoryProvider
{
    public class InMemoryOperatorProvider : OperatorProvider
    {
        public override List<OperatorEntity> Fetch()
        {
            if (CacheManager.Operators == null)
            {
                List<OperatorEntity> list = new List<OperatorEntity>();
                //TODO: Dynamically load operators
                OperatorEntity op = new OperatorEntity();
                op.Email = "test@123.com";
                op.EntityId = 1;
                op.IsAdmin = true;
                op.IsOnline = true;
                op.Name = "operator";
                op.Password = "operator";
                list.Add(op);

                CacheManager.Operators = list.AsQueryable();
            }
            return CacheManager.Operators.ToList();
        }

        public override List<OperatorEntity> GetOnlineOperator()
        {
            return CacheManager.Operators.Where(o => o.IsOnline).ToList();
        }

        public override bool IsOperatorOnline()
        {
            return CacheManager.Operators.Count(o => o.IsOnline) > 0;
        }

        public override int Create(string name, string password, string email, bool isAdmin)
        {
            throw new NotImplementedException();
        }

        public override void UpdateStatus(int operatorId, bool isOnline)
        {
            throw new NotImplementedException();
        }

        public override bool Save(OperatorEntity updatedEntity)
        {
            throw new NotImplementedException();
        }

        public override OperatorEntity LogIn(string name, string password)
        {
            return CacheManager.Operators.SingleOrDefault(o => o.Name == name && o.Password == password);
        }

        public override bool Remove(int operatorId)
        {
            throw new NotImplementedException();
        }

        public override List<ChannelEntity> GetChatChannel(int operatorId)
        {
            throw new NotImplementedException();
        }

        public override List<DepartmentEntity> GetOnlineDepartment()
        {
            DepartmentEntity dep = new DepartmentEntity();
            dep.EntityId = 1;
            dep.DepartmentName = "Support";

            List<DepartmentEntity> list = new List<DepartmentEntity>();
            list.Add(dep);
            return list;

        }

        public override List<DepartmentEntity> FetchDepartment()
        {
            return GetOnlineDepartment();
        }

        public override int CreateDepartment(string departmentName)
        {
            throw new NotImplementedException();
        }

        public override bool RemoveDepartment(int departmentId)
        {
            throw new NotImplementedException();
        }

        public override bool AddOperatorToDepartment(int departmentId, int operatorId)
        {
            throw new NotImplementedException();
        }

        public override bool RemoveOperatorFromDepartment(int departmentId, int operatorId)
        {
            throw new NotImplementedException();
        }

        public override List<OperatorEntity> GetOperator(int departmentId)
        {
            throw new NotImplementedException();
        }
    }
}
