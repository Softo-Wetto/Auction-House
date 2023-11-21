using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace AuctionHouse
{
    class Program //class for this main page
    {
        public static void Main() //start of program
        {
            var main_menu_num = new string[] { "1", "2", "3" }; //User selection. Anything outside will be rejected

            Console.WriteLine("+------------------------------+");
            Console.WriteLine("| Welcome to the Auction House |"); //A welcome message 
            Console.WriteLine("+------------------------------+");

            while (true) //while it is looping
            {
                Console.WriteLine("\nMain Menu");
                Console.WriteLine("---------");
                Console.WriteLine("(1) Register");
                Console.WriteLine("(2) Sign In");
                Console.WriteLine("(3) Exit");
                Console.WriteLine("\nPlease select an option between 1 and 3");
                Console.Write("> "); string UserInput = Console.ReadLine(); //Reads Users input

                if (UserInput == "1")
                {
                    Registering(); //A method that goes to the registering page when a user presses "1"
                }

                else if (UserInput == "2")
                {
                    Sign(); //A method that goes to the sign in page when a user presses "2"
                }

                else if (UserInput == "3")
                {
                    Farewell(); //A metod that goes to the farewell page when a user presses "3"
                }

                else if (string.IsNullOrEmpty(UserInput)) //If the input is null
                {
                    Console.WriteLine("\nEnter a valid number between 1 and 3."); //A error message
                }
                else if (!main_menu_num.Contains(UserInput)) //If the input does not contain in the array
                {
                    Console.WriteLine("\nEnter a valid number between 1 and 3."); //A error message
                }
                else
                    break; //If the input is satisfactory, then break.
            }
        }

        static void Farewell() // A farewell message when the user inputs "3"
        {
            Console.WriteLine("+--------------------------------------------------+");
            Console.WriteLine("| Good bye, thank you for using the Auction House! |");
            Console.WriteLine("+--------------------------------------------------+");
            Environment.Exit(-1); //Exits gracefully
        }

        static void Registering() // Registration Page
        {
            string fileContents = File.ReadAllText(@"email.txt"); //Read all contents in the email text file

            Console.WriteLine("\nRegistration");
            Console.WriteLine("------------\n");
            //REGEX
            string username, email, password;
            string username_check = @"^[a-zA-Z].{2,}$"; //regular expression for username, email and password.
            string email_check = @"(?:[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|)@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])";
            string password_check = @"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W]).{8,32}$";

            while (true) // Entering the username 
            {
                Console.WriteLine("Please enter your name");
                Console.Write("> "); username = Console.ReadLine(); //Waits for user input.

                if (string.IsNullOrEmpty(username)) //If the user input is null
                {
                    Console.WriteLine("\tThe supplied value is not a valid name.\n"); //Error message
                }
                else if (!Regex.IsMatch(username, username_check)) //If the input does not match the array then display error
                {
                    Console.WriteLine("\tThe supplied value is not a valid name.\n"); //Error message
                }
                else
                    break; //If everything is satisfactory then break.
            }

            while (true) // Entering the email address
            {
                Console.WriteLine("\nPlease enter your email address");
                Console.Write("> "); email = Console.ReadLine();

                if (string.IsNullOrEmpty(email)) //Same as above.
                {
                    Console.WriteLine("\tThe supplied value is not a valid email address.");
                }
                else if (!Regex.IsMatch(email, email_check)) //if email does not match regular expression.
                {
                    Console.WriteLine("\tThe supplied value is not a valid email address.");
                }
                else if ((fileContents.Contains(email))) //If the email already exists, then don't proceed.
                {
                    Console.WriteLine("\tThe supplied address is already in use.");
                }
                else
                    break; //break if all the requirements are met.
            }

            while (true) // Entering the password.
            {
                Console.WriteLine("\nPlease choose a password"); //Below are all the requirements the user needs to follow:
                Console.WriteLine("* At least 8 characters");
                Console.WriteLine("* No white space characters");
                Console.WriteLine("* At least one upper-case letter");
                Console.WriteLine("* At least one lower-case letter");
                Console.WriteLine("* At least one digit");
                Console.WriteLine("* At least one special character");
                Console.Write("> "); password = Console.ReadLine(); //User input for the password

                if (string.IsNullOrEmpty(password))
                {
                    Console.WriteLine("\tThe supplied value is not a valid password."); //If the password is empty, loop.
                }
                else if (!Regex.IsMatch(password, password_check)) //Check if password matches with regex.
                {
                    Console.WriteLine("\tThe supplied value is not a valid password.");
                }
                else
                    break; //Break when all the requirement all met.
            }
            //Stream write all the values in the text file.
            using (StreamWriter sw = new StreamWriter(@"username.txt", true))
            {
                sw.WriteLine(username);
            }
            using (StreamWriter sw = new StreamWriter(@"email.txt", true)) //true = append
            {
                sw.WriteLine(email);
            }
            using (StreamWriter sw = new StreamWriter(@"password.txt", true))
            {
                sw.WriteLine(password);
            }
            Console.WriteLine("\nClient " + username + "(" + email + ") has successfully registered at the Auction House."); //If successful, display message
            return; //return to main menu.
        }

        static void Sign() //Signing in page.
        {
            string[] usernameArray = File.ReadAllLines(@"username.txt"); //Store all these values into list.
            List<string> username = new List<string>(usernameArray);
            string[] emailArray = File.ReadAllLines(@"email.txt");
            List<string> email = new List<string>(emailArray);
            string[] passwordArray = File.ReadAllLines(@"password.txt");
            List<string> password = new List<string>(passwordArray);

            Console.WriteLine("\nSign In");
            Console.WriteLine("-------\n");

            int listNo;
            string passCheck, input_email;

            while (true) //keep looping until the requirements are met.
            {
                Console.WriteLine("Please enter your email address");
                Console.Write("> "); input_email = Console.ReadLine(); //user input their email.

                listNo = email.IndexOf(input_email);//sets the listNo to the index number of the password list that matched

                if (!emailArray.Contains(input_email))  //if user doesn't input the email in the list, then display error.
                {
                    Console.WriteLine("\tProvided email is incorrect\n");
                }
                else if (emailArray.Contains(input_email))  //if the user input the email in the list, then proceed.
                {
                    passCheck = Convert.ToString(password[listNo]);//sets the passCheck var to the string index no found at the same index as the user name

                    Console.WriteLine("\nPlease enter your password");
                    Console.Write("> "); string input_password = Console.ReadLine(); //user input their password

                    if (input_password == passCheck) //if the input and the passCheck are the same you logged in
                    {
                        Console.WriteLine();
                        break; //break if the email and password are the same.
                    }
                    else
                        Console.WriteLine("\tProvided email or password is incorrect.\n"); //if not, then display error
                }
            }

            string ChosenEmail = Convert.ToString(email[listNo]); //sets the email to the index of ChosenEmail
            string ChosenUser = Convert.ToString(username[listNo]); //sets the username to the index of ChosenUser
            Client(ChosenUser, ChosenEmail); //Go to next method if everything succeeds.
        }

        static void Client(string ChosenUser, string ChosenEmail) //User info. After signing in.
        {
            int unit, stNumber, postcode; //Proceeds to this page when the user successfully logs in
            string input_stName, input_stSuffix, input_city, input_state;
            var states = new string[] { "ACT", "QLD", "NSW", "NT", "SA", "TAS", "VIC", "WA" };

            string[] user_infoArray = File.ReadAllLines(@"user_info.txt");
            List<string> user_info = new List<string>(user_infoArray); //Stores the user_info value into list

            string fileContents = File.ReadAllText(@"user_info.txt"); //Reading the text file of user_info.

            if (!fileContents.Contains(ChosenEmail)) //if the email does not exist inside user_info then ask for these.
            {
                Console.WriteLine("Personal Details for {0}({1})", ChosenUser, ChosenEmail);
                Console.WriteLine("-----------------------------------------------------\n");
                Console.WriteLine("Please provide your home address.\n");

                while (true)
                {
                    Console.WriteLine("Unit number (0 = none):");
                    Console.Write("> "); string input_unit = Console.ReadLine(); //User input.

                    if (!int.TryParse(input_unit, out unit)) //If the user does not input anything, then display error.
                    {
                        Console.WriteLine("\tUnit number must be a non-negative integer.\n");
                    }
                    else if (int.Parse(input_unit) < 0 || int.Parse(input_unit) > 999) //If unit number is not between 0 and 999, then error.
                    {
                        Console.WriteLine("\tUnit number must be a non-negative integer.\n");
                    }
                    else
                        break; //break when everthing is done.
                }
                while (true)
                {
                    Console.WriteLine("\nStreet number:");
                    Console.Write("> "); string input_stNumber = Console.ReadLine();

                    if (!int.TryParse(input_stNumber, out stNumber)) //If no input, then display this.
                    {
                        Console.WriteLine("\tStreet number must be a postive integer.");
                    }
                    else if (stNumber < 0) //cannot be less than a 0
                    {
                        Console.WriteLine("\tStreet number must be a postive integer.");
                    }
                    else if (stNumber == 0) //cannot be equal to 0
                    {
                        Console.WriteLine("\tStreet number must be greater than 0.");
                    }
                    else
                        break; //break out of loop when its done.
                }
                while (true)
                {
                    Console.WriteLine("\nStreet name:");
                    Console.Write("> "); input_stName = Console.ReadLine();

                    if (string.IsNullOrEmpty(input_stName)) //input cannot be empty.
                    {
                        Console.WriteLine("\tPlease enter a value.");
                    }
                    else
                        break;
                }
                while (true)
                {
                    Console.WriteLine("\nStreet suffix:");
                    Console.Write("> "); input_stSuffix = Console.ReadLine();

                    if (string.IsNullOrEmpty(input_stSuffix)) //input cannot be empty again.
                    {
                        Console.WriteLine("\tPlease enter a value."); 
                    }
                    else
                        break;
                }
                while (true)
                {
                    Console.WriteLine("\nCity:");
                    Console.Write("> "); input_city = Console.ReadLine();

                    if (string.IsNullOrEmpty(input_city)) //input cannot be empty...again.
                    {
                        Console.WriteLine("\tPlease enter a value.");
                    }
                    else
                        break;
                }
                while (true)
                {
                    Console.WriteLine("\nState (ACT, NSW, NT, QLD, SA, TAS, VIC, WA):"); //ask for state.
                    Console.Write("> "); input_state = Console.ReadLine();
                    input_state = input_state.ToUpper(); //convert the input into uppercase

                    if (string.IsNullOrEmpty(input_state))  //cannot be empty.
                    {
                        Console.WriteLine("\tPlease enter a value.");
                    }
                    else if (!states.Contains(input_state.ToUpper())) //if the input does not contain any of the array.
                    {
                        Console.WriteLine("\tPlease enter the right state.");
                    }
                    else
                        break;
                }
                while (true)
                {
                    Console.WriteLine("\nPostcode (1000 .. 9999):");
                    Console.Write("> "); string input_postcode = Console.ReadLine();

                    if (!int.TryParse(input_postcode, out postcode)) //input cannot be empty.
                    {
                        Console.WriteLine("\tPlease enter a valid number.");
                    }
                    else if (int.Parse(input_postcode) < 1000 || int.Parse(input_postcode) > 9999) //postcode must be between 1000 and 9999
                    {
                        Console.WriteLine("\tThe supplied value is not a valid postcode.");
                    }
                    else
                        break;
                }

                using (StreamWriter sw = new StreamWriter(@"user_info.txt", true)) //write all the inputted values into text file by using streamwriter.
                {
                    sw.WriteLine(ChosenEmail);
                    sw.WriteLine(unit);
                    sw.WriteLine(stNumber);
                    sw.WriteLine(input_stName);
                    sw.WriteLine(input_stSuffix);
                    sw.WriteLine(input_city);
                    sw.WriteLine(input_state);
                    sw.WriteLine(postcode);
                }
                //Display message when everything is successful.
                Console.WriteLine("\nAddress has been updated to {0}/{1} {2} {3}, {4} {5} {6}\n", unit, stNumber, input_stName, input_stSuffix, input_city, input_state, postcode);
                ClientMenu.Client2(ChosenUser, ChosenEmail); //Go to the next method when this is done.
            }
            else
                ClientMenu.Client2(ChosenUser, ChosenEmail); //Will go to here if the email already exists in the user_info text file.
        }
    }

    class ClientMenu //New class for the client menu
    {
        static void Rewrite(string newText, string fileName, int line_to_edit) //Rewrite when asked to rewrite a value in the text file.
        {
            string[] arrLine = File.ReadAllLines(fileName);
            arrLine[line_to_edit] = newText;
            File.WriteAllLines(fileName, arrLine);
        }

        public static void Client2(string ChosenUser, string ChosenEmail) //A new method, new me.
        {
            //CLIENT MENU
            var menu_num = new string[] { "1", "2", "3", "4", "5", "6" }; //menu string array. has to contain from here.

            while (true) //Read all these textfile and put them into list when asked for it.
            {
                string[] product_emailArray = File.ReadAllLines(@"product_email.txt");
                List<string> product_email = new List<string>(product_emailArray);
                string[] product_nameArray = File.ReadAllLines(@"product_name.txt");
                List<string> product_name = new List<string>(product_nameArray);
                string[] product_descriptionArray = File.ReadAllLines(@"product_description.txt");
                List<string> product_description = new List<string>(product_descriptionArray);
                string[] product_priceArray = File.ReadAllLines(@"product_price.txt");
                List<string> product_price = new List<string>(product_priceArray);
                //Bidder Lists
                string[] bid_nameArray = File.ReadAllLines(@"bid_name.txt");
                List<string> bid_name = new List<string>(bid_nameArray);
                string[] bid_emailArray = File.ReadAllLines(@"bid_email.txt");
                List<string> bid_email = new List<string>(bid_emailArray);
                string[] bid_amtArray = File.ReadAllLines(@"bid_amt.txt");
                List<string> bid_amt = new List<string>(bid_amtArray);
                string[] deliveryArray = File.ReadAllLines(@"delivery.txt");
                List<string> delivery = new List<string>(deliveryArray);
                //Purcahsed lists
                string[] purchased_emailArray = File.ReadAllLines(@"purchased_email.txt");
                List<string> purchased_email = new List<string>(purchased_emailArray);
                string[] sold_emailArray = File.ReadAllLines(@"sold_email.txt");
                List<string> sold_email = new List<string>(sold_emailArray);
                string[] sold_nameArray = File.ReadAllLines(@"sold_name.txt");
                List<string> sold_name = new List<string>(sold_nameArray);
                string[] sold_descriptionArray = File.ReadAllLines(@"sold_description.txt");
                List<string> sold_description = new List<string>(sold_descriptionArray);
                string[] sold_priceArray = File.ReadAllLines(@"sold_price.txt");
                List<string> sold_price = new List<string>(sold_priceArray);
                string[] sold_bidPriceArray = File.ReadAllLines(@"sold_bidPrice.txt");
                List<string> sold_bidPrice = new List<string>(sold_bidPriceArray);
                string[] sold_deliveryArray = File.ReadAllLines(@"sold_delivery.txt");
                List<string> sold_delivery = new List<string>(sold_deliveryArray);

                //HOME DELIVERY 2
                int unit, stNumber, postcode;
                string input_stName, input_stSuffix, input_city, input_state;
                string p_name, p_description, input_price;
                var states = new string[] { "ACT", "QLD", "NSW", "NT", "SA", "TAS", "VIC", "WA" };

                //REGEX
                string price_check = @"^([$])[0-9,]*\.[0-9]{2}"; //regular expresion for price

                //___PRODUCT INFORMATION___
                int item_num = 0, item_change = 1; //used for checking the item in case 3 and case 4
                int chosen_item; // a new variable
                string EMAIL = ChosenEmail; //make the variable more readable.
                string fileContents = File.ReadAllText(@"product_email.txt"); //checking the files 
                string fileContents_name = File.ReadAllText(@"product_name.txt");
                string fileContents_bid = File.ReadAllText(@"bid_email.txt");
                string fileContents_purchased = File.ReadAllText(@"purchased_email.txt");

                //SELECTING the array of list specifically. Used in cases 2, 3, and 5.
                int[] VALUE_Self = product_emailArray.Select((something, i) => something == EMAIL ? i : -1).Where(i => i != -1).ToArray();
                int[] VALUE_Others = product_emailArray.Select((something, i) => something != EMAIL ? i : -1).Where(i => i != -1).ToArray();
                int[] VALUE_Purchased = purchased_emailArray.Select((something, i) => something == EMAIL ? i : -1).Where(i => i != -1).ToArray();

                Console.WriteLine("Client Menu"); //Client Menu after successful login
                Console.WriteLine("-----------");
                Console.WriteLine("(1) Advertise Product");
                Console.WriteLine("(2) View My Product List");
                Console.WriteLine("(3) Search For Advertised Products");
                Console.WriteLine("(4) View Bids On My Products");
                Console.WriteLine("(5) View My Purchased Items");
                Console.WriteLine("(6) Log off");
                Console.WriteLine("\nPlease select an option between 1 and 6");
                Console.Write("> "); string UserInput2 = Console.ReadLine(); //Wait for user input

                if (UserInput2 == "1") //If user inputs "1", then go here
                {
                    Console.WriteLine("\nProduct Advertisement for {0}({1})", ChosenUser, ChosenEmail); //Displays logged in name and email.
                    Console.WriteLine("-----------------------------------------------------------------------\n");

                    while (true)
                    {
                        Console.WriteLine("Product name");
                        Console.Write("> "); p_name = Console.ReadLine();

                        if (string.IsNullOrEmpty(p_name)) //input cannot be null.
                        {
                            Console.WriteLine("\tPlease enter a value.\n");
                        }
                        else
                            break; //go to the next while loop if successful.
                    }
                    while (true)
                    {
                        Console.WriteLine("\nProduct description");
                        Console.Write("> "); p_description = Console.ReadLine();

                        if (string.IsNullOrEmpty(p_description)) //input cannot be null.
                        {
                            Console.WriteLine("\tPlease enter a value.");
                        }
                        else
                            break;
                    }
                    while (true)
                    {
                        Console.WriteLine("\nProduct price ($d.cc)");
                        Console.Write("> "); input_price = Console.ReadLine();

                        if (string.IsNullOrEmpty(input_price)) //input cannot be null.
                        {
                            Console.WriteLine("\tPlease enter a value.");
                        }
                        else if (!Regex.IsMatch(input_price, price_check)) //price needs to be in certain format like the error message below.
                        {
                            Console.WriteLine("\tA currency value is required, e.g. $54.95, $9.99, $2314.15.");
                        }
                        else
                            break; //break when successful.
                    }
                    //Stream write all these values when the user successfully finishes the previous task.
                    using (StreamWriter sw = new StreamWriter(@"product_email.txt", true))
                    {
                        sw.WriteLine(ChosenEmail); //input chosen email into product email.
                    }
                    using (StreamWriter sw = new StreamWriter(@"product_name.txt", true))
                    {
                        sw.WriteLine(p_name); //input product name
                    }
                    using (StreamWriter sw = new StreamWriter(@"product_description.txt", true))
                    {
                        sw.WriteLine(p_description); //description
                    }
                    using (StreamWriter sw = new StreamWriter(@"product_price.txt", true))
                    {
                        sw.WriteLine(input_price); //price
                    }
                    using (StreamWriter sw = new StreamWriter(@"bid_name.txt", true))
                    {
                        sw.WriteLine("-"); // input a "-" for a placeholder.
                    }
                    using (StreamWriter sw = new StreamWriter(@"bid_email.txt", true))
                    {
                        sw.WriteLine("-"); //same as above
                    }
                    using (StreamWriter sw = new StreamWriter(@"bid_amt.txt", true))
                    {
                        sw.WriteLine("-"); //same as above
                    }
                    using (StreamWriter sw = new StreamWriter(@"delivery.txt", true))
                    {
                        sw.WriteLine("-"); //same as above
                    }
                    //display this message when everything is successful.
                    Console.WriteLine("\nSuccessfully added product {0}, {1}, {2}.\n", p_name, p_description, input_price);
                }

                else if (UserInput2 == "2") //Go here when the user inputs 2.
                {
                    Console.WriteLine("\nProduct List for {0}({1})", ChosenUser, ChosenEmail);
                    Console.WriteLine("-----------------------------------------------------------------------\n");

                    if (!fileContents.Contains(ChosenEmail)) //if chosen email does not contain in the product_email tex file, then display this message.
                    {
                        Console.WriteLine("You have no advertised products at the moment.");
                    }
                    else if (fileContents.Contains(ChosenEmail)) //if chosen email does exist in the product_email text file, then display this.
                    {
                        Console.WriteLine("Item # \t Product name \t Description \t List price \t Bidder name \t Bidder email \t Bid amt");
                        foreach (int i in VALUE_Self) //foreach item in this array of list.
                        {
                            item_num++; //display all the information
                            Console.WriteLine(item_num + "\t " + product_name[i] + "\t " + product_description[i] + "\t " + product_price[i] + "\t " + bid_name[i] + "\t " + bid_email[i] + "\t " + bid_amt[i]);
                        }
                    }
                    Console.WriteLine();
                }

                else if (UserInput2 == "3") //if user inputs "3", then go here.
                {
                    Console.WriteLine("\nProduct Search for {0}({1})", ChosenUser, ChosenEmail);
                    Console.WriteLine("-----------------------------------------------------------------------\n");
                    Console.WriteLine("\nPlease supply a search phrase (ALL to see all products)");
                    Console.Write("> "); string input_search = Console.ReadLine(); //wait for user input
                    string search = input_search.ToUpper(); //turn input into uppercase
                    
                    int[] search_input = new int[0]; //make the input into an int before processing.
                    var thing_search_list = search_input.ToList(); //make the int into list.
                    int[] VALUE_Search = product_emailArray.Select((something, i) => something != EMAIL ? i : -1).Where(i => i != -1).ToArray(); //selecting the array
                    
                    string[] results = Array.FindAll(product_nameArray, element => element.Contains(input_search)); //search product name in array.
                    string[] results2 = Array.FindAll(product_descriptionArray, element => element.Contains(input_search)); //search product description in array.
                    string selected_Name, selected_Desc, selected_Price, selected_BidName, selected_BidEmail, selected_BidAmount, selected_Delivery; //defining variables

                    foreach (string result in results) //foreach search, add the index of result into search list for both product name and description.
                    {
                        thing_search_list.Add(product_name.IndexOf(result));
                    }
                    foreach (string result in results2)
                    {
                        thing_search_list.Add(product_description.IndexOf(result));
                    }

                    List<int> intersection_Search = (from num in VALUE_Search select num).Intersect(thing_search_list).ToList(); //when the array intersects.

                    //new lists for upcoming task.
                    List<int> item_List = new List<int>();
                    List<string> name_List = new List<string>();
                    List<string> desc_List = new List<string>();
                    List<string> price_List = new List<string>();
                    List<string> bidname_List = new List<string>();
                    List<string> bidemail_List = new List<string>();
                    List<string> bidamt_List = new List<string>();
                    List<string> delivery_List = new List<string>();

                    while (true)
                    {
                        if (search == "ALL" && fileContents.Length <= 0) //if input seatch is "all" and the there is no content in text file, then display this.
                        {
                            Console.WriteLine("\n\tThere are no products to display.\n");
                            Client2(ChosenUser, ChosenEmail);
                        }
                        else if (search != "ALL" && fileContents.Length <= 0) //if input seatch is not "all" and the there is no content in text file, then display this.
                        {
                            Console.WriteLine("\n\tThere are no products to display.\n");
                            Client2(ChosenUser, ChosenEmail);
                        }
                        else if (search == "ALL" && fileContents.Length > 0) //if input is "all" and ther is content in txt file, display this.
                        {
                            Console.WriteLine("\nSearch results"); //display the search result when the above is successful.
                            Console.WriteLine("--------------\n");
                            Console.WriteLine("Item # \t Product name \t Description \t List price \t Bidder name \t Bidder email \t Bid amt");
                            foreach (int i in VALUE_Others) 
                            {
                                item_num++; //item counter
                                Console.WriteLine(item_num + "\t " + product_name[i] + "\t " + product_description[i] + "\t " + product_price[i] + "\t " + bid_name[i] + "\t " + bid_email[i] + "\t " + bid_amt[i]);

                                item_List.Add(item_num);
                                name_List.Add(product_name[i]);
                                desc_List.Add(product_description[i]);
                                price_List.Add(product_price[i]); //add the following values into the list above.
                                bidname_List.Add(bid_name[i]);
                                bidemail_List.Add(bid_email[i]);
                                bidamt_List.Add(bid_amt[i]);
                                delivery_List.Add(delivery[i]);
                            }
                            break; //break when this task is done.
                        }
                        else if (search != "ALL" && fileContents.Length > 0) //if the search isn't "all" and there is content in the txt file, display this.
                        {
                            Console.WriteLine("\nSearch results");
                            Console.WriteLine("--------------\n");
                            Console.WriteLine("Item # \t Product name \t Description \t List price \t Bidder name \t Bidder email \t Bid amt");
                            foreach (int i in intersection_Search) //uses the intersection for product_name and description when it is searched.
                            {
                                item_num++;
                                Console.WriteLine(item_num + "\t " + product_name[i] + "\t " + product_description[i] + "\t " + product_price[i] + "\t " + bid_name[i] + "\t " + bid_email[i] + "\t " + bid_amt[i]);

                                item_List.Add(item_num);
                                name_List.Add(product_name[i]);
                                desc_List.Add(product_description[i]);
                                price_List.Add(product_price[i]);
                                bidname_List.Add(bid_name[i]); //add the following values into the list above.
                                bidemail_List.Add(bid_email[i]);
                                bidamt_List.Add(bid_amt[i]);
                                delivery_List.Add(delivery[i]);
                            }
                            break; //break when task is met
                        } return; //if not then return to the client menu.
                    } 
                    while (true)
                    {
                        Console.WriteLine("\nWould you like to place a bid on any of these items (yes or no)?"); //ask if the user wants to bid
                        Console.Write("> "); string input_ask = Console.ReadLine();

                        if (string.IsNullOrEmpty(input_ask)) //input cannot be empty
                        {
                            Console.WriteLine("\tPlease enter yes or no");
                        }
                        else if (input_ask == "no") //if input is "no" then go back to client menu
                        {
                            Console.WriteLine();
                            Client2(ChosenUser, ChosenEmail);
                        }
                        else if (input_ask == "yes") //if input is yes, break out of loop and proceed to next task.
                        {
                            break;
                        } return;
                    }
                    while (true) //looping
                    {
                        Console.WriteLine("\nPlease enter a non-negative integer between {0} and {1}:", item_change, item_num); //display message.
                        Console.Write("> "); string input_item = Console.ReadLine(); //wait for user input.

                        if (!int.TryParse(input_item, out chosen_item)) //if the input is null, then display error message.
                        {
                            Console.WriteLine("\tPlease enter a valid number");
                        }
                        else if (chosen_item > item_num || chosen_item < item_change) //if the input is outside of the selection.
                        {
                            Console.WriteLine("\tPlease select an option between {0} and {1}", item_change, item_num);
                        }
                        else if (chosen_item < item_num || chosen_item > item_change || chosen_item == item_num || chosen_item == item_change) //if the input is selected
                        {
                            int productlistNo = item_List.IndexOf(chosen_item); //the product listNo is the index of the chosen item.
                            //if the user selects the number. it will select the specfic array in the list.
                            selected_Name = Convert.ToString(name_List[productlistNo]);
                            selected_Desc = Convert.ToString(desc_List[productlistNo]);
                            selected_Price = Convert.ToString(price_List[productlistNo]);
                            selected_BidName = Convert.ToString(bidname_List[productlistNo]);
                            selected_BidEmail = Convert.ToString(bidemail_List[productlistNo]);
                            selected_Delivery = Convert.ToString(delivery_List[productlistNo]);
                            selected_BidAmount = Convert.ToString(bidamt_List[productlistNo]);

                            if (selected_BidAmount == "-") //if the bid amount has a dash on it, do these steps.
                            {
                                Rewrite("$0.00", "bid_amt.txt", productlistNo); //rewrite the "-" into $0.00

                                string[] bid_amt2Array = File.ReadAllLines(@"bid_amt.txt"); //read the file again so it updates
                                List<string> bid_amt2 = new List<string>(bid_amt2Array);
                                selected_BidAmount = Convert.ToString(bid_amt2[productlistNo]); //put the new value into new list.
  
                                Console.WriteLine("\nBidding for {0} (regular price {1}), current highest bid {2}", selected_Name, selected_Price, selected_BidAmount);
                                break; //after successful, break out of the loop.
                            }
                            else //if bid amount does not have the dash and it already has the number, then display this.
                            Console.WriteLine("\nBidding for {0} (regular price {1}), current highest bid {2}", selected_Name, selected_Price, selected_BidAmount);
                            break; //after success, break out of this loop.
                        } 
                        else
                            Console.WriteLine("\tPlease select an option between {0} and {1}", item_change, item_num); //if required number isn't chosen, display this.
                    } 
                    int bidlistNo = product_name.IndexOf(selected_Name); //bidlist Number is the index of the selected name in the product name list
                    while (true)
                    {
                        Console.WriteLine("\nHow much do you bid?");
                        Console.Write("> "); string input_bidamt = Console.ReadLine(); //wait for user input

                        string result = input_bidamt.Remove(0, 1); //remove the first string in input which would be the $ symbol
                        string result2 = selected_BidAmount.Remove(0, 1); //same as above but with the selected bid amount.

                        int wrong_input; //setting new variable

                        decimal input_value;
                        Decimal.TryParse(result, out input_value); //parse the variable into decimals and only accept decimals

                        decimal value;
                        Decimal.TryParse(result2, out value); //same as above but with selected bid amount

                        if (string.IsNullOrEmpty(input_bidamt)) //input cannot be empty
                        {
                            Console.WriteLine("\tPlease enter a value");
                        }
                        else if (int.TryParse(input_bidamt, out wrong_input)) //input cannot be a int and must be decimal.
                        {
                            Console.WriteLine("\tValue must be a decimal number");
                        }
                        else if (decimal.Parse(result) <= decimal.Parse(result2)) //input must not be equal or lower than the selected bid amount
                        {
                            Console.WriteLine("\tBid amount must be greater than {0}", selected_BidAmount);
                        }
                        else if (Regex.IsMatch(input_bidamt, price_check)) //if the task above matches and it also matches the regular expression.
                        {

                            Rewrite(ChosenUser, "bid_name.txt", bidlistNo); //rewrites the dash "-" into the chosen user
                            Rewrite(ChosenEmail, "bid_email.txt", bidlistNo); //same as above but with chosen email
                            Rewrite(input_bidamt, "bid_amt.txt", bidlistNo); //sam as above but with newly inputted bid amount

                            Console.WriteLine("\nYour bid of {0} for {1} is placed.", input_bidamt, selected_Name); //message when all is done
                            break; //break out of this loop
                        }
                        else
                            Console.WriteLine("\tThe currency value is required, e.g. $55.95, $8.99, 4206.15"); //if requirement is not met, then display this message.
                    }                 
                    while (true) //while loop
                    {
                        var delivery_menu_num = new string[] { "1", "2" }; //delivery menu must be 1 or 2

                        Console.WriteLine("\nDelivery Instructions");
                        Console.WriteLine("---------------------");
                        Console.WriteLine("(1) Click and collect");
                        Console.WriteLine("(2) Home Delivery");
                        Console.WriteLine("\nPlease select an option between 1 and 2");
                        Console.Write("> "); string input_delivery = Console.ReadLine(); //wait for user input

                        switch (input_delivery) //using switch case 
                        {
                            case "1": //if the user inputs 1
                                string start_time, end_time; //variable
                                DateTime time = DateTime.Now; //finding live date set on the computer
                                DateTime start_date; //datetime variable for start date
                                DateTime end_date; //datetime variable for end date

                                while (true)
                                {
                                    Console.WriteLine("\nDelivery window start (dd/mm/yyyy hh:mm)");
                                    Console.Write("> "); start_time = Console.ReadLine(); //waiting for input

                                    if (string.IsNullOrEmpty(start_time)) //input cannot be null
                                    {
                                        Console.WriteLine("\tPlease enter a valid date and time.");
                                    }
                                    else if (!DateTime.TryParseExact(start_time, "dd'/'MM'/'yyyy HH:mm",
                                                                CultureInfo.InvariantCulture,
                                                                DateTimeStyles.None,
                                                                out start_date)) //the date format must be the same as above
                                    {
                                        Console.WriteLine("\tPlease enter the correct format shown above.");
                                    }
                                    else if (start_date < time.AddHours(1)) //if the start date(live) is less than 1 hour, display error.
                                    {
                                        Console.WriteLine("\tDelivery window start must be at least one hour in the future.");
                                    }
                                    else
                                        break; //break when succeeded.
                                }
                                while (true)
                                {
                                    Console.WriteLine("\nDelivery window end (dd/mm/yyyy hh:mm)");
                                    Console.Write("> "); end_time = Console.ReadLine();

                                    if (string.IsNullOrEmpty(end_time)) //input cannot be empty
                                    {
                                        Console.WriteLine("\tPlease enter a valid date and time.");
                                    }
                                    else if (!DateTime.TryParseExact(end_time, "dd'/'MM'/'yyyy HH:mm",
                                                                CultureInfo.InvariantCulture,
                                                                DateTimeStyles.None,
                                                                out end_date)) //the date format must be the same as above.
                                    {
                                        Console.WriteLine("\tPlease enter the correct format shown above.");
                                    }
                                    else if (end_date < start_date.AddHours(1)) //if the date is less than 1 hour from start date, display this 
                                    {
                                        Console.WriteLine("\tDelivery window end must be at least one hour later than the start.");
                                    }
                                    else
                                        break; //break when everything is finished.
                                }

                                Rewrite("Deliver at " + start_time + " and " + end_time, "delivery.txt", bidlistNo); //rewrite the dash "-" with time.

                                Console.WriteLine("\nThank you for your bid. If successful, the item will be provided via collection between {0} and {1}\n", start_time, end_time);
                                break; //break switch

                            case "2": //if the user inputs "2" then display this instead.
                                Console.WriteLine("\nPlease provide your delivery address.");

                                while (true) //inserting delivery address (again)
                                {
                                    Console.WriteLine("\nUnit number (0 = none):");
                                    Console.Write("> "); string input_unit = Console.ReadLine();

                                    if (!int.TryParse(input_unit, out unit)) //input cannot be empty
                                    {
                                        Console.WriteLine("\tPlease enter a valid number");
                                    }
                                    else if (int.Parse(input_unit) < 0 || int.Parse(input_unit) > 999) //must be between 0 and 999
                                    {
                                        Console.WriteLine("\tThe supplied value is not a valid unit number");
                                    }
                                    else
                                        break;
                                }
                                while (true)
                                {
                                    Console.WriteLine("\nStreet number:");
                                    Console.Write("> "); string input_stNumber = Console.ReadLine();

                                    if (!int.TryParse(input_stNumber, out stNumber)) //input cannot be null
                                    {
                                        Console.WriteLine("\tPlease enter a valid number");
                                    }
                                    else if (stNumber < 0) //cannot be less than 0
                                    {
                                        Console.WriteLine("\tStreet number must be a postive integer");
                                    }
                                    else if (stNumber == 0) //cannot be equal to 0
                                    {
                                        Console.WriteLine("\tStreet number must be greater than 0");
                                    }
                                    else
                                        break;
                                }
                                while (true)
                                {
                                    Console.WriteLine("\nStreet name:");
                                    Console.Write("> "); input_stName = Console.ReadLine();

                                    if (string.IsNullOrEmpty(input_stName)) //input cannot be null
                                    {
                                        Console.WriteLine("\tPlease enter a value");
                                    }
                                    else
                                        break;
                                }
                                while (true)
                                {
                                    Console.WriteLine("\nStreet suffix:");
                                    Console.Write("> "); input_stSuffix = Console.ReadLine();

                                    if (string.IsNullOrEmpty(input_stSuffix)) //input cannot be null
                                    {
                                        Console.WriteLine("\tPlease enter a value");
                                    }
                                    else
                                        break;
                                }
                                while (true)
                                {
                                    Console.WriteLine("\nCity:");
                                    Console.Write("> "); input_city = Console.ReadLine();

                                    if (string.IsNullOrEmpty(input_city)) //input cannot be null
                                    {
                                        Console.WriteLine("\tPlease enter a value");
                                    }
                                    else
                                        break;
                                }
                                while (true)
                                {
                                    Console.WriteLine("\nState (ACT, NSW, NT, QLD, SA, TAS, VIC, WA):");
                                    Console.Write("> "); input_state = Console.ReadLine();
                                    input_state = input_state.ToUpper();

                                    if (string.IsNullOrEmpty(input_state)) //cannot be null
                                    {
                                        Console.WriteLine("\tPlease enter a value");
                                    }
                                    else if (!states.Contains(input_state.ToUpper())) //must be the input in the array
                                    {
                                        Console.WriteLine("\tPlease enter the right state");
                                    }
                                    else
                                        break;
                                }
                                while (true)
                                {
                                    Console.WriteLine("\nPostcode (1000 .. 9999):");
                                    Console.Write("> "); string input_postcode = Console.ReadLine();

                                    if (!int.TryParse(input_postcode, out postcode)) //cannot be null
                                    {
                                        Console.WriteLine("\tPlease enter a valid number");
                                    }
                                    else if (int.Parse(input_postcode) < 1000 || int.Parse(input_postcode) > 9999) //must be between 1000 and 9999
                                    {
                                        Console.WriteLine("\tThe supplied value is not a valid postcode");
                                    }
                                    else
                                        break; //rewrite the values below.
                                }
                                Rewrite("Deliver to " + unit + "/" + stNumber + " " + input_stName + " " + input_stSuffix + " " + input_city + " " + input_state + " " + postcode, "delivery.txt", bidlistNo);

                                Console.WriteLine("\nThank you for your bid. If successful, the item will be provided via delivery to {0}/{1} {2} {3}, {4} {5} {6}\n", unit, stNumber, input_stName, input_stSuffix, input_city, input_state, postcode);
                                break; //switch break
                        } 
                        if (string.IsNullOrEmpty(input_delivery)) //the delivery menu cannot be null or empty.
                        {
                            Console.WriteLine("\tPlease enter either 1 or 2.");
                        }
                        else if (!delivery_menu_num.Contains(input_delivery)) //the delivery menu must have an input that is contained in the array.
                        {
                            Console.WriteLine("\tPlease enter either 1 or 2.");
                        }
                        else
                            break; //break if everything is successsful.
                    } 
                } 

                else if (UserInput2 == "4") //goes here when the user inputs "4"
                {
                    Console.WriteLine("\nList Product Bids for {0}({1})", ChosenUser, ChosenEmail);
                    Console.WriteLine("-----------------------------------------------------------------------\n");
                    //Setting new variable
                    string selected_SellName, selected_SellDesc, selected_SellPrice, selected_SellBidName, selected_SellBidEmail, selected_SellBidAmount, selected_DeliveryBid;

                    int[] BIDDER = bid_emailArray.Select((something, i) => something != "-" ? i : -1).Where(i => i != -1).ToArray(); //selecting the bid_email array which does not contain the "-"
                    int[] VALUE_Bid = product_emailArray.Select((something, i) => something == EMAIL ? i : -1).Where(i => i != -1).ToArray(); //selecting the product email which equals to the chosen email
                    List<int> intersection_Bid = (from num in BIDDER select num).Intersect(VALUE_Bid).ToList(); //intersection between BIDDER and VALUE_Bid

                    List<int> item_BidsList = new List<int>(); //make a new list for bidding item
                    List<string> name_BidsList = new List<string>(); //the following below is also making a new list 
                    List<string> desc_BidsList = new List<string>();
                    List<string> price_BidsList = new List<string>();
                    List<string> bidname_BidsList = new List<string>();
                    List<string> bidemail_BidsList = new List<string>();
                    List<string> bidamt_BidsList = new List<string>();
                    List<string> delivery_BidsList = new List<string>();

                    while (true)
                    {
                        if (BIDDER.Length <= 0) //if there is no bid email in the text file, then display this message and go back to client menu
                        {
                            Console.WriteLine("No bids were found.\n");
                            Client2(ChosenUser, ChosenEmail); //goes back to client menu
                        }
                        else if (BIDDER.Length >= 1) //if there is content inside bid email, then display all this
                        {
                            Console.WriteLine("Item # \t Product name \t Description \t List price \t Bidder name \t Bidder email \t Bid amt");
                            foreach (int i in intersection_Bid) //using the intersection to output a specific list.
                            {
                                item_num++; //counter for items
                                Console.WriteLine(item_num + "\t " + product_name[i] + "\t " + product_description[i] + "\t " + product_price[i] + "\t " + bid_name[i] + "\t " + bid_email[i] + "\t " + bid_amt[i]);

                                item_BidsList.Add(item_num); //add the following values into the list above
                                name_BidsList.Add(product_name[i]);
                                desc_BidsList.Add(product_description[i]);
                                price_BidsList.Add(product_price[i]);
                                bidname_BidsList.Add(bid_name[i]);
                                bidemail_BidsList.Add(bid_email[i]);
                                bidamt_BidsList.Add(bid_amt[i]);
                                delivery_BidsList.Add(delivery[i]);
                            }
                            break; //return and break when the following task is done
                        } return;
                    }
                    while (true)
                    {
                        Console.WriteLine("\nWould you like to sell something (yes or no)?"); //ask if the user wants to sell something or not
                        Console.Write("> "); string input_ask2 = Console.ReadLine();

                        if (string.IsNullOrEmpty(input_ask2)) //input cannot be empty
                        {
                            Console.WriteLine("\tPlease enter yes or no");
                        }
                        else if (input_ask2 == "no") //if input is "no" then go back to client menu
                        {
                            Console.WriteLine();
                            Client2(ChosenUser, ChosenEmail);
                        }
                        else if (input_ask2 == "yes") //if input is "yes" then break.
                        {
                            break;
                        } return;
                    }
                    while (true)
                    {
                        Console.WriteLine("\nPlease enter an integer between {0} and {1}:", item_change, item_num);
                        Console.Write("> "); string input_item = Console.ReadLine(); //ask for user input

                        if (!int.TryParse(input_item, out chosen_item)) //input cannot be null
                        {
                            Console.WriteLine("\tPlease enter a valid number\n");
                        }
                        else if (chosen_item > item_num || chosen_item < item_change) // if input is less than 1st value and more than last value, display error.
                        {
                            Console.WriteLine("\tPlease select an option between {0} and {1}", item_change, item_num);
                        }
                        else if (chosen_item < item_num || chosen_item > item_change || chosen_item == item_num || chosen_item == item_change) //if input is right, proceed
                        {
                            int sellinglistNo = item_BidsList.IndexOf(chosen_item); //find the index of chosen item. 

                            selected_SellName = Convert.ToString(name_BidsList[sellinglistNo]);
                            selected_SellDesc = Convert.ToString(desc_BidsList[sellinglistNo]);
                            selected_SellPrice = Convert.ToString(price_BidsList[sellinglistNo]);
                            selected_SellBidName = Convert.ToString(bidname_BidsList[sellinglistNo]); //get the selected product from the list
                            selected_SellBidEmail = Convert.ToString(bidemail_BidsList[sellinglistNo]);
                            selected_SellBidAmount = Convert.ToString(bidamt_BidsList[sellinglistNo]);
                            selected_DeliveryBid = Convert.ToString(delivery_BidsList[sellinglistNo]);
                            //display message after user have successfully chose the right product
                            Console.WriteLine("\nYou have sold {0} to {1} for {2}.\n", selected_SellName, selected_SellBidName, selected_SellBidAmount);

                            using (StreamWriter sw = new StreamWriter(@"purchased_email.txt", true)) //stream write all the chosen product
                            {
                                sw.WriteLine(selected_SellBidEmail); //insert bidder email into purchased email txt file.
                            }
                            using (StreamWriter sw = new StreamWriter(@"sold_email.txt", true))
                            {
                                sw.WriteLine(ChosenEmail); //insert the curernt chosen email into seller email
                            }
                            using (StreamWriter sw = new StreamWriter(@"sold_name.txt", true))
                            {
                                sw.WriteLine(selected_SellName); //insert selected product name into sold_name txt file
                            }
                            using (StreamWriter sw = new StreamWriter(@"sold_description.txt", true))
                            {
                                sw.WriteLine(selected_SellDesc); //same as above but description
                            }
                            using (StreamWriter sw = new StreamWriter(@"sold_price.txt", true))
                            {
                                sw.WriteLine(selected_SellPrice); //same as above but price
                            }
                            using (StreamWriter sw = new StreamWriter(@"sold_bidPrice.txt", true))
                            {
                                sw.WriteLine(selected_SellBidAmount); //same as above but bid price
                            }
                            using (StreamWriter sw = new StreamWriter(@"sold_delivery.txt", true))
                            {
                                sw.WriteLine(selected_DeliveryBid); //same as above but delivery information
                            }

                            int deletelistNo = product_description.IndexOf(selected_SellDesc); //find the index of description

                            Rewrite("", @"product_email.txt", deletelistNo);
                            Rewrite("", @"product_name.txt", deletelistNo);
                            Rewrite("", @"product_description.txt", deletelistNo);
                            Rewrite("", @"product_price.txt", deletelistNo); //rewrite the following value in the file into null or white space
                            Rewrite("", @"bid_name.txt", deletelistNo);
                            Rewrite("", @"bid_email.txt", deletelistNo);
                            Rewrite("", @"bid_amt.txt", deletelistNo);
                            Rewrite("", @"delivery.txt", deletelistNo);
                            //DELETE all the whitespace using the following code below.
                            var lines_email = File.ReadAllLines(@"product_email.txt").Where(arg => !string.IsNullOrWhiteSpace(arg));
                            File.WriteAllLines(@"product_email.txt", lines_email);
                            var lines_name = File.ReadAllLines(@"product_name.txt").Where(arg => !string.IsNullOrWhiteSpace(arg));
                            File.WriteAllLines(@"product_name.txt", lines_name);
                            var lines_description = File.ReadAllLines(@"product_description.txt").Where(arg => !string.IsNullOrWhiteSpace(arg));
                            File.WriteAllLines(@"product_description.txt", lines_description);
                            var lines_price = File.ReadAllLines(@"product_price.txt").Where(arg => !string.IsNullOrWhiteSpace(arg));
                            File.WriteAllLines(@"product_price.txt", lines_price);
                            var lines_bidName = File.ReadAllLines(@"bid_name.txt").Where(arg => !string.IsNullOrWhiteSpace(arg));
                            File.WriteAllLines(@"bid_name.txt", lines_bidName);
                            var lines_bidEmail = File.ReadAllLines(@"bid_email.txt").Where(arg => !string.IsNullOrWhiteSpace(arg));
                            File.WriteAllLines(@"bid_email.txt", lines_bidEmail);
                            var lines_bidAmt = File.ReadAllLines(@"bid_amt.txt").Where(arg => !string.IsNullOrWhiteSpace(arg));
                            File.WriteAllLines(@"bid_amt.txt", lines_bidAmt);
                            var lines_delivery = File.ReadAllLines(@"delivery.txt").Where(arg => !string.IsNullOrWhiteSpace(arg));
                            File.WriteAllLines(@"delivery.txt", lines_delivery);

                            break; //break when everything is finished
                        }
                        else
                            Console.WriteLine("\tPlease select a number between {0} and {1}", item_change, item_num); //display this when requirements are not met
                    } 
                }

                else if (UserInput2 == "5") //select this when the user inputs "5".
                {
                    Console.WriteLine("\nPurchased Items for {0}({1})", ChosenUser, ChosenEmail);
                    Console.WriteLine("-----------------------------------------------------------------------\n");

                    if (fileContents_purchased.Length <= 0) //if the purchased email txt file do not contain anything display this.
                    {
                        Console.WriteLine("You have no purchased products at the moment.");
                    }
                    else if (!fileContents_purchased.Contains(ChosenEmail)) //if the purchased email txt file do not contain the chosen email, display this
                    {
                        Console.WriteLine("You have no purchased products at the moment.");
                    }
                    else if (fileContents_purchased.Length >= 1) //if the purchased email does contain the file and has more than 1 line of content, display this.
                    {
                        Console.WriteLine("Item # \t Seller Email \t Product name \t Description \t List Price \t Amt Paid \t Delivery option");
                        foreach (int i in VALUE_Purchased) //using the selection of array mentioned above
                        {
                            item_num++; //counter for item.
                            Console.WriteLine(item_num + "\t " + sold_email[i] + "\t " + sold_name[i] + "\t " + sold_description[i] + "\t " + sold_price[i] + "\t " + sold_bidPrice[i] + "\t " + sold_delivery[i]);
                        }
                    }
                    Console.WriteLine();
                }

                else if (UserInput2 == "6") //select this when the user input "6"
                {
                    return; //return to the main menu.
                }

                else if (string.IsNullOrEmpty(UserInput2)) //input cannot be null
                {
                    Console.WriteLine("\nEnter a valid number between 1 and 6.\n");
                }
                else if (!menu_num.Contains(UserInput2)) //input must be in the array
                {
                    Console.WriteLine("\nEnter a valid number between 1 and 6.\n");
                }
                else
                    break; //break when the requirement is met.
            }
        }
    }
} //END OF CODE