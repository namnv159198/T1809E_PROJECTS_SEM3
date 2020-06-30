using T1809E_PROJECT_SEM3.Models;

namespace T1809E_PROJECT_SEM3.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<T1809E_PROJECT_SEM3.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(T1809E_PROJECT_SEM3.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.(

            Random random = new Random();
            var listProvince = context.Province.ToList();
      
            int indexProvince = random.Next(listProvince.Count());

            var listDistrict = context.District.Where(x => x.province_id == listProvince[indexProvince].id).ToList();

            int indexDistrict = random.Next(listDistrict.Count());
            

            var office = new Office()
            {
                ID = "Office5",
                PinCode = "OF5",
                Name = "NamNguyen",
                Email = "namnv@gmail.com",
                VAT = random.Next(0,20),
                PhoneNumber = "0963404604",
                Address = "Số 1 hà đông",
                District_id = listDistrict[indexDistrict].id,
                Province_id = listProvince[indexProvince].id,
                Status = Office.StatusEnum.Online
            };
            context.Offices.AddOrUpdate(office);

        }
    }
}
