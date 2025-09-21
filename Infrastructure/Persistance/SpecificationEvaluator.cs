﻿using DomainLayer.InterFaceRepostory_Contracts_;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    //Helper Class
    static class SpecificationEvaluator
    {
        //Create Dynamic Query
        public static IQueryable<TEntity> CreateQuery<TEntity,TKey>(IQueryable<TEntity> InputQuery , ISpecifications<TEntity, TKey> specifications) where TEntity:BaseEntity<TKey>
        {
            var Query = InputQuery;
            if (specifications.Criteria is not null)
            {
                Query = Query.Where(specifications.Criteria);
            }

            if (specifications.OrderBy is not null)
            {
                Query = Query.OrderBy(specifications.OrderBy);
            }

            if (specifications.OrderByDescending is not null)
            {
                Query = Query.OrderByDescending(specifications.OrderByDescending);
            }

            if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Count > 0)
            {
                /*foreach (var exp in specifications.IncludeExpressions)
                {
                    Query = Query.Include(exp);
                }*/
                Query = specifications.IncludeExpressions.Aggregate(Query,(CurrentQuery,IncludeExp) =>CurrentQuery.Include(IncludeExp));
            }

            if (specifications.IsPaginated)
            {
                Query = Query.Skip(specifications.Skip).Take(specifications.Take);
            }




            return Query;
        }
    }
}
