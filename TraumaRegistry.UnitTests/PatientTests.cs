using System;
using TraumaRegistry.Client;
using Xunit;

namespace TraumaRegistry.UnitTests
{
    public class PatientTests
    {
        [Fact]
        public async System.Threading.Tasks.Task GetAllPatientsTestAsync()
        {
            var client = new PatientsClient();
            var recs = await client.GetPatientsAsync();
            Assert.True(recs.Count > 0);
        }
    }
}
