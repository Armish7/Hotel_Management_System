using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HMS.Models;

public partial class ManagementSystemContext : DbContext
{
    public ManagementSystemContext()
    {
    }

    public ManagementSystemContext(DbContextOptions<ManagementSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Guest> Guests { get; set; }

    public virtual DbSet<Hotelamenity> Hotelamenities { get; set; }

    public virtual DbSet<Maintenance> Maintenances { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Roomamenity> Roomamenities { get; set; }

    public virtual DbSet<Roomservice> Roomservices { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=ep-small-surf-a4mix6cc-pooler.us-east-1.aws.neon.tech;Database=ManagementSystem;Username=ManagementSystem_owner;Password=npg_62AIaKFeJRuH");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Bookingid).HasName("booking_pkey");

            entity.ToTable("booking");

            entity.Property(e => e.Bookingid)
                .ValueGeneratedNever()
                .HasColumnName("bookingid");
            entity.Property(e => e.Bookingdate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("bookingdate");
            entity.Property(e => e.Checkin).HasColumnName("checkin");
            entity.Property(e => e.Checkout).HasColumnName("checkout");
            entity.Property(e => e.Guestid).HasColumnName("guestid");
            entity.Property(e => e.Roomid).HasColumnName("roomid");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.Guest).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.Guestid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("booking_guestid_fkey");

            entity.HasOne(d => d.Room).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.Roomid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("booking_roomid_fkey");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Employeeid).HasName("employee_pkey");

            entity.ToTable("employee");

            entity.Property(e => e.Employeeid)
                .ValueGeneratedNever()
                .HasColumnName("employeeid");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .HasColumnName("firstname");
            entity.Property(e => e.Hiredate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("hiredate");
            entity.Property(e => e.Jobtitle)
                .HasMaxLength(100)
                .HasColumnName("jobtitle");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .HasColumnName("lastname");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(15)
                .HasColumnName("phonenumber");
        });

        modelBuilder.Entity<Guest>(entity =>
        {
            entity.HasKey(e => e.Guestid).HasName("guest_pkey");

            entity.ToTable("guest");

            entity.Property(e => e.Guestid)
                .ValueGeneratedNever()
                .HasColumnName("guestid");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .HasColumnName("lastname");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(15)
                .HasColumnName("phonenumber");
        });

        modelBuilder.Entity<Hotelamenity>(entity =>
        {
            entity.HasKey(e => e.Amenityid).HasName("hotelamenity_pkey");

            entity.ToTable("hotelamenity");

            entity.Property(e => e.Amenityid)
                .ValueGeneratedNever()
                .HasColumnName("amenityid");
            entity.Property(e => e.Amenityname)
                .HasMaxLength(100)
                .HasColumnName("amenityname");
            entity.Property(e => e.Availabilitystatus)
                .HasMaxLength(50)
                .HasColumnName("availabilitystatus");
            entity.Property(e => e.Description).HasColumnName("description");
        });

        modelBuilder.Entity<Maintenance>(entity =>
        {
            entity.HasKey(e => e.Maintenanceid).HasName("maintenance_pkey");

            entity.ToTable("maintenance");

            entity.Property(e => e.Maintenanceid)
                .ValueGeneratedNever()
                .HasColumnName("maintenanceid");
            entity.Property(e => e.Issuedescription).HasColumnName("issuedescription");
            entity.Property(e => e.Maintenancetype)
                .HasMaxLength(100)
                .HasColumnName("maintenancetype");
            entity.Property(e => e.Requestdate).HasColumnName("requestdate");
            entity.Property(e => e.Roomid).HasColumnName("roomid");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.Room).WithMany(p => p.Maintenances)
                .HasForeignKey(d => d.Roomid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("maintenance_roomid_fkey");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Paymentid).HasName("payment_pkey");

            entity.ToTable("payment");

            entity.Property(e => e.Paymentid)
                .ValueGeneratedNever()
                .HasColumnName("paymentid");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.Bookingid).HasColumnName("bookingid");
            entity.Property(e => e.Paymentdate).HasColumnName("paymentdate");
            entity.Property(e => e.Paymentmethod)
                .HasMaxLength(50)
                .HasColumnName("paymentmethod");
            entity.Property(e => e.Paymentstatus)
                .HasMaxLength(50)
                .HasColumnName("paymentstatus");
            entity.Property(e => e.Transactionnumber)
                .HasMaxLength(100)
                .HasColumnName("transactionnumber");

            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Bookingid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_bookingid_fkey");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Reviewid).HasName("review_pkey");

            entity.ToTable("review");

            entity.Property(e => e.Reviewid)
                .ValueGeneratedNever()
                .HasColumnName("reviewid");
            entity.Property(e => e.Guestid).HasColumnName("guestid");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Reviewdate).HasColumnName("reviewdate");
            entity.Property(e => e.Reviewtext).HasColumnName("reviewtext");
            entity.Property(e => e.Roomid).HasColumnName("roomid");
            entity.Property(e => e.Serviceid).HasColumnName("serviceid");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");

            entity.HasOne(d => d.Guest).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.Guestid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("review_guestid_fkey");

            entity.HasOne(d => d.Room).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.Roomid)
                .HasConstraintName("review_roomid_fkey");

            entity.HasOne(d => d.Service).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.Serviceid)
                .HasConstraintName("review_serviceid_fkey");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Roomid).HasName("room_pkey");

            entity.ToTable("room");

            entity.HasIndex(e => e.Roomnumber, "room_roomnumber_key").IsUnique();

            entity.Property(e => e.Roomid)
                .ValueGeneratedNever()
                .HasColumnName("roomid");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.Pricepernight)
                .HasPrecision(10, 2)
                .HasColumnName("pricepernight");
            entity.Property(e => e.Roomnumber)
                .HasMaxLength(10)
                .HasColumnName("roomnumber");
            entity.Property(e => e.Roomtype)
                .HasMaxLength(50)
                .HasColumnName("roomtype");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Roomamenity>(entity =>
        {
            entity.HasKey(e => e.Roomamenityid).HasName("roomamenity_pkey");

            entity.ToTable("roomamenity");

            entity.Property(e => e.Roomamenityid)
                .ValueGeneratedNever()
                .HasColumnName("roomamenityid");
            entity.Property(e => e.Amenityid).HasColumnName("amenityid");
            entity.Property(e => e.Assigneddate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("assigneddate");
            entity.Property(e => e.Roomid).HasColumnName("roomid");

            entity.HasOne(d => d.Amenity).WithMany(p => p.Roomamenities)
                .HasForeignKey(d => d.Amenityid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("roomamenity_amenityid_fkey");

            entity.HasOne(d => d.Room).WithMany(p => p.Roomamenities)
                .HasForeignKey(d => d.Roomid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("roomamenity_roomid_fkey");
        });

        modelBuilder.Entity<Roomservice>(entity =>
        {
            entity.HasKey(e => e.Serviceid).HasName("roomservice_pkey");

            entity.ToTable("roomservice");

            entity.Property(e => e.Serviceid)
                .ValueGeneratedNever()
                .HasColumnName("serviceid");
            entity.Property(e => e.Employeeid).HasColumnName("employeeid");
            entity.Property(e => e.Roomid).HasColumnName("roomid");
            entity.Property(e => e.Servicedate).HasColumnName("servicedate");
            entity.Property(e => e.Servicedetails).HasColumnName("servicedetails");
            entity.Property(e => e.Servicetype)
                .HasMaxLength(100)
                .HasColumnName("servicetype");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.Employee).WithMany(p => p.Roomservices)
                .HasForeignKey(d => d.Employeeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("roomservice_employeeid_fkey");

            entity.HasOne(d => d.Room).WithMany(p => p.Roomservices)
                .HasForeignKey(d => d.Roomid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("roomservice_roomid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
