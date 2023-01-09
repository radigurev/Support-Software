﻿using AutoMapper;
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

        #region Ticket
        public List<TicketsVM> getToDoTickets(TicketStatusVM status)
        {
            //Взимане на лист с даден статус
            var ticketList = applicationDbContext.tickets.Where(x => x.StatusId == status.Id).ToList();

            return ticketList.Select(x => new TicketsVM
            {
                Client = x.Client,
                ClientId = x.ClientId,
                Id = x.Id,
                ClosedDate = x.ClosedDate,
                CreatedDate = x.CreatedDate,
                Problem = x.Problem,
                Status = x.Status,
                StatusId = x.StatusId,
                Title = x.Title,
                Worker = x.Worker,
                WorkerId = x.WorkerId
            }).ToList();
        }
        #endregion

        #region TicketStatus
        public TicketStatusVM getTicketStatus(string statusCode)
        {
            //Взимане на статус от базата данни по дадения код

            var ticket = applicationDbContext.ticketStatuses.Where(x => x.Code == statusCode).First();

            return new TicketStatusVM()
            {
             Code = ticket.Code,
             Name = ticket.Name,
             NameBG = ticket.NameBG,
             Id = ticket.Id
            };
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
        #endregion
    }
}
