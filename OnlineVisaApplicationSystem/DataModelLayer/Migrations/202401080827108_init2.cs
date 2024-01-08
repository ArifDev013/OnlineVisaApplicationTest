namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VisaApplicants", "MaritalStatus", c => c.Int(nullable: false));
            DropColumn("dbo.VisaApplicants", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VisaApplicants", "MyProperty", c => c.Int(nullable: false));
            DropColumn("dbo.VisaApplicants", "MaritalStatus");
        }
    }
}
