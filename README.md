# XAF Blazor Application with XPO ORM
## See Demo Here https://www.loom.com/share/a4b57e6aab3348078bfc7ad0392bf96c?sid=69762e01-2d87-4d2e-a412-5bbe8ca51e34
## Project Overview

This project is an XAF Blazor application that uses XPO (eXpress Persistent Objects) as the ORM (Object-Relational Mapping). The application is designed to manage a fleet of vehicles and their associated drivers. It includes functionality for maintaining directories of drivers and vehicles, linking drivers to vehicles with specific rights, and enforcing business rules related to vehicle usage.

## Features and Functionality

### 1. Directories:
- **Drivers Directory:**
  - A list view that displays all drivers.
  - Ability to create a new driver using a detailed view (UI).
- **Vehicles Directory:**
  - A list view that displays all vehicles.
  - Ability to create a new vehicle using a detailed view (UI).
- **Driver-Vehicle Associations:**
  - Establishes a many-to-many relationship between drivers and vehicles.
  - The UI allows users to link vehicles to drivers.

### 2. Business Logic:
- **EndDate for Vehicle Usage:**
  - Each driver has an individual right to use a vehicle until a specified `EndDate`.
  - When creating a vehicle-driver association, the user must specify this `EndDate`.
  - The DatePicker control is configured to prevent selecting past dates and weekends (Saturdays and Sundays).
  - The interface displays only active associations where the `EndDate` is today or later.
- **EditDate Field:**
  - An `EditDate` field is automatically populated when creating or updating a vehicle-driver association record.
  - The `EditDate` is not displayed in the interface but is stored in the database for tracking purposes.
- **Localization:**
  - The detailed view of the car is localized into Ukrainian.
  - The dropdown list for fuel types is also localized into Ukrainian.

## Technical Implementation

### Setting Up the Application:

- Use XAF Blazor as the application framework.
- Use XPO as the ORM for database interaction.
- Connect the application to an SQL database where all entities and associations are stored.

### Entities:

#### **Driver Entity:**
- Fields: `Name`, `LicenseNumber`, `PhoneNumber`.

#### **Car Entity:**
- Fields: `Model`, `Fuel`.
  - `Fuel` is an enumeration with values `Petrol`, `Diesel`, `Electric`, each localized into Ukrainian.

#### **DriverCar Entity (Linking Table):**
- Fields: `Driver`, `Car`, `EndDate`, `EditDate`.
  - Enforces business rules to prevent past dates and weekend selections for `EndDate`.
  - Automatically updates the `EditDate` on creation or modification.

### Controllers:

#### **ActiveAssignmentsController:**
- Filters the list view to display only active driver-car associations where `EndDate` is today or later.

#### **Custom Controllers for UI:**
- Ensures the UI displays the `Car.Model` instead of `Car.Id` in lists and dropdowns.
- Controls the DatePicker to enforce business rules on date selection.

### Localization:

- Implement Ukrainian localization for car details and fuel types using `[XafDisplayName]` attributes and resource files.
- Ensure that the UI elements and error messages are correctly localized.

## Running the Application

### Database Setup:

- Ensure that the SQL database is configured and connected to the application.
- Use XPO's migration tools to ensure the database schema is up to date with the application's entities.

### Running the Application:

- Build and run the Blazor application.
- Access the application through the provided URL and interact with the drivers and vehicles directories.

### Testing the Application:

- Test the creation, modification, and deletion of driver and vehicle records.
- Verify that the business rules for `EndDate` and `EditDate` are enforced.
- Ensure that the UI displays correctly localized text for car details and fuel types.

## Future Enhancements
- Add e2e tests
- Add unit test for controllers
- Implement additional business rules as required by the business needs.
- Use docker to run database & apps
- Add lint rules for code style when we create pr, & run tests
- Add auto deploy when we merge develop into main/master

