namespace BudgetPlanner.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdjustedTransaction : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Transaction", "Name");
            DropColumn("dbo.Transaction", "ExcludeTransaction");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transaction", "ExcludeTransaction", c => c.Boolean(nullable: false));
            AddColumn("dbo.Transaction", "Name", c => c.String(maxLength: 20));
        }
    }
}
