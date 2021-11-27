using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotificationService.Entities;

namespace NotificationService.Application.Interfaces
{
    public interface INotificationDbContext
    {
        DbSet<EmailMessage> EmailMessages { get; set; }
        DbSet<SmsMessage> SmsMessages { get; set; }
        DbSet<TelegramMessage> TelegramMessages { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}