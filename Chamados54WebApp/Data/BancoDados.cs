using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Chamados54WebApp.Data
{
    public class BancoDados : DbContext
    {
        //Mapeamento das tabelas do banco de dados
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Tecnico> Tecnicos { get; set; }        
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Chamado> Chamados { get; set; }
        public DbSet<ChamadoServico> ChamadosServicos { get; set; }

        public BancoDados()
        {            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //configuração do banco de dados
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=Chamados54Bd;Integrated Security=True;TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //desabilita a exclusão em cascata
            foreach (var relacionamento in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relacionamento.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(BancoDados).Assembly);
        }

    }
}
