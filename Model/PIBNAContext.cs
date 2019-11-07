using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PIBNAAPI.Model
{
    public partial class PIBNAContext : DbContext
    {
        public PIBNAContext()
        {
        }

        public PIBNAContext(DbContextOptions<PIBNAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PApprovedStatus> PApprovedStatus { get; set; }
        public virtual DbSet<PClub> PClub { get; set; }
        public virtual DbSet<PClubHost> PClubHost { get; set; }
        public virtual DbSet<PClubOfficial> PClubOfficial { get; set; }
        public virtual DbSet<PContactUs> PContactUs { get; set; }
        public virtual DbSet<PDivision> PDivision { get; set; }
        public virtual DbSet<PImages> PImages { get; set; }
        public virtual DbSet<PMember> PMember { get; set; }
        public virtual DbSet<PMemberApproval> PMemberApproval { get; set; }
        public virtual DbSet<PPosition> PPosition { get; set; }
        public virtual DbSet<PRole> PRole { get; set; }
        public virtual DbSet<PRoleRules> PRoleRules { get; set; }
        public virtual DbSet<PSeason> PSeason { get; set; }
        public virtual DbSet<PTeam> PTeam { get; set; }
        public virtual DbSet<PTeamOfficial> PTeamOfficial { get; set; }
        public virtual DbSet<PTeamRoster> PTeamRoster { get; set; }
        public virtual DbSet<PTeamStatus> PTeamStatus { get; set; }
        public virtual DbSet<PTournament> PTournament { get; set; }
        public virtual DbSet<PTournamentTrx> PTournamentTrx { get; set; }
        public virtual DbSet<PUser> PUser { get; set; }
        public virtual DbSet<PUserRole> PUserRole { get; set; }
        public virtual DbSet<PUserType> PUserType { get; set; }
        public virtual DbSet<PWebContent> PWebContent { get; set; }
        public virtual DbSet<PWebContentType> PWebContentType { get; set; }
        public virtual DbSet<PWebPageContentType> PWebPageContentType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //string cn = @"Data Source=tcp:sql2k1602.discountasp.net;Initial Catalog=***;User ID=**;Password=**;";
                //optionsBuilder.UseSqlServer(cn);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<PApprovedStatus>(entity =>
            {
                entity.HasKey(e => e.ApprovedStatusId);

                entity.ToTable("p_ApprovedStatus");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<PClub>(entity =>
            {
                entity.HasKey(e => e.ClubId);

                entity.ToTable("p_Club");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ClubName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.ClubShortName).HasMaxLength(25);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.WebSite).HasMaxLength(250);
            });

            modelBuilder.Entity<PClubHost>(entity =>
            {
                entity.HasKey(e => e.HostId);

                entity.ToTable("p_ClubHost");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.HasOne(d => d.Club)
                    .WithMany(p => p.PClubHost)
                    .HasForeignKey(d => d.ClubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_p_ClubHost_p_Club");

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.PClubHost)
                    .HasForeignKey(d => d.SeasonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_p_ClubHost_p_Season");
            });

            modelBuilder.Entity<PClubOfficial>(entity =>
            {
                entity.HasKey(e => e.ClubOfficialId);

                entity.ToTable("p_ClubOfficial");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.FromDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.HasOne(d => d.Club)
                    .WithMany(p => p.PClubOfficial)
                    .HasForeignKey(d => d.ClubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_p_ClubOfficial_p_Club");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.PClubOfficial)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_p_ClubOfficial_p_Position1");
            });

            modelBuilder.Entity<PContactUs>(entity =>
            {
                entity.ToTable("p_ContactUs");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateFrom).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .HasColumnType("text");

                entity.Property(e => e.ResponseDate).HasColumnType("datetime");

                entity.Property(e => e.Subject).HasColumnName("subject");
            });

            modelBuilder.Entity<PDivision>(entity =>
            {
                entity.HasKey(e => e.DivisionId);

                entity.ToTable("p_Division");

                entity.Property(e => e.DivisionName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.DivisionShortName).HasMaxLength(50);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.MaxHeightRequired).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<PImages>(entity =>
            {
                entity.HasKey(e => e.ImageId);

                entity.ToTable("p_Images");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.ImageName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Club)
                    .WithMany(p => p.PImages)
                    .HasForeignKey(d => d.ClubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_p_Images_p_Club");
            });

            modelBuilder.Entity<PMember>(entity =>
            {
                entity.HasKey(e => e.MemberId);

                entity.ToTable("p_Member");

                entity.Property(e => e.DateBlockListed).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.HasOne(d => d.Club)
                    .WithMany(p => p.PMember)
                    .HasForeignKey(d => d.ClubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_p_Member_p_Club");
            });

            modelBuilder.Entity<PMemberApproval>(entity =>
            {
                entity.HasKey(e => e.MemberApprovalId);

                entity.ToTable("p_MemberApproval");

                entity.Property(e => e.ApprovedDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.HasOne(d => d.ApprovedStatus)
                    .WithMany(p => p.PMemberApproval)
                    .HasForeignKey(d => d.ApprovedStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_p_MemberApproval_p_ApprovedStatus");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.PMemberApproval)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_p_MemberApproval_p_Member");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PMemberApproval)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_p_MemberApproval_p_User");
            });

            modelBuilder.Entity<PPosition>(entity =>
            {
                entity.HasKey(e => e.PositionId);

                entity.ToTable("p_Position");

                entity.Property(e => e.PositionDescription)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<PRole>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("p_role");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<PRoleRules>(entity =>
            {
                entity.HasKey(e => e.RoleRules);

                entity.ToTable("p_RoleRules");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.PRoleRules)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_p_RoleRules_p_role");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.PRoleRules)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_p_RoleRules_p_UserType");
            });

            modelBuilder.Entity<PSeason>(entity =>
            {
                entity.HasKey(e => e.SeasonId);

                entity.ToTable("p_Season");

                entity.Property(e => e.SeasonName)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<PTeam>(entity =>
            {
                entity.HasKey(e => e.TeamId);

                entity.ToTable("p_Team");

                entity.Property(e => e.ApprovedDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.SubmitForApprovalDate).HasColumnType("datetime");

                entity.Property(e => e.TeamName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.TeamShortName).HasMaxLength(50);

                entity.Property(e => e.TeamStatusId).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Club)
                    .WithMany(p => p.PTeam)
                    .HasForeignKey(d => d.ClubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_p_Team_p_Club");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.PTeam)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_p_Team_p_Division");

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.PTeam)
                    .HasForeignKey(d => d.SeasonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_p_Team_p_Season");

                entity.HasOne(d => d.TeamStatus)
                    .WithMany(p => p.PTeam)
                    .HasForeignKey(d => d.TeamStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_p_Team_p_TeamStatus");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PTeam)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_p_Team_p_User");
            });

            modelBuilder.Entity<PTeamOfficial>(entity =>
            {
                entity.HasKey(e => e.TeamOfficialId);

                entity.ToTable("p_TeamOfficial");

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.PTeamOfficial)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_p_TeamOfficial_p_Position");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.PTeamOfficial)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_p_TeamOfficial_p_Team");
            });

            modelBuilder.Entity<PTeamRoster>(entity =>
            {
                entity.HasKey(e => e.TeamRosterId);

                entity.ToTable("p_TeamRoster");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.PTeamRoster)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_p_TeamRoster_p_Member");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.PTeamRoster)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_p_TeamRoster_p_Team");
            });

            modelBuilder.Entity<PTeamStatus>(entity =>
            {
                entity.HasKey(e => e.TeamStatusId);

                entity.ToTable("p_TeamStatus");

                entity.Property(e => e.TeamStatusDescription)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PTournament>(entity =>
            {
                entity.HasKey(e => e.TournamentId);

                entity.ToTable("p_Tournament");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.FromDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.PTournament)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_p_Tournament_p_Division");

                entity.HasOne(d => d.HostCityNavigation)
                    .WithMany(p => p.PTournament)
                    .HasForeignKey(d => d.HostCity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_p_Tournament_p_Club");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PTournament)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_p_Tournament_p_User");
            });

            modelBuilder.Entity<PTournamentTrx>(entity =>
            {
                entity.HasKey(e => e.TournamentTrx);

                entity.ToTable("p_TournamentTrx");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.FromDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.PTournamentTrx)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_p_TournamentTrx_p_Team");

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.PTournamentTrx)
                    .HasForeignKey(d => d.TournamentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_p_TournamentTrx_p_Tournament");
            });

            modelBuilder.Entity<PUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("p_User");

                entity.Property(e => e.AccessCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.Club)
                    .WithMany(p => p.PUser)
                    .HasForeignKey(d => d.ClubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_p_User_p_Club");
            });

            modelBuilder.Entity<PUserRole>(entity =>
            {
                entity.HasKey(e => e.UserRoleId);

                entity.ToTable("p_UserRole");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PUserRole)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_p_UserRole_p_User");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.PUserRole)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_p_UserRole_p_UserType");
            });

            modelBuilder.Entity<PUserType>(entity =>
            {
                entity.HasKey(e => e.UserTypeId);

                entity.ToTable("p_UserType");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<PWebContent>(entity =>
            {
                entity.HasKey(e => e.WebContentId)
                    .HasName("PK_WebContent");

                entity.ToTable("p_WebContent");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.PublishStartDate).HasColumnType("datetime");

                entity.Property(e => e.PublishedEndDate).HasColumnType("datetime");

                entity.Property(e => e.WebContent).IsRequired();

                entity.Property(e => e.WebTitle)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.PostedBy)
                    .WithMany(p => p.PWebContent)
                    .HasForeignKey(d => d.PostedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_p_WebContent_p_User");

                entity.HasOne(d => d.WebContentType)
                    .WithMany(p => p.PWebContent)
                    .HasForeignKey(d => d.WebContentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_p_WebContent_p_WebContentType");

                entity.HasOne(d => d.WebContentTypeNavigation)
                    .WithMany(p => p.PWebContent)
                    .HasForeignKey(d => d.WebContentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_p_WebContent_p_WebPageContentType");
            });

            modelBuilder.Entity<PWebContentType>(entity =>
            {
                entity.HasKey(e => e.WebContentTypeId);

                entity.ToTable("p_WebContentType");

                entity.Property(e => e.WebContentTypeName)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<PWebPageContentType>(entity =>
            {
                entity.HasKey(e => e.WebPageContentTypeId);

                entity.ToTable("p_WebPageContentType");

                entity.Property(e => e.WebPageContentTypeName)
                    .IsRequired()
                    .HasMaxLength(150);
            });
        }
    }
}
