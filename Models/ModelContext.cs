using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace yogago.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Classcategory> Classcategories { get; set; }

    public virtual DbSet<Classmember> Classmembers { get; set; }

    public virtual DbSet<Contactu> Contactus { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Homemanagementimg> Homemanagementimgs { get; set; }

    public virtual DbSet<Homemanagementtext> Homemanagementtexts { get; set; }

    public virtual DbSet<Invoiceinfo> Invoiceinfos { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Memberplan> Memberplans { get; set; }

    public virtual DbSet<Paymentinfo> Paymentinfos { get; set; }

    public virtual DbSet<Plan> Plans { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Testimonial> Testimonials { get; set; }

    public virtual DbSet<Trainer> Trainers { get; set; }

    public virtual DbSet<Userinfo> Userinfos { get; set; }

    public virtual DbSet<Userlogin> Userlogins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=DESKTOP-6PHK2D8)(PORT=1521))(CONNECT_DATA=(SID=xe)));User Id=C##Asel; Password=711;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("C##ASEL")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Classid).HasName("SYS_C008426");

            entity.ToTable("CLASSES");

            entity.Property(e => e.Classid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("CLASSID");
            entity.Property(e => e.Categoryid)
                .HasColumnType("NUMBER")
                .HasColumnName("CATEGORYID");
            entity.Property(e => e.Classdays)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CLASSDAYS");
            entity.Property(e => e.Classname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CLASSNAME");
            entity.Property(e => e.Classtime)
                .HasPrecision(6)
                .HasColumnName("CLASSTIME");
            entity.Property(e => e.Imgcover)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("IMGCOVER");
            entity.Property(e => e.Trainerid)
                .HasColumnType("NUMBER")
                .HasColumnName("TRAINERID");

            entity.HasOne(d => d.Category).WithMany(p => p.Classes)
                .HasForeignKey(d => d.Categoryid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008427");

            entity.HasOne(d => d.Trainer).WithMany(p => p.Classes)
                .HasForeignKey(d => d.Trainerid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008428");
        });

        modelBuilder.Entity<Classcategory>(entity =>
        {
            entity.HasKey(e => e.Categoryid).HasName("SYS_C008420");

            entity.ToTable("CLASSCATEGORY");

            entity.Property(e => e.Categoryid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("CATEGORYID");
            entity.Property(e => e.Categoryname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CATEGORYNAME");
        });

        modelBuilder.Entity<Classmember>(entity =>
        {
            entity.HasKey(e => e.Classmemberid).HasName("SYS_C008442");

            entity.ToTable("CLASSMEMBER");

            entity.Property(e => e.Classmemberid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("CLASSMEMBERID");
            entity.Property(e => e.Classid)
                .HasColumnType("NUMBER")
                .HasColumnName("CLASSID");
            entity.Property(e => e.Memberid)
                .HasColumnType("NUMBER")
                .HasColumnName("MEMBERID");

            entity.HasOne(d => d.Class).WithMany(p => p.Classmembers)
                .HasForeignKey(d => d.Classid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008443");

            entity.HasOne(d => d.Member).WithMany(p => p.Classmembers)
                .HasForeignKey(d => d.Memberid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008444");
        });

        modelBuilder.Entity<Contactu>(entity =>
        {
            entity.HasKey(e => e.Contactid).HasName("SYS_C008491");

            entity.ToTable("CONTACTUS");

            entity.Property(e => e.Contactid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("CONTACTID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Message)
                .HasColumnType("CLOB")
                .HasColumnName("MESSAGE");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Subject)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SUBJECT");
            entity.Property(e => e.Submitteddate)
                .HasDefaultValueSql("SYSDATE\n")
                .HasColumnType("DATE")
                .HasColumnName("SUBMITTEDDATE");
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.Discountid).HasName("SYS_C008484");

            entity.ToTable("DISCOUNT");

            entity.HasIndex(e => e.Discountcode, "SYS_C008485").IsUnique();

            entity.Property(e => e.Discountid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("DISCOUNTID");
            entity.Property(e => e.Discountcode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DISCOUNTCODE");
            entity.Property(e => e.Discountname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DISCOUNTNAME");
            entity.Property(e => e.Discountpercentage)
                .HasColumnType("NUMBER(5,2)")
                .HasColumnName("DISCOUNTPERCENTAGE");
            entity.Property(e => e.Enddate)
                .HasColumnType("DATE")
                .HasColumnName("ENDDATE");
            entity.Property(e => e.Isactive)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("'Y' ")
                .IsFixedLength()
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.Startdate)
                .HasColumnType("DATE")
                .HasColumnName("STARTDATE");
        });

        modelBuilder.Entity<Homemanagementimg>(entity =>
        {
            entity.HasKey(e => e.Imgid).HasName("SYS_C008468");

            entity.ToTable("HOMEMANAGEMENTIMG");

            entity.Property(e => e.Imgid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("IMGID");
            entity.Property(e => e.Imgname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IMGNAME");
            entity.Property(e => e.Imgpath)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IMGPATH");
        });

        modelBuilder.Entity<Homemanagementtext>(entity =>
        {
            entity.HasKey(e => e.Textid).HasName("SYS_C008472");

            entity.ToTable("HOMEMANAGEMENTTEXT");

            entity.Property(e => e.Textid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("TEXTID");
            entity.Property(e => e.Content)
                .HasColumnType("CLOB")
                .HasColumnName("CONTENT");
            entity.Property(e => e.Textname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TEXTNAME");
        });

        modelBuilder.Entity<Invoiceinfo>(entity =>
        {
            entity.HasKey(e => e.Invoiceid).HasName("SYS_C008458");

            entity.ToTable("INVOICEINFO");

            entity.Property(e => e.Invoiceid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("INVOICEID");
            entity.Property(e => e.Amount)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("AMOUNT");
            entity.Property(e => e.Emailsent)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("'N' ")
                .IsFixedLength()
                .HasColumnName("EMAILSENT");
            entity.Property(e => e.Generateddate)
                .HasDefaultValueSql("SYSDATE")
                .HasColumnType("DATE")
                .HasColumnName("GENERATEDDATE");
            entity.Property(e => e.Memberplanid)
                .HasColumnType("NUMBER")
                .HasColumnName("MEMBERPLANID");

            entity.HasOne(d => d.Memberplan).WithMany(p => p.Invoiceinfos)
                .HasForeignKey(d => d.Memberplanid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008459");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.Memberid).HasName("SYS_C008435");

            entity.ToTable("MEMBER");

            entity.Property(e => e.Memberid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("MEMBERID");
            entity.Property(e => e.Joindate)
                .HasDefaultValueSql("SYSDATE  -- Date the user became a member\n")
                .HasColumnType("DATE")
                .HasColumnName("JOINDATE");
            entity.Property(e => e.Userid)
                .HasColumnType("NUMBER")
                .HasColumnName("USERID");

            entity.HasOne(d => d.User).WithMany(p => p.Members)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008436");
        });

        modelBuilder.Entity<Memberplan>(entity =>
        {
            entity.HasKey(e => e.Memberplanid).HasName("SYS_C008438");

            entity.ToTable("MEMBERPLAN");

            entity.Property(e => e.Memberplanid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("MEMBERPLANID");
            entity.Property(e => e.Memberid)
                .HasColumnType("NUMBER")
                .HasColumnName("MEMBERID");
            entity.Property(e => e.Planid)
                .HasColumnType("NUMBER")
                .HasColumnName("PLANID");
            entity.Property(e => e.Startdate)
                .HasDefaultValueSql("SYSDATE\n")
                .HasColumnType("DATE")
                .HasColumnName("STARTDATE");

            entity.HasOne(d => d.Member).WithMany(p => p.Memberplans)
                .HasForeignKey(d => d.Memberid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008439");

            entity.HasOne(d => d.Plan).WithMany(p => p.Memberplans)
                .HasForeignKey(d => d.Planid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008440");
        });

        modelBuilder.Entity<Paymentinfo>(entity =>
        {
            entity.HasKey(e => e.Cardid).HasName("SYS_C008453");

            entity.ToTable("PAYMENTINFO");

            entity.Property(e => e.Cardid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("CARDID");
            entity.Property(e => e.Cardnumberencrypted)
                .HasColumnType("CLOB")
                .HasColumnName("CARDNUMBERENCRYPTED");
            entity.Property(e => e.Createddate)
                .HasDefaultValueSql("SYSDATE -- Timestamp when the record was created\n")
                .HasColumnType("DATE")
                .HasColumnName("CREATEDDATE");
            entity.Property(e => e.Cvvencrypted)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("CVVENCRYPTED");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Expirymonth)
                .HasPrecision(2)
                .HasColumnName("EXPIRYMONTH");
            entity.Property(e => e.Expiryyear)
                .HasPrecision(4)
                .HasColumnName("EXPIRYYEAR");
            entity.Property(e => e.Fullname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FULLNAME");
            entity.Property(e => e.Memberid)
                .HasColumnType("NUMBER")
                .HasColumnName("MEMBERID");
            entity.Property(e => e.Zipcode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ZIPCODE");

            entity.HasOne(d => d.Member).WithMany(p => p.Paymentinfos)
                .HasForeignKey(d => d.Memberid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008454");
        });

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.Planid).HasName("SYS_C008433");

            entity.ToTable("PLAN");

            entity.Property(e => e.Planid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("PLANID");
            entity.Property(e => e.Durationmonths)
                .HasColumnType("NUMBER")
                .HasColumnName("DURATIONMONTHS");
            entity.Property(e => e.Planname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PLANNAME");
            entity.Property(e => e.Price)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("PRICE");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("SYS_C008396");

            entity.ToTable("ROLES");

            entity.HasIndex(e => e.Rolename, "SYS_C008397").IsUnique();

            entity.Property(e => e.Roleid)
                .HasColumnType("NUMBER")
                .HasColumnName("ROLEID");
            entity.Property(e => e.Rolename)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ROLENAME");
        });

        modelBuilder.Entity<Testimonial>(entity =>
        {
            entity.HasKey(e => e.Testimonialid).HasName("SYS_C008477");

            entity.ToTable("TESTIMONIAL");

            entity.Property(e => e.Testimonialid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("TESTIMONIALID");
            entity.Property(e => e.Content)
                .HasColumnType("CLOB")
                .HasColumnName("CONTENT");
            entity.Property(e => e.Memberid)
                .HasColumnType("NUMBER")
                .HasColumnName("MEMBERID");
            entity.Property(e => e.Rating)
                .HasColumnType("NUMBER")
                .HasColumnName("RATING");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("'Pending' ")
                .HasColumnName("STATUS");
            entity.Property(e => e.Submitteddate)
                .HasDefaultValueSql("SYSDATE                             -- Timestamp of submission\n")
                .HasColumnType("DATE")
                .HasColumnName("SUBMITTEDDATE");

            entity.HasOne(d => d.Member).WithMany(p => p.Testimonials)
                .HasForeignKey(d => d.Memberid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008478");
        });

        modelBuilder.Entity<Trainer>(entity =>
        {
            entity.HasKey(e => e.Trainerid).HasName("SYS_C008409");

            entity.ToTable("TRAINER");

            entity.Property(e => e.Trainerid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("TRAINERID");
            entity.Property(e => e.Experience)
                .HasColumnType("NUMBER")
                .HasColumnName("EXPERIENCE");
            entity.Property(e => e.Specialization)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SPECIALIZATION");
            entity.Property(e => e.Userid)
                .HasColumnType("NUMBER")
                .HasColumnName("USERID");

            entity.HasOne(d => d.User).WithMany(p => p.Trainers)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008410");
        });

        modelBuilder.Entity<Userinfo>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("SYS_C008405");

            entity.ToTable("USERINFO");

            entity.HasIndex(e => e.Username, "SYS_C008406").IsUnique();

            entity.Property(e => e.Userid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("USERID");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.Dateadded)
                .HasDefaultValueSql("SYSDATE\n")
                .HasColumnType("DATE")
                .HasColumnName("DATEADDED");
            entity.Property(e => e.Dateofbirth)
                .HasColumnType("DATE")
                .HasColumnName("DATEOFBIRTH");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Fullname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FULLNAME");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PHONE");
            entity.Property(e => e.Profilepicture)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("PROFILEPICTURE");
            entity.Property(e => e.Roleid)
                .HasColumnType("NUMBER")
                .HasColumnName("ROLEID");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USERNAME");

            entity.HasOne(d => d.Role).WithMany(p => p.Userinfos)
                .HasForeignKey(d => d.Roleid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SYS_C008407");
        });

        modelBuilder.Entity<Userlogin>(entity =>
        {
            entity.HasKey(e => e.Loginid).HasName("SYS_C008414");

            entity.ToTable("USERLOGIN");

            entity.HasIndex(e => e.Username, "SYS_C008415").IsUnique();

            entity.Property(e => e.Loginid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("LOGINID");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Roleid)
                .HasColumnType("NUMBER")
                .HasColumnName("ROLEID");
            entity.Property(e => e.Userid)
                .HasColumnType("NUMBER")
                .HasColumnName("USERID");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USERNAME");

            entity.HasOne(d => d.Role).WithMany(p => p.Userlogins)
                .HasForeignKey(d => d.Roleid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008417");

            entity.HasOne(d => d.User).WithMany(p => p.Userlogins)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008416");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
