<Query Kind="Program" />

//  This LINQ examples is to show you what is expected in Take-home #3 (LINQ-OLPT)
void Main()
{
	#region Driver  
	try
	{
		//  The driver must at minimum perform 3 different task. 
		//  Task 1
		//		add a new pet
		PetView addPet = new PetView();
		addPet.Name = "Billy Bob";
		addPet.Breed = "Bull Dog";
		addPet.Dump("Before Add");

		PetView afterAdd = NewEditPet(addPet);
		afterAdd.Dump("After Add - Updated PetID");

		Console.WriteLine("-----------");
		//  Task 2
		//	edit an existing pet
		afterAdd.Dump("Before Edit");

		//	update properties
		afterAdd.Name = "Rain Drop";
		afterAdd.Breed = "Wolfhound";

		PetView afteredit = NewEditPet(afterAdd);
		afteredit.Dump("After Edit - Updated Name and Breed");


		//  Task 3
		//		attempt to add new pet with invalid data that will trigger all the business in this exercise

		//	Missing name & breed 
		//	comment out after testing
		PetView missingNameBreed = new PetView();
		//	NewEditPet(missingNameBreed);

		//	Pet name cannot be a single word (Buddy) but 2 words (Red Rover)
		//	comment out after testing
		PetView singleName = new PetView();
		singleName.Name = "John";
		singleName.Breed = "Anything";
		//	NewEditPet(singleName);

		//	The first word in the name must be a mininum of 3 letters
		//	comment out after testing
		PetView minCharacterName = new PetView();
		minCharacterName.Name = "Ab Smith";
		minCharacterName.Breed = "Anything";
		//	NewEditPet(minCharacterName);
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


public PetView NewEditPet(PetView pet)
{
	// --- Business Logic and Parameter Exception Section ---
	#region Business Logic and Parameter Exceptions
	//	create a list<Exception> to contain all discovered errors
	List<Exception> errorList = new List<Exception>();
	//	All business rules are placed here. 
	//	Rule:	Pet name is required
	//	Rule:	Pet name cannot be a single word (Buddy) but 2 words (Red Rover)
	//	Rule:	The first word in the name must be a mininum of 3 letters
	//	Rule:	Pet breed is required

	//	These are processing rules that need to be satisfied
	//		for valid data

	//	Rule:	Pet name is required
	if (string.IsNullOrWhiteSpace(pet.Name))
	{
		errorList.Add(new Exception("Pet name is required"));
	}

	//	Rule:	Pet breed is required
	if (string.IsNullOrWhiteSpace(pet.Breed))
	{
		errorList.Add(new Exception("Pet Breed is required"));
	}

	//	verify that we have a pet name 
	if (!string.IsNullOrWhiteSpace(pet.Name))
	{
		//	Rule:	Pet name cannot be a single word (Buddy) but 2 words (Red Rover)
		if (pet.Name.Split(' ').Count() < 2)
		{
			errorList.Add(new Exception("Pet name cannot be a single word"));
		}

		//	Rule:	The first word in the name must be a mininum of 3 letters
		var petName = pet.Name.Split(' ');
		if ((petName.Count() > 1) && petName[0].Length < 3)
		{
			errorList.Add(new Exception("The first word in the name must be a mininum of 3 letters"));
		}
	}
	#endregion

	// --- Main Method Logic Section ---
	#region Method Code
	PetView newPetView = new();
	newPetView.PetID = pet.PetID;
	newPetView.Name = pet.Name;
	newPetView.Breed = pet.Breed;

	//	Fake code for adding to database
	List<PetView> pets = new List<PetView>();
	if (pet.PetID == 0)
	{
		pets.Add(newPetView);
	}
	#endregion

	// --- Error Handling and Database Changes Section ---
	#region Check for errors and SaveChanges

	// Check for the presence of any errors.
	if (errorList.Count() > 0)
	{
		// If errors are present, clear any changes tracked by Entity Framework 
		// to avoid persisting erroneous data.
		//	ChangeTracker.Clear();

		// Throw an aggregate exception containing all errors found during processing.
		throw new AggregateException("Unable to proceed!  Check concerns", errorList);
	}
	else
	{
		// If no errors are present, commit changes to the database.
		//SaveChanges();
		// fake code to save record and get new PetID
		if (newPetView.PetID == 0)
		{
			Random rand = new Random();
			newPetView.PetID = rand.Next(1, 100); //returns random number between 1-99
		}
	}

	return newPetView;
	#endregion
}

public class PetView
{
	public int PetID { get; set; }
	public string Name { get; set; }
	public string Breed { get; set; }
}