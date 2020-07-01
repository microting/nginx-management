using Microsoft.EntityFrameworkCore;
using NginxManagement.Infrastructure.Constants;
using NginxManagement.Infrastructure.Data.Entities;
using System;

namespace NginxManagement.Infrastructure.Data
{
    public class NginxManagemenDbContext : DbContext
    {
        public NginxManagemenDbContext(DbContextOptions<NginxManagemenDbContext> options) : base(options)
        {
           
        }

        public DbSet<PluginConfigurationValues> PluginConfigurationValues { get; set; }

        public DbSet<Host> Hosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PluginConfigurationValues>().HasData(
                 new PluginConfigurationValues() {Id  = 1, Name = "BaseSettings:ServiceAccessToken", Value = "", CreatedAt = DateTime.Now, CreatedByUserId = -1, Version = 1, WorkflowState  = WorkflowStates.Created },
                 new PluginConfigurationValues() {Id  = 2, Name = "BaseSettings:Host", Value = "microting.com", CreatedAt = DateTime.Now, CreatedByUserId = -1, Version = 1, WorkflowState = WorkflowStates.Created },
                 new PluginConfigurationValues() {Id  = 3, Name = "BaseSettings:AccessLogTemplate", Value = "/var/log/nginx/nginx.{0}.access.log", CreatedAt = DateTime.Now, CreatedByUserId = -1, Version = 1, WorkflowState = WorkflowStates.Created },
                 new PluginConfigurationValues() {Id  = 4, Name = "BaseSettings:ErrorLogTemplate", Value = "/var/log/nginx/nginx.{0}.error.log", CreatedAt = DateTime.Now, CreatedByUserId = -1, Version = 1, WorkflowState = WorkflowStates.Created },
                 new PluginConfigurationValues() {Id  = 5, Name = "BaseSettings:PostExecuteCommand", Value = "/etc/init.d/nginx reload", CreatedAt = DateTime.Now, CreatedByUserId = -1, Version = 1, WorkflowState = WorkflowStates.Created },
                 new PluginConfigurationValues() {Id  = 6, Name = "BaseSettings:DestinationPath", Value = "/etc/nginx/sites-enabled/", CreatedAt = DateTime.Now, CreatedByUserId = -1, Version = 1, WorkflowState = WorkflowStates.Created }
                );
        }
    }
}
