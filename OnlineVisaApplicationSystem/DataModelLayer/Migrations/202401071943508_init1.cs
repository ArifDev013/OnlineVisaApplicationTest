namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VisaApplicants",
                c => new
                    {
                        VisaApplicantID = c.Long(nullable: false, identity: true),
                        ApplicantName = c.String(nullable: false, maxLength: 100),
                        Surename = c.String(nullable: false, maxLength: 100),
                        Gender = c.String(),
                        Address1 = c.String(nullable: false, maxLength: 200),
                        Address2 = c.String(maxLength: 200),
                        City = c.String(nullable: false, maxLength: 200),
                        State = c.String(nullable: false, maxLength: 200),
                        PostalCode = c.String(nullable: false, maxLength: 200),
                        Country = c.String(nullable: false, maxLength: 30),
                        Nationality = c.String(nullable: false, maxLength: 30),
                        DOB = c.DateTime(nullable: false),
                        MobileNumber = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 50),
                        PassportNumber = c.String(nullable: false, maxLength: 10),
                        PassportExpiryDate = c.DateTime(nullable: false),
                        MyProperty = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VisaApplicantID);
            
            CreateTable(
                "dbo.VisaApplication",
                c => new
                    {
                        ApplicationID = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        RefNumber = c.String(),
                        VisaApplicantID = c.Long(nullable: false),
                        VisaTypeID = c.Long(nullable: false),
                        AppliedDate = c.DateTime(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        TotalCharge = c.Double(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ApplicationID)
                .ForeignKey("dbo.VisaApplicants", t => t.VisaApplicantID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.VisaTypes", t => t.VisaTypeID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.VisaApplicantID)
                .Index(t => t.VisaTypeID);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        DocumentID = c.Long(nullable: false, identity: true),
                        ApplicationID = c.Long(nullable: false),
                        FileType = c.String(),
                        FileName = c.String(),
                        FileMemeType = c.String(),
                        FilePath = c.String(),
                    })
                .PrimaryKey(t => t.DocumentID)
                .ForeignKey("dbo.VisaApplication", t => t.ApplicationID, cascadeDelete: true)
                .Index(t => t.ApplicationID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserID = c.Long(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 50),
                        Mobile = c.String(),
                        UserType = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.UserID)
                .Index(t => t.Username, unique: true)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.VisaTypes",
                c => new
                    {
                        VisaTypeID = c.Long(nullable: false, identity: true),
                        VisaName = c.String(),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.VisaTypeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VisaApplication", "VisaTypeID", "dbo.VisaTypes");
            DropForeignKey("dbo.VisaApplication", "UserID", "dbo.User");
            DropForeignKey("dbo.Documents", "ApplicationID", "dbo.VisaApplication");
            DropForeignKey("dbo.VisaApplication", "VisaApplicantID", "dbo.VisaApplicants");
            DropIndex("dbo.User", new[] { "Name" });
            DropIndex("dbo.User", new[] { "Username" });
            DropIndex("dbo.Documents", new[] { "ApplicationID" });
            DropIndex("dbo.VisaApplication", new[] { "VisaTypeID" });
            DropIndex("dbo.VisaApplication", new[] { "VisaApplicantID" });
            DropIndex("dbo.VisaApplication", new[] { "UserID" });
            DropTable("dbo.VisaTypes");
            DropTable("dbo.User");
            DropTable("dbo.Documents");
            DropTable("dbo.VisaApplication");
            DropTable("dbo.VisaApplicants");
        }
    }
}
