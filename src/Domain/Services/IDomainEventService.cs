using Domain.Common;

namespace Domain.Services
{
    public interface IDomainEventService
    {
        Task PublishAsync(IDomainEvent domainEvent);
        Task PublishManyAsync(IEnumerable<IDomainEvent> domainEvents);
    }
}
