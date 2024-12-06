﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityFramework;

public partial class FitnessCenterDbContext : DbContext
{
    public FitnessCenterDbContext()
    {
    }

    public FitnessCenterDbContext(DbContextOptions<FitnessCenterDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cita> Citas { get; set; }

    public virtual DbSet<Clase> Clases { get; set; }

    public virtual DbSet<Cupone> Cupones { get; set; }

    public virtual DbSet<Descuento> Descuentos { get; set; }

    public virtual DbSet<DetalleEntrenamiento> DetalleEntrenamientos { get; set; }

    public virtual DbSet<DuracionCita> DuracionCitas { get; set; }

    public virtual DbSet<Ejercicio> Ejercicios { get; set; }

    public virtual DbSet<EjercicioEquipo> EjercicioEquipos { get; set; }

    public virtual DbSet<Equipo> Equipos { get; set; }

    public virtual DbSet<InscripcionClase> InscripcionClases { get; set; }

    public virtual DbSet<Medida> Medidas { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<RegistrosEntrenamiento> RegistrosEntrenamientos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Rutina> Rutinas { get; set; }

    public virtual DbSet<RutinaEjercicio> RutinaEjercicios { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<DTO.Dtos.Ejercicio> ejercicioSP { get; set; }
    public virtual DbSet<DTO.Dtos.Rutina> rutinaSP { get; set; }
    public virtual DbSet<DTO.Dtos.QueryUser> userSP { get; set; }
    public virtual DbSet<DTO.Dtos.Cita> citaSP { get; set; }
    public virtual DbSet<DTO.Dtos.ClientesProximosACita> correosSP { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-UAHO3RF\\SQLEXPRESS;Database=FitnessCenterDB;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cita>(entity =>
        {
            entity.HasKey(e => e.CitaId).HasName("PK__Citas__F0E2D9F2556E9C13");

            entity.Property(e => e.CitaId).HasColumnName("CitaID");
            entity.Property(e => e.EntrenadorId).HasColumnName("EntrenadorID");
            entity.Property(e => e.Estado).HasMaxLength(50);
            entity.Property(e => e.FechaHora).HasColumnType("datetime");
            entity.Property(e => e.FechaHoraFin).HasColumnType("datetime");
            entity.Property(e => e.TipoCita).HasMaxLength(50);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Entrenador).WithMany(p => p.CitaEntrenadors)
                .HasForeignKey(d => d.EntrenadorId)
                .HasConstraintName("FK__Citas__Entrenado__44FF419A");

            entity.HasOne(d => d.Usuario).WithMany(p => p.CitaUsuarios)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Citas__UsuarioID__440B1D61");
        });

        modelBuilder.Entity<Clase>(entity =>
        {
            entity.HasKey(e => e.ClaseId).HasName("PK__Clases__F54296B30E7E66FD");

            entity.Property(e => e.ClaseId).HasColumnName("ClaseID");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Horario).HasColumnType("datetime");
            entity.Property(e => e.InstructorId).HasColumnName("InstructorID");
            entity.Property(e => e.NombreClase).HasMaxLength(100);

            entity.HasOne(d => d.Instructor).WithMany(p => p.Clases)
                .HasForeignKey(d => d.InstructorId)
                .HasConstraintName("FK__Clases__Instruct__5AEE82B9");
        });

        modelBuilder.Entity<Cupone>(entity =>
        {
            entity.HasKey(e => e.CuponId).HasName("PK__Cupones__C43568B78A3A377F");

            entity.Property(e => e.CuponId).HasColumnName("CuponID");
            entity.Property(e => e.Codigo).HasMaxLength(50);
            entity.Property(e => e.DescuentoId).HasColumnName("DescuentoID");
            entity.Property(e => e.Estado).HasMaxLength(50);
            entity.Property(e => e.FechaExpiracion).HasColumnType("datetime");

            entity.HasOne(d => d.Descuento).WithMany(p => p.Cupones)
                .HasForeignKey(d => d.DescuentoId)
                .HasConstraintName("FK__Cupones__Descuen__6B24EA82");
        });

        modelBuilder.Entity<Descuento>(entity =>
        {
            entity.HasKey(e => e.DescuentoId).HasName("PK__Descuent__0C2C1138689BA8C4");

            entity.Property(e => e.DescuentoId).HasColumnName("DescuentoID");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.FechaFin).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.Porcentaje).HasColumnType("decimal(5, 2)");
        });

