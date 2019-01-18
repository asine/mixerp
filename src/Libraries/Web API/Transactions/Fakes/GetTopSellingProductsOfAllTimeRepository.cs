// ReSharper disable All
using System;
using System.Collections.Generic;
using MixERP.Net.Schemas.Transactions.Data;
using MixERP.Net.Entities.Transactions;

namespace MixERP.Net.Api.Transactions.Fakes
{
    public class GetTopSellingProductsOfAllTimeRepository : IGetTopSellingProductsOfAllTimeRepository
    {
        public int Top { get; set; }

        public IEnumerable<DbGetTopSellingProductsOfAllTimeResult> Execute()
        {
            return new List<DbGetTopSellingProductsOfAllTimeResult>();
        }
    }
}