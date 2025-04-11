Thanks for the clarification! Since the frontend is developed using **C# WebForms**, the **README** needs to be updated to reflect the specifics of a **WebForms** application and its integration with the **RESTful API**.

Here's how you can adjust your **README** to include **C# WebForms** frontend instructions:

---

# Administrative System for Managing Drugs, Tenders, Suppliers, and Pharmacy

## Description

This project is an **administrative system** designed to manage the core functionalities of a **pharmacy business**. The system includes modules for managing **drugs**, **tenders**, **suppliers**, and **general pharmacy operations**. The backend consists of a **RESTful API** built using **C# with .NET Framework** that interacts with a **Microsoft SQL Server** database. The frontend is developed using **C# WebForms** for easy integration with .NET-based applications.

### Key Features:
- **Drug Management**: Add, update, delete, and view drugs in the system, including details like name, dosage, manufacturer, price, and stock levels.
- **Tender Management**: Track and manage tenders for the pharmacy, including details like tender ID, drug list, supplier, price, and expiration dates.
- **Supplier Management**: Store and manage supplier details, track orders, and handle supplier communications.
- **Pharmacy Operations**: Oversee all daily operations of the pharmacy, including sales tracking, customer orders, and inventory updates.

### Project Structure:
1. **API Backend**: A RESTful API to handle all business logic and data interactions with the database.
2. **Administrative Frontend (WebForms)**: A WebForms-based system for administrators to manage and interact with the drugs, tenders, suppliers, and pharmacy data.

## Technologies Used:
- **C#**: Programming language used for both backend (API) and frontend (WebForms) development.
- **.NET Framework**: Framework used to build the RESTful API and the WebForms frontend.
- **Microsoft SQL Server**: Database for storing drugs, tenders, suppliers, and pharmacy-related data.
- **Entity Framework**: ORM used for database interactions in the backend.
- **WebForms**: Used to create dynamic web pages for the administrative system.
- **Bootstrap**: Frontend CSS framework used for responsive design.

## Setup Instructions

### Step 1: Run the API Backend

Before running the administrative system, you need to first set up and run the RESTful API that handles all data operations.

#### 1.1 Clone the Repository
Clone the repository to your local machine:
```bash
git clone https://github.com/yourusername/Pharmaceutical-Admin-System.git](https://github.com/YoonusAnees/State-Pharmaceutical-Cooperation-System.git
```

#### 1.2 Open the Solution in Visual Studio
- Open **Visual Studio**.
- Open the solution file (`.sln`) of the project.

#### 1.3 Install Dependencies via NuGet
The necessary packages and dependencies for the project are installed using **NuGet Package Manager**.

1. Right-click the solution in **Solution Explorer** and click **Manage NuGet Packages for Solution**.
2. Ensure that all required packages, such as **Entity Framework**, **ASP.NET WebForms**, and **Microsoft SQL Server**, are installed. You can search for the following packages if not installed:
   - **Entity Framework**: `EntityFramework.SqlServer`
   - **Microsoft SQL Server**: `Microsoft.EntityFrameworkCore.SqlServer`
   - **ASP.NET WebForms**: Ensure the WebForms feature is enabled in Visual Studio.

#### 1.4 Configure the Database
1. Create a new **SQL Server database** (e.g., `PharmaDB`) using **SQL Server Management Studio (SSMS)** or any SQL client.
2. Update the connection string in the **`web.config`** file with your **SQL Server** credentials:

```xml
<connectionStrings>
  <add name="DefaultConnection" connectionString="Server=localhost;Database=PharmaDB;User Id=your_username;Password=your_password;" providerName="System.Data.SqlClient" />
</connectionStrings>
```

#### 1.5 Apply Migrations (If Using Entity Framework)
To set up the database schema, open the **Package Manager Console** and run:

```bash
Update-Database
```

This will apply any pending migrations and create the necessary tables in your **PharmaDB**.

#### 1.6 Run the API
1. https://github.com/YoonusAnees/StatePharmaceuticalCooperationAPI.git

### Step 2: Run the WebForms Administrative System

Once the **API** is running, you can proceed to run the **WebForms-based administrative system**.

#### 2.1 Clone the Frontend (if applicable)
If the frontend is a separate project, clone the frontend repository:
```bash
git clone https://github.com/yourusername/Pharmaceutical-Admin-Frontend.git
```

#### 2.2 Open the Frontend Project in Visual Studio
- Open **Visual Studio**.
- Open the **WebForms** project (`.csproj`) that serves as the frontend for the administrative system.

#### 2.3 Configure API Endpoint in WebForms
Make sure that the WebForms application is set to interact with the correct **API endpoint** (e.g., `http://localhost:5000/api`). You may need to update a configuration file or **Web.config** in the WebForms project to point to the backend API.

#### 2.4 Running the WebForms Application
1. Press **F5** or click **Start** to run the project in **Visual Studio**.
2. The WebForms application should be accessible in the browser at `http://localhost:PortNumber/` (PortNumber will vary based on your project setup).

---

## WebForms Pages

### Key Pages in the WebForms Admin System:
1. **Drugs.aspx**: Manage drugs, view stock, and update drug details.
2. **Tenders.aspx**: Track and manage tender information.
3. **Suppliers.aspx**: Add and manage suppliers for the pharmacy.
4. **PharmacyDashboard.aspx**: Overview of pharmacy operations and statistics.

---

## API Endpoints

Here are some of the key API endpoints provided by the backend:

- `GET /api/drugs`: Retrieves a list of all drugs available in the pharmacy.
- `POST /api/drugs`: Adds a new drug to the system.
- `GET /api/tenders`: Retrieves all tender records.
- `POST /api/tenders`: Creates a new tender record.
- `GET /api/suppliers`: Retrieves a list of suppliers.
- `POST /api/suppliers`: Adds a new supplier to the system.
- `GET /api/pharmacy`: Retrieves the current status of the pharmacy (e.g., current sales, stock levels).

## Usage

### Example API Request for Adding a Drug
To add a new drug to the system, send a **POST** request to `/api/drugs` with the following JSON payload:

```json
{
  "name": "Aspirin",
  "dosage": "500mg",
  "manufacturer": "XYZ Pharmaceuticals",
  "price": 10.0,
  "stock": 150
}
```

### Example API Request for Adding a Tender
To create a new tender, send a **POST** request to `/api/tenders` with the following JSON payload:

```json
{
  "drugId": 1,
  "supplierId": 2,
  "price": 8.0,
  "quantity": 200,
  "expirationDate": "2023-12-31"
}
```

### Example API Request for Retrieving All Suppliers
To get a list of all suppliers, send a **GET** request to `/api/suppliers`.

---

## Contributing

We welcome contributions! If you'd like to contribute to this project, follow these steps:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature-branch`).
3. Make your changes.
4. Commit your changes (`git commit -am 'Add new feature'`).
5. Push to the branch (`git push origin feature-branch`).
6. Open a pull request.

---

## License

This project is open-source and available under the [MIT License](LICENSE).

---

### Notes:
- **Replace** `"your_username"` and `"your_password"` in the connection string with your actual credentials.
- **Run the API first**, then the WebForms application. The administrative system relies on the API to fetch and update data.
- **Testing**: Use Postman or Swagger to test the API endpoints.

---

This **README** now includes instructions for setting up and running both the **API** and **WebForms frontend** for your pharmacy management system. Let me know if you need further customization or have additional questions!