        modelBuilder.Entity<DetalleEntrenamiento>(entity =>
        {
            entity.HasKey(e => e.DetalleId).HasName("PK__DetalleE__6E19D6FA406B23BB");

            entity.ToTable("DetalleEntrenamiento");

            entity.Property(e => e.DetalleId).HasColumnName("DetalleID");
            entity.Property(e => e.EjercicioId).HasColumnName("EjercicioID");
            entity.Property(e => e.Peso).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.RegistroId).HasColumnName("RegistroID");

            entity.HasOne(d => d.Ejercicio).WithMany(p => p.DetalleEntrenamientos)
                .HasForeignKey(d => d.EjercicioId)
                .HasConstraintName("FK__DetalleEn__Ejerc__74AE54BC");

            entity.HasOne(d => d.Registro).WithMany(p => p.DetalleEntrenamientos)
                .HasForeignKey(d => d.RegistroId)
                .HasConstraintName("FK__DetalleEn__Regis__73BA3083");
        });

        modelBuilder.Entity<DuracionCita>(entity =>
        {
            entity.HasKey(e => e.DuracionId).HasName("PK__duracion__3B2C07DA2413E2C8");

            entity.ToTable("duracionCitas");

            entity.Property(e => e.DuracionId).HasColumnName("DuracionID");
        });

        modelBuilder.Entity<Ejercicio>(entity =>
        {
            entity.HasKey(e => e.EjercicioId).HasName("PK__Ejercici__812226A1579B3B09");

            entity.Property(e => e.EjercicioId).HasColumnName("EjercicioID");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.NombreEjercicio).HasMaxLength(100);
            entity.Property(e => e.TipoEjercicio).HasMaxLength(50);
        });

        modelBuilder.Entity<EjercicioEquipo>(entity =>
        {
            entity.HasKey(e => e.EjercicioEquipoId).HasName("PK__Ejercici__A8CC48FDB85B7151");

            entity.Property(e => e.EjercicioEquipoId).HasColumnName("EjercicioEquipoID");
            entity.Property(e => e.EjercicioId).HasColumnName("EjercicioID");
            entity.Property(e => e.EquipoId).HasColumnName("EquipoID");

            entity.HasOne(d => d.Ejercicio).WithMany(p => p.EjercicioEquipos)
                .HasForeignKey(d => d.EjercicioId)
                .HasConstraintName("FK__Ejercicio__Ejerc__571DF1D5");

            entity.HasOne(d => d.Equipo).WithMany(p => p.EjercicioEquipos)
                .HasForeignKey(d => d.EquipoId)
                .HasConstraintName("FK__Ejercicio__Equip__5812160E");
        });

        modelBuilder.Entity<Equipo>(entity =>
        {
            entity.HasKey(e => e.EquipoId).HasName("PK__Equipos__DE8A0BFFA22BEA11");

            entity.Property(e => e.EquipoId).HasColumnName("EquipoID");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.NombreEquipo).HasMaxLength(100);
        });

        modelBuilder.Entity<InscripcionClase>(entity =>
        {
            entity.HasKey(e => e.InscripcionId).HasName("PK__Inscripc__168316991F67D233");

            entity.Property(e => e.InscripcionId).HasColumnName("InscripcionID");
            entity.Property(e => e.ClaseId).HasColumnName("ClaseID");
            entity.Property(e => e.Estado).HasMaxLength(50);
            entity.Property(e => e.FechaInscripcion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Clase).WithMany(p => p.InscripcionClases)
                .HasForeignKey(d => d.ClaseId)
                .HasConstraintName("FK__Inscripci__Clase__5DCAEF64");

            entity.HasOne(d => d.Usuario).WithMany(p => p.InscripcionClases)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Inscripci__Usuar__5EBF139D");
        });

        modelBuilder.Entity<Medida>(entity =>
        {
            entity.HasKey(e => e.MedidaId).HasName("PK__medidas__1C299EFA6221D68B");

            entity.ToTable("medidas");

            entity.Property(e => e.MedidaId).HasColumnName("medidaId");
            entity.Property(e => e.Altura)
                .HasColumnType("decimal(5, 1)")
                .HasColumnName("altura");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.FkUsuario).HasColumnName("fkUsuario");
            entity.Property(e => e.Genero)
                .HasMaxLength(100)
                .HasColumnName("genero");
            entity.Property(e => e.Imc)
                .HasColumnType("decimal(5, 1)")
                .HasColumnName("imc");
            entity.Property(e => e.Peso)
                .HasColumnType("decimal(5, 1)")
                .HasColumnName("peso");

            entity.HasOne(d => d.FkUsuarioNavigation).WithMany(p => p.Medida)
                .HasForeignKey(d => d.FkUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioMedida");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.PagoId).HasName("PK__Pagos__F00B6158C2211D16");

            entity.Property(e => e.PagoId).HasColumnName("PagoID");
            entity.Property(e => e.Estado).HasMaxLength(50);
            entity.Property(e => e.FechaPago)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MetodoPago).HasMaxLength(50);
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Pagos__UsuarioID__6383C8BA");
        });

        modelBuilder.Entity<RegistrosEntrenamiento>(entity =>
        {
            entity.HasKey(e => e.RegistroId).HasName("PK__Registro__B897313EAB9C2539");

            entity.ToTable("RegistrosEntrenamiento");

            entity.Property(e => e.RegistroId).HasColumnName("RegistroID");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RutinaId).HasColumnName("RutinaID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Rutina).WithMany(p => p.RegistrosEntrenamientos)
                .HasForeignKey(d => d.RutinaId)
                .HasConstraintName("FK__Registros__Rutin__6FE99F9F");

            entity.HasOne(d => d.Usuario).WithMany(p => p.RegistrosEntrenamientos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Registros__Usuar__6EF57B66");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__Roles__F92302D1C65ABB7F");

            entity.Property(e => e.RolId).HasColumnName("RolID");
            entity.Property(e => e.NombreRol).HasMaxLength(50);
        });

        modelBuilder.Entity<Rutina>(entity =>
        {
            entity.HasKey(e => e.RutinaId).HasName("PK__Rutinas__E76E165A11753A49");

            entity.Property(e => e.RutinaId).HasColumnName("RutinaID");
            entity.Property(e => e.EntrenadorId).HasColumnName("EntrenadorID");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Observaciones).HasMaxLength(255);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Entrenador).WithMany(p => p.RutinaEntrenadors)
                .HasForeignKey(d => d.EntrenadorId)
                .HasConstraintName("FK__Rutinas__Entrena__4AB81AF0");

            entity.HasOne(d => d.Usuario).WithMany(p => p.RutinaUsuarios)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Rutinas__Usuario__49C3F6B7");
        });

        modelBuilder.Entity<RutinaEjercicio>(entity =>
        {
            entity.HasKey(e => e.RutinaEjercicioId).HasName("PK__RutinaEj__56420023FA3B532E");

            entity.Property(e => e.RutinaEjercicioId).HasColumnName("RutinaEjercicioID");
            entity.Property(e => e.EjercicioId).HasColumnName("EjercicioID");
            entity.Property(e => e.Peso).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.RutinaId).HasColumnName("RutinaID");

            entity.HasOne(d => d.Ejercicio).WithMany(p => p.RutinaEjercicios)
                .HasForeignKey(d => d.EjercicioId)
                .HasConstraintName("FK__RutinaEje__Ejerc__52593CB8");

            entity.HasOne(d => d.Rutina).WithMany(p => p.RutinaEjercicios)
                .HasForeignKey(d => d.RutinaId)
                .HasConstraintName("FK__RutinaEje__Rutin__5165187F");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.ServicioId).HasName("PK__Servicio__D5AEEC221B5A2D82");

            entity.Property(e => e.ServicioId).HasColumnName("ServicioID");
            entity.Property(e => e.Costo).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.NombreServicio).HasMaxLength(100);
            entity.Property(e => e.TipoServicio).HasMaxLength(50);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE7981B26D7DC");

            entity.HasIndex(e => e.Telefono, "UQ__Usuarios__4EC5048011B00D5E").IsUnique();

            entity.HasIndex(e => e.NombreUsuario, "UQ__Usuarios__6B0F5AE04376F638").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D1053404A4BEF2").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Contrasena).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Estado).HasMaxLength(50);
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.NombreUsuario).HasMaxLength(50);
            entity.Property(e => e.RolId).HasColumnName("RolID");
            entity.Property(e => e.Telefono).HasMaxLength(15);

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("FK__Usuarios__RolID__3D5E1FD2");
        });

        modelBuilder.Entity<DTO.Dtos.Rutina>(entity => {
            entity.HasNoKey();
            entity.Property(e => e.RutinaId).HasColumnName("RutinaId");
            entity.Property(e => e.UsuarioID).HasColumnName("UsuarioID");
            entity.Property(e => e.EntrenadorID).HasColumnName("EntrenadorID");
            entity.Property(e => e.FechaCreacion).HasColumnName("FechaCreacion");
            entity.Property(e => e.Observaciones).HasColumnName("Observaciones");
        });

        modelBuilder.Entity<DTO.Dtos.QueryUser>(entity => {
            entity.HasNoKey();
            entity.Property(e => e.UsuarioID).HasColumnName("UsuarioID");
            entity.Property(e => e.Nombre).HasColumnName("Nombre");
            entity.Property(e => e.Apellido).HasColumnName("Apellido");
            entity.Property(e => e.Email).HasColumnName("Email");
            entity.Property(e => e.Telefono).HasColumnName("Telefono");
            entity.Property(e => e.FechaNacimiento).HasColumnName("FechaNacimiento");
            entity.Property(e => e.FechaRegistro).HasColumnName("FechaRegistro");
        });
        modelBuilder.Entity<DTO.Dtos.Ejercicio>(entity =>
        {
            entity.HasNoKey();
            entity.Property(e => e.EjercicioID).HasColumnName("EjercicioID");
            entity.Property(e => e.NombreEjercicio).HasColumnName("NombreEjercicio");
            entity.Property(e => e.TipoEjercicio).HasColumnName("TipoEjercicio");
            entity.Property(e => e.Descripcion).HasColumnName("Descripcion");
        });

        modelBuilder.Entity<DTO.Dtos.Cita>(entity =>
        {
            entity.HasNoKey();
            entity.Property(e => e.Id).HasColumnName("CitaID");
            entity.Property(e => e.fkCliente).HasColumnName("UsuarioID");
            entity.Property(e => e.fkEntrenador).HasColumnName("EntrenadorID");
            entity.Property(e => e.fechaInicioCita).HasColumnName("FechaHora");
            entity.Property(e => e.fechaFinCita).HasColumnName("FechaHoraFin");
            entity.Property(e => e.tipoCita).HasColumnName("TipoCita");
            entity.Property(e => e.estado).HasColumnName("Estado");
        });


        modelBuilder.Entity<DTO.Dtos.ClientesProximosACita>(entity =>
        {
            entity.HasNoKey();
            entity.Property(e => e.correo).HasColumnName("Email");
            entity.Property(e => e.fechaCita).HasColumnName("FechaHora");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
