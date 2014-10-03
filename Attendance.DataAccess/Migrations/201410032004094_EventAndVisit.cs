namespace Attendance.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventAndVisit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Visit",
                c => new
                    {
                        PersonId = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.PersonId, t.EventId })
                .ForeignKey("dbo.Event", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.Person", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId)
                .Index(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Visit", "PersonId", "dbo.Person");
            DropForeignKey("dbo.Visit", "EventId", "dbo.Event");
            DropIndex("dbo.Visit", new[] { "EventId" });
            DropIndex("dbo.Visit", new[] { "PersonId" });
            DropTable("dbo.Visit");
            DropTable("dbo.Event");
        }
    }
}
