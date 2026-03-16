using NF.Models;
using Microsoft.EntityFrameworkCore;

namespace NF.Data
{
    public class NFContext : DbContext
    {
        public NFContext(DbContextOptions<NFContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Peca> Pecas { get; set; }
        public DbSet<OrdemServico> OrdemServicos { get; set; }
        public DbSet<OrdemServico_Peca> OrdemServico_Pecas { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            // Chave composta
            mb.Entity<OrdemServico_Peca>()
                .HasKey(x => new { x.IdOs, x.IdPeca });

            // Relacionamentos OrdemServico_Peca
            mb.Entity<OrdemServico_Peca>()
                .HasOne(x => x.OrdemServico)
                .WithMany(o => o.OrdemServico_Pecas)
                .HasForeignKey(x => x.IdOs);

            mb.Entity<OrdemServico_Peca>()
                .HasOne(x => x.Peca)
                .WithMany(p => p.OrdemServico_Pecas)
                .HasForeignKey(x => x.IdPeca);

            // Relacionamentos Cliente
            mb.Entity<Veiculo>()
                .HasOne(v => v.Cliente)
                .WithMany(c => c.Veiculos)
                .HasForeignKey(v => v.IdCliente);

            mb.Entity<OrdemServico>()
                .HasOne(o => o.Cliente)
                .WithMany(c => c.OrdemServicos)
                .HasForeignKey(o => o.IdCliente)
                .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<OrdemServico>()
                .HasOne(o => o.Veiculo)
                .WithMany(v => v.OrdemServicos)
                .HasForeignKey(o => o.IdVeiculo)
               .OnDelete(DeleteBehavior.Restrict);

            // Índices únicos
            mb.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            mb.Entity<Cliente>()
                .HasIndex(c => c.CNPJ)
                .IsUnique();
        }
    }
}