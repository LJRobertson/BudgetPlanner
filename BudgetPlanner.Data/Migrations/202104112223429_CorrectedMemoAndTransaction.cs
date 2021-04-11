namespace BudgetPlanner.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectedMemoAndTransaction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transaction", "Memo_TransactionId", c => c.Int());
            CreateIndex("dbo.Transaction", "Memo_TransactionId");
            AddForeignKey("dbo.Transaction", "Memo_TransactionId", "dbo.Memo", "TransactionId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transaction", "Memo_TransactionId", "dbo.Memo");
            DropIndex("dbo.Transaction", new[] { "Memo_TransactionId" });
            DropColumn("dbo.Transaction", "Memo_TransactionId");
        }
    }
}
