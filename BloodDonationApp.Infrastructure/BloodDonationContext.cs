using BloodDonationApp.Domain.DomainModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        public DbSet<QuestionnaireQuestion> QuestionnaireQuestions { get; set; }
        public DbSet<TransfusionAction> TransfusionActions { get; set; }
        public DbSet<Official> Officials { get; set; }
        public DbSet<RedCross> RedCross { get; set; }
        public DbSet<CallToDonate> CallsToDonate { get; set; }
        public DbSet<CallToVolunteer> CallsToVolunteer { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Donor>()
            .HasKey(d => d.JMBG);

            modelBuilder.Entity<Volunteer>()
            .HasKey(v => v.VolunteerID);

            modelBuilder.Entity<Official>()
            .HasKey(o => o.OfficialID);

            modelBuilder.Entity<TransfusionAction>(entity => {

                entity.HasMany(v => v.ListOfVolunteers)
                   .WithMany(a => a.ListOfActions)
                   .UsingEntity<CallToVolunteer>(
                   s => s.HasOne(ctv => ctv.Volunteer).WithMany(v => v.CallsToVolunteer).HasForeignKey(ctv => ctv.VolunteerID).OnDelete(DeleteBehavior.Restrict),
                   r => r.HasOne(ctv => ctv.Action).WithMany(a => a.ListOfCallsToVolunteers).HasForeignKey(ctv => ctv.ActionID).OnDelete(DeleteBehavior.Cascade),
                   j => j.HasKey(ctv => new { ctv.VolunteerID, ctv.ActionID })
            );

                entity.HasMany(d => d.ListOfDonors)
                   .WithMany(a => a.ListOfActions)
                   .UsingEntity<CallToDonate>(
                   s => s.HasOne(ctd => ctd.Donor).WithMany(d => d.CallsToDonate).HasForeignKey(ctd => ctd.JMBG).OnDelete(DeleteBehavior.Restrict),
                   r => r.HasOne(ctd => ctd.Action).WithMany(a => a.ListOfCallsToDonors).HasForeignKey(ctd => ctd.ActionID).OnDelete(DeleteBehavior.Cascade),
                   j => j.HasKey(ctd => new { ctd.JMBG, ctd.ActionID })
            );

                entity.HasMany(d => d.ListOfDonors)
                   .WithMany(a => a.ListOfActions)
                   .UsingEntity<Questionnaire>(
                   s => s.HasOne(q => q.Donor).WithMany(d => d.ListOfQuestionnaires).HasForeignKey(q => q.JMBG).OnDelete(DeleteBehavior.Cascade),
                   r => r.HasOne(q => q.Action).WithMany(a => a.ListOfQuestionnaires).HasForeignKey(q => q.ActionID).OnDelete(DeleteBehavior.Restrict),
                   j => j.HasKey(q => new { q.JMBG, q.ActionID })
            );

                entity.HasMany(o => o.ListOfActionOfficials)
                   .WithMany(a => a.ListOfActions)
                   .UsingEntity("AssignedOfficial",
                   s => s.HasOne(typeof(Official)).WithMany().HasForeignKey("OfficialID").HasPrincipalKey(nameof(Official.OfficialID)).OnDelete(DeleteBehavior.NoAction),
                   r => r.HasOne(typeof(TransfusionAction)).WithMany().HasForeignKey("ActionID").HasPrincipalKey(nameof(TransfusionAction.ActionID)).OnDelete(DeleteBehavior.NoAction),
                   j => j.HasKey("OfficialID","ActionID")
                   );

                entity.HasOne(a => a.ActionCoordinator)
                   .WithMany(o => o.CreatedActions)
                   .HasForeignKey(a => a.OfficialID)
                   .OnDelete(DeleteBehavior.NoAction);
            });
                             

            modelBuilder.Entity<Questionnaire>(entity =>
            {
                entity.Property(q => q.DateOfMaking).HasDefaultValueSql("GETUTCDATE()");

                entity.HasMany(q => q.Questions)
                .WithMany()
                .UsingEntity<QuestionnaireQuestion>(
                    j => j.Property(k => k.Answer).HasDefaultValue(false));
            });


            modelBuilder.Entity<Questionnaire>()
                .Property(y => y.RowVersion)
                  .IsRowVersion();


            modelBuilder.Entity<QuestionnaireQuestion>()
                .Property(y => y.RowVersion)
                .IsRowVersion();

            modelBuilder.Entity<Official>()
            .HasDiscriminator<string>("Discriminator")
            .HasValue<MedicalWorker>("MedicalWorker")
            .HasValue<RedCrossWorker>("RedCrossWorker");

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

            modelBuilder.Entity<Donor>().HasData(
                new Donor() { JMBG = "0101995700001",  DonorFullName = "Mladen Mijailovic", DonorEmailAddress = "mijailovicmladen5@gmail.com", BloodType = BloodType.OPozitivna, IsActive = true, LastDonationDate = new DateTime(2024, 1, 13), PlaceID = 12, Sex = Sex.Musko },
                new Donor() { JMBG = "1104345940234",  DonorFullName = "Vladimir Lazarevic", DonorEmailAddress = "vladimir.lazarevic@fonis.rs", BloodType = BloodType.ABPozitivna, IsActive = true, LastDonationDate = new DateTime(2024, 1, 1), PlaceID = 4, Sex = Sex.Musko },
                new Donor() { JMBG = "0303995900003",  DonorFullName = "Sara Djokic", DonorEmailAddress = "sara.jana.djokic@gmail.com", BloodType = BloodType.ABNegativna, IsActive = true, LastDonationDate = new DateTime(2023, 11, 11), PlaceID = 1, Sex = Sex.Zensko },
                new Donor() { JMBG = "0407945940004",  DonorFullName = "Nemanja Markovic", DonorEmailAddress = "markovicc26@gmail.com", BloodType = BloodType.BPozitivna, IsActive = false, LastDonationDate = new DateTime(2023, 5, 4), PlaceID = 2, Sex = Sex.Musko },
                new Donor() { JMBG = "1604345940234",  DonorFullName = "Djordje Mirkovic", DonorEmailAddress = "djordjemirkovic001@gmail.com", BloodType = BloodType.ANegativna, IsActive = true, LastDonationDate = new DateTime(2023, 4, 11), PlaceID = 2, Sex = Sex.Musko },
                new Donor() { JMBG = "1104001765020",  DonorFullName = "Sandra Kovacevic", DonorEmailAddress = "saki@gmail.com", BloodType = BloodType.ANegativna, IsActive = true, LastDonationDate = new DateTime(2022, 4, 11), PlaceID = 6, Sex = Sex.Zensko },
                new Donor() { JMBG = "1107001543432",  DonorFullName = "Petar Nikodijevic", DonorEmailAddress = "pera@gmail.com", BloodType = BloodType.ANegativna, IsActive = true, LastDonationDate = new DateTime(2023, 4, 11), PlaceID = 1, Sex = Sex.Musko },
                new Donor() { JMBG = "1505001498898",  DonorFullName = "Stefan Kotlaja", DonorEmailAddress = "kotlajic@gmail.com", BloodType = BloodType.OPozitivna, IsActive = true, LastDonationDate = new DateTime(2024, 1, 1), PlaceID = 11, Sex = Sex.Musko }
                );

            modelBuilder.Entity<RedCross>().HasData(
                new RedCross() { RedCrossID = 1, InstitutionName = "CKSmederevo", PlaceID = 1 },
                new RedCross() { RedCrossID = 2, InstitutionName = "CK_Vozdovac", PlaceID = 1 },
                new RedCross() { RedCrossID = 3, InstitutionName = "Crveni krst NBG", PlaceID = 3 },
                new RedCross() { RedCrossID = 4, InstitutionName = "CK Sremska Mitrovica", PlaceID = 4 }
                );

            modelBuilder.Entity<Official>().HasData(
                new Official() { OfficialID = 1, OfficialFullName = "Dunja Nesic"},
                new Official() { OfficialID = 2, OfficialFullName = "Stefan Jovanovic" },
                new Official() { OfficialID = 3, OfficialFullName = "Pavle Gasic" }
                );

            modelBuilder.Entity<Volunteer>().HasData(
                new Volunteer { VolunteerID = 1,  VolunteerFullName = "Iva Djokovic", Sex = Sex.Zensko, DateFreeFrom = new DateTime(2024, 10, 22), DateFreeTo = new DateTime(2024, 11, 10), DateOfBirth = new DateTime(2001, 4, 29), RedCrossID = 2, VolunteerEmailAddress = "iva.djokovic@fonis.rs" },
                new Volunteer { VolunteerID = 2,  VolunteerFullName = "Nevena Dukic", Sex = Sex.Zensko, DateFreeFrom = new DateTime(2024, 5, 1), DateFreeTo = new DateTime(2024, 7, 11), DateOfBirth = new DateTime(2001, 7, 15), RedCrossID = 2,  VolunteerEmailAddress = "nevenadukic4@gmail.com" },
                new Volunteer { VolunteerID = 3,  VolunteerFullName = "Predrag Tanaskovic", Sex = Sex.Musko, DateFreeFrom = new DateTime(2024, 3, 5), DateFreeTo = new DateTime(2024, 6, 8), DateOfBirth = new DateTime(2001, 1, 13), RedCrossID = 4,  VolunteerEmailAddress = "predrag.tanaskovic@fonis.rs" },
                new Volunteer { VolunteerID = 4,  VolunteerFullName = "Veljko Nedeljkovic", Sex = Sex.Musko, DateFreeFrom = new DateTime(2024, 3, 3), DateFreeTo = new DateTime(2024, 5, 9), DateOfBirth = new DateTime(2001, 6, 6), RedCrossID = 1, VolunteerEmailAddress = "zippy@gmail.com" },
                new Volunteer { VolunteerID = 5,  VolunteerFullName = "Vasilije Nesic", Sex = Sex.Musko, DateFreeFrom = new DateTime(2024, 1, 15), DateFreeTo = new DateTime(2024, 4, 17), DateOfBirth = new DateTime(2002, 5, 12), RedCrossID = 1, VolunteerEmailAddress = "nesicvasilije02@gmail.com" },
                new Volunteer { VolunteerID = 6,  VolunteerFullName = "Minja Filip", Sex = Sex.Zensko, DateFreeFrom = new DateTime(2024, 3, 10), DateFreeTo = new DateTime(2024, 4, 10), DateOfBirth = new DateTime(2001, 9, 24), RedCrossID = 1, VolunteerEmailAddress = "filip.minja95@gmail.com" },
                new Volunteer { VolunteerID = 7,  VolunteerFullName = "Sofija Filip", Sex = Sex.Zensko, DateFreeFrom = new DateTime(2024, 2, 10), DateFreeTo = new DateTime(2024, 4, 10), DateOfBirth = new DateTime(2001, 9, 24), RedCrossID = 1, VolunteerEmailAddress = "sfilip2022.10215@atssb.edu.rs" }
                );

            modelBuilder.Entity<TransfusionAction>().HasData(
                new TransfusionAction { ActionID = 1, ActionName = "FON humanitarna akcija", ActionDate = new DateTime(2024, 3, 15), ActionTimeFromTo = "10:00 - 16:00", ExactLocation = "Fakultet organizacionih nauka", PlaceID = 2, OfficialID = 1 },
                new TransfusionAction { ActionID = 2, ActionName = "Sportski centar Smederevo", ActionDate = new DateTime(2024, 4, 5), ActionTimeFromTo = "08:00 - 18:00", ExactLocation = "Sportski centar", PlaceID = 1, OfficialID = 1 },
                new TransfusionAction { ActionID = 3, ActionName = "Dobrovoljno davanje krvi u Vozdovim kapijama", ActionDate = new DateTime(2024, 5, 20), ActionTimeFromTo = "09:00 - 14:00", ExactLocation = "Crveni Krst Vozdovac", PlaceID = 2, OfficialID = 1 },
                new TransfusionAction { ActionID = 4, ActionName = "Krv za zivot", ActionDate = new DateTime(2024, 8, 10), ActionTimeFromTo = "11:00 - 17:00", ExactLocation = "Opšta bolnica Niš", PlaceID = 6, OfficialID = 1 },
                new TransfusionAction { ActionID = 5, ActionName = "Daj krv, spasi zivot", ActionDate = new DateTime(2024, 7, 3), ActionTimeFromTo = "10:00 - 15:00", ExactLocation = "Dom Zdravlja NP", PlaceID = 12, OfficialID = 1 });

            modelBuilder.Entity<Questionnaire>().HasData(
                new Questionnaire { JMBG = "0101995700001", ActionID = 5, QuestionnaireTitle = "Upitnik za akciju Daj krv, spasi zivot u Novom Pazaru", Remark = "Odbijen zbog niskog krvnog pritiska", Approved = false },
                new Questionnaire { JMBG = "1104345940234", ActionID = 1, QuestionnaireTitle = "Upitnik za akciju na FON-u", Remark = "/", Approved = true },
                new Questionnaire { JMBG = "1104345940234", ActionID = 3, QuestionnaireTitle = "Upitnik za akciju u Vozdovim kapijama", Remark = "Sve ok", Approved = true }
                );
            modelBuilder.Entity<Question>().HasData(
                new Question { QuestionID = 1, QuestionText = "Da li redovno (svakodnevno) uzimate bilo kakve lekove?" },
                new Question { QuestionID = 2, QuestionText = "Da li ste poslednja 2-3 dana uzimali bilo kakve lekove (npr. Brufen, Kafetin, Analgin...)?" },
                new Question { QuestionID = 3, QuestionText = "Da li stalno uzimate Aspirin (Cardiopirin)? Da li ste ga uzimali u poslednjih 5 dana?" },
                new Question { QuestionID = 4, QuestionText = "Da li ste do sada ispitivani ili lečeni u bolnici ili ste trenutno na ispitivanju ili bolovanju?" },
                new Question { QuestionID = 5, QuestionText = "Da li ste vadili zub u proteklih 7 dana?" },
                new Question { QuestionID = 6, QuestionText = "Da li ste u poslednjih 7 do 10 dana imali temperaturu preko 38 C, kijavicu, prehladu ili uzimali antibiotike?" },
                new Question { QuestionID = 7, QuestionText = "Da li ste u poslednjih 6 meseci naglo izgubili na težini?" },
                new Question { QuestionID = 8, QuestionText = "Da li ste imali ubode krpelja u proteklih 12 meseci i da li ste se zbog toga javljali lekaru?" },
                new Question { QuestionID = 9, QuestionText = "Da li ste ikada lečeni od epilepsije (padavice), šećerne bolesti, astme, tuberkuloze, infarkta, moždanog udara, malignih oboljenja, mentalnih bolesti ili malarije?" },
                new Question { QuestionID = 10, QuestionText = "Da li bolujete od neke druge hronične bolesti: srca, pluća, bubrega, jetre, želuca i creva, kostiju i zglobova, nervnog sistema, krvi i krvnih sudova?" },
                new Question { QuestionID = 11, QuestionText = "Da li ste u proteklih 6 meseci putovali ili živeli u inostranstvu?" },
                new Question { QuestionID = 12, QuestionText = "Da li ste ikada imali problema sa štitastom žlezdom, hipofizom i/ili primali hormone?" },
                new Question { QuestionID = 13, QuestionText = "Da li imate neke promene na koži ili bolujete od alergije?" },
                new Question { QuestionID = 14, QuestionText = "Da li dugo krvavite posle povrede ili spontano dobijate modrice?" },
                new Question { QuestionID = 15, QuestionText = "Da li ste bolovali ili bolujete od hepatitisa (žutice) A, B ili C?" },
                new Question { QuestionID = 16, QuestionText = "Da li ste u proteklih 6 meseci imali akupunkturu, piercing ili tetovažu?" },
                new Question { QuestionID = 17, QuestionText = "Da li mislite da je postojala mogućnost da se zarazite HIV-om?" },
                new Question { QuestionID = 18, QuestionText = "Da li ste ikada koristili bilo koju vrstu droge?" },
                new Question { QuestionID = 19, QuestionText = "Da li ste ikada koristili preparate koji se zvanično ne izdaju na recept i/ili preparate za bodi bilding (steroide)?" },
                new Question { QuestionID = 20, QuestionText = "Da li znate na koje sve načine ste mogli izložiti sebe riziku od zaraznih, krvlju prenosivih bolesti?" },
                new Question { QuestionID = 21, QuestionText = "Da li ste u proteklih 6 meseci imali neku operaciju ili primili krv?" },
                new Question { QuestionID = 22, QuestionText = "Da li je davalac u dobrom opštem zdravstvenom stanju?", Flag = 1 },
                new Question { QuestionID = 23, QuestionText = "Da li davalac ima normalne vitalne znakove (krvni pritisak, puls, temperatura)?", Flag = 1 },
                new Question { QuestionID = 24, QuestionText = "Da li su nalazi krvne slike davaoca u granicama normale?", Flag = 1 },
                new Question { QuestionID = 25, QuestionText = "Da li je koža davaoca bez osipa, rana ili infekcija?", Flag = 1 },
                new Question { QuestionID = 26, QuestionText = "Da li je srčani ritam davaoca regularan bez znakova aritmije?", Flag = 1 },
                new Question { QuestionID = 27, QuestionText = "Da li su pluća davaoca čista i bez znakova infekcije ili zagušenja?", Flag = 1 },
                new Question { QuestionID = 28, QuestionText = "Da li davalac ima normalan nivo hemoglobina?", Flag = 1 },
                new Question { QuestionID = 29, QuestionText = "Da li davalac pokazuje znake anemije ili drugih krvnih poremećaja?", Flag = 1 },
                new Question { QuestionID = 30, QuestionText = "Da li je rezultat testa za HIV, hepatitis B, hepatitis C i sifilis negativan?", Flag = 1 },
                new Question { QuestionID = 31, QuestionText = "Da li davalac ima adekvatan nivo hidratacije i nije dehidriran?", Flag = 1 }
                );
            modelBuilder.Entity<CallToDonate>().HasData(
                new CallToDonate { JMBG = "0101995700001", ActionID = 1, AcceptedTheCall = true, ShowedUp = true },
                new CallToDonate { JMBG = "0101995700001", ActionID = 2, AcceptedTheCall = false, ShowedUp = false },
                new CallToDonate { JMBG = "0101995700001", ActionID = 3, AcceptedTheCall = true, ShowedUp = false },
                new CallToDonate { JMBG = "0101995700001", ActionID = 4, AcceptedTheCall = true, ShowedUp = true },
                new CallToDonate { JMBG = "0101995700001", ActionID = 5, AcceptedTheCall = false, ShowedUp = true }
                );
            modelBuilder.Entity<CallToVolunteer>().HasData(
                new CallToVolunteer { VolunteerID = 1, ActionID = 1, AcceptedTheCall = true, ShowedUp = true },
                new CallToVolunteer { VolunteerID = 1, ActionID = 2, AcceptedTheCall = true, ShowedUp = false },
                new CallToVolunteer { VolunteerID = 1, ActionID = 3, AcceptedTheCall = false, ShowedUp = true },
                new CallToVolunteer { VolunteerID = 1, ActionID = 4, AcceptedTheCall = false, ShowedUp = false },
                new CallToVolunteer { VolunteerID = 1, ActionID = 5, AcceptedTheCall = true, ShowedUp = true }
                );
        }

    }
}