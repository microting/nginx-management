using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace NginxManagement.Infrastructure.Data.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLength(255)]
        public string WorkflowState { get; set; }
        public int CreatedByUserId { get; set; }
        public int UpdatedByUserId { get; set; }
        public int Version { get; set; }


        public async Task Create(NginxManagemenDbContext dbContext)
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            Version = 1;
            WorkflowState = Constants.WorkflowStates.Created;

            await dbContext.AddAsync(this);
            await dbContext.SaveChangesAsync();
        }

        public async Task Update<T>(NginxManagemenDbContext dbContext) where T : BaseEntity
        {
            await UpdateInternal<T>(dbContext);
        }

        public async Task Delete<T>(NginxManagemenDbContext dbContext) where T : BaseEntity
        {
            await UpdateInternal<T>(dbContext, Constants.WorkflowStates.Removed);
        }

        private async Task UpdateInternal<T>(NginxManagemenDbContext dbContext, string state = null) where T : BaseEntity
        {
            var record = await dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == Id);
            if (record == null)
                throw new NullReferenceException($"Could not find {this.GetType().Name} with ID: {Id}");

            var initialState = record.WorkflowState;
            if (state != null)
                record.WorkflowState = state;

            if (dbContext.ChangeTracker.HasChanges())
            {
                record.WorkflowState = initialState;

                Id = 0;
                UpdatedAt = DateTime.UtcNow;
                UpdatedByUserId = UpdatedByUserId;
                Version = record.Version + 1;
                CreatedAt = record.CreatedAt;
                CreatedByUserId = record.CreatedByUserId;

                if (state != null)
                    WorkflowState = state;

                await dbContext.AddAsync(this);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
