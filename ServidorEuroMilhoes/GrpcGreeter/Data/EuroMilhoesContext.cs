using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Servidor.Models;

#nullable disable

namespace Servidor.Data
{
    public partial class EuroMilhoesContext : DbContext
    {
        public EuroMilhoesContext()
        {
        }

        public EuroMilhoesContext(DbContextOptions<EuroMilhoesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApostasArquivada> ApostasArquivadas { get; set; }
        public virtual DbSet<ApostasAtuai> ApostasAtuais { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=EuroMilhoesContext");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ApostasArquivada>(entity =>
            {
                entity.HasKey(e => new { e.Nif, e.DataAposta })
                    .HasName("PK__ApostasA__839469AF50F0464C");

                entity.Property(e => e.Estrelas).IsUnicode(false);

                entity.Property(e => e.Numeros).IsUnicode(false);
            });

            modelBuilder.Entity<ApostasAtuai>(entity =>
            {
                entity.HasKey(e => new { e.Nif, e.DataAposta })
                    .HasName("PK__ApostasA__839469AF90EABA56");

                entity.Property(e => e.DataAposta).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Estrelas).IsUnicode(false);

                entity.Property(e => e.Numeros).IsUnicode(false);

                entity.Property(e => e.SorteioAtual).HasDefaultValueSql("((1))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
