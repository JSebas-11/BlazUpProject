using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Domain.Models.Audit;
using Domain.Models.Lookups;

namespace Infrastructure.Data;

internal partial class BlazUpProjectContext : DbContext {
    //--------------------------INITIALIZATIION--------------------------
    public BlazUpProjectContext() { }

    public BlazUpProjectContext(DbContextOptions<BlazUpProjectContext> options)
        : base(options)
    { }

    //--------------------------MAPPED TABLES--------------------------
    public virtual DbSet<StateEntity> EntityStates { get; set; }
    public virtual DbSet<Goal> Goals { get; set; }
    public virtual DbSet<LevelPriority> LevelPriorities { get; set; }
    public virtual DbSet<NotificationApp> NotificationApps { get; set; }
    public virtual DbSet<StateNotification> NotificationStates { get; set; }
    public virtual DbSet<Project> Projects { get; set; }
    public virtual DbSet<ProjectMember> ProjectMembers { get; set; }
    public virtual DbSet<Requirement> Requirements { get; set; }
    public virtual DbSet<RequirementType> RequirementTypes { get; set; }
    public virtual DbSet<TaskApp> Tasks { get; set; }
    public virtual DbSet<UserInfo> UserInfos { get; set; }
    public virtual DbSet<UserLog> UserLogs { get; set; }
    public virtual DbSet<UserRole> UserRoles { get; set; }
    public virtual DbSet<UserTask> UserTasks { get; set; }

    //--------------------------MAPPING--------------------------
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<StateEntity>(entity => {
            entity.HasKey(e => e.EntStateId).HasName("PK__EntitySt__56C4F041CD7E954A");

            entity.ToTable("EntityState");

            entity.HasIndex(e => e.EntStateDescription, "UQ__EntitySt__437DD393F0B17163").IsUnique();

            entity.Property(e => e.EntStateId).ValueGeneratedNever();
            entity.Property(e => e.EntStateDescription).HasMaxLength(48);
        });

        modelBuilder.Entity<Goal>(entity => {
            entity.HasKey(e => e.GoalId).HasName("PK__Goal__8A4FFFD1B2C76648");

            entity.ToTable("Goal");

            entity.Property(e => e.GoalDescription).HasMaxLength(512);
            entity.Property(e => e.GoalName).HasMaxLength(64);

            entity.HasOne(d => d.GoalState).WithMany(p => p.Goals)
                .HasForeignKey(d => d.GoalStateId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Goal_EntityState");
            entity.HasOne(d => d.Priority).WithMany(p => p.Goals)
                .HasForeignKey(d => d.PriorityId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Goal_LevelPriority");
            entity.HasOne(d => d.Project).WithMany(p => p.Goals)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Goal_Project");
        });

        modelBuilder.Entity<LevelPriority>(entity => {
            entity.HasKey(e => e.PriorityId).HasName("PK__LevelPri__D0A3D0BEB132CFA6");

            entity.ToTable("LevelPriority");

            entity.HasIndex(e => e.PriorityDescription, "UQ__LevelPri__EDDC7095E43D6D96").IsUnique();

            entity.Property(e => e.PriorityId).ValueGeneratedNever();
            entity.Property(e => e.PriorityDescription).HasMaxLength(48);
        });

        modelBuilder.Entity<NotificationApp>(entity => {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E12D0029B18");

            entity.ToTable("NotificationApp");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NotificationMessage).HasMaxLength(128);
            entity.Property(e => e.NotificationTitle).HasMaxLength(64);

            entity.HasOne(d => d.NotState).WithMany(p => p.NotificationApps)
                .HasForeignKey(d => d.NotStateId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_NotificationApp_NotificationState");
        });

        modelBuilder.Entity<StateNotification>(entity => {
            entity.HasKey(e => e.NotStateId).HasName("PK__Notifica__AC8FE614E33E2D84");

            entity.ToTable("NotificationState");

            entity.HasIndex(e => e.NotStateDescription, "UQ__Notifica__F2A77255EC65014A").IsUnique();

            entity.Property(e => e.NotStateId).ValueGeneratedNever();
            entity.Property(e => e.NotStateDescription).HasMaxLength(48);
        });

        modelBuilder.Entity<Project>(entity => {
            entity.HasKey(e => e.ProjectId).HasName("PK__Project__761ABEF0370BACE8");

            entity.ToTable("Project");

            entity.Property(e => e.DeadLine).HasColumnType("datetime");
            entity.Property(e => e.InitialDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Progress).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.ProjectDescription).HasMaxLength(512);
            entity.Property(e => e.ProjectName).HasMaxLength(64);

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.Projects)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Project_UserInfo");
            entity.HasOne(d => d.ProjectState).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ProjectStateId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Project_EntityState");
        });

