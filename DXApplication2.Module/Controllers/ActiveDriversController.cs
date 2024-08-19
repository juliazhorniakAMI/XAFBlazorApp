using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;

public class ActiveDriversViewController : ViewController<ListView>
{
    protected override void OnActivated()
    {
        base.OnActivated();

        if (View.ObjectTypeInfo.Type == typeof(DriverCar))
        {
            CriteriaOperator activeDriversCriteria = CriteriaOperator.Parse("EndDate >= ?", DateTime.Today);
            View.CollectionSource.Criteria["ActiveDrivers"] = activeDriversCriteria;
        }
    }

    protected override void OnDeactivated()
    {
        View.CollectionSource.Criteria.Clear();
        base.OnDeactivated();
    }
}
