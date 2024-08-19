using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp;
using System.Linq;
using System.ComponentModel;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.DC;

[DefaultClassOptions]
[DefaultProperty("Name")]
public class Driver : XPObject
{
    public Driver(Session session) : base(session) { }

    private string name;
    private string phoneNumber;
    private string licenseNumber;

    [XafDisplayName("Ім'я")]
    [RuleRequiredField(DefaultContexts.Save, CustomMessageTemplate = "Будь ласка, вкажіть ім'я")]
    public string Name
    {
        get => name;
        set => SetPropertyValue(nameof(Name), ref name, value);
    }

    [XafDisplayName("Водійське посвідчення")]
    [RuleRequiredField(DefaultContexts.Save, CustomMessageTemplate = "Будь ласка, вкажіть водійське посвідчення")]
    public string LicenseNumber
    {
        get => licenseNumber;
        set => SetPropertyValue(nameof(LicenseNumber), ref licenseNumber, value);
    }

    [XafDisplayName("Телефон")]
    [RuleRequiredField(DefaultContexts.Save, CustomMessageTemplate = "Будь ласка, вкажіть номер телефону")]
    public string PhoneNumber
    {
        get => phoneNumber;
        set => SetPropertyValue(nameof(PhoneNumber), ref phoneNumber, value);
    }

    [Association("Driver-DriverCars")]
    public XPCollection<DriverCar> DriverCars
    {
        get => GetCollection<DriverCar>(nameof(DriverCars));
    }

    protected override void OnSaving()
    {
        base.OnSaving();

        var existingDriver = Session.Query<Driver>()
            .FirstOrDefault(d => d.LicenseNumber == LicenseNumber);

        if (existingDriver != null)
        {
            throw new UserFriendlyException("Водій з таким номером ліцензії вже існує.");
        }
    }
}
