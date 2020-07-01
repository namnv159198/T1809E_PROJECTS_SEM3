using System.Collections.Generic;
using System.Web.Mvc;
using T1809E_PROJECT_SEM3.Models;

namespace T1809E_PROJECT_SEM3.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<T1809E_PROJECT_SEM3.Models.ApplicationDbContext>
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(T1809E_PROJECT_SEM3.Models.ApplicationDbContext context)
        {
            Random random = new Random();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.(

            // -------------------------------------------------- Seeding Service --------------------------------------------------

            /*context.Services.AddOrUpdate(
                new Service()
                {
                    ID = "FD100D",
                    TypeDelivery = Service.EnumServiceType.Fast_Delivery,
                    From = 0,
                    To = 100,
                    PriceStep = 0.01,
                    TypeCaculalor = Service.EnumType.Distance,
                    VAT = 5,
                    Status = Service.StatusEnumService.Online
                },
                new Service()
                {
                    ID = "FD500D",
                    TypeDelivery = Service.EnumServiceType.Fast_Delivery,
                    From = 100,
                    To = 500,
                    PriceStep = 0.05,
                    TypeCaculalor = Service.EnumType.Distance,
                    VAT = 5,
                    Status = Service.StatusEnumService.Online
                },
                new Service()
                {
                    ID = "FD1000D",
                    TypeDelivery = Service.EnumServiceType.Fast_Delivery,
                    From = 500,
                    To = 1000,
                    PriceStep = 0.1,
                    TypeCaculalor = Service.EnumType.Distance,
                    VAT = 5,
                    Status = Service.StatusEnumService.Online
                },
                new Service()
                {
                    ID = "FD2000D",
                    TypeDelivery = Service.EnumServiceType.Fast_Delivery,
                    From = 1000,
                    To = 2000,
                    PriceStep = 0.2,
                    TypeCaculalor = Service.EnumType.Distance,
                    VAT = 5,
                    Status = Service.StatusEnumService.Online
                }, new Service()
                {
                    ID = "FD3000D",
                    TypeDelivery = Service.EnumServiceType.Fast_Delivery,
                    From = 2000,
                    To = 3000,
                    PriceStep = 0.3,
                    TypeCaculalor = Service.EnumType.Distance,
                    VAT = 5,
                    Status = Service.StatusEnumService.Online
                }, new Service()
                {
                    ID = "FD100W",
                    TypeDelivery = Service.EnumServiceType.Fast_Delivery,
                    From = 0,
                    To = 100,
                    PriceStep = 2,
                    TypeCaculalor = Service.EnumType.Weight,
                    VAT = 5,
                    Status = Service.StatusEnumService.Online
                },
                new Service()
                {
                    ID = "FD500W",
                    TypeDelivery = Service.EnumServiceType.Fast_Delivery,
                    From = 100,
                    To = 500,
                    PriceStep = 10,
                    TypeCaculalor = Service.EnumType.Weight,
                    VAT = 5,
                    Status = Service.StatusEnumService.Online
                },
                new Service()
                {
                    ID = "FD1000W",
                    TypeDelivery = Service.EnumServiceType.Fast_Delivery,
                    From = 500,
                    To = 1000,
                    PriceStep = 12,
                    TypeCaculalor = Service.EnumType.Weight,
                    VAT = 5,
                    Status = Service.StatusEnumService.Online
                },
                new Service()
                {
                    ID = "FD2000W",
                    TypeDelivery = Service.EnumServiceType.Fast_Delivery,
                    From = 1000,
                    To = 2000,
                    PriceStep = 20,
                    TypeCaculalor = Service.EnumType.Weight,
                    VAT = 5,
                    Status = Service.StatusEnumService.Online
                }, new Service()
                {
                    ID = "FD3000W",
                    TypeDelivery = Service.EnumServiceType.Fast_Delivery,
                    From = 2000,
                    To = 3000,
                    PriceStep = 30,
                    TypeCaculalor = Service.EnumType.Weight,
                    VAT = 5,
                    Status = Service.StatusEnumService.Online
                }, new Service()
                {
                    ID = "SD100D",
                    TypeDelivery = Service.EnumServiceType.Savings_Delivery,
                    From = 0,
                    To = 100,
                    PriceStep = 0.005,
                    TypeCaculalor = Service.EnumType.Distance,
                    VAT = 3,
                    Status = Service.StatusEnumService.Online
                },
                new Service()
                {
                    ID = "SD500D",
                    TypeDelivery = Service.EnumServiceType.Savings_Delivery,
                    From = 100,
                    To = 500,
                    PriceStep = 0.025,
                    TypeCaculalor = Service.EnumType.Distance,
                    VAT = 3,
                    Status = Service.StatusEnumService.Online
                },
                new Service()
                {
                    ID = "SD1000D",
                    TypeDelivery = Service.EnumServiceType.Savings_Delivery,
                    From = 500,
                    To = 1000,
                    PriceStep = 0.5,
                    TypeCaculalor = Service.EnumType.Distance,
                    VAT = 3,
                    Status = Service.StatusEnumService.Online
                },
                new Service()
                {
                    ID = "SD2000D",
                    TypeDelivery = Service.EnumServiceType.Savings_Delivery,
                    From = 1000,
                    To = 2000,
                    PriceStep = 0.1,
                    TypeCaculalor = Service.EnumType.Distance,
                    VAT = 3,
                    Status = Service.StatusEnumService.Online
                }, new Service()
                {
                    ID = "SD3000D",
                    TypeDelivery = Service.EnumServiceType.Savings_Delivery,
                    From = 2000,
                    To = 3000,
                    PriceStep = 0.15,
                    TypeCaculalor = Service.EnumType.Distance,
                    VAT = 3,
                    Status = Service.StatusEnumService.Online
                }, new Service()
                {
                    ID = "SD100W",
                    TypeDelivery = Service.EnumServiceType.Savings_Delivery,
                    From = 0,
                    To = 100,
                    PriceStep = 1,
                    TypeCaculalor = Service.EnumType.Weight,
                    VAT = 3,
                    Status = Service.StatusEnumService.Online
                },
                new Service()
                {
                    ID = "SD500W",
                    TypeDelivery = Service.EnumServiceType.Savings_Delivery,
                    From = 100,
                    To = 500,
                    PriceStep = 6,
                    TypeCaculalor = Service.EnumType.Weight,
                    VAT = 3,
                    Status = Service.StatusEnumService.Online
                },
                new Service()
                {
                    ID = "SD1000W",
                    TypeDelivery = Service.EnumServiceType.Savings_Delivery,
                    From = 500,
                    To = 1000,
                    PriceStep = 9.5,
                    TypeCaculalor = Service.EnumType.Weight,
                    VAT = 3,
                    Status = Service.StatusEnumService.Online
                },
                new Service()
                {
                    ID = "SD2000W",
                    TypeDelivery = Service.EnumServiceType.Savings_Delivery,
                    From = 1000,
                    To = 2000,
                    PriceStep = 16.5,
                    TypeCaculalor = Service.EnumType.Weight,
                    VAT = 3,
                    Status = Service.StatusEnumService.Online
                }, new Service()
                {
                    ID = "SD3000W",
                    TypeDelivery = Service.EnumServiceType.Savings_Delivery,
                    From = 2000,
                    To = 3000,
                    PriceStep = 24.5,
                    TypeCaculalor = Service.EnumType.Weight,
                    VAT = 3,
                    Status = Service.StatusEnumService.Online
                },
                new Service()
                {
                    ID = "VPPW",
                    TypeDelivery = Service.EnumServiceType.VPP,
                    From = 0,
                    To = 1,
                    PriceStep = 0,
                    TypeCaculalor = Service.EnumType.Weight,
                    VAT = 0,
                    Status = Service.StatusEnumService.Online
                }, new Service()
                {
                    ID = "VPPD",
                    TypeDelivery = Service.EnumServiceType.VPP,
                    From = 0,
                    To = 1,
                    PriceStep = 0,
                    TypeCaculalor = Service.EnumType.Distance,
                    VAT = 0,
                    Status = Service.StatusEnumService.Online
                }
            ); 

             // -------------------------------------------------- Seeding Type Item --------------------------------------------------

            context.TypeItems.AddOrUpdate(
                new TypeItem() { ID = 1,Name ="Letter" ,Description ="Letter",Percent = 30},
                            new TypeItem() { ID = 2, Name = "Breakable goods.", Description = "Breakable goods.", Percent = 10 }
            );*/



            // -------------------------------------------------- Seeding Office --------------------------------------------------


            /*var listDistrict = context.District.ToList();
            


            for (int i = 1; i <= 63; i++)
            {
                listDistrict = context.District.Where(x => x.province_id == i).ToList();
                for (int j = 1; j <= 3; j++)
                {
                    var indexDistrict = random.Next(listDistrict.Count());
                    var office = new Office()
                    {
                        ID = "Office" + i + DateTime.Now.Millisecond,
                        PinCode = listDistrict[indexDistrict].Province._code + DateTime.Now.Millisecond,
                        Name = listDistrict[indexDistrict].Province._code + DateTime.Now.Millisecond + DateTime.Now.Year,
                        Email = listDistrict[indexDistrict].Province._code + DateTime.Now.Millisecond + i + "@gmail.com",
                        PhoneNumber = String.Concat("0", "9", random.Next(9), random.Next(9), random.Next(9), random.Next(9), random.Next(9), random.Next(9), random.Next(9), random.Next(9)),
                        Address = listDistrict[indexDistrict].Province._code + DateTime.Now.Millisecond + DateTime.Now.Day+ ","+listDistrict[indexDistrict]._name + "," + listDistrict[indexDistrict].Province._name,
                        District_id = listDistrict[indexDistrict].id,
                        Province_id = listDistrict[indexDistrict].Province.id,
                        Status = Office.StatusEnum.Online
                    };
                    context.Offices.AddOrUpdate(office);
                }
            }*/

            // -------------------------------------------------- Seeding Order --------------------------------------------------

            /*var listProvince = context.Province.ToList();
            var listOffice = context.Offices.ToList();
            var listTypeItem = context.TypeItems.ToList();
            var listService = context.Services.ToList();



            string[] RandomNames =
            {
                "abby", "abigail", "adele", "adrian" ,"john","dadee",
                "Rufus", "Bear", "Dakota", "Fido",
                "Vanya", "Samuel", "Koani", "Volodya",
                "Prince", "Yiska","Maggie", "Penny", "Saya", "Princess",
                "Abby", "Laila", "Sadie", "Olivia",
                "Starlight", "Talla","Adelaide","Aboli","Drusilla","Durra","Erin","Esperanza",
                "Faye","Fayola","Frida","Ganesa","Gemma","Glenda","Jade","Ladonna","Keva","Oscar","Pandora","Peach","Philomena","Phoenix","Radley",
                "Rose","Rosie","Rowan","Zel","Zelda","Zulema","Zoey","Xavia","Usha","Heulwen","Ronaldo","Messi"
            };

                int[] RandomDate =
                {
                   0, -1, -2, -3, -7, -14, -30, -60, -180, -365, -400, -287, -700 , -800 , -30*2, -30*3,-30*4,-30*6,-600,-888,-345,-500,-300
                };

            for (int i = 1; i <= 1000; i++)
            {
                int IndexSenderProvince = random.Next(0, listProvince.Count());
                var ListIndexSenderOffice = listOffice.Where(x => x.Province_id == listProvince[IndexSenderProvince].id).ToList();
                var indexSenderOffice = random.Next(0, ListIndexSenderOffice.Count());

                int IndexReceiverProvince = random.Next(0, listProvince.Count());
                var ListIndexReceiverOffice = listOffice.Where(x => x.Province_id == listProvince[IndexReceiverProvince].id).ToList();
                var indexReceiverOffice = random.Next(0, ListIndexReceiverOffice.Count());

                int indexItemType = random.Next(0, listTypeItem.Count());
                int indexService = random.Next(0, listService.Count());
                int indexNameSender = random.Next(0, RandomNames.Length);
                int indexNameReceiver = random.Next(0, RandomNames.Length);
                int indexDate = random.Next(0, RandomDate.Length);

                var order = new Order()
                {
                    ID = "OD"+i+DateTime.Now.Millisecond+DateTime.Now.Year,
                    SenderName = RandomNames[indexNameSender],
                    SenderAddress = listProvince[IndexSenderProvince]._code + DateTime.Now.Millisecond + DateTime.Now.Day + "," + listProvince[IndexSenderProvince]._name,
                    SenderPhone = String.Concat("0", "9", random.Next(9), random.Next(9), random.Next(9), random.Next(9), random.Next(9), random.Next(9), random.Next(9), random.Next(9)),
                    SenderProvinceID = listProvince[IndexSenderProvince].id,
                    SenderOfficeID = ListIndexSenderOffice[indexSenderOffice].ID,
                    ReceiverName = RandomNames[indexNameReceiver] ,
                    ReceiverAddress = listProvince[IndexReceiverProvince]._code + DateTime.Now.Millisecond + DateTime.Now.Day + "," + listProvince[IndexReceiverProvince]._name,
                    ReceiverPhone = String.Concat("0", "9", random.Next(9), random.Next(9), random.Next(9), random.Next(9), random.Next(9), random.Next(9), random.Next(9), random.Next(9)),
                    ReceiverProvinceID = listProvince[IndexReceiverProvince].id,
                    ReceiverOfficeID = ListIndexReceiverOffice[indexReceiverOffice].ID,
                    Distance = random.Next(0, 10000),
                    Weight = random.Next(0, 10000),
                    TypeItemId = listTypeItem[indexItemType].ID,
                    ServiceId = listService[indexService].ID,
                    Status = Order.EnumOrderStatus.Finished,
                    CreateAt = DateTime.Now.AddDays(RandomDate[indexDate]).AddHours(random.Next(-3, 0)).AddMinutes(random.Next(-300, -100))
                };
                if (order.Distance > 3000 && order.Weight <= 3000)
                {
                    order.PriceShip = (0.06 * order.Distance) + order.Weight * 0.02;
                }
                else if (order.Weight > 3000 && order.Distance <= 3000)
                {
                    order.PriceShip = (0.03 * order.Distance + 1) + order.Weight * 0.025;
                }
                else if (order.Distance > 3000 && order.Weight > 3000)
                {
                    order.PriceShip = (0.061 * order.Distance) + order.Weight * 0.023;
                }
                else if (order.Distance <= 3000 && order.Weight <= 3000)
                {
                    order.PriceShip = (0.065 * order.Distance) + order.Weight * 0.02;
                }
                else
                {
                    order.PriceShip = (0.06 * order.Distance) + order.Weight * 0.02;
                }
                order.PriceShip = Math.Round(order.PriceShip, 2);
                context.Orders.AddOrUpdate(order);
            }*/

        }

    }
}
