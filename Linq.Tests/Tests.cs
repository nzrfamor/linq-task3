using System;
using System.Collections.Generic;
using System.Linq;
using Linq.Objects;
using NUnit.Framework;
using Linq.Tests.Comparers;

namespace Linq.Tests
{
    [TestFixture]
    public class Tests
    {
        private readonly YearSchoolComparer _yearSchoolComparer = new YearSchoolComparer();
        private readonly NumberPairComparer _numberPairComparer = new NumberPairComparer();
        private readonly MaxDiscountOwnerComparer _discountOwnerComparer = new MaxDiscountOwnerComparer();
        private readonly CountryStatComparer _countryStatComparer = new CountryStatComparer();

        [Test]
        public void Task15Test()
        {
            foreach (var (goodList, storePriceList, expected) in Task15Data())
            {
                var actualResult = Tasks.Task15(goodList, storePriceList);
                AssertIsLinq(actualResult);
                AssertIsAsExpected(expected, actualResult, _countryStatComparer);
            }
        }

        private IEnumerable<(IEnumerable<Good> goodList, IEnumerable<StorePrice> storePriceList,
            IEnumerable<CountryStat> expected)> Task15Data()
        {
            yield return (
                    goodList: new[]
                    {
                        new Good{Id = 1, Country = "Ukraine", Category = "Food"},
                        new Good{Id = 2, Country = "Ukraine", Category = "Food"},
                        new Good{Id = 3, Country = "Ukraine", Category = "Food"},
                        new Good{Id = 4, Country = "Ukraine", Category = "Food"},
                        new Good{Id = 5, Country = "Germany", Category = "Food"},
                        new Good{Id = 6, Country = "Germany", Category = "Food"},
                        new Good{Id = 7, Country = "Germany", Category = "Food"},
                        new Good{Id = 8, Country = "Germany", Category = "Food"},
                        new Good{Id = 9, Country = "Greece", Category = "Food"},
                        new Good{Id = 10, Country = "Greece", Category = "Food"},
                        new Good{Id = 11, Country = "Greece", Category = "Food"},
                        new Good{Id = 12, Country = "Italy", Category = "Food"},
                        new Good{Id = 13, Country = "Italy", Category = "Food"},
                        new Good{Id = 14, Country = "Italy", Category = "Food"},
                        new Good{Id = 15, Country = "Slovenia", Category = "Food"}
                    },
                    storePriceList: new []
                    {
                        new StorePrice{GoodId = 1, Price = 1.25M, Shop = "shop1"},
                        new StorePrice{GoodId = 3, Price = 2.25M, Shop = "shop1"},
                        new StorePrice{GoodId = 5, Price = 4.25M, Shop = "shop1"},
                        new StorePrice{GoodId = 7, Price = 9.25M, Shop = "shop1"},
                        new StorePrice{GoodId = 9, Price = 11.25M, Shop = "shop1"},
                        new StorePrice{GoodId = 11, Price = 12.25M, Shop = "shop1"},
                        new StorePrice{GoodId = 13, Price = 13.25M, Shop = "shop1"},
                        new StorePrice{GoodId = 14, Price = 14.25M, Shop = "shop1"},
                        new StorePrice{GoodId = 5, Price = 11.25M, Shop = "shop2"},
                        new StorePrice{GoodId = 4, Price = 16.25M, Shop = "shop2"},
                        new StorePrice{GoodId = 3, Price = 18.25M, Shop = "shop2"},
                        new StorePrice{GoodId = 2, Price = 11.25M, Shop = "shop2"},
                        new StorePrice{GoodId = 1, Price = 1.50M, Shop = "shop2"},
                        new StorePrice{GoodId = 3, Price = 4.25M, Shop = "shop3"},
                        new StorePrice{GoodId = 7, Price = 3.25M, Shop = "shop3"},
                        new StorePrice{GoodId = 10, Price = 13.25M, Shop = "shop3"},
                        new StorePrice{GoodId = 14, Price = 14.25M, Shop = "shop3"},
                        new StorePrice{GoodId = 3, Price = 11.25M, Shop = "shop4"},
                        new StorePrice{GoodId = 2, Price = 14.25M, Shop = "shop4"},
                        new StorePrice{GoodId = 12, Price = 2.25M, Shop = "shop4"},
                        new StorePrice{GoodId = 6, Price = 5.25M, Shop = "shop4"},
                        new StorePrice{GoodId = 8, Price = 6.25M, Shop = "shop4"},
                        new StorePrice{GoodId = 10, Price = 11.25M, Shop = "shop4"},
                        new StorePrice{GoodId = 4, Price = 15.25M, Shop = "shop5"},
                        new StorePrice{GoodId = 7, Price = 18.25M, Shop = "shop5"},
                        new StorePrice{GoodId = 8, Price = 13.25M, Shop = "shop5"},
                        new StorePrice{GoodId = 12, Price = 14.25M, Shop = "shop5"},
                        new StorePrice{GoodId = 1, Price = 3.25M, Shop = "shop6"},
                        new StorePrice{GoodId = 3, Price = 2.25M, Shop = "shop6"},
                        new StorePrice{GoodId = 1, Price = 1.20M, Shop = "shop7"}
                    },
                    expected: new []
                    {
                        new CountryStat{Country = "Germany", MinPrice = 3.25M, StoresNumber = 5},
                        new CountryStat{Country = "Greece", MinPrice = 11.25M, StoresNumber = 3},
                        new CountryStat{Country = "Italy", MinPrice = 2.25M, StoresNumber = 4},
                        new CountryStat{Country = "Slovenia", MinPrice = 0.0M, StoresNumber = 0},
                        new CountryStat{Country = "Ukraine", MinPrice = 1.20M, StoresNumber = 7},
                    });
        }

        #region Utility

        private void AssertIsLinq<T>(IEnumerable<T> result)
        {
            Assert.AreEqual("System.Linq", result.GetType().Namespace, "Result is not linq");
        }

        private void AssertIsAsExpected<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            Assert.AreEqual(expected, actual);
        }

        private void AssertIsAsExpected<T>(IEnumerable<T> expected, IEnumerable<T> actual, IEqualityComparer<T> comparer)
        {
            Assert.True(expected.SequenceEqual(actual, comparer), "Result is not as expected");
        }

        #endregion
    }
}