using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Api.Entities;
using System;
using System.IO;
using System.Linq;

namespace Sat.Recruitment.Api.Data
{
    public class Seed
    {
        public async static void SeedUSersAsync(DataContext context)
        {
            var reader = ReadUsersFromFile();

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                var user = new User
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = line.Split(',')[4].ToString(),
                    Money = decimal.Parse(line.Split(',')[5].ToString()),
                };

                //Normalize email
                var aux = user.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

                var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

                aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

                user.Email = string.Join("@", new string[] { aux[0], aux[1] });

                context.Users.Add(user);    
            }
            reader.Close();
            context.SaveChanges();
        }
        private static StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
    }
}
