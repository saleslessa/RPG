namespace DaemonCharacter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_Attribute",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                        description = c.String(maxLength: 250),
                        type = c.Int(nullable: false),
                        minimum = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tb_campaign",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                        shortDescription = c.String(maxLength: 255),
                        briefing = c.String(nullable: false),
                        startYear = c.Int(nullable: false),
                        maxPlayers = c.Int(nullable: false),
                        remainingPlayers = c.Int(nullable: false),
                        img = c.Binary(),
                        campaignStatus = c.Int(nullable: false),
                        UserProfileModel_UserId = c.Int(),
                        idMaster = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.UserProfile", t => t.UserProfileModel_UserId)
                .ForeignKey("dbo.UserProfile", t => t.idMaster, cascadeDelete: true)
                .Index(t => t.UserProfileModel_UserId)
                .Index(t => t.idMaster);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        accessLevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.tb_character",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                        maxLife = c.Int(nullable: false),
                        remainingLife = c.Int(nullable: false),
                        race = c.Int(nullable: false),
                        gender = c.Int(nullable: false),
                        type = c.Int(),
                        chalengeLevel = c.Int(),
                        publicAnnotations = c.String(),
                        level = c.Int(),
                        age = c.Int(),
                        experience = c.Int(),
                        background = c.String(),
                        pointsToDistribute = c.Int(),
                        remainingPoints = c.Int(),
                        money = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        idUser = c.Int(nullable: false),
                        idCampaign = c.Int(),
                        UserProfileModel_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.UserProfile", t => t.idUser, cascadeDelete: true)
                .ForeignKey("dbo.tb_campaign", t => t.idCampaign)
                .ForeignKey("dbo.UserProfile", t => t.UserProfileModel_UserId)
                .Index(t => t.idUser)
                .Index(t => t.idCampaign)
                .Index(t => t.UserProfileModel_UserId);
            
            CreateTable(
                "dbo.tb_character_attribute",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        value = c.Int(nullable: false),
                        idCharacter = c.Int(),
                        idAttribute = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tb_character", t => t.idCharacter)
                .ForeignKey("dbo.tb_Attribute", t => t.idAttribute)
                .Index(t => t.idCharacter)
                .Index(t => t.idAttribute);
            
            CreateTable(
                "dbo.tb_campaign_session",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        dayScheduled = c.DateTime(nullable: false),
                        idCampaign = c.Int(nullable: false),
                        timeScheduled = c.DateTime(nullable: false),
                        briefing = c.String(),
                        privateBeforeAnnotations = c.String(),
                        duringAnnotations = c.String(),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.id, t.dayScheduled, t.idCampaign })
                .ForeignKey("dbo.tb_campaign", t => t.idCampaign, cascadeDelete: true)
                .Index(t => t.idCampaign);
            
            CreateTable(
                "dbo.tb_item",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                        effect = c.String(maxLength: 255),
                        price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tb_item_attribute",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        value = c.Int(nullable: false),
                        idItem = c.Int(nullable: false),
                        idAttribute = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tb_item", t => t.idItem, cascadeDelete: true)
                .ForeignKey("dbo.tb_Attribute", t => t.idAttribute, cascadeDelete: true)
                .Index(t => t.idItem)
                .Index(t => t.idAttribute);
            
            CreateTable(
                "dbo.tb_attribute_bonus",
                c => new
                    {
                        idAttribute = c.Int(nullable: false),
                        idAttributeBonus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.idAttribute, t.idAttributeBonus })
                .ForeignKey("dbo.tb_Attribute", t => t.idAttribute)
                .ForeignKey("dbo.tb_Attribute", t => t.idAttributeBonus)
                .Index(t => t.idAttribute)
                .Index(t => t.idAttributeBonus);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.tb_attribute_bonus", new[] { "idAttributeBonus" });
            DropIndex("dbo.tb_attribute_bonus", new[] { "idAttribute" });
            DropIndex("dbo.tb_item_attribute", new[] { "idAttribute" });
            DropIndex("dbo.tb_item_attribute", new[] { "idItem" });
            DropIndex("dbo.tb_campaign_session", new[] { "idCampaign" });
            DropIndex("dbo.tb_character_attribute", new[] { "idAttribute" });
            DropIndex("dbo.tb_character_attribute", new[] { "idCharacter" });
            DropIndex("dbo.tb_character", new[] { "UserProfileModel_UserId" });
            DropIndex("dbo.tb_character", new[] { "idCampaign" });
            DropIndex("dbo.tb_character", new[] { "idUser" });
            DropIndex("dbo.tb_campaign", new[] { "idMaster" });
            DropIndex("dbo.tb_campaign", new[] { "UserProfileModel_UserId" });
            DropForeignKey("dbo.tb_attribute_bonus", "idAttributeBonus", "dbo.tb_Attribute");
            DropForeignKey("dbo.tb_attribute_bonus", "idAttribute", "dbo.tb_Attribute");
            DropForeignKey("dbo.tb_item_attribute", "idAttribute", "dbo.tb_Attribute");
            DropForeignKey("dbo.tb_item_attribute", "idItem", "dbo.tb_item");
            DropForeignKey("dbo.tb_campaign_session", "idCampaign", "dbo.tb_campaign");
            DropForeignKey("dbo.tb_character_attribute", "idAttribute", "dbo.tb_Attribute");
            DropForeignKey("dbo.tb_character_attribute", "idCharacter", "dbo.tb_character");
            DropForeignKey("dbo.tb_character", "UserProfileModel_UserId", "dbo.UserProfile");
            DropForeignKey("dbo.tb_character", "idCampaign", "dbo.tb_campaign");
            DropForeignKey("dbo.tb_character", "idUser", "dbo.UserProfile");
            DropForeignKey("dbo.tb_campaign", "idMaster", "dbo.UserProfile");
            DropForeignKey("dbo.tb_campaign", "UserProfileModel_UserId", "dbo.UserProfile");
            DropTable("dbo.tb_attribute_bonus");
            DropTable("dbo.tb_item_attribute");
            DropTable("dbo.tb_item");
            DropTable("dbo.tb_campaign_session");
            DropTable("dbo.tb_character_attribute");
            DropTable("dbo.tb_character");
            DropTable("dbo.UserProfile");
            DropTable("dbo.tb_campaign");
            DropTable("dbo.tb_Attribute");
        }
    }
}
