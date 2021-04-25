namespace BudgetPlanner.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedToDecimal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BudgetCategory", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Budget", "BudgetAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Transaction", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Transaction", "Amount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budget", "BudgetAmount", c => c.Double(nullable: false));
            DropColumn("dbo.BudgetCategory", "Amount");
        }
    }
}
