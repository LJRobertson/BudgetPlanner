namespace BudgetPlanner.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BudgetCategory",
                c => new
                    {
                        BudgetId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BudgetId, t.CategoryId })
                .ForeignKey("dbo.Budget", t => t.BudgetId, cascadeDelete: true)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.BudgetId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Budget",
                c => new
                    {
                        BudgetId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        BudgetName = c.String(nullable: false, maxLength: 30),
                        BudgetAmount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.BudgetId);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Transaction",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        Name = c.String(maxLength: 20),
                        Amount = c.Double(nullable: false),
                        TransactionDate = c.DateTime(nullable: false),
                        MerchantName = c.String(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        ExcludeTransaction = c.Boolean(nullable: false),
                        Budget_BudgetId = c.Int(),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.Budget", t => t.Budget_BudgetId)
                .Index(t => t.Budget_BudgetId);
            
            CreateTable(
                "dbo.Memo",
                c => new
                    {
                        TransactionId = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                        MemoContent = c.String(nullable: false, maxLength: 75),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.Transaction", t => t.TransactionId)
                .Index(t => t.TransactionId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.CategoryBudget",
                c => new
                    {
                        Category_CategoryId = c.Int(nullable: false),
                        Budget_BudgetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_CategoryId, t.Budget_BudgetId })
                .ForeignKey("dbo.Category", t => t.Category_CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Budget", t => t.Budget_BudgetId, cascadeDelete: true)
                .Index(t => t.Category_CategoryId)
                .Index(t => t.Budget_BudgetId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.BudgetCategory", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.BudgetCategory", "BudgetId", "dbo.Budget");
            DropForeignKey("dbo.Transaction", "Budget_BudgetId", "dbo.Budget");
            DropForeignKey("dbo.Memo", "TransactionId", "dbo.Transaction");
            DropForeignKey("dbo.CategoryBudget", "Budget_BudgetId", "dbo.Budget");
            DropForeignKey("dbo.CategoryBudget", "Category_CategoryId", "dbo.Category");
            DropIndex("dbo.CategoryBudget", new[] { "Budget_BudgetId" });
            DropIndex("dbo.CategoryBudget", new[] { "Category_CategoryId" });
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.Memo", new[] { "TransactionId" });
            DropIndex("dbo.Transaction", new[] { "Budget_BudgetId" });
            DropIndex("dbo.BudgetCategory", new[] { "CategoryId" });
            DropIndex("dbo.BudgetCategory", new[] { "BudgetId" });
            DropTable("dbo.CategoryBudget");
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Memo");
            DropTable("dbo.Transaction");
            DropTable("dbo.Category");
            DropTable("dbo.Budget");
            DropTable("dbo.BudgetCategory");
        }
    }
}
