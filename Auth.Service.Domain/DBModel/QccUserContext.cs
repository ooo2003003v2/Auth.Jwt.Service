using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Auth.Service.Domain.DBModel
{
    public partial class QccUserContext : DbContext
    {
        public QccUserContext()
        {
        }

        public QccUserContext(DbContextOptions<QccUserContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<AccountAdditionalType> AccountAdditionalType { get; set; }
        public virtual DbSet<AccountEditLog> AccountEditLog { get; set; }
        public virtual DbSet<AccountGroup> AccountGroup { get; set; }
        public virtual DbSet<AccountGroupAttr> AccountGroupAttr { get; set; }
        public virtual DbSet<AccountImportLog> AccountImportLog { get; set; }
        public virtual DbSet<AccountLoginLog> AccountLoginLog { get; set; }
        public virtual DbSet<AccountType> AccountType { get; set; }
        public virtual DbSet<AccountTypeAttr> AccountTypeAttr { get; set; }
        public virtual DbSet<ClientMaster> ClientMaster { get; set; }
        public virtual DbSet<Label> Label { get; set; }
        public virtual DbSet<LabelAttr> LabelAttr { get; set; }
        public virtual DbSet<Lang> Lang { get; set; }
        public virtual DbSet<RefreshToken> RefreshToken { get; set; }
        public virtual DbSet<SystemParam> SystemParam { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=NS-W7-DEV\\SQLEXPRESS; Database=QccUser;User ID=admin;Password=star01moon;Trusted_Connection=False; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasIndex(e => new { e.Id, e.ChangePassDeadline, e.CreatedDate, e.LastLoginDate, e.AccountTypeId, e.Email, e.IsActive, e.IsRequiredChangePass, e.Name })
                    .HasName("IX_Account");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ChangePassDeadline).HasDefaultValueSql("('1900-01-01')");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("([dbo].[GetLocalDate](DEFAULT))");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(60)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.OfficeIosCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("([dbo].[GetLocalDate](DEFAULT))");

                entity.HasOne(d => d.AccountType)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.AccountTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Account_Type");
            });

            modelBuilder.Entity<AccountAdditionalType>(entity =>
            {
                entity.HasIndex(e => new { e.Id, e.AccountAdditionalTypeId, e.AccountId })
                    .HasName("IX_Account_Additional_Type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountAdditionalTypeId).HasDefaultValueSql("((-1))");

                entity.Property(e => e.AccountId).HasDefaultValueSql("((-1))");

                entity.HasOne(d => d.AccountAdditionalTypeNavigation)
                    .WithMany(p => p.AccountAdditionalType)
                    .HasForeignKey(d => d.AccountAdditionalTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Additional_Type_Account_Type");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountAdditionalType)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Additional_Type_Account");
            });

            modelBuilder.Entity<AccountEditLog>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountId).HasDefaultValueSql("((-1))");

                entity.Property(e => e.ActionCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((-1))");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("([dbo].[GetLocalDate](DEFAULT))");

                entity.Property(e => e.Json)
                    .IsRequired()
                    .HasColumnName("json")
                    .HasColumnType("ntext")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Remark)
                    .IsRequired()
                    .HasColumnName("remark")
                    .HasMaxLength(500)
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountEditLog)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Edit_Log_Account");
            });

            modelBuilder.Entity<AccountGroup>(entity =>
            {
                entity.HasIndex(e => new { e.Id, e.GroupCode, e.IsActive })
                    .HasName("IX_Account_Group");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GroupCode)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<AccountGroupAttr>(entity =>
            {
                entity.HasIndex(e => new { e.Id, e.GroupId, e.GroupName, e.LangCode })
                    .HasName("IX_Account_Group_Attr");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GroupId).HasDefaultValueSql("((-1))");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LangCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.AccountGroupAttr)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Group_Attr_Account_Group");
            });

            modelBuilder.Entity<AccountImportLog>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("([dbo].[GetLocalDate](DEFAULT))");

                entity.Property(e => e.FilePath)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LogMsg)
                    .IsRequired()
                    .HasColumnName("LogMsg ")
                    .HasMaxLength(500)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<AccountLoginLog>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccessIp)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.AccountId).HasDefaultValueSql("((-1))");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("([dbo].[GetLocalDate](DEFAULT))");

                entity.Property(e => e.UserAgent)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountLoginLog)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Login_Log_Account");
            });

            modelBuilder.Entity<AccountType>(entity =>
            {
                entity.HasIndex(e => new { e.Id, e.GroupId, e.Level, e.AccountTypeCode, e.IsActive })
                    .HasName("IX_Account_Type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountTypeCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.GroupId).HasDefaultValueSql("((-1))");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Level).HasColumnName("level");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.AccountType)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Type_Account_Group");
            });

            modelBuilder.Entity<AccountTypeAttr>(entity =>
            {
                entity.HasIndex(e => new { e.Id, e.AccountTypeId, e.LangCode, e.Name })
                    .HasName("IX_Account_Type_Attr");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountTypeId).HasDefaultValueSql("((-1))");

                entity.Property(e => e.LangCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('en')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.AccountType)
                    .WithMany(p => p.AccountTypeAttr)
                    .HasForeignKey(d => d.AccountTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Type_Attr_Account_Type");
            });

            modelBuilder.Entity<ClientMaster>(entity =>
            {
                entity.HasKey(e => e.ClientKeyId)
                    .HasName("PK__ClientMa__7296A60798A71EFC");

                entity.Property(e => e.AllowedOrigin)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ClientId)
                    .IsRequired()
                    .HasColumnName("ClientID")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ClientKey)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ClientName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ClientSecret)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Label>(entity =>
            {
                entity.HasIndex(e => new { e.Id, e.IsPrestore, e.LabelKey })
                    .HasName("IX_Label");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LabelKey)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<LabelAttr>(entity =>
            {
                entity.ToTable("Label_Attr");

                entity.HasIndex(e => new { e.Id, e.LabelId, e.LangCode })
                    .HasName("IX_Label_Attr");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LabelDesc)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LangCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('en')");

                entity.HasOne(d => d.Label)
                    .WithMany(p => p.LabelAttr)
                    .HasForeignKey(d => d.LabelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Label_Attr_Label");
            });

            modelBuilder.Entity<Lang>(entity =>
            {
                entity.HasIndex(e => new { e.Id, e.IsDefaultLang, e.LangCode, e.Ref })
                    .HasName("IX_Lang");

                entity.Property(e => e.LangCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ref)
                    .IsRequired()
                    .HasColumnName("ref")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ExpiredTime).HasColumnType("datetime");

                entity.Property(e => e.IssuedTime).HasColumnType("datetime");

                entity.Property(e => e.ProtectedTicket)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.RefreshToken)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RefreshToken_Account");

                entity.HasOne(d => d.ClientKey)
                    .WithMany(p => p.RefreshToken)
                    .HasForeignKey(d => d.ClientKeyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RefreshToken_ClientMaster");
            });

            modelBuilder.Entity<SystemParam>(entity =>
            {
                entity.HasIndex(e => new { e.Id, e.ParamKey })
                    .HasName("IX_System_Param");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ParamKey)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("value")
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
