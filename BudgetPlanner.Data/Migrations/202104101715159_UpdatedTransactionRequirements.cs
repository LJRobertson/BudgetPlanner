namespace BudgetPlanner.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedTransactionRequirements : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Transaction", "Name", c => c.String(maxLength: 20));
            AlterColumn("dbo.Transaction", "MerchantName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Transaction", "MerchantName", c => c.String());
            AlterColumn("dbo.Transaction", "Name", c => c.String());
        }
    }
}
