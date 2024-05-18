using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTests.Application
{
    public abstract class ApplicationTestBase : IntegrationTestBase
    {

        protected Task Handle<TRequest>(TRequest request)
            where TRequest : IRequest<Unit> => Handle<TRequest, Unit>(request);      

        protected async Task<TResponse> Handle<TRequest, TResponse>(TRequest request)
            where TRequest: IRequest<TResponse>
        {
            return await Mediator!.Send(request, CancellationToken.None);
        }

        protected TEntity InsertOne<TEntity>(TEntity entity)
            where TEntity : class
        {
            DataBaseContext!.Set<TEntity>().Add(entity);
            DataBaseContext.SaveChanges();
            return entity;
        }
        
        protected void InsertMany<TEntity>(IEnumerable<TEntity> entity)
            where TEntity : class
        {
            DataBaseContext!.Set<TEntity>().AddRange(entity);
            DataBaseContext.SaveChanges();
        }

        protected IList<TEntity> GetEntities<TEntity>() 
            where TEntity : class 
        {
            return DataBaseContext.Set<TEntity>().AsNoTracking().ToList();
        
        }
    }
}
