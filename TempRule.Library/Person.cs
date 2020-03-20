using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;


[Serializable]
public class Person : BusinessBase<Person>
{
    public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(nameof(Name));
    //[Required] - handled in Business Rule
    public string Name
    {
        get => GetProperty(NameProperty);
        set => SetProperty(NameProperty, value);
    }

    protected override void AddBusinessRules()
    {
        base.AddBusinessRules();
        BusinessRules.AddRule(new DelayRule(NameProperty));
        //BusinessRules.AddRule(new DelayRuleAsync(NameProperty));
    }

    public async static Task<Person> NewPersonAsync()
    {
        Person item = await DataPortal.CreateAsync<Person>();
        return item;
    }

}

public class DelayRuleAsync : BusinessRuleAsync
{
    public DelayRuleAsync(Csla.Core.IPropertyInfo primaryProperty)
        : base(primaryProperty)
    {
        InputProperties.Add(PrimaryProperty);
    }

    protected override async Task ExecuteAsync(IRuleContext context)
    {
        var value = (string)context.InputPropertyValues[PrimaryProperty];
        if (string.IsNullOrWhiteSpace(value))
        {
            context.AddInformationResult("Name is required");
        }
    }
}

public class DelayRule : BusinessRule
{
    public DelayRule(Csla.Core.IPropertyInfo primaryProperty)
        : base(primaryProperty)
    {
        InputProperties.Add(PrimaryProperty);
    }

    protected override void Execute(IRuleContext context)
    {
        var value = (string)context.InputPropertyValues[PrimaryProperty];
        if (string.IsNullOrWhiteSpace(value))
        {
            context.AddInformationResult("Name is required");
        }
    }
}