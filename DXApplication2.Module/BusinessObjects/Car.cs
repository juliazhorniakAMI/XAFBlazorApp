using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using System.ComponentModel;
using System;
using DevExpress.Persistent.Validation;

[DefaultClassOptions]
[DefaultProperty("Model")]
public class Car : XPObject
{
    public Car(Session session) : base(session) { }

    private string model;

    [XafDisplayName("Модель")]
    [RuleRequiredField(DefaultContexts.Save, CustomMessageTemplate = "Будь ласка, вкажіть модель автомобіля.")]
    public string Model
    {
        get => model;
        set => SetPropertyValue(nameof(Model), ref model, value);
    }

    private Type type;

    [XafDisplayName("Тип палива")]
    public Type Type
    {
        get => type;
        set => SetPropertyValue(nameof(Type), ref type, value);
    }

    [Association("Car-DriverCars")]
    public XPCollection<DriverCar> DriverCars
    {
        get => GetCollection<DriverCar>(nameof(DriverCars));
    }

    public override string ToString()
    {
        return Model;
    }
}

public enum Type
{
    [XafDisplayName("Бензин")]
    Petrol,
    [XafDisplayName("Дизель")]
    Diesel,
    [XafDisplayName("Електро")]
    Electric
}