using BloodDonationApp.Domain.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationApp.Infrastructure
{
    public class BloodDonationContext : DbContext
    {
        public BloodDonationContext(DbContextOptions<BloodDonationContext> options) : base(options)
        {
        }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Questionnaire> Questionnaires { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<TransfusionAction> TransfusionActions { get; set; }
        public DbSet<TransfusionCenterCoordinator> TransfusionCoordinators { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransfusionAction>()
                .HasMany(v => v.ListOfVolunteers)
                .WithMany(a => a.ListOfActions)
                .UsingEntity("CallToVolunteer",
                s => s.HasOne(typeof(Volunteer)).WithMany().HasForeignKey("VolunteerID").HasPrincipalKey(nameof(Volunteer.VolunteerID))
                .OnDelete(DeleteBehavior.Restrict),
                r => r.HasOne(typeof(TransfusionAction)).WithMany().HasForeignKey("ActionID").HasPrincipalKey(nameof(TransfusionAction.ActionID))
                .OnDelete(DeleteBehavior.Restrict),
                j => j.HasKey("VolunteerID", "ActionID")
                );

          
            modelBuilder.Entity<TransfusionAction>()
                .HasMany(d => d.ListOfDonors)
                .WithMany(a => a.ListOfActions)
                .UsingEntity("CallToDonate",
                s => s.HasOne(typeof(Donor)).WithMany().HasForeignKey("JMBG").HasPrincipalKey(nameof(Donor.JMBG))
                     .OnDelete(DeleteBehavior.Restrict), 
                r => r.HasOne(typeof(TransfusionAction)).WithMany().HasForeignKey("ActionID").HasPrincipalKey(nameof(TransfusionAction.ActionID))
                     .OnDelete(DeleteBehavior.Restrict), 
                j => j.HasKey("JMBG", "ActionID")
                );

            modelBuilder.Entity<Questionnaire>(entity =>
            {
                entity.HasKey(q => new { q.JMBG, q.QuestionnaireID, q.ActionID });

                entity.HasOne(q => q.Donor)
                      .WithMany(d => d.ListOfQuestionnaires)
                      .HasForeignKey(q => q.JMBG)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(q => q.TransfusionAction)
                      .WithMany(a => a.ListOfQuestionnaires)
                      .HasForeignKey(q => q.ActionID)
                      .OnDelete(DeleteBehavior.Restrict);          
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(q => new { q.JMBG, q.QuestionnaireID, q.ActionID, q.QuestionID });

                entity.HasOne(q => q.Questionnaire)
                      .WithMany(qn => qn.ListOfQuestions)
                      .HasForeignKey(q => new { q.JMBG, q.QuestionnaireID, q.ActionID })
                      .IsRequired(); 
            });

            modelBuilder.Entity<Donor>(
              x => {
                  x.Property(y => y.RowVersion)
                  .IsRowVersion();
              });

            modelBuilder.Entity<Volunteer>(
            x => {
                x.Property(y => y.RowVersion)
                .IsRowVersion();
            });

            Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
        private static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Place>().HasData(
                new Place() { PlaceID = 1, PlaceName = "Smederevo" },
                new Place() { PlaceID = 2, PlaceName = "Beograd" },
                new Place() { PlaceID = 3, PlaceName = "Sremska Mitrovica" },
                new Place() { PlaceID = 4, PlaceName = "Cacak" },
                new Place() { PlaceID = 5, PlaceName = "Novi Sad" },
                new Place() { PlaceID = 6, PlaceName = "Nis" },
                new Place() { PlaceID = 7, PlaceName = "Kraljevo" },
                new Place() { PlaceID = 8, PlaceName = "Subotica" },
                new Place() { PlaceID = 9, PlaceName = "Zrenjanin" },
                new Place() { PlaceID = 10, PlaceName = "Pancevo" },
                new Place() { PlaceID = 11, PlaceName = "Vlasotince" },
                new Place() { PlaceID = 12, PlaceName = "Novi Pazar" },
                new Place() { PlaceID = 13, PlaceName = "Kragujevac" });          

            modelBuilder.Entity<TransfusionCenterCoordinator>().HasData(
                new TransfusionCenterCoordinator() { CoordinatorID = 1, CoordinatorFullName = "Dunja Nesic", CoordinatorCode = "DN4213", Password = "Dunja.Nesic13" },
                new TransfusionCenterCoordinator() { CoordinatorID = 2, CoordinatorFullName = "Petar Nikodijevic", CoordinatorCode = "PN1107", Password = "PeraSD" },
                new TransfusionCenterCoordinator() { CoordinatorID = 3, CoordinatorFullName = "Stefan Jovanovic", CoordinatorCode = "SJ3107", Password = "StefanJo3107" });

            modelBuilder.Entity<Donor>().HasData(
                new Donor() { JMBG = "0101995700001", DonorFullName = "Mladen Mijailovic", DonorEmailAddress = "mijailovicmladen5@gmail.com", BloodType = BloodType.OPozitivna, IsActive = IsActive.Da, LastDonationDate = new DateTime(2024, 1, 13), PlaceID = 12 },
                new Donor() { JMBG = "0202995800002", DonorFullName = "Vladimir Lazarevic", DonorEmailAddress = "vladimir.lazarevic@fonis.rs", BloodType = BloodType.ABPozitivna, IsActive = IsActive.Da, LastDonationDate = new DateTime(2024, 1, 1), PlaceID = 4 },
                new Donor() { JMBG = "0303995900003", DonorFullName = "Sara Djokic", DonorEmailAddress = "sara.jana.djokic@gmail.com", BloodType = BloodType.ABNegativna, IsActive = IsActive.Da, LastDonationDate = new DateTime(2023, 11, 11), PlaceID = 1 },
                new Donor() { JMBG = "0407945940004", DonorFullName = "Nemanja Markovic", DonorEmailAddress = "markovicc26@gmail.com", BloodType = BloodType.BPozitivna, IsActive = IsActive.Ne, LastDonationDate = new DateTime(2023, 5, 4), PlaceID = 2 },
                new Donor() { JMBG = "1104345940234", DonorFullName = "Djordje Mirkovic", DonorEmailAddress = "djordjemirkovic001@gmail.com", BloodType = BloodType.ANegativna, IsActive = IsActive.Da, LastDonationDate = new DateTime(2023, 4, 11), PlaceID = 2 }
                );

            modelBuilder.Entity<Volunteer>().HasData(
                new Volunteer { VolunteerID = 1, VolunteerFullName = "Minja Filip", Sex = Sex.Zensko, DateFreeFrom = new DateTime(2024, 3, 10), DateFreeTo = new DateTime(2024, 4, 10), DateOfBirth = new DateTime(2001, 9, 24), PlaceID = 1,  VolunteerEmailAddress = "filip.minja95@gmail.com" },
                new Volunteer { VolunteerID = 2, VolunteerFullName = "Nevena Dukic", Sex = Sex.Zensko, DateFreeFrom = new DateTime(2024, 5, 1), DateFreeTo = new DateTime(2024, 7, 11), DateOfBirth = new DateTime(2001, 7, 15), PlaceID = 2,  VolunteerEmailAddress = "nevenadukic4@gmail.com" },
                new Volunteer { VolunteerID = 3, VolunteerFullName = "Sofija Filip", Sex = Sex.Zensko, DateFreeFrom = new DateTime(2024, 2, 10), DateFreeTo = new DateTime(2024, 4, 10), DateOfBirth = new DateTime(2001, 9, 24), PlaceID = 1,  VolunteerEmailAddress = "sfilip2022.10215@atssb.edu.rs" },
                new Volunteer { VolunteerID = 4, VolunteerFullName = "Vasilije Nesic", Sex = Sex.Zensko, DateFreeFrom = new DateTime(2024, 1, 15), DateFreeTo = new DateTime(2024, 4, 17), DateOfBirth = new DateTime(2002, 5, 12), PlaceID = 1, VolunteerEmailAddress = "nesicvasilije02@gmail.com" },
                new Volunteer { VolunteerID = 5, VolunteerFullName = "Iva Djokovic", Sex = Sex.Zensko, DateFreeFrom = new DateTime(2024, 10, 22), DateFreeTo = new DateTime(2024, 11, 10), DateOfBirth = new DateTime(2001, 4, 29), PlaceID = 2,  VolunteerEmailAddress = "iva.djokovic@fonis.rs" },
                new Volunteer { VolunteerID = 6, VolunteerFullName = "Veljko Nedeljkovic", Sex = Sex.Musko, DateFreeFrom = new DateTime(2024, 3, 3), DateFreeTo = new DateTime(2024, 5, 9), DateOfBirth = new DateTime(2001, 6, 6), PlaceID = 1,  VolunteerEmailAddress = "zippy@gmail.com" },
                new Volunteer { VolunteerID = 7, VolunteerFullName = "Predrag Tanaskovic", Sex = Sex.Musko, DateFreeFrom = new DateTime(2024, 3, 5), DateFreeTo = new DateTime(2024, 6, 8), DateOfBirth = new DateTime(2001, 1, 13), PlaceID = 4,  VolunteerEmailAddress = "predrag.tanaskovic@fonis.rs" });

            modelBuilder.Entity<TransfusionAction>().HasData(
                new TransfusionAction { ActionID = 1, ActionName = "FON humanitarna akcija", ActionDate = new DateTime(2024, 3, 15), ActionTimeFromTo = "10:00 - 16:00", ExactLocation = "Fakultet organizacionih nauka", PlaceID = 2 },
                new TransfusionAction { ActionID = 2, ActionName = "Sportski centar Smederevo", ActionDate = new DateTime(2024, 4, 5), ActionTimeFromTo = "08:00 - 18:00", ExactLocation = "Sportski centar", PlaceID = 1 },
                new TransfusionAction { ActionID = 3, ActionName = "Dobrovoljno davanje krvi u Vozdovim kapijama", ActionDate = new DateTime(2024, 5, 20), ActionTimeFromTo = "09:00 - 14:00", ExactLocation = "Crveni Krst Vozdovac", PlaceID = 2 },
                new TransfusionAction { ActionID = 4, ActionName = "Krv za zivot", ActionDate = new DateTime(2024, 6, 10), ActionTimeFromTo = "11:00 - 17:00", ExactLocation = "Opšta bolnica Niš", PlaceID = 6 },
                new TransfusionAction { ActionID = 5, ActionName = "Daj krv, spasi zivot", ActionDate = new DateTime(2024, 7, 3), ActionTimeFromTo = "10:00 - 15:00", ExactLocation = "Dom Zdravlja NP", PlaceID = 12 });

            modelBuilder.Entity<Questionnaire>().HasData(
                new Questionnaire { QuestionnaireID = 1, JMBG = "0101995700001", ActionID = 5, QuestionnaireTitle = "Upitnik za akciju Daj krv, spasi zivot u Novom Pazaru", Remark = "Odbijen zbog niskog krvnog pritiska" },
                new Questionnaire { QuestionnaireID = 2, JMBG = "1104345940234", ActionID = 1, QuestionnaireTitle = "Upitnik za akciju na FON-u", Remark = "/" },
                new Questionnaire { QuestionnaireID = 3, JMBG = "1104345940234", ActionID = 3, QuestionnaireTitle = "Upitnik za akciju u Vozdovim kapijama", Remark = "Sve ok" }
                );
        }

    }
}