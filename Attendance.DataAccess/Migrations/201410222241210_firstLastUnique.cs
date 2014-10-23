namespace Attendance.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstLastUnique : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Person", "FirstName", c => c.String(maxLength: 30));
            AlterColumn("dbo.Person", "LastName", c => c.String(maxLength: 30));
            CreateIndex("dbo.Person", new[] { "FirstName", "LastName" }, unique: true, name: "IX_FirstLastName");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Person", "IX_FirstLastName");
            AlterColumn("dbo.Person", "LastName", c => c.String());
            AlterColumn("dbo.Person", "FirstName", c => c.String());
        }
    }
}
