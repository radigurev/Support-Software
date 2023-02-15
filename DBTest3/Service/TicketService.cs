using AutoMapper;
using DBTest3.Config;
using DBTest3.Data;
using DBTest3.Data.Entity;
using DBTest3.Data.ViewModels;

namespace DBTest3.Service
{
    public class TicketService : ITicketService
    {

        private readonly ApplicationDbContext applicationDbContext;

        public TicketService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public TicketsVM getTicketById(int id)
        {
            return this.applicationDbContext.tickets.Where(x => x.Id == id).First().To<TicketsVM>();
        }

        #region Ticket
        public List<TicketsVM> getTicketsByStatusAdmin(TicketStatusVM status, UserVM user = null)
        {
            List<TicketsVM> ticketList;

            //Взимане на лист с даден статус и ако имаме подаден user(Админ) взимаме конкретните билети свъзрани с него
            if (status != null)
            {
                if (user == null)
                    ticketList = applicationDbContext.tickets.Where(x => x.StatusId == status.Id).To<TicketsVM>().ToList();
                else
                    ticketList = applicationDbContext.tickets.Where(x => x.StatusId == status.Id && x.WorkerId == user.Id).To<TicketsVM>().ToList();
            }
            else
            {
                ticketList = applicationDbContext.tickets.Where(x => x.StatusId == null).To<TicketsVM>().ToList();
            }
            return ticketList;
        }

        public List<TicketsVM> getTicketsByStatusUser(TicketStatusVM status, UserVM user = null)
        {
            List<TicketsVM> ticketList;

            //Взимане на лист с даден статус и ако имаме подаден user(Админ) взимаме конкретните билети свъзрани с него
            if (user == null)
                ticketList = applicationDbContext.tickets.Where(x => x.StatusId == status.Id).To<TicketsVM>().ToList();
            else
                ticketList = applicationDbContext.tickets.Where(x => x.StatusId == status.Id && x.ClientId == user.Id).To<TicketsVM>().ToList();

            return ticketList;
        }
        #endregion

        #region TicketStatus
        public TicketStatusVM getTicketStatus(string statusCode)
        {
            //Взимане на статус от базата данни по дадения код

            var ticket = applicationDbContext.ticketStatuses.Where(x => x.Code == statusCode).First();

            return ticket.To<TicketStatusVM>();
        }

        public async Task initTicketStatuses()
        {
            if (!applicationDbContext.ticketStatuses.Any())
            {

                var statuses = new List<TicketStatus>()
                {
                    new TicketStatus
                    {
                        Name = "ToDo",
                        NameBG = "Предстоящ",
                        Code = "ToDoStatus"
                    },
                    new TicketStatus
                    {
                        Name = "Progress",
                        NameBG = "В прогрес",
                        Code = "InProgressStatus"
                    },
                    new TicketStatus
                    {
                        NameBG = "В изчакване",
                        Name = "Waiting",
                        Code = "WaitingAnswerStatus"
                    },
                    new TicketStatus
                    {
                        Name = "Closed",
                        NameBG = "Затоверна",
                        Code = "ClosedStatus"
                    }
                };

                await applicationDbContext.AddRangeAsync(statuses);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        public TicketsVM saveTicket(TicketsVM ticketVM)
        {
            var ticket = ticketVM.To<Tickets>();

            this.applicationDbContext.Add(ticket);
            this.applicationDbContext.SaveChanges();

            return ticket.To<TicketsVM>();
        }
        #endregion
    }
}
