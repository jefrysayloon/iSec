using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace iSec.Models
{
    public partial class iSecDBContext : DbContext
    {
        public iSecDBContext(DbContextOptions<iSecDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TextTable> TextTables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TextTable>(entity =>
            {
                entity.ToTable("text_table");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DecryptedText)
                    .IsUnicode(false)
                    .HasColumnName("decrypted_text");

                entity.Property(e => e.EncryptedText)
                    .IsUnicode(false)
                    .HasColumnName("encrypted_text");

                entity.Property(e => e.EnteredText)
                    .IsUnicode(false)
                    .HasColumnName("entered_text");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
