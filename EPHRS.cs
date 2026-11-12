using System;
using System.Collections.Generic;
using System.Linq;

public class Employee
{
    public int Id { get; set; }                 // Col 1: Unique identifier (auto-increment starting at 1001)
    public string FirstName { get; set; }       // Col 2: First name
    public string LastName { get; set; }        // Col 3: Last name
    public string Department { get; set; }      // Col 4: Department name (e.g., IT, HR, Sales, Finance)
    public string JobTitle { get; set; }        // Col 5: Position title (e.g., Software Engineer, Manager)
    public decimal MonthlySalary { get; set; }  // Col 6: Base monthly salary amount[cite: 7]
    public double TaxRate { get; set; }         // Col 7: Tax deduction percentage (e.g., 0.12 for 12%)[cite: 7]
    public DateTime HireDate { get; set; }      // Col 8: Date of employment joining[cite: 7]
    public bool IsActive { get; set; }          // Col 9: Employment status flag (True = Active, False = Inactive)[cite: 7]
    public string Email { get; set; }           // Col 10: Company email address[cite: 7]
}

class Program
{
    // Starts with an empty list (0 pre-defined records)[cite: 7]
    static List<Employee> employees = new List<Employee>();
    static int nextId = 1001;

    static void Main(string[] args)
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("=== EMPLOYEE PAYROLL & HR MANAGEMENT SYSTEM Version 2.0 ===");
            Console.WriteLine("1. Create (Add Employee)");
            Console.WriteLine("2. Read (View All Employees)");
            Console.WriteLine("3. Search / Filter Employees");
            Console.WriteLine("4. Update Employee");
            Console.WriteLine("5. Delete Employee");
            Console.WriteLine("6. Exit");
            Console.Write("\nSelect an option: ");
            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    CreateEmployee();
                    break;
                case "2":
                    ReadEmployees();
                    break;
                case "3":
                    SearchEmployees();
                    break;
                case "4":
                    UpdateEmployee();
                    break;
                case "5":
                    DeleteEmployee();
                    break;
                case "6":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

            if (running)
            {
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }
    }

    static void CreateEmployee()
    {
        Console.WriteLine("--- Add New Employee ---");
        Console.Write("Enter First Name: ");
        string firstName = Console.ReadLine();

        Console.Write("Enter Last Name: ");
        string lastName = Console.ReadLine();

        Console.Write("Enter Department (e.g., IT, HR, Sales, Finance): ");
        string department = Console.ReadLine();

        Console.Write("Enter Job Title (e.g., Software Engineer, Manager): ");
        string jobTitle = Console.ReadLine();

        Console.Write("Enter Monthly Salary: ");
        decimal.TryParse(Console.ReadLine(), out decimal monthlySalary);

        Console.Write("Enter Tax Rate (e.g., 0.12 for 12%): ");
        double.TryParse(Console.ReadLine(), out double taxRate);

        Console.Write("Enter Hire Date (YYYY-MM-DD): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime hireDate))
        {
            hireDate = DateTime.Now;
        }

        Console.Write("Is Active? (true/false): ");
        bool.TryParse(Console.ReadLine(), out bool isActive);

        Console.Write("Enter Company Email: ");
        string email = Console.ReadLine();

        Employee newEmp = new Employee
        {
            Id = nextId++,
            FirstName = firstName,
            LastName = lastName,
            Department = department,
            JobTitle = jobTitle,
            MonthlySalary = monthlySalary,
            TaxRate = taxRate,
            HireDate = hireDate,
            IsActive = isActive,
            Email = email
        };

        employees.Add(newEmp);
        Console.WriteLine($"\nEmployee added successfully! Assigned ID: {newEmp.Id}");
    }

    static void ReadEmployees()
    {
        Console.WriteLine("--- Employee Directory ---");
        if (employees.Count == 0)
        {
            Console.WriteLine("No employees found in the system.");
            return;
        }

        foreach (var e in employees)
        {
            decimal netSalary = e.MonthlySalary - (e.MonthlySalary * (decimal)e.TaxRate);
            Console.WriteLine($"ID: {e.Id} | Name: {e.FirstName} {e.LastName} | Dept: {e.Department} | Title: {e.JobTitle} | Gross: {e.MonthlySalary:C} | Net: {netSalary:C} | Active: {e.IsActive} | Email: {e.Email}");
        }
    }

    static void SearchEmployees()
    {
        Console.WriteLine("--- Search & Filter Employees ---");
        if (employees.Count == 0)
        {
            Console.WriteLine("No employees to search.");
            return;
        }

        Console.Write("Enter search keyword (Name or Department): ");
        string keyword = Console.ReadLine()?.ToLower();

        // Using LINQ Where clause for filtering
        var results = employees.Where(e => 
            e.FirstName.ToLower().Contains(keyword) || 
            e.LastName.ToLower().Contains(keyword) || 
            e.Department.ToLower().Contains(keyword)).ToList();

        if (results.Count == 0)
        {
            Console.WriteLine("No matching employees found.");
            return;
        }

        Console.WriteLine($"\nFound {results.Count} match(es):");
        foreach (var e in results)
        {
            Console.WriteLine($"ID: {e.Id} | {e.FirstName} {e.LastName} | {e.Department} | {e.JobTitle}");
        }
    }

    static void UpdateEmployee()
    {
        ReadEmployees();
        if (employees.Count == 0) return;

        Console.Write("\nEnter Employee ID to update: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            // Using LINQ FirstOrDefault to find employee
            Employee emp = employees.FirstOrDefault(e => e.Id == id);
            if (emp != null)
            {
                Console.Write($"Enter new First Name (current: {emp.FirstName}): ");
                string val = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(val)) emp.FirstName = val;

                Console.Write($"Enter new Department (current: {emp.Department}): ");
                val = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(val)) emp.Department = val;

                Console.Write($"Enter new Monthly Salary (current: {emp.MonthlySalary}): ");
                string salInput = Console.ReadLine();
                if (decimal.TryParse(salInput, out decimal newSal)) emp.MonthlySalary = newSal;

                Console.WriteLine("\nEmployee updated successfully!");
            }
            else
            {
                Console.WriteLine("\nEmployee not found.");
            }
        }
        else
        {
            Console.WriteLine("\nInvalid ID format.");
        }
    }

    static void DeleteEmployee()
    {
        ReadEmployees();
        if (employees.Count == 0) return;

        Console.Write("\nEnter Employee ID to delete: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            int removedCount = employees.RemoveAll(e => e.Id == id);
            if (removedCount > 0)
            {
                Console.WriteLine("\nEmployee deleted successfully.");
            }
            else
            {
                Console.WriteLine("\nEmployee ID not found.");
            }
        }
        else
        {
            Console.WriteLine("\nInvalid ID format.");
        }
    }
}