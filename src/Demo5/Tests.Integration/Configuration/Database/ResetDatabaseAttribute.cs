using System.Reflection;
using Xunit.Sdk;

namespace Tests.Integration.Configuration.Database
{
    public class ResetDatabaseAttribute : BeforeAfterTestAttribute
    {
        public override void Before(MethodInfo methodUnderTest)
        {
            ServerFixture.ResetDatabase();
        }
    }
}
