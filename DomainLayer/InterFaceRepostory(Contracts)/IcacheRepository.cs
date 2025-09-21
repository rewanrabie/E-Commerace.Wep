﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.InterFaceRepostory_Contracts_
{
    public interface IcacheRepository
    {
        Task<string?> GetAsync(string CacheKey);
        Task SetAsync(string CacheKey,string CacheValue,TimeSpan TimeToLive);
    }
}
