using Microsoft.Data.SqlClient;

namespace RestApi.Infrastructure.EF;

internal interface IRepository<T>
{
    Task<List<T>> Get(CancellationToken cancellationToken);
    Task<T> GetById(PK id, CancellationToken cancellationToken);
    Task Add(T entity);
    Task Update(PK id, T entity);
    Task Delete(PK id);
    Task SaveChanges();
}

internal class GenericRepository<T> : IRepository<T> where T : class
{
    protected CategoriesDbContext Context { get; }
    protected DbSet<T> Set { get; }
    public IMapper Mapper { get; }

    public GenericRepository(CategoriesDbContext context, IMapper mapper)
    {
        Context = context;
        Set = context.Set<T>();
        Mapper = mapper;
    }

    public virtual Task<List<T>> Get(CancellationToken cancellationToken)
    {
        return Set.ToListAsync(cancellationToken);
    }

    public virtual async Task<T> GetById(PK pk, CancellationToken cancellationToken = default)
    {
        var entity = await Set.FindAsync(new object[] { pk.Id }, cancellationToken);
        if (entity is null)
        {
            throw new EntityNotFoundException(typeof(T), pk.Id);
        }

        return entity;
    }

    public async Task Add(T entity)
    {
        await Set.AddAsync(entity);
    }

    public async Task Update(PK pk, T entity)
    {
        var dbEntity = await GetById(pk);
        Mapper.Map(entity, dbEntity);
    }

    public async Task Delete(PK pk)
    {
        var entity = await GetById(pk);
        Context.Remove(entity);
    }

    public async Task SaveChanges()
    {
        try
        {
            await Context.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            if (IsUniqueConstraintViolated(e))
            {
                throw new DuplicatedEntityException();
            }
        }
    }

    private bool IsUniqueConstraintViolated(DbUpdateException e)
    {
        return e.InnerException is not null
            && e.InnerException is SqlException sqlException
            && sqlException.Number == 2601;
    }
}
