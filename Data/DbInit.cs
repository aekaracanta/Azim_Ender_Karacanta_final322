using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Data
{
	public class DbInit
	{
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Songs.Any())
            {
                return;
            }

            var customers = new Customer[]
            {
            new Customer{CustomerName="Azim Ender",CustomerEmail="azim@ender.com", CustomerPassword="1234", CustomerSurname="Karaçanta", CustomerTelephone="05554442233" },
            new Customer{CustomerName="Azim Ender",CustomerEmail="admin@ender.com", CustomerPassword="1234", CustomerSurname="Karaçanta", CustomerTelephone="05554442233" },
            };
            foreach (Customer s in customers)
            {
                context.Customers.Add(s);
            }
            context.SaveChanges();




            var categories = new Category[]
            {
            new Category{CategoryName="Alaturka" },
            new Category{CategoryName="Türkü" },
            new Category{CategoryName="Pop" },
            new Category{CategoryName="Rock" },
            new Category{CategoryName="TSM" },
            new Category{CategoryName="Rap" },
            new Category{CategoryName="Opera" },
            };
            foreach (Category s in categories)
            {
                context.Categories.Add(s);
            }
            context.SaveChanges();



            var composers = new Composer[]
            {
            new Composer{ComposerName="Neşet", ComposerSurname ="Ertaş", ComposerEmail="neset@ertasplak.com",ComposerGrade=0},
            };
            foreach (Composer s in composers)
            {
                context.Composers.Add(s);
            }
            context.SaveChanges();




            var songs = new Song[]
            {
            new Song{SongName="Mihriban",ComposerId=1,CategoryId=2,SongLyrics=@"Sarı saçlarını deli gönlüme
Bağlamışım çözülmüyor
Mihriban",SongLength=100,SongYear=1980,SongPrice=5000},
            new Song{SongName="Gönül Dağı",ComposerId=1,CategoryId=2,SongLyrics=@"Gönül dağı yağmur yağmur boran olunca
Akar can özümde sel gizli gizli
Bir tenhada can cananı bulunca
Sinemi yaralar
Yar oy yar oy yar oy yar oy yar oy yar oy",SongLength=100,SongYear=1980,SongPrice=5500},
                new Song{SongName="Mihriban",ComposerId=1,CategoryId=2,SongLyrics=@"Sarı saçlarını deli gönlüme
Bağlamışım çözülmüyor
Mihriban",SongLength=100,SongYear=1980,SongPrice=5000},
            new Song{SongName="Gönül Dağı",ComposerId=1,CategoryId=2,SongLyrics=@"Gönül dağı yağmur yağmur boran olunca
Akar can özümde sel gizli gizli
Bir tenhada can cananı bulunca
Sinemi yaralar
Yar oy yar oy yar oy yar oy yar oy yar oy",SongLength=100,SongYear=1980,SongPrice=5500},
            };
            foreach (Song s in songs)
            {
                context.Songs.Add(s);
            }
            context.SaveChanges();




            var payments = new Payment[]
            {
            new Payment{SongId=1, CustomerId=1, PaymentCompleted=true,},
            };
            foreach (Payment s in payments)
            {
                context.Payments.Add(s);
            }
            context.SaveChanges(); 


            
            

        }
    }
}
