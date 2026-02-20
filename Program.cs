using System;
using System.IO;

namespace TicketManagerIO
{
    internal static class Program
    {
        private static void Main()
        {
            //create a var of your new TicketManager() class. Initialize as new. Suggested name is 'manager'
            var manager = new TicketManager();
			
			//Greet the user warmly
            Console.WriteLine("=== Welcome to the IT Support Ticket Manager ===");

			//Encapsulate the UI in a while loop
            bool running = true;
            while (running)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Add Ticket");
                Console.WriteLine("2. Remove Ticket");
                Console.WriteLine("3. Display All Tickets");
                Console.WriteLine("4. Close Ticket");
                Console.WriteLine("5. Reopen Ticket");
                Console.WriteLine("6. Save Tickets to File");
                Console.WriteLine("7. Load Tickets from File");
                Console.WriteLine("8. Show Open Ticket Count");
                Console.WriteLine("9. Exit");
                Console.Write("Choose: ");
                string? choice = Console.ReadLine()?.Trim();

                try
                {
                    switch (choice)
                    {
                        //Call each case, 1-8 and call the appropriate method with your TicketManager() instance
                        //Don't forget that each switch statement requires a break;
                        case "1":
                            Console.Write("Enter ID: ");
                            string id = Console.ReadLine()!.Trim();
                            Console.Write("Enter Description: ");
                            string desc = Console.ReadLine()!.Trim();
                            Console.Write("Enter Priority: ");
                            string priority = Console.ReadLine()!.Trim();
                            Console.Write("Enter Status: ");
                            string status = Console.ReadLine()!.Trim();

                            var ticket = new Ticket(id, desc, priority, status);
                            manager.AddTicket(ticket);
                            Console.WriteLine("Ticket added successfully.");
                            break;

                        case "2":
                            Console.Write("Enter ID to remove: ");
                            string removeId = Console.ReadLine()!.Trim();
                            if (manager.RemoveTicket(removeId))
                                Console.WriteLine("Ticket removed successfully.");
                            else
                                Console.WriteLine("Ticket not found.");
                            break;

                        case "3":
                            foreach (var t in manager.GetAllTickets())
                                Console.WriteLine(t.GetDisplayText());
                            break;

                        case "4":
                            Console.Write("Enter ID to close: ");
                            string closeId = Console.ReadLine()!.Trim();
                            var closeTicket = manager.FindTicket(closeId);
                            if (closeTicket != null)
                            {
                                closeTicket.CloseTicket();
                                Console.WriteLine("Ticket closed.");
                            }
                            else
                                Console.WriteLine("Ticket not found.");
                            break;

                        case "5":
                            Console.Write("Enter ID to reopen: ");
                            string reopenId = Console.ReadLine()!.Trim();
                            var reopenTicket = manager.FindTicket(reopenId);
                            if (reopenTicket != null)
                            {
                                reopenTicket.Status = "Open";
                                Console.WriteLine("Ticket reopened.");
                            }
                            else
                                Console.WriteLine("Ticket not found.");
                            break;

                        case "6":
                            Console.Write("Enter file path to save tickets: ");
                            string savePath = Console.ReadLine()!.Trim();
                            manager.SaveTickets(savePath);
                            Console.WriteLine("Tickets saved successfully.");
                            break;

                        case "7":
                            Console.Write("Enter file path to load tickets: ");
                            string loadPath = Console.ReadLine()!.Trim();
                            manager.LoadTickets(loadPath);
                            Console.WriteLine("Tickets loaded successfully.");
                            break;

                        case "8":
                            int openCount = manager.CountOpenTickets();
                            Console.WriteLine($"There are {openCount} open tickets.");
                            break;

                        case "9": running = false; break;
                        default: Console.WriteLine("Invalid option."); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            //Give the use a goodbye message
            Console.WriteLine("\nThank you for using the IT Support Ticket Manager. Goodbye!");
        }

        private static void AddTicketMenu(TicketManager manager)
        {
            Console.Write("Enter Ticket ID (e.g., T1001): ");
            string id = Console.ReadLine() ?? "";

            Console.Write("Enter Description: ");
            string desc = Console.ReadLine() ?? "";

            Console.Write("Enter Priority (Low/Medium/High): ");
            string priority = NormalizeCase(Console.ReadLine());

            Console.Write("Enter Status (Open/In Progress/Closed): ");
            string status = NormalizeCase(Console.ReadLine());

            //Create a new Ticket using the overloaded Constructor
            var ticket = new Ticket(id, desc, priority, status);
            manager.AddTicket(ticket);

            //Then use AddTicket from your ticketmanager to pass it to the list
            Console.WriteLine("Ticket added.");
        }

        private static void RemoveTicketMenu(TicketManager manager)
        {
            Console.Write("Enter Ticket ID to remove: ");
            string id = Console.ReadLine() ?? "";
            //Check if the Ticket ID exists, then call RemoveTicket()
            //Update the user if it was removed or not. Ternary operator is your friend.
            bool removed = manager.RemoveTicket(id);
            Console.WriteLine(removed ? "Ticket removed successfully." : "Ticket not found.");
        }
        }

		//Create another Private Static Void to Close and Reopen tickets.
		//There are many ways you could do this. Try to use one void that accepts either option from the UI
        private static void UpdateTicketStatusMenu(TicketManager manager, string action)
        {
            Console.Write($"Enter Ticket ID to {action.ToLower()}: ");
            string id = Console.ReadLine() ?? "";

            var ticket = manager.FindTicket(id);
            if (ticket == null)
            {
                Console.WriteLine("Ticket not found.");
                return;
            }

            // Update the status based on action
            ticket.Status = action.Equals("Close", StringComparison.OrdinalIgnoreCase) ? "Closed" : "Open";
            Console.WriteLine($"Ticket {action.ToLower()}d successfully.");
        }
        //This method should require no change. Learn from this for your LoadMenu() module
        private static void SaveMenu(TicketManager manager)
        {
            Console.Write("Enter path to save CSV (e.g., tickets.csv): ");
            string path = Console.ReadLine() ?? "";
            try
            {
                manager.SaveTickets(path);
                Console.WriteLine($"Saved to {Path.GetFullPath(path)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Save failed: {ex.Message}");
            }
        }

        private static void LoadMenu(TicketManager manager)
        {
            Console.Write("Enter path to load CSV (e.g., tickets.csv): ");
            string path = Console.ReadLine() ?? "";
            //Create a Try/Catch/Catch block here
            //You're going to try to open the file

            //One catch will be if the file does not exists

            //At least one other catch will be if the file is not formatted correctly
       
            try
            {
                manager.LoadTickets(path);
                Console.WriteLine("Tickets loaded successfully.");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Error: The specified file does not exist.");
            }
            catch (InvalidDataException ex)
            {
                Console.WriteLine($"Error: File is not formatted correctly. {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
    }

		//Used to prevent rejection based on "low" instead of "Low" ect.
        private static string NormalizeCase(string? input)
        {
            if (string.IsNullOrWhiteSpace(input)) return "";
            // Title case for expected values, e.g., "in progress" -> "In Progress"
            var s = input.Trim().ToLowerInvariant();
            if (s == "low") return "Low";
            if (s == "medium" || s == "med") return "Medium";
            if (s == "high") return "High";
            if (s == "open") return "Open";
            if (s == "in progress" || s == "in-progress" || s == "progress") return "In Progress";
            if (s == "closed" || s == "close") return "Closed";
            return input.Trim(); // leave as-is; Ticket validation will enforce allowed set
        }
    }
}