        modelBuilder.Entity<ProjectMember>(entity => {
            entity.HasKey(e => new { e.UserId, e.ProjectId });

            entity.ToTable("ProjectMember");

            entity.Property(e => e.JoinedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectMembers)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProjectMember_Project");
            entity.HasOne(d => d.User).WithMany(p => p.ProjectMembers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_ProjectMember_UserInfo");
        });

        modelBuilder.Entity<Requirement>(entity => {
            entity.HasKey(e => e.ReqId).HasName("PK__Requirem__28A9A382280AB6C0");

            entity.ToTable("Requirement");

            entity.Property(e => e.DeadLine).HasColumnType("datetime");
            entity.Property(e => e.Progress).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.ReqDescription).HasMaxLength(512);
            entity.Property(e => e.ReqName).HasMaxLength(64);

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.Requirements)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Requirement_UserInfo");
            entity.HasOne(d => d.Priority).WithMany(p => p.Requirements)
                .HasForeignKey(d => d.PriorityId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Requirement_LevelPriority");
            entity.HasOne(d => d.Project).WithMany(p => p.Requirements)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Requirement_Project");
            entity.HasOne(d => d.ReqState).WithMany(p => p.Requirements)
                .HasForeignKey(d => d.ReqStateId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Requirement_EntityState");
            entity.HasOne(d => d.ReqType).WithMany(p => p.Requirements)
                .HasForeignKey(d => d.ReqTypeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Requirement_RequirementType");
        });

        modelBuilder.Entity<RequirementType>(entity => {
            entity.HasKey(e => e.ReqTypeId).HasName("PK__Requirem__AC88A41E6D87AC5E");

            entity.ToTable("RequirementType");

            entity.HasIndex(e => e.ReqTypeDescription, "UQ__Requirem__5C452D52976CE299").IsUnique();

            entity.Property(e => e.ReqTypeId).ValueGeneratedNever();
            entity.Property(e => e.ReqTypeDescription).HasMaxLength(48);
        });

        modelBuilder.Entity<TaskApp>(entity => {
            entity.HasKey(e => e.TaskId).HasName("PK__Task__7C6949B16996824A");

            entity.ToTable("Task");

            entity.Property(e => e.DeadLine).HasColumnType("datetime");
            entity.Property(e => e.InitialDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Progress).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.TaskDescription).HasMaxLength(512);
            entity.Property(e => e.TaskName).HasMaxLength(64);

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Task_UserInfo");
            entity.HasOne(d => d.Priority).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.PriorityId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Task_LevelPriority");
            entity.HasOne(d => d.Requirement).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.RequirementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Task_Requirement");
            entity.HasOne(d => d.TaskState).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.TaskStateId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Task_EntityState");
        });

        modelBuilder.Entity<UserInfo>(entity => {
            entity.HasKey(e => e.UserInfoId).HasName("PK__UserInfo__D07EF2E4E3AEC1C3");

            entity.ToTable("UserInfo", tb =>
                {
                    tb.HasTrigger("UserInfo_AD");
                    tb.HasTrigger("UserInfo_AI");
                });

            entity.HasIndex(e => e.Dni, "UQ__UserInfo__C0308575DAAE8B3B").IsUnique();

            entity.Property(e => e.Dni)
                .HasMaxLength(13)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash).HasMaxLength(60);
            entity.Property(e => e.UserName).HasMaxLength(64);

            entity.HasOne(d => d.Role).WithMany(p => p.UserInfos)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_UserInfo_UserRole");
            entity.HasMany(d => d.Notifications).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserNotification",
                    r => r.HasOne<NotificationApp>().WithMany()
                        .HasForeignKey("NotificationId")
                        .HasConstraintName("FK_UserNotification_NotificationApp"),
                    l => l.HasOne<UserInfo>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserNotification_UserInfo"),
                    j =>
                    {
                        j.HasKey("UserId", "NotificationId");
                        j.ToTable("UserNotification");
                    });
        });

        modelBuilder.Entity<UserLog>(entity => {
            entity.HasKey(e => e.UserLogId).HasName("PK__UserLog__7F8B81318D50F199");

            entity.ToTable("UserLog");

            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.RegisteredAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<UserRole>(entity => {
            entity.HasKey(e => e.UserRoleId).HasName("PK__UserRole__3D978A35475899C8");

            entity.ToTable("UserRole");

            entity.HasIndex(e => e.UserRoleDescription, "UQ__UserRole__ADB0590D3BA2E078").IsUnique();

            entity.Property(e => e.UserRoleId).ValueGeneratedNever();
            entity.Property(e => e.UserRoleDescription).HasMaxLength(48);
        });

        modelBuilder.Entity<UserTask>(entity => {
            entity.HasKey(e => new { e.UserId, e.TaskId });

            entity.ToTable("UserTask");

            entity.Property(e => e.AssignedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Task).WithMany(p => p.UserTasks)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserTask_Task");
            entity.HasOne(d => d.User).WithMany(p => p.UserTasks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserTask_UserInfo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
