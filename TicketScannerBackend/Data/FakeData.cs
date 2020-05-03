using System;
using System.Collections.Generic;
using TicketScannerBackend.Models;

namespace TicketScannerBackend.Data
{
    public class FakeData
    {
        private BazaContext db;

        string[] fakeBarcodes = {"17531864","30948310","20212394","61595994","15630791","17566011","16380573","46626455","57570713"};
        public FakeData(BazaContext db){
            this.db=db;
        }
        public void populateDb()
        {
            Events events = new Events()
            {
                Name = "Black Coffee Tirane",
                EventDate = DateTime.ParseExact("2019-04-08 23:30:00", "yyyy-MM-dd HH:mm:ss",
                                      System.Globalization.CultureInfo.InvariantCulture),
                Tickets = new List<Tickets>()
            };

            // Random rnd = new Random();
            
            // events.Tickets.Add(new Tickets(){
            //     Barcode = "3850322007099",
            //     IsActivated = false
            // });
            // int x = 1;
            // while(x<50){
            //     long barcode = rnd.Next(10000000,99999999);
            //     foreach(Tickets tickets in events.Tickets){
            //         if(tickets.Barcode.Equals(barcode+"")){
            //             continue;
            //         }
            //     }
            //     var ticket = new Tickets(){
            //         Barcode = barcode.ToString(),
            //         IsActivated = false
            //     };

            //     events.Tickets.Add(ticket);
            //     x++;
            // }
            Console.WriteLine(fakeBarcodes.Length+"");
            for(int x=0;x<fakeBarcodes.Length;x++){
                var ticket = new Tickets(){
                    Barcode = fakeBarcodes[x],
                    IsActivated = false
                };

                events.Tickets.Add(ticket);
                x++;
            }

            db.Events.Add(events);
            db.SaveChanges();
        }
    }
}