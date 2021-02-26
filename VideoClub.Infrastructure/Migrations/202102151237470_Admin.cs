namespace VideoClub.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Admin : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'19cb5b66-cc47-4ec7-9401-697d20bbceba', N'Admin')
                INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'd4d3e1db-0d67-47d0-8f2c-f53020c42769', N'admin@videomail.com', 0, N'AKCSd0PxflwDDJgZT4JWok7nHUU2Y1IH7OTj5pUlTRm4X9ZTs9dMjIN3+zKtvmjXQg==', N'484c6d44-d79e-40c5-b0ea-33cbadd101ad', NULL, 0, 0, NULL, 1, 0, N'admin@videomail.com')
                INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd4d3e1db-0d67-47d0-8f2c-f53020c42769', N'19cb5b66-cc47-4ec7-9401-697d20bbceba')
            ");
        }
        
        public override void Down()
        {
        }
    }
}
