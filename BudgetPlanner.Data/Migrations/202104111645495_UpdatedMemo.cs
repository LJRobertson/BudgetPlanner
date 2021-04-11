namespace BudgetPlanner.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedMemo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transaction", "MemoId", "dbo.Memo");
            DropIndex("dbo.Transaction", new[] { "MemoId" });
            DropPrimaryKey("dbo.Memo");
            AddColumn("dbo.Memo", "TransactionId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Memo", "TransactionId");
            DropColumn("dbo.Transaction", "MemoId");
            DropColumn("dbo.Memo", "MemoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Memo", "MemoId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Transaction", "MemoId", c => c.Int());
            DropPrimaryKey("dbo.Memo");
            DropColumn("dbo.Memo", "TransactionId");
            AddPrimaryKey("dbo.Memo", "MemoId");
            CreateIndex("dbo.Transaction", "MemoId");
            AddForeignKey("dbo.Transaction", "MemoId", "dbo.Memo", "MemoId");
        }
    }
}
