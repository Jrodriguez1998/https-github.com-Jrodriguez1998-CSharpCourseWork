using System;

namespace TicketManagerIO
{
    public class Ticket
    {
        // Allowed values (kept as strings to match the assignment spec)
        public static readonly string[] AllowedPriorities = { "Low", "Medium", "High" };
        public static readonly string[] AllowedStatuses   = { "Open", "In Progress", "Closed" };

		//Private variables initialized and named according to C# guidelines
        private string _id = "";
        private string _description = "";
        private string _priority = "Low";
        private string _status = "Open";

        public string Id
        {
            get => _id;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("ID cannot be empty.",  nameof(value));
                }
                _id = value;
            }
			//Create a set=> method that rejects null or whitespace values and returns an argument that the ID cannot be empty.
        }

        public string Description
        {
            get => _description;
			//Create a set=> method that rejects null or whitespace values and returns an argument that the description cannot be empty.
            set
            {  if (string.IsNullOrEmpty(value)) 
                {   throw new ArgumentException("Description cannot be empty.", nameof(value));
                }
            _description = value;
        }

        public string Priority
        {
            get => _priority;
			//This one is a freebie, use it for help below
            set
            {
                var v = (value ?? "").Trim();
                if (Array.IndexOf(AllowedPriorities, v) < 0)
                    throw new ArgumentException($"Priority must be one of: {string.Join(", ", AllowedPriorities)}");
                _priority = v;
            }
        }

        public string Status
        {
            get => _status;
            //Make a set method in the same style as above, rejecting anything not in the Allowed Statuses Array
            set
            {
                var v = (value ?? "").Trim();
                if (Array.IndexOf(AllowedStatuses, v) < 0)
                    throw new ArgumentException($"Status must be one of: {string.Join(", ", AllowedStatuses)}");
                _status = v;
            }
        }
		
		//Gifting you this one, it's C#'s way to get the current DateTime from the system
        public DateTime DateCreated { get; private set; } = DateTime.UtcNow;

        // Constructors
        //Default Constructor
		public Ticket(string id, string description, string priority, string status) 
        {
            Id = id;
            Description = description;
            Priority = priority;
            Status = status;
        }

		//Make an overloaded constructor that accepts string id, string description, string priority and string status
		//Then assign those values to the public class variables

		
		//make a public void called CloseTicket() that sets Status to "Closed"
		public void CloseTicket()
        {
            Status = "Closed";
        }
		
		//make a public void called ReopenTicket() that sets Status to "Open"
        public void ReopenTicket()
        {
            Status = "Open";
        }
        //make a public string that returns all public variables in the following format:
        //[T1001] (High) - "Printer not working" | Status: Open | Created: 2025-10-29
        public string GetDisplayText()
        {
            return $"[{Id}] ({Priority}) - \"{Description}\" | Status: {Status} | Created: {Created:yyyy-MM-dd}";
        }
    }
}
