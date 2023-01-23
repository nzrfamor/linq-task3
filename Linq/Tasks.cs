using System;
using System.Collections.Generic;
using System.Linq;
using Linq.Objects;

namespace Linq
{
    public static class Tasks
    {
        public static IEnumerable<CountryStat> Task15(IEnumerable<Good> goodList,
            IEnumerable<StorePrice> storePriceList)
        {
            return goodList.Where(x => !storePriceList.Select(n => n.GoodId).Contains(x.Id)).Select(x => new CountryStat
            {
                Country = x.Country,
                MinPrice = 0.0m,
                StoresNumber = 0
            }).Distinct().Union(storePriceList.Join(goodList, sPL => sPL.GoodId, gL => gL.Id, (sPL, gL) => new
            {
                country = gL.Country,
                price = sPL.Price,
                stores = sPL.Shop
            }).GroupBy(x => x.country, x => (x.price, x.stores), (con, dat) => new CountryStat
            {
                Country = con,
                MinPrice = dat.Select(x => x.price).Min(),
                StoresNumber = dat.Select(x => x.stores).Distinct().Count()
            })).OrderBy(x => x.Country);

        }

    }
}
