using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;

namespace DXApplication2.Module.DatabaseUpdate;

public class Updater : ModuleUpdater
{
    public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
        base(objectSpace, currentDBVersion)
    {
    }

    public override void UpdateDatabaseAfterUpdateSchema()
    {
        base.UpdateDatabaseAfterUpdateSchema();

        string driverName = "Driver1";
        Driver driver = ObjectSpace.FirstOrDefault<Driver>(d => d.Name == driverName);
        if (driver == null)
        {
            driver = ObjectSpace.CreateObject<Driver>();
            driver.Name = driverName;
            driver.PhoneNumber = "111111111";
        }

        string carName = "Car1";
        Car car = ObjectSpace.FirstOrDefault<Car>(c => c.Model == carName);
        if (car == null)
        {
            car = ObjectSpace.CreateObject<Car>();
            car.Model = carName;
        }

        DriverCar driverCar = ObjectSpace.FirstOrDefault<DriverCar>(dc => dc.Driver == driver && dc.Car == car);
        if (driverCar == null)
        {
            driverCar = ObjectSpace.CreateObject<DriverCar>();
            driverCar.Driver = driver;
            driverCar.Car = car;
            driverCar.EndDate = DateTime.Now.AddYears(2);
        }

        ObjectSpace.CommitChanges();
    }

    public override void UpdateDatabaseBeforeUpdateSchema()
    {
        base.UpdateDatabaseBeforeUpdateSchema();
    }
}
