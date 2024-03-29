﻿using System;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System.Collections.Generic;
using System.Linq;
using Entities.DTOs;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Core.Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, ReCapContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new ReCapContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };

                return result.ToList();
            }
        }

        public GetUserClaimsDto GetClaimsByUserId(int userId)
        {
            using (var context = new ReCapContext())
            {
                var operationClaims = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == userId
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };

                var userClaims = context.Set<UserOperationClaim>().Where(u => u.UserId == userId).ToList();

                return new GetUserClaimsDto() { operationClaim = operationClaims.ToList(), userOperationClaim = userClaims };
            }
        }

        public void AddFindeks(int userId, int findeks)
        {
            using (ReCapContext context = new ReCapContext())
            {
                var updatedUser = Get(u => u.Id == userId);
                updatedUser.Findeks = updatedUser.Findeks + findeks / 10;
                Update(updatedUser);
            }
        }

        public void AddCLaim(UserOperationClaim userOperationClaim)
        {
            using (ReCapContext context = new ReCapContext())
            {
                var addedEntity = context.Entry(userOperationClaim);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void DeleteClaim(UserOperationClaim userOperationClaim)
        {
            using (ReCapContext context = new ReCapContext())
            {
                var deletedEntity = context.Entry(userOperationClaim);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public OperationClaim GetOperationClaim(Expression<Func<OperationClaim, bool>> filter = null)
        {
            using(ReCapContext context = new ReCapContext())
            {
                return filter == null ? context.Set<OperationClaim>().SingleOrDefault() : context.Set<OperationClaim>().SingleOrDefault(filter);
            }
        }

        public UserOperationClaim GetUserOperationClaim(Expression<Func<UserOperationClaim, bool>> filter = null)
        {
            using (ReCapContext context = new ReCapContext())
            {
                return filter == null ? context.Set<UserOperationClaim>().SingleOrDefault() : context.Set<UserOperationClaim>().SingleOrDefault(filter);
            }
        }

        public List<OperationClaim> GetAllClaims(Expression<Func<OperationClaim, bool>> filter = null)
        {
            using(ReCapContext context = new ReCapContext())
            {
                return filter == null ? context.Set<OperationClaim>().ToList() : context.Set<OperationClaim>().Where(filter).ToList();
            }
        }

        public void DisableUser(User user)
        {
            using(ReCapContext context = new ReCapContext())
            {
                user.Status = false;
                Update(user);
            }
        }
    }
}
