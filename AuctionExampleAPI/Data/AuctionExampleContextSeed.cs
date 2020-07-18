using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionExampleAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionExampleAPI.Data
{
    public class AuctionExampleContextSeed
    {
        public static async Task SeedAsync(AuctionExampleContext auctionExampleContext)
        {
            // Only run this if using a real database
            // context.Database.Migrate();

            try
            {
                if (!auctionExampleContext.Item.Any())
                {
                    await auctionExampleContext.Item.AddRangeAsync(GetPreconfiguredItems());
                    await auctionExampleContext.SaveChangesAsync();
                }
                if (!auctionExampleContext.Rate.Any())
                {
                    await auctionExampleContext.Rate.AddRangeAsync(GetPreconfiguredRates());
                    await auctionExampleContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                //TODO: Add Logger and Exceptions
            }
        }

        private static IEnumerable<Item> GetPreconfiguredItems()
        {
            return new List<Item>
            {
                new Item {
                    ItemId = 1, 
                    Name = "Лот №1", 
                    StartPrice = 300, 
                    CurrentPrice = 700, 
                    StartTime = new DateTime(2020, 10, 12, 15, 30, 25),
                    EndTime = new DateTime(2020, 10, 19, 15, 30, 25), 
                    Status = false
                },
                new Item {
                    ItemId = 2,
                    Name = "Лот №2",
                    StartPrice = 450,
                    CurrentPrice = 550,
                    StartTime = new DateTime(2020, 10, 13, 14, 20, 00),
                    EndTime = new DateTime(2020, 10, 20, 14, 20, 00),
                    Status = false
                },
                new Item {
                    ItemId = 3,
                    Name = "Лот №3",
                    StartPrice = 500,
                    CurrentPrice = 500,
                    StartTime = new DateTime(2020, 10, 10, 10, 00, 00),
                    EndTime = new DateTime(2020, 10, 17, 10, 00, 00),
                    Status = false
                },
                new Item {
                    ItemId = 4,
                    Name = "Лот №4",
                    StartPrice = 1200,
                    CurrentPrice = 1200,
                    StartTime = new DateTime(2020, 10, 5, 9, 45, 15),
                    EndTime = new DateTime(2020, 10, 16, 9, 45, 15),
                    Status = false
                },
                new Item {
                    ItemId = 5,
                    Name = "Лот №5",
                    StartPrice = 800,
                    CurrentPrice = 1350,
                    StartTime = new DateTime(2020, 10, 7, 15, 30, 00),
                    EndTime = new DateTime(2020, 10, 14, 15, 30, 00),
                    Status = false
                }
            };
        }

        private static IEnumerable<Rate> GetPreconfiguredRates()
        {
            return new List<Rate>()
            {
                new Rate()
                {
                    RateId = 1,
                    ItemId = 1,
                    UserName = "John",
                    Price = 400,
                    RateTime = new DateTime(2020, 10, 13, 14, 16, 35)
                },
                new Rate()
                {
                    RateId = 2,
                    ItemId = 1,
                    UserName = "Albert",
                    Price = 500,
                    RateTime = new DateTime(2020, 10, 13, 15, 21, 47)
                },
                new Rate()
                {
                    RateId = 3,
                    ItemId = 1,
                    UserName = "John",
                    Price = 600,
                    RateTime = new DateTime(2020, 10, 14, 9, 35, 40)
                },
                new Rate()
                {
                    RateId = 4,
                    ItemId = 1,
                    UserName = "Jack",
                    Price = 700,
                    RateTime = new DateTime(2020, 10, 15, 17, 28, 15)
                },
                new Rate()
                {
                    RateId = 5,
                    ItemId = 2,
                    UserName = "Alisa",
                    Price = 500,
                    RateTime = new DateTime(2020, 10, 15, 13, 31, 15)
                },
                new Rate()
                {
                    RateId = 6,
                    ItemId = 2,
                    UserName = "John",
                    Price = 550,
                    RateTime = new DateTime(2020, 10, 16, 17, 26, 10)
                },
                new Rate()
                {
                    RateId = 7,
                    ItemId = 5,
                    UserName = "John",
                    Price = 1350,
                    RateTime = new DateTime(2020, 10, 14, 12, 12, 55)
                },
            };
        }
    }
}
