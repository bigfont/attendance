namespace Attendance.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpandUniqueIndex : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Visit", "IX_PersonIdEventId");
            CreateIndex("dbo.Visit", new[] { "PersonId", "EventId", "DateTime" }, unique: true, name: "IX_PersonIdEventIdDateTime");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Visit", "IX_PersonIdEventIdDateTime");
            CreateIndex("dbo.Visit", new[] { "PersonId", "EventId" }, unique: true, name: "IX_PersonIdEventId");
        }
    }
}
