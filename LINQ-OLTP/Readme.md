
# TRX Implementation Exercise (10 marks)

## **Weight 5%** - Take Home
---
## Marking Rubric

| Area                | Weight | Earned Mark | Notes |
|:--------------------|:------:|:-----------:|:------|
| Main: Driver routine   |   3    |   **TBA**   |       |
| AddEmployeeRegistration Method |   6    |   **TBA**   |       |
| GetEmployeeRegistration Method        |   1    |   **TBA**   |       |
| Penalties (Missing methods, rules, logic)        |   max -3    |   **TBA**   |       |
| **Total**           |  *10*   |   **TBA**   |       |

----

## Form Purpose (Reference Only)

The image displayed on this page serves as a reference for the task at hand but will not be implemented as code until our next take-home assignment. This simple data entry screen is designed to allow for adding a new employee and their skills, registering new skills for an existing employee, or updating an employee's name using their phone number. Please note that there is no ability to remove an existing skill with this form. **If a skill already exists and is not selected on the website, the skill will not be removed**. It's important to emphasize that the entire form will be processed as a single transaction (SaveChanges()) in the Business Logic Layer (BLL).

<br>
----


![Registration Sheet Data](Employee%20-%20New%20Skill%20Registrtation.png)

## Requirements

When processing data in the web form and implementing the required service method(s), ensure the following:

### Clear 
**Not used in this assignment**

- The "Clear" button should reset the current form, removing any data entered.

### Register

- First name, last name, and phone number are mandatory fields. 
- For a new employee, at least one new valid skill must be added.
- You may update the skill of an existing employee.  However, all employee information is required.
- No new skills are required if you are updating employee personal information (First/Last Name) for an existing employee.
- Active should be set to true.
- Individuals can possess multiple skills. Each skill selection must adhere to the following:
  - A valid "Level" is required.
  - "Years of Experience" (YOE) is optional but must meet specific criteria:
    - YOE must be a positive, non-zero integer or null.
    - If YOE is provided, it must fall within the range of 1 to 50 (inclusive).
  - "Hourly Wage" is required and must meet these conditions:
    - Hourly Wage should be a positive, non-zero decimal.
    - Hourly Wage must be within the range of $15.00 to $100.00 (inclusive).


---
## Code:

### Create a new LINQPad file using the "C# Program" and copy the provided code snippet into it.
```csharp
void Main()
{
	//  YOUR NAME HERE:

	#region Driver  //  3 Marks
	try
	{
        //  The driver must, at minimum perform three different task. 
        //  Task 1
        //  -   Add a new employee and register their skills (minimun of two skills). 
        	    
        	    
        //  Task 2 update an employee and their skill list. 
        //  -   Updating their first or last name
        //  -   Updating one existing skill
        //  -   adding a minimum of one new skill
        	    
        	    
        //  Task 3 attempts to register new skills with invalid data that will trigger all the business in this exercise
        //  Refer to business rules for all test cases
	    
	}
	#endregion

	#region catch all exceptions 
	catch (AggregateException ex)
	{
		foreach (var error in ex.InnerExceptions)
		{
			error.Message.Dump();
		}
	}
	catch (ArgumentNullException ex)
	{
		GetInnerException(ex).Message.Dump();
	}
	catch (Exception ex)
	{
		GetInnerException(ex).Message.Dump();
	}
	#endregion
}
private Exception GetInnerException(Exception ex)
{
	while (ex.InnerException != null)
		ex = ex.InnerException;
	return ex;
}



#region Methods

#region AddEditEmployeeRegistration Method   //  6 Marks
public EmployeeRegistrationView AddEditEmployeeRegistration(EmployeeRegistrationView employeeRegistration)
{
	// --- Business Logic and Parameter Exception Section --- 
	#region Business Logic and Parameter Exception  //  2 Marks


	#endregion

	// --- Main Method Logic Section --- 
	#region Method Code //  3 Marks

	// Actual logic to add or edit data in the database goes here. 

	#endregion

	#region Check for errors and saving of data //  1 Marks

	// --- Error handling and saving

	#endregion		
	return null;
}
#endregion

#region GetEmployeeRegistration Method    //  1 Marks
//  your code here
#endregion

#endregion

/// <summary> 
/// Contains class definitions that are referenced in the current LINQ file. 
/// </summary> 
/// <remarks> 
/// It's crucial to highlight that in standard development practices, code and class definitions  
/// should not be mixed in the same file. Proper separation of concerns dictates that classes  
/// should have their own dedicated files, promoting modularity and maintainability. 
/// </remarks> 
#region Class/View Model   

public class EmployeeRegistrationView
{
	public int EmployeeID { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string HomePhone { get; set; }
	public bool Active { get; set; }
	public List<EmployeeSkillView> EmployeeSkills { get; set; } = new();
}

public class EmployeeSkillView
{
	public int EmployeeSkillID { get; set; }
	public int EmployeeID { get; set; }
	public int SkillID { get; set; }
	public int Level { get; set; }
	public int? YearsOfExperience { get; set; }
	public decimal HourlyWage { get; set; }
}

#endregion

#region Supporting Methods
/// <summary>
/// Generates a random phone number.
/// The generated phone number ensures the first digit is not 0 or 1.
/// </summary>
/// <returns>A random phone number.</returns>
public static string RandomPhoneNumber()
{
	var random = new Random();
	string phoneNumber = string.Empty;

	// Ensure the first digit isn't 0 or 1.
	int firstDigit = random.Next(2, 10); // Generates a random digit between 2 and 9.
	phoneNumber = $"{firstDigit}";

	// Generate the rest of the digits.
	for (int i = 1; i < 10; i++)
	{
		int currentDigit = random.Next(10);
		phoneNumber = $"{phoneNumber}{currentDigit}";

		// Add periods after every third digit (except for the last period).
		if (i % 3 == 2 && i != 8)
		{
			phoneNumber = $"{phoneNumber}.";
		}
	}

	return phoneNumber;
}

/// <summary>
/// Generates a random name of a given length.
/// The generated name follows a pattern of alternating consonants and vowels.
/// </summary>
/// <param name="len">The desired length of the generated name.</param>
/// <returns>A random name of the specified length.</returns>
public static string GenerateName(int len)
{
	// Create a new Random instance.
	Random r = new Random();

	// Define consonants and vowels to use in the name generation.
	string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
	string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };

	string Name = "";

	// Start the name with an uppercase consonant and a vowel.
	Name += consonants[r.Next(consonants.Length)].ToUpper();
	Name += vowels[r.Next(vowels.Length)];

	// Counter for tracking the number of characters added.
	int b = 2;

	// Add alternating consonants and vowels until we reach the desired length.
	while (b < len)
	{
		Name += consonants[r.Next(consonants.Length)];
		b++;
		Name += vowels[r.Next(vowels.Length)];
		b++;
	}

	return Name;
}
#endregion
```
---

![ERD](workschedule_erd_jan2021.png)