using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveChat.Entities;
using LiveChat.DAL;
using LiveChat.Providers;

namespace LiveChat.SQLProvider
{
    public class SQLOperatorProvider : OperatorProvider
    {
        private static void FillEntity(OperatorEntity newEntity, Operator existingEntity)
        {
            newEntity.EntityId = existingEntity.OperatorId;
            newEntity.Name = existingEntity.Name;
            newEntity.Password = existingEntity.Password;
            newEntity.Email = existingEntity.Email;
            newEntity.IsOnline = existingEntity.IsOnline;
            newEntity.IsAdmin = existingEntity.IsAdmin;
        }

        private static void FillDepartmentEntity(DepartmentEntity newEntity, Department existingEntity)
        {
            newEntity.EntityId = existingEntity.DepartmentId;
            newEntity.DepartmentName = existingEntity.DepartmentName;
            newEntity.IsActive = existingEntity.IsActive;
        }

        public override List<OperatorEntity> GetOnlineOperator()
        {
            List<OperatorEntity> results = new List<OperatorEntity>();
            OperatorEntity entity = null;
            foreach (var o in Operators.Fetch(true))
            {
                entity = new OperatorEntity();
                FillEntity(entity, o);
                results.Add(entity);
            }
            return results;
        }

        public override bool IsOperatorOnline()
        {
            return Operators.IsOperatorAvailable();
        }

        public override int Create(string name, string password, string email, bool isAdmin)
        {
            return Operators.Create(name, password, email, isAdmin);
        }

        public override void UpdateStatus(int operatorId, bool isOnline)
        {
            Operators.ChangeStatus(operatorId, isOnline);
        }

        public override OperatorEntity LogIn(string name, string password)
        {
            var op = Operators.LogIn(name, password);
            OperatorEntity o = new OperatorEntity();
            if (op != null)
            {
                FillEntity(o, op);
            }
            return o;
        }

        public override bool Remove(int operatorId)
        {
            return Operators.Remove(operatorId);
        }

        public override List<ChannelEntity> GetChatChannel(int operatorId)
        {
            List<ChannelEntity> results = new List<ChannelEntity>();
            if (Operators.HasNewRequests(operatorId))
            {
                ChannelEntity entity = null;
                foreach (Channel c in Channels.Fetch(operatorId))
                {
                    entity = new ChannelEntity();
                    entity.EntityId = c.ChannelId.ToString();
                    entity.RequestId = c.RequestId;
                    entity.OperatorId = c.OperatorId;
                    entity.OpenDate = c.OpenDate;
                    entity.AcceptDate = c.AcceptDate;
                    entity.CloseDate = c.CloseDate;
                    results.Add(entity);
                }
            }
            return results;
        }

        public override List<DepartmentEntity> GetOnlineDepartment()
        {
            List<DepartmentEntity> onlineDepartment = new List<DepartmentEntity>();
            DepartmentEntity current = null;
            foreach (Department d in Departments.FetchOnline())
            {
                current = new DepartmentEntity();
                FillDepartmentEntity(current, d);
                onlineDepartment.Add(current);
            }
            return onlineDepartment;

        }

        public override List<OperatorEntity> Fetch()
        {
            List<OperatorEntity> result = new List<OperatorEntity>();
            OperatorEntity entity = null;
            foreach (var op in Operators.Fetch())
            {
                entity = new OperatorEntity();
                FillEntity(entity, op);
                result.Add(entity);
            }

            return result;
        }

        public override bool Save(OperatorEntity updatedEntity)
        {
            return Operators.Save(updatedEntity.EntityId, updatedEntity.Name, updatedEntity.Password, updatedEntity.Email, updatedEntity.IsAdmin);
        }

        public override List<DepartmentEntity> FetchDepartment()
        {
            List<DepartmentEntity> result = new List<DepartmentEntity>();
            DepartmentEntity entity = null;
            foreach (var dep in Departments.Fetch())
            {
                entity = new DepartmentEntity();
                FillDepartmentEntity(entity, dep);
                result.Add(entity);
            }

            return result;
        }

        public override int CreateDepartment(string departmentName)
        {
            return Departments.Create(departmentName);
        }

        public override bool RemoveDepartment(int departmentId)
        {
            return Departments.Remove(departmentId);
        }

        public override bool AddOperatorToDepartment(int departmentId, int operatorId)
        {
            return Departments.AddOperator(departmentId, operatorId);
        }

        public override bool RemoveOperatorFromDepartment(int departmentId, int operatorId)
        {
            return Departments.RemoveOperator(departmentId, operatorId);
        }

        public override List<OperatorEntity> GetOperator(int departmentId)
        {
            List<OperatorEntity> result = new List<OperatorEntity>();
            OperatorEntity entity = null;
            foreach (var op in Departments.FetchOperator(departmentId))
            {
                entity = new OperatorEntity();
                FillEntity(entity, op);
                result.Add(entity);
            }
            return result;
        }
    }
}
