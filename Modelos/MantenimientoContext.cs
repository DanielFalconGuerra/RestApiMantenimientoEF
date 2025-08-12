using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace RestApiMantenimientoEF.Modelos;

public partial class MantenimientoContext : DbContext
{
    public MantenimientoContext()
    {
    }

    public MantenimientoContext(DbContextOptions<MantenimientoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Areasreporte> Areasreportes { get; set; }

    public virtual DbSet<Areassupport> Areassupports { get; set; }

    public virtual DbSet<Carro> Carros { get; set; }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<Despacha> Despachas { get; set; }

    public virtual DbSet<Detallecolaboradore> Detallecolaboradores { get; set; }

    public virtual DbSet<Detalleestado> Detalleestados { get; set; }

    public virtual DbSet<Detalleocurrencium> Detalleocurrencia { get; set; }

    public virtual DbSet<Equipo> Equipos { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Evento> Eventos { get; set; }

    public virtual DbSet<Problema> Problemas { get; set; }

    public virtual DbSet<Problemaequipo> Problemaequipos { get; set; }

    public virtual DbSet<Problemasareareporte> Problemasareareportes { get; set; }

    public virtual DbSet<Solicitudtrabajo> Solicitudtrabajos { get; set; }

    public virtual DbSet<Ubicacione> Ubicaciones { get; set; }

    public virtual DbSet<Ubicacionsolicitud> Ubicacionsolicituds { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=mantenimiento_v2;port=3307;user=root;password=__Wzd__", Microsoft.EntityFrameworkCore.ServerVersion.Parse("11.7.2-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_uca1400_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Areasreporte>(entity =>
        {
            entity.HasKey(e => e.IdAreaR).HasName("PRIMARY");

            entity
                .ToTable("areasreportes")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.IdAreaR).HasColumnType("int(11)");
            entity.Property(e => e.NombreArea).HasMaxLength(100);
        });

        modelBuilder.Entity<Areassupport>(entity =>
        {
            entity.HasKey(e => e.IdAreaS).HasName("PRIMARY");

            entity
                .ToTable("areassupports")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.IdAreaS).HasColumnType("int(11)");
            entity.Property(e => e.NombreAreaS).HasMaxLength(100);
        });

        modelBuilder.Entity<Carro>(entity =>
        {
            entity.HasKey(e => e.IdCarro).HasName("PRIMARY");

            entity
                .ToTable("carro")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.IdCarro).HasColumnType("int(11)");
            entity.Property(e => e.Letra).HasMaxLength(5);
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.HasKey(e => e.IdColor).HasName("PRIMARY");

            entity
                .ToTable("color")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.IdColor).HasColumnType("int(11)");
            entity.Property(e => e.Color1)
                .HasMaxLength(45)
                .HasColumnName("Color");
            entity.Property(e => e.Dia).HasMaxLength(20);
        });

        modelBuilder.Entity<Despacha>(entity =>
        {
            entity.HasKey(e => e.IdDespacha).HasName("PRIMARY");

            entity
                .ToTable("despacha")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.IdCarro, "despacha_FK");

            entity.HasIndex(e => e.IdSolicitudTrabajo, "despacha_FK_1");

            entity.HasIndex(e => e.IdColor, "despacha_FK_2");

            entity.Property(e => e.IdDespacha).HasColumnType("int(11)");
            entity.Property(e => e.FechaDespacho).HasColumnType("datetime");
            entity.Property(e => e.IdCarro).HasColumnType("int(11)");
            entity.Property(e => e.IdColor).HasColumnType("int(11)");
            entity.Property(e => e.IdSolicitudTrabajo).HasColumnType("int(11)");

            entity.HasOne(d => d.IdCarroNavigation).WithMany(p => p.Despachas)
                .HasForeignKey(d => d.IdCarro)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("despacha_FK");

            entity.HasOne(d => d.IdColorNavigation).WithMany(p => p.Despachas)
                .HasForeignKey(d => d.IdColor)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("despacha_FK_2");

            entity.HasOne(d => d.IdSolicitudTrabajoNavigation).WithMany(p => p.Despachas)
                .HasForeignKey(d => d.IdSolicitudTrabajo)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("despacha_FK_1");
        });

        modelBuilder.Entity<Detallecolaboradore>(entity =>
        {
            entity.HasKey(e => e.IdDetalleC).HasName("PRIMARY");

            entity
                .ToTable("detallecolaboradores")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.IdEvento, "FK_DetalleColaboradores_Evento");

            entity.HasIndex(e => e.IdColaborador, "FK_DetalleColaboradores_User");

            entity.HasIndex(e => e.IdAsignador, "detallecolaboradores_FK");

            entity.Property(e => e.IdDetalleC).HasColumnType("int(11)");
            entity.Property(e => e.Accion).HasMaxLength(100);
            entity.Property(e => e.IdAsignador).HasColumnType("int(11)");
            entity.Property(e => e.IdColaborador).HasColumnType("int(11)");
            entity.Property(e => e.IdEvento).HasColumnType("int(11)");

            entity.HasOne(d => d.IdColaboradorNavigation).WithMany(p => p.Detallecolaboradores)
                .HasForeignKey(d => d.IdColaborador)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_DetalleColaboradores_User");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.Detallecolaboradores)
                .HasForeignKey(d => d.IdEvento)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_DetalleColaboradores_Evento");
        });

        modelBuilder.Entity<Detalleestado>(entity =>
        {
            entity.HasKey(e => e.IdDetalleEstado).HasName("PRIMARY");

            entity
                .ToTable("detalleestados")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.IdEstado, "FK_DetalleEstados_Estado");

            entity.HasIndex(e => e.IdEvento, "FK_DetalleEstados_Evento");

            entity.Property(e => e.IdDetalleEstado).HasColumnType("int(11)");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime");
            entity.Property(e => e.IdEstado).HasColumnType("int(11)");
            entity.Property(e => e.IdEvento).HasColumnType("int(11)");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Detalleestados)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_DetalleEstados_Estado");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.Detalleestados)
                .HasForeignKey(d => d.IdEvento)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_DetalleEstados_Evento");
        });

        modelBuilder.Entity<Detalleocurrencium>(entity =>
        {
            entity.HasKey(e => e.IdOcurrencia).HasName("PRIMARY");

            entity
                .ToTable("detalleocurrencia")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.IdProblemaEquipo, "detalleocurrencia_FK");

            entity.Property(e => e.IdOcurrencia).HasColumnType("int(11)");
            entity.Property(e => e.FactorDeUrgencia)
                .HasColumnType("int(11)")
                .HasColumnName("Factor_de_Urgencia");
            entity.Property(e => e.IdProblemaEquipo).HasColumnType("int(11)");
            entity.Property(e => e.Ocurrencia).HasColumnType("int(11)");

            entity.HasOne(d => d.IdProblemaEquipoNavigation).WithMany(p => p.Detalleocurrencia)
                .HasForeignKey(d => d.IdProblemaEquipo)
                .HasConstraintName("detalleocurrencia_FK");
        });

        modelBuilder.Entity<Equipo>(entity =>
        {
            entity.HasKey(e => e.IdEquipo).HasName("PRIMARY");

            entity
                .ToTable("equipos")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.IdEquipo).HasColumnType("int(11)");
            entity.Property(e => e.NombreEquipo).HasMaxLength(100);
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PRIMARY");

            entity
                .ToTable("estados")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.IdEstado).HasColumnType("int(11)");
            entity.Property(e => e.NombreEstado).HasMaxLength(100);
        });

        modelBuilder.Entity<Evento>(entity =>
        {
            entity.HasKey(e => e.IdEvento).HasName("PRIMARY");

            entity
                .ToTable("eventos")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.IdProblemasEquipos, "FK_Eventos_Problema");

            entity.HasIndex(e => e.IdUbicacion, "eventos_FK");

            entity.Property(e => e.IdEvento).HasColumnType("int(11)");
            entity.Property(e => e.Activo).HasDefaultValueSql("'1'");
            entity.Property(e => e.ComentariosFinales).HasColumnType("text");
            entity.Property(e => e.Disabled)
                .HasDefaultValueSql("'0'")
                .HasColumnType("int(11)")
                .HasColumnName("disabled");
            entity.Property(e => e.Enlace)
                .HasMaxLength(300)
                .HasColumnName("enlace");
            entity.Property(e => e.EnlaceMantenimiento)
                .HasMaxLength(200)
                .HasColumnName("enlace_mantenimiento");
            entity.Property(e => e.FechaReporte).HasColumnType("datetime");
            entity.Property(e => e.FechaResolucion).HasColumnType("datetime");
            entity.Property(e => e.IdProblemasEquipos).HasColumnType("int(11)");
            entity.Property(e => e.IdUbicacion).HasColumnType("int(11)");
            entity.Property(e => e.Observacion).HasColumnType("text");

            entity.HasOne(d => d.IdProblemasEquiposNavigation).WithMany(p => p.Eventos)
                .HasForeignKey(d => d.IdProblemasEquipos)
                .HasConstraintName("eventos_FK_1");

            entity.HasOne(d => d.IdUbicacionNavigation).WithMany(p => p.Eventos)
                .HasForeignKey(d => d.IdUbicacion)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("eventos_FK");
        });

        modelBuilder.Entity<Problema>(entity =>
        {
            entity.HasKey(e => e.IdProblema).HasName("PRIMARY");

            entity
                .ToTable("problemas")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.IdAreaS, "FK_Problemas_AreasSupport");

            entity.HasIndex(e => e.IdAreaR, "problemas_FK");

            entity.Property(e => e.IdProblema).HasColumnType("int(11)");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.IdAreaR).HasColumnType("int(11)");
            entity.Property(e => e.IdAreaS).HasColumnType("int(11)");
            entity.Property(e => e.NombreProblema).HasMaxLength(100);

            entity.HasOne(d => d.IdAreaRNavigation).WithMany(p => p.Problemas)
                .HasForeignKey(d => d.IdAreaR)
                .HasConstraintName("problemas_FK");

            entity.HasOne(d => d.IdAreaSNavigation).WithMany(p => p.Problemas)
                .HasForeignKey(d => d.IdAreaS)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Problemas_AreasSupport");
        });

        modelBuilder.Entity<Problemaequipo>(entity =>
        {
            entity.HasKey(e => e.IdProblemaequipo).HasName("PRIMARY");

            entity
                .ToTable("problemaequipos")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.IdProblema, "problemaequipos_FK");

            entity.HasIndex(e => e.IdEquipos, "problemaequipos_FK_1");

            entity.Property(e => e.IdProblemaequipo)
                .HasColumnType("int(11)")
                .HasColumnName("Id_problemaequipo");
            entity.Property(e => e.Detectabilidad).HasColumnType("int(11)");
            entity.Property(e => e.IdEquipos).HasColumnType("int(11)");
            entity.Property(e => e.IdProblema).HasColumnType("int(11)");
            entity.Property(e => e.Severidad).HasColumnType("int(11)");

            entity.HasOne(d => d.IdEquiposNavigation).WithMany(p => p.Problemaequipos)
                .HasForeignKey(d => d.IdEquipos)
                .HasConstraintName("problemaequipos_FK_1");

            entity.HasOne(d => d.IdProblemaNavigation).WithMany(p => p.Problemaequipos)
                .HasForeignKey(d => d.IdProblema)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("problemaequipos_FK");
        });

        modelBuilder.Entity<Problemasareareporte>(entity =>
        {
            entity.HasKey(e => e.IdProblemasareareporte).HasName("PRIMARY");

            entity
                .ToTable("problemasareareporte")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.IdProblema, "problemasareareporte_FK");

            entity.HasIndex(e => e.IdAreaR, "problemasareareporte_FK_1");

            entity.Property(e => e.IdProblemasareareporte)
                .HasColumnType("int(11)")
                .HasColumnName("Id_problemasareareporte");
            entity.Property(e => e.IdAreaR).HasColumnType("int(11)");
            entity.Property(e => e.IdProblema).HasColumnType("int(11)");
        });

        modelBuilder.Entity<Solicitudtrabajo>(entity =>
        {
            entity.HasKey(e => e.IdSolicitudTrabajo).HasName("PRIMARY");

            entity
                .ToTable("solicitudtrabajo")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.IdUbicacionSolicitud, "solicitudtrabajo_FK");

            entity.HasIndex(e => e.IdProblema, "solicitudtrabajo_FK_1");

            entity.HasIndex(e => e.Solicita, "solicitudtrabajo_FK_2");

            entity.Property(e => e.IdSolicitudTrabajo).HasColumnType("int(11)");
            entity.Property(e => e.Estatus).HasColumnType("int(11)");
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.IdProblema).HasColumnType("int(11)");
            entity.Property(e => e.IdUbicacionSolicitud).HasColumnType("int(11)");
            entity.Property(e => e.Solicita).HasColumnType("int(11)");

            entity.HasOne(d => d.IdProblemaNavigation).WithMany(p => p.Solicitudtrabajos)
                .HasForeignKey(d => d.IdProblema)
                .HasConstraintName("solicitudtrabajo_FK_1");

            entity.HasOne(d => d.IdUbicacionSolicitudNavigation).WithMany(p => p.Solicitudtrabajos)
                .HasForeignKey(d => d.IdUbicacionSolicitud)
                .HasConstraintName("solicitudtrabajo_FK");

            entity.HasOne(d => d.SolicitaNavigation).WithMany(p => p.Solicitudtrabajos)
                .HasForeignKey(d => d.Solicita)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("solicitudtrabajo_FK_2");
        });

        modelBuilder.Entity<Ubicacione>(entity =>
        {
            entity.HasKey(e => e.IdUbicacion).HasName("PRIMARY");

            entity
                .ToTable("ubicaciones")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.IdAreaR, "ubicaciones_FK");

            entity.Property(e => e.IdUbicacion).HasColumnType("int(11)");
            entity.Property(e => e.IdAreaR).HasColumnType("int(11)");
            entity.Property(e => e.NombreUbicacion).HasMaxLength(100);

            entity.HasOne(d => d.IdAreaRNavigation).WithMany(p => p.Ubicaciones)
                .HasForeignKey(d => d.IdAreaR)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ubicaciones_FK");
        });

        modelBuilder.Entity<Ubicacionsolicitud>(entity =>
        {
            entity.HasKey(e => e.IdUbicacionSolicitud).HasName("PRIMARY");

            entity
                .ToTable("ubicacionsolicitud")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.IdUbicacion, "ubicacionsolicitud_FK");

            entity.Property(e => e.IdUbicacionSolicitud).HasColumnType("int(11)");
            entity.Property(e => e.Celula).HasMaxLength(45);
            entity.Property(e => e.IdUbicacion).HasColumnType("int(11)");

            entity.HasOne(d => d.IdUbicacionNavigation).WithMany(p => p.Ubicacionsolicituds)
                .HasForeignKey(d => d.IdUbicacion)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ubicacionsolicitud_FK");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.NoColaborador).HasName("PRIMARY");

            entity
                .ToTable("users")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.IdAreaS, "FK_Users_AreasSupport");

            entity.Property(e => e.NoColaborador)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.ApellidoM).HasMaxLength(100);
            entity.Property(e => e.ApellidoP).HasMaxLength(100);
            entity.Property(e => e.Disponible).HasColumnType("text");
            entity.Property(e => e.IdAreaS).HasColumnType("int(11)");
            entity.Property(e => e.NoSupervisor).HasColumnType("int(11)");
            entity.Property(e => e.Nombre).HasMaxLength(100);

            entity.HasOne(d => d.IdAreaSNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdAreaS)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Users_AreasSupport");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
