using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using BusinessObject.DataAccess;
using Microsoft.AspNetCore.Identity;

namespace BusinessObject
{
    public class SeedUser
    {
        //password hasher
        private static PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
        private static List<User> users = new List<User>();

        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var db = new AMSContext())
            {
                if (!db.Users.Any())
                {
                    SeedStudent();
                    SeedTeacher();
                    db.Users.AddRange(users);
                    db.SaveChanges();
                }
            }
        }

        private static void SeedStudent()
        {
            List<User> students = new List<User>();
            for (int i = 0; i < 30; i++)
            {
                students.Add(new Faker<User>()
                    .RuleFor(s => s.FullName, f => f.Name.FullName())
                    .RuleFor(s => s.UserEmail, (f, s) => f.Internet.Email($"{s.FullName.Replace(" ", "")}@example.com"))
                    .Generate());
            }

            foreach (var user in students)
            {
                user.UserRoleId = 1;
                user.UserPassword = passwordHasher.HashPassword(user, "123456");
            }
            //add to list users
            users.AddRange(students);
        }

        private static void SeedTeacher()
        {
            List<User> teachers = new List<User>();
            for (int i = 0; i < 10; i++)
            {
                teachers.Add(new Faker<User>()
                    .RuleFor(s => s.FullName, f => f.Name.FullName())
                    .RuleFor(s => s.UserEmail, (f, s) => f.Internet.Email($"{s.FullName.Replace(" ", "")}@example.com"))
                    .Generate());
            }

            foreach (var user in teachers)
            {
                user.UserRoleId = 2;
                user.UserPassword = passwordHasher.HashPassword(user, "123456");
            }

            //add to list users
            users.AddRange(teachers);
        }
    }
}
