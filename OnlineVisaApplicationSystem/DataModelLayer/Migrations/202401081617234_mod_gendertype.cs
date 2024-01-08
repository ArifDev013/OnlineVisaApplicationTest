namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mod_gendertype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VisaApplicants", "Gender", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VisaApplicants", "Gender", c => c.String());
        }
    }
}
