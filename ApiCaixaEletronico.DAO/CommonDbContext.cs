using ApiCaixaEletronico.DTO.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiCaixaEletronico.DAO
{
    public class CommonDbContext : DbContext
    {
        public CommonDbContext(DbContextOptions<CommonDbContext> options) : base(options) { }

        public DbSet<ContaContext> Contas { get; set; }

        public DbSet<CaixaEletronicoContext> Caixas { get; set; }

        public DbSet<ClienteContext> Clientes { get; set; }

        public DbSet<AgenciaContext> Agencias { get; set; }

        public DbSet<BancoContext> Bancos { get; set; }

        public DbSet<TipoContaContext> TiposContas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContaContext>(entity =>
            {
                entity.HasKey(e => new { e.IdConta });
            });

            modelBuilder.Entity<ClienteContext>(entity =>
            {
                entity.HasKey(e => new { e.CpfCli });
            });

            modelBuilder.Entity<AgenciaContext>(entity =>
            {
                entity.HasKey(e => new { e.Id_Agencia });
            });

            modelBuilder.Entity<BancoContext>(entity =>
            {
                entity.HasKey(e => new { e.Id_Banco });
            });

            modelBuilder.Entity<TipoContaContext>(entity =>
            {
                entity.HasKey(e => new { e.Codigo_TipoConta });
            });

            modelBuilder.Entity<CaixaEletronicoContext>(entity =>
            {
                entity.HasKey(e => new { e.Id_Terminal });
            });
        }
    }
}
