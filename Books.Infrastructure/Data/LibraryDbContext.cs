using Books.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Data;

public class LibraryDbContext:DbContext
{
    public DbSet<BookEntity> Books { get; set; }
    public DbSet<AuthorEntity> Authors { get; set; }
    public DbSet<GenreEntity> Genres { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options):base(options)
    {
        
    }
    public DbSet<BookAuthorEntity> BookAuthors { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        if (Database.IsMySql())
        {
            modelBuilder.Entity<BookEntity>()
                .Property(b => b.CreatedAt)
                .HasColumnType("datetime(6)")
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .ValueGeneratedOnAdd();
        }
        else if (Database.IsSqlServer())
        {
            modelBuilder.Entity<BookEntity>()
                .Property(b => b.CreatedAt)
                .HasDefaultValueSql("SYSDATETIME()");
        }

        modelBuilder.Entity<UserEntity>().HasIndex(u => u.Email).IsUnique();

        
        modelBuilder.Entity<BookAuthorEntity>()
            .HasKey(ba => new { ba.BookId, ba.AuthorId });

        modelBuilder.Entity<BookAuthorEntity>()
            .HasOne(ba => ba.Book)
            .WithMany(b => b.BookAuthors)
            .HasForeignKey(ba => ba.BookId);

        modelBuilder.Entity<BookAuthorEntity>()
            .HasOne(ba => ba.Author)
            .WithMany(a => a.BookAuthors)
            .HasForeignKey(ba => ba.AuthorId);
    }
}
