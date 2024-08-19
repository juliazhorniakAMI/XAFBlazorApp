using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Xpo.Metadata;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp;
using System.ComponentModel;

[DefaultClassOptions]
public class DriverCar : XPObject
{
    public DriverCar(Session session) : base(session) { }

    private Car car;
    private Driver driver;
    private DateTime endDate;
    private DateTime editDate;

    [Association("Car-DriverCars")]
    [ImmediatePostData]
    public Car Car
    {
        get => car;
        set => SetPropertyValue(nameof(Car), ref car, value);
    }

    [Association("Driver-DriverCars")]
    public Driver Driver
    {
        get => driver;
        set => SetPropertyValue(nameof(Driver), ref driver, value);
    }

    [NonPersistent]
    [Browsable(false)]
    [RuleFromBoolProperty(
      "EndDateValidationRule",
      DefaultContexts.Save,
      "Дата завершення не може бути в минулому або на вихідних.",
      UsedProperties = "EndDate")]
    public bool IsEndDateValid => EndDate >= DateTime.Today && EndDate.DayOfWeek != DayOfWeek.Saturday && EndDate.DayOfWeek != DayOfWeek.Sunday;

    [RuleRequiredField(DefaultContexts.Save)]
    [EditorAlias(EditorAliases.DateTimePropertyEditor)]
    public DateTime EndDate
    {
        get => endDate;
        set => SetPropertyValue(nameof(EndDate), ref endDate, value);
    }

    [Persistent("EditDate")]
    [ValueConverter(typeof(UtcDateTimeConverter))]
    protected DateTime EditDate
    {
        get => editDate;
        set => SetPropertyValue(nameof(EditDate), ref editDate, value);
    }

    protected override void OnSaving()
    {
        base.OnSaving();

        var latestDriverCar = Session.Query<DriverCar>()
           .Where(dc => dc.Car == Car)
           .OrderByDescending(dc => dc.EndDate)
           .FirstOrDefault();

        if (latestDriverCar != null
            && latestDriverCar.EndDate >= DateTime.Today
            && Driver != latestDriverCar.Driver)
        {
            throw new UserFriendlyException("Ви не можете створити новий запис, поки існує активний прив'язаний водій.");
        }

        if (!IsDeleted)
        {
            EditDate = DateTime.UtcNow;
        }
        base.OnSaving();
    }

    public override void AfterConstruction()
    {
        base.AfterConstruction();
        EditDate = DateTime.UtcNow;
    }
}
