using AdvertisementDB.Model;
using System;
using Microsoft.EntityFrameworkCore;

namespace AdvertisementApp
{
    internal class Program
    {
        static int _userId;
        static string _userName;
        static void InitializeDatabase()
        {
            using var dbContext = new TodoDbContext();
            dbContext.Database.EnsureCreated();
        }

        static void ShowMenu()
        {
            Console.WriteLine("1 -> Add a new add");
            Console.WriteLine("2 -> Modify add");
            Console.WriteLine("3 -> Delete add");
            Console.WriteLine("4 -> Show all adds");
            Console.WriteLine("5-> Add Review");
            Console.WriteLine("0 -> EXIT");
        }
        static void Login()
        {
            Console.WriteLine("Enter username : ");
            string username=Console.ReadLine(); 

            var dbContext=new TodoDbContext();

            var user=dbContext.Users.SingleOrDefault(u=>u.Name==username);

            if(user==null)
            {
                user = new User { Name = username };
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
            }
            _userName = user.Name;
            _userId=user.Id;

            Console.WriteLine("Hello " + _userName);

        }

        static void NewAdd()
        {
            var dbContext = new TodoDbContext();
            var user = dbContext.Users.SingleOrDefault(u=>u.Name==_userName);
            Console.WriteLine("Title...");
            string title = Console.ReadLine();
            Console.WriteLine("Price...");
            float Price=float.Parse(Console.ReadLine()); 
            Console.WriteLine("Description...");
            string description=Console.ReadLine();

            Advertisement add = new Advertisement() { Title = title, price = Price, Description = description, UserId = _userId };

            dbContext.Advertisements.Add(add);
            
            dbContext.SaveChanges();
        }

        static void NewReview()
        {
            var dbContext = new TodoDbContext();

            Console.WriteLine("Add ID:");
            int id = int.Parse(Console.ReadLine());

            Advertisement add = dbContext.Advertisements.SingleOrDefault(u=>u.Id==id);

            Console.WriteLine("Give stars nr :");
            float starsnr = float.Parse(Console.ReadLine());

            Console.WriteLine("Title:");
            string title=Console.ReadLine();

            Console.WriteLine("Description: ");
            string description = Console.ReadLine();

            Review review = new Review { AdvertisementId = id, Description = description, Stars = starsnr, Title = title, DateTime=DateTime.Now };

            dbContext.Reviews.Add(review);

            dbContext.SaveChanges();
        }

        static void ModifyAdd()
        {
            var dbContext = new TodoDbContext();
            User user = dbContext.Users.Include(u=>u.Advertisements).SingleOrDefault(u=>u.Id==_userId);

            Console.WriteLine("Add Id: ");
            int id =int.Parse(Console.ReadLine());

            var add=user.Advertisements.SingleOrDefault(ad=>ad.Id==id);

            if(add!=null)
            {
                Console.WriteLine("New Title: ");
                string title=Console.ReadLine();
                if(title!="")
                    add.Title=title;

                Console.WriteLine("New Price: ");
                float price=float.Parse(Console.ReadLine());
                if (price != null)
                    add.price = price;

                Console.WriteLine("New Description:");
                string description = Console.ReadLine();
                if (description != "")
                    add.Description = description;
                
            }
            dbContext.SaveChanges();
           
        }

        static void DeleteAdd()
        {
            Console.WriteLine("Add Id:");
            int id = int.Parse(Console.ReadLine());

            var dbContext = new TodoDbContext();

            User user = dbContext.Users.Include(u => u.Advertisements).SingleOrDefault(u => u.Id == _userId);

            Advertisement advert = dbContext.Advertisements.SingleOrDefault(ad => ad.Id == id);

            if (advert != null)
            {
                dbContext.Advertisements.Remove(advert);
                dbContext.SaveChanges();
            }
            
        }

        static void ShowAdds()
        {
            var dbContext= new TodoDbContext();
            User user = dbContext.Users.Include(u=>u.Advertisements).ThenInclude(u=>u.Reviews).SingleOrDefault(u=>u.Id==_userId);
            Console.WriteLine("Your Adds");

            foreach (var add in user.Advertisements)
            {
                Console.WriteLine("Advertisement ID : " + add.Id.ToString());
                Console.WriteLine(add.Title.ToString()+" Price: "+add.price.ToString());
                Console.WriteLine(add.Description.ToString());
                Console.WriteLine();
                Console.WriteLine("Reviews :");

                foreach(var review in add.Reviews)
                {
                    Console.WriteLine("Date: "+review.DateTime);
                    Console.WriteLine("Stars: "+review.Stars.ToString());
                    Console.WriteLine(review.Title);
                    Console.WriteLine(review.Description);
                    Console.WriteLine();
                }
            }
        }


        static void CRUD(int command)
        {
                switch(command)
                {
                    case 1:
                        NewAdd();
                        break;
                    case 2:
                        ModifyAdd();
                        break;
                    case 3:
                        DeleteAdd();
                        break;
                    case 4:
                        ShowAdds();
                        break;
                    case 5:
                        NewReview();
                        break;
                }
        }

        static void Main(string[] args)
        {
            InitializeDatabase();
            
            Login();
            ShowMenu();
            int command;
            do
            {
                Console.WriteLine("Choose a command ");
                command = int.Parse(Console.ReadLine());
                CRUD(command);
            } while (command != 0);
        }
    }
}