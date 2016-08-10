using DaemonCharacter.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DomainTests.Entity
{
    [TestClass]
    public class CampaignTests
    {

        [TestMethod]
        public void Campaign_IsConsistent_True()
        {
            var obj = new Campaign()
            {
                CampaignBriefing = "test",
                CampaignMaxPlayers = 5,
                CampaignName = "test",
                CampaignShortDescription = "test",
                CampaignUserMaster = "a@a.com.br",
                Players = new List<Player>(),
                CampaignStartYear=123
            };

            var validationResult = obj.IsValid();

            Assert.IsTrue(validationResult);
        }

        [TestMethod]
        public void Campaign_IsConsistent_False()
        {
            var obj = new Campaign()
            {
                CampaignBriefing = "test",
                CampaignMaxPlayers = 5,
                CampaignName = "test",
                CampaignShortDescription = "test",
                Players = new List<Player>(),
                CampaignStartYear = 123
            };

            var validationResult = obj.IsValid();

            Assert.IsFalse(validationResult);
        }
    }
}
