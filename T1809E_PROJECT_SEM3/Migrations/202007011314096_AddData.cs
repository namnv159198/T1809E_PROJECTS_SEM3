namespace T1809E_PROJECT_SEM3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Districts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        _name = c.String(),
                        _prefix = c.String(),
                        province_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Provinces", t => t.province_id, cascadeDelete: true)
                .Index(t => t.province_id);
            
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        _name = c.String(),
                        _code = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Offices",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        PinCode = c.String(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false),
                        PhoneNumber = c.String(),
                        Address = c.String(nullable: false),
                        District_id = c.Int(),
                        Province_id = c.Int(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Districts", t => t.District_id)
                .ForeignKey("dbo.Provinces", t => t.Province_id)
                .Index(t => t.District_id)
                .Index(t => t.Province_id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        SenderName = c.String(nullable: false, maxLength: 50),
                        SenderProvinceID = c.Int(nullable: false),
                        SenderOfficeID = c.String(nullable: false, maxLength: 128),
                        SenderAddress = c.String(nullable: false, maxLength: 50),
                        SenderPhone = c.String(nullable: false),
                        ReceiverName = c.String(nullable: false, maxLength: 50),
                        ReceiverProvinceID = c.Int(nullable: false),
                        ReceiverOfficeID = c.String(nullable: false, maxLength: 128),
                        ReceiverAddress = c.String(nullable: false),
                        ReceiverPhone = c.String(nullable: false),
                        Distance = c.Double(nullable: false),
                        Weight = c.Double(nullable: false),
                        CreateAt = c.DateTime(),
                        PriceShip = c.Double(nullable: false),
                        TypeItemId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        ServiceId = c.String(nullable: false, maxLength: 128),
                        CreatedById = c.String(maxLength: 128),
                        UpdatedById = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById)
                .ForeignKey("dbo.Offices", t => t.ReceiverOfficeID, cascadeDelete: false)
                .ForeignKey("dbo.Provinces", t => t.ReceiverProvinceID, cascadeDelete: false)
                .ForeignKey("dbo.Offices", t => t.SenderOfficeID, cascadeDelete: false)
                .ForeignKey("dbo.Provinces", t => t.SenderProvinceID, cascadeDelete: false)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .ForeignKey("dbo.TypeItems", t => t.TypeItemId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedById)
                .Index(t => t.SenderProvinceID)
                .Index(t => t.SenderOfficeID)
                .Index(t => t.ReceiverProvinceID)
                .Index(t => t.ReceiverOfficeID)
                .Index(t => t.TypeItemId)
                .Index(t => t.ServiceId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        TypeDelivery = c.Int(nullable: false),
                        From = c.Double(nullable: false),
                        To = c.Double(nullable: false),
                        PriceStep = c.Double(nullable: false),
                        TypeCaculalor = c.Int(nullable: false),
                        VAT = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TypeItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Percent = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Orders", "UpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "TypeItemId", "dbo.TypeItems");
            DropForeignKey("dbo.Orders", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.Orders", "SenderProvinceID", "dbo.Provinces");
            DropForeignKey("dbo.Orders", "SenderOfficeID", "dbo.Offices");
            DropForeignKey("dbo.Orders", "ReceiverProvinceID", "dbo.Provinces");
            DropForeignKey("dbo.Orders", "ReceiverOfficeID", "dbo.Offices");
            DropForeignKey("dbo.Orders", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Offices", "Province_id", "dbo.Provinces");
            DropForeignKey("dbo.Offices", "District_id", "dbo.Districts");
            DropForeignKey("dbo.Districts", "province_id", "dbo.Provinces");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Orders", new[] { "UpdatedById" });
            DropIndex("dbo.Orders", new[] { "CreatedById" });
            DropIndex("dbo.Orders", new[] { "ServiceId" });
            DropIndex("dbo.Orders", new[] { "TypeItemId" });
            DropIndex("dbo.Orders", new[] { "ReceiverOfficeID" });
            DropIndex("dbo.Orders", new[] { "ReceiverProvinceID" });
            DropIndex("dbo.Orders", new[] { "SenderOfficeID" });
            DropIndex("dbo.Orders", new[] { "SenderProvinceID" });
            DropIndex("dbo.Offices", new[] { "Province_id" });
            DropIndex("dbo.Offices", new[] { "District_id" });
            DropIndex("dbo.Districts", new[] { "province_id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.TypeItems");
            DropTable("dbo.Services");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Orders");
            DropTable("dbo.Offices");
            DropTable("dbo.Provinces");
            DropTable("dbo.Districts");
        }
    }
}
