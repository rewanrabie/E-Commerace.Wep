﻿using DomainLayer.InterFaceRepostory_Contracts_;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Serives.Specifications
{
    abstract class BaseSpecifications<TEntity, Tkey> : ISpecifications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        protected BaseSpecifications(Expression<Func<TEntity, bool>>? CriteriaExpression)
        {
            Criteria = CriteriaExpression;
        }
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        #region Include
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
           => IncludeExpressions.Add(includeExpression);
        #endregion

        #region Sorting
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExp) => OrderBy = orderByExp;
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescExp) => OrderByDescending = orderByDescExp;
        #endregion

        #region Pagination
        public int Take  { get; private set; }

        public int Skip  { get; private set; }

        public bool IsPaginated { get; set; }

        protected void ApplyPagination(int PageSize,int PageIndex)
        {
            IsPaginated = true;
            Take = PageSize;
            Skip = (PageIndex-1) * PageSize;
        }
      #endregion
    }
}
