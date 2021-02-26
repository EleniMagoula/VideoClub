namespace VideoClub.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OnModelCreatingChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DVDs", "MovieId", "dbo.Movies");
            DropIndex("dbo.Bookings", new[] { "CustomerId" });
            AlterColumn("dbo.Bookings", "CustomerId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Bookings", "CustomerId");
            AddForeignKey("dbo.DVDs", "MovieId", "dbo.Movies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DVDs", "MovieId", "dbo.Movies");
            DropIndex("dbo.Bookings", new[] { "CustomerId" });
            AlterColumn("dbo.Bookings", "CustomerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Bookings", "CustomerId");
            AddForeignKey("dbo.DVDs", "MovieId", "dbo.Movies", "Id", cascadeDelete: true);
        }
    }
}
