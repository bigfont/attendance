namespace Attendance.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VisitId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Visit", "Id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Visit", "Id");
        }
    }
}
