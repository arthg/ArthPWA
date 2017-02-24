using System.Configuration;
/*
using CFS.SkyNet.Api.Authentication;
using CFS.SkyNet.Api.Checks;
using CFS.SkyNet.Api.Deposits;
using CFS.SkyNet.Api.Workflow.Activities;
using CFS.SkyNet.Api.Workflow.Rules;
using CFS.SkyNet.Infrastructure;
using CFS.SkyNet.Infrastructure.EventLog;
using CFS.SkyNet.Infrastructure.Workflow;
using CFS.SkyNet.Models;
*/
using ArthPWA.Tests.Common.Helpers;
using ArthPWA.MongoDB;
using MongoDB.Driver;
using NUnit.Framework;

namespace ArthPWA.Tests.Integration.MongoDB
{
    public abstract class TestWithDatabaseBase
    {
        /*
        protected SecurityContext SecurityContext { get; private set; }
        protected EventLogRepository EventLogRepository { get; private set; }
        */
        protected ArthNetDatabase Database { get; private set; }

        [OneTimeSetUp]
        public void PrepareTestDatabase()
        {
            SetBsonMappings();
            var connectionString = ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString;
            var mongoUri = MongoUrl.Create(connectionString);

            var dbName = "Test" + RandomData.GetString(8);
            var stringWithoutDbName = "mongodb://" + mongoUri.Server + "/" + dbName;

            Database = new ArthNetDatabase(stringWithoutDbName);
            /*
            SecurityContext = new SecurityContext();
            SecurityContext.PrepareContext(new AuthorizationTicket
            {
                UserId = "integrationuserID",
                UserName = "integrationuser"
            });
            EventLogRepository = new EventLogRepository(Database, SecurityContext, new ServerInfoProvider());
            */
        }

        [OneTimeTearDown]
        public void DropTestDatabase()
        {
            Database.Database.Drop();
        }

        private static void SetBsonMappings()
        {
            /*
            var moneySerializer = new MoneySerializer();
            var checkBsonConfig = new CheckBsonMapper(moneySerializer, new CheckImageCollectionSerializer());
            checkBsonConfig.InitializeMap();

            var depositBsonConfig = new DepositBsonMapper(moneySerializer);
            depositBsonConfig.InitializeMap();

            new CheckPayorStatisticsBsonMapper(moneySerializer).InitializeMap();
            
            var stringEnumMapper = new StringEnumBaseBsonMapperConfiguration(
                new[]
                {
                    typeof(CheckValidity),
                    typeof(ActivityName), 
                    typeof(ActivityExecutionStatus), 
                    typeof(SecurityEvent), 
                    typeof(RuleName), 
                    typeof(RuleExecutionStatus), 
                    typeof(ValidityFailureReason),
                    typeof(ValidateDepositActivityExecutionStatus),
                    typeof(CheckReturnEvent),
                    typeof(Origin)
                },
                new StringEnumBaseSerializer());
            stringEnumMapper.InitializeMap();
                */
        }

    }
}