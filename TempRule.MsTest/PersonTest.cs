using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

[TestClass]
public class PersonTest
{
    [TestMethod]
    public async Task PersonBusinessRulesTest()
    {
        // SETUP
        // In Person.cs uncomment line 22 or 23 to compare using DelayRule or DelayRuleAsync
        //  * DelayRule      (line 22) the test passes
        //  * DelayRuleAsync (line 23) the test fails

        Person item = await Person.GetPersonAsync();
        Assert.IsTrue(item.BrokenRulesCollection.Count == 1);
        item.Name = "ABC123";
        Assert.IsTrue(item.BrokenRulesCollection.Count == 0);

        item = await Person.NewPersonAsync();
        Assert.IsTrue(item.BrokenRulesCollection.Count == 1);
        item.Name = "ABC123";
        Assert.IsTrue(item.BrokenRulesCollection.Count == 0);

    }
}
