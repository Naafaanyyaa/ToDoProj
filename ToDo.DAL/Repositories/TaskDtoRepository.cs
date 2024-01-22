
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ToDo.DAL.Entities;
using ToDo.DAL.Interfaces;

namespace ToDo.DAL.Repositories
{
    public class TaskDtoRepository : Repository<TaskDto>, ITaskDtoRepository
    {
        private readonly ILogger<TaskDtoRepository> _logger;
        public TaskDtoRepository(ApplicationDbContext db, ILogger<TaskDtoRepository> logger) : base(db)
        {
            _logger = logger;
        }

        public override async Task<TaskDto> AddAsync(TaskDto entity)
        {
            await using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                await _db.AddAsync(entity);
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();

                return entity;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                _logger.LogError("Error while creating task: {EMessage}", e.Message);
            }

            return null;
        }

        public override Task<TaskDto?> GetByIdAsync(string id)
        {
            return _db.Tasks.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public override async Task UpdateAsync(TaskDto entity)
        {

            await using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                _db.Tasks.Update(entity);
                _db.Entry(entity).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
                _db.Entry(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                _logger.LogError("Error while updating task: {EMessage}", e.Message);
            }
        }
    }
}
