using Microsoft.EntityFrameworkCore;
using TaskManagement.Api.Domain.Entities;

namespace TaskManagement.Api.Infrastructure.Persistence;

public class TaskDbContext(DbContextOptions<TaskDbContext> options) : DbContext(options)
{
    public DbSet<TaskItem> Tasks => Set<TaskItem>();
}
