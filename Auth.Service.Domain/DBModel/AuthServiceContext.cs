using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Auth.Service.Domain.DBModel
{
    public partial class AuthServiceContext : DbContext
    {
        public AuthServiceContext()
        {
        }

        public AuthServiceContext(DbContextOptions<AuthServiceContext> options)
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
        public virtual DbSet<TokenPayload> TokenPayload { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=NS-W7-DEV\\SQLEXPRESS; Database=AuthService;User ID=admin;Password=star01moon;Trusted_Connection=False; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasIndex(e => new { e.Id, e.ChangePassDeadline, e.CreatedDate, e.LastLoginDate, e.AccountTypeId, e.Email, e.IsActive, e.IsRequiredChangePass, e.Name })
                    .HasName("IX_Account");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountTypeId).HasColumnName("account_type_id");

                entity.Property(e => e.ChangePassDeadline)
                    .HasColumnName("change_pass_deadline")
                    .HasDefaultValueSql("('1900-01-01')");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasDefaultValueSql("([dbo].[GetLocalDate](DEFAULT))");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsRequiredChangePass).HasColumnName("is_required_change_pass");

                entity.Property(e => e.LastLoginDate).HasColumnName("last_login_date");

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
                    .HasColumnName("office_ios_code")
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasColumnName("pass")
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasDefaultValueSql("([dbo].[GetLocalDate](DEFAULT))");

                entity.HasOne(d => d.AccountType)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.AccountTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Account_Type");
            });

            modelBuilder.Entity<AccountAdditionalType>(entity =>
            {
                entity.ToTable("Account_Additional_Type");

                entity.HasIndex(e => new { e.Id, e.AccountAdditionalTypeId, e.AccountId })
                    .HasName("IX_Account_Additional_Type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountAdditionalTypeId)
                    .HasColumnName("account_additional_type_id")
                    .HasDefaultValueSql("((-1))");

                entity.Property(e => e.AccountId)
                    .HasColumnName("account_id")
                    .HasDefaultValueSql("((-1))");

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
                entity.ToTable("Account_Edit_Log");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountId)
                    .HasColumnName("account_id")
                    .HasDefaultValueSql("((-1))");

                entity.Property(e => e.ActionCode)
                    .IsRequired()
                    .HasColumnName("action_code")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("((-1))");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasDefaultValueSql("([dbo].[GetLocalDate](DEFAULT))");

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
                entity.ToTable("Account_Group");

                entity.HasIndex(e => new { e.Id, e.GroupCode, e.IsActive })
                    .HasName("IX_Account_Group");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GroupCode)
                    .IsRequired()
                    .HasColumnName("group_code")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<AccountGroupAttr>(entity =>
            {
                entity.ToTable("Account_Group_Attr");

                entity.HasIndex(e => new { e.Id, e.GroupId, e.GroupName, e.LangCode })
                    .HasName("IX_Account_Group_Attr");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GroupId)
                    .HasColumnName("group_id")
                    .HasDefaultValueSql("((-1))");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasColumnName("group_name")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LangCode)
                    .IsRequired()
                    .HasColumnName("lang_code")
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
                entity.ToTable("Account_Import_Log");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasDefaultValueSql("([dbo].[GetLocalDate](DEFAULT))");

                entity.Property(e => e.FilePath)
                    .IsRequired()
                    .HasColumnName("file_path")
                    .HasMaxLength(500)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LogMsg)
                    .IsRequired()
                    .HasColumnName("log_msg ")
                    .HasMaxLength(500)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<AccountLoginLog>(entity =>
            {
                entity.ToTable("Account_Login_Log");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccessIp)
                    .IsRequired()
                    .HasColumnName("access_ip")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.AccountId)
                    .HasColumnName("account_id")
                    .HasDefaultValueSql("((-1))");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasDefaultValueSql("([dbo].[GetLocalDate](DEFAULT))");

                entity.Property(e => e.UserAgent)
                    .IsRequired()
                    .HasColumnName("user_agent")
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
                entity.ToTable("Account_Type");

                entity.HasIndex(e => new { e.Id, e.GroupId, e.Level, e.AccountTypeCode, e.IsActive })
                    .HasName("IX_Account_Type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountTypeCode)
                    .IsRequired()
                    .HasColumnName("account_type_code")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.GroupId)
                    .HasColumnName("group_id")
                    .HasDefaultValueSql("((-1))");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
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
                entity.ToTable("Account_Type_Attr");

                entity.HasIndex(e => new { e.Id, e.AccountTypeId, e.LangCode, e.Name })
                    .HasName("IX_Account_Type_Attr");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountTypeId)
                    .HasColumnName("account_type_id")
                    .HasDefaultValueSql("((-1))");

                entity.Property(e => e.LangCode)
                    .IsRequired()
                    .HasColumnName("lang_code")
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
                    .HasName("PK__ClientMa__7296A607C1F71260");

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

                entity.Property(e => e.IsPrestore).HasColumnName("is_prestore");

                entity.Property(e => e.LabelKey)
                    .IsRequired()
                    .HasColumnName("label_key")
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
                    .HasColumnName("label_desc")
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LabelId).HasColumnName("label_id");

                entity.Property(e => e.LangCode)
                    .IsRequired()
                    .HasColumnName("lang_code")
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

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IsDefaultLang).HasColumnName("is_default_lang");

                entity.Property(e => e.LangCode)
                    .IsRequired()
                    .HasColumnName("lang_code")
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
                entity.ToTable("System_Param");

                entity.HasIndex(e => new { e.Id, e.ParamKey })
                    .HasName("IX_System_Param");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ParamKey)
                    .IsRequired()
                    .HasColumnName("param_key")
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("value")
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<TokenPayload>(entity =>
            {
                entity.HasKey(e => e.MidToken)
                    .IsClustered(false);

                entity.ToTable("Token_Payload");

                entity.Property(e => e.MidToken)
                    .HasColumnName("mid_token")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasColumnName("ip")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.JwtToken)
                    .IsRequired()
                    .HasColumnName("jwt_token")
                    .HasColumnType("ntext")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LastAccess)
                    .HasColumnName("last_access")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("([dbo].[GetLocalDate](DEFAULT))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
