namespace Attendance.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uniqueIndex : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Visit", new[] { "PersonId" });
            DropIndex("dbo.Visit", new[] { "EventId" });
            CreateIndex("dbo.Visit", new[] { "PersonId", "EventId" }, unique: true, name: "IX_PersonIdEventId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Visit", "IX_PersonIdEventId");
            CreateIndex("dbo.Visit", "EventId");
            CreateIndex("dbo.Visit", "PersonId");
        }
    }
}
