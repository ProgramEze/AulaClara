using AulaClara.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace AulaClara.Infraestructura.Persistencia;

public class ContextoAulaClara : DbContext
{
    public ContextoAulaClara(DbContextOptions<ContextoAulaClara> opciones)
        : base(opciones)
    {
    }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Alumno> Alumnos => Set<Alumno>();
    public DbSet<Materia> Materias => Set<Materia>();
    public DbSet<AlumnoMateria> AlumnoMaterias => Set<AlumnoMateria>();
    public DbSet<Clase> Clases => Set<Clase>();
    public DbSet<MaterialClase> MaterialesClase => Set<MaterialClase>();
    public DbSet<EjercicioClase> EjerciciosClase => Set<EjercicioClase>();
    public DbSet<SeguimientoClase> SeguimientosClase => Set<SeguimientoClase>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigurarUsuario(modelBuilder);
        ConfigurarAlumno(modelBuilder);
        ConfigurarMateria(modelBuilder);
        ConfigurarAlumnoMateria(modelBuilder);
        ConfigurarClase(modelBuilder);
        ConfigurarMaterialClase(modelBuilder);
        ConfigurarEjercicioClase(modelBuilder);
        ConfigurarSeguimientoClase(modelBuilder);
    }

    private static void ConfigurarUsuario(ModelBuilder modelBuilder)
    {
        var entidad = modelBuilder.Entity<Usuario>();

        entidad.ToTable("Usuarios");

        entidad.HasKey(usuario => usuario.Id);

        entidad.Property(usuario => usuario.Nombre)
            .IsRequired()
            .HasMaxLength(120);

        entidad.Property(usuario => usuario.Email)
            .IsRequired()
            .HasMaxLength(180);

        entidad.HasIndex(usuario => usuario.Email)
            .IsUnique();

        entidad.Property(usuario => usuario.HashContrasenia)
            .IsRequired()
            .HasMaxLength(500);

        entidad.Property(usuario => usuario.Activo)
            .IsRequired();
    }

    private static void ConfigurarAlumno(ModelBuilder modelBuilder)
    {
        var entidad = modelBuilder.Entity<Alumno>();

        entidad.ToTable("Alumnos");

        entidad.HasKey(alumno => alumno.Id);

        entidad.Property(alumno => alumno.Nombre)
            .IsRequired()
            .HasMaxLength(120);

        entidad.Property(alumno => alumno.Observaciones)
            .HasMaxLength(1000);

        entidad.Property(alumno => alumno.ResponsableNombre)
            .HasMaxLength(120);

        entidad.Property(alumno => alumno.ResponsableContacto)
            .HasMaxLength(180);

        entidad.HasOne(alumno => alumno.Usuario)
            .WithMany(usuario => usuario.Alumnos)
            .HasForeignKey(alumno => alumno.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigurarMateria(ModelBuilder modelBuilder)
    {
        var entidad = modelBuilder.Entity<Materia>();

        entidad.ToTable("Materias");

        entidad.HasKey(materia => materia.Id);

        entidad.Property(materia => materia.Nombre)
            .IsRequired()
            .HasMaxLength(120);

        entidad.Property(materia => materia.Descripcion)
            .HasMaxLength(500);

        entidad.HasIndex(materia => new { materia.UsuarioId, materia.Nombre })
            .IsUnique();

        entidad.HasOne(materia => materia.Usuario)
            .WithMany(usuario => usuario.Materias)
            .HasForeignKey(materia => materia.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigurarAlumnoMateria(ModelBuilder modelBuilder)
    {
        var entidad = modelBuilder.Entity<AlumnoMateria>();

        entidad.ToTable("AlumnoMaterias");

        entidad.HasKey(alumnoMateria => alumnoMateria.Id);

        entidad.HasIndex(alumnoMateria => new
        {
            alumnoMateria.AlumnoId,
            alumnoMateria.MateriaId
        }).IsUnique();

        entidad.HasOne(alumnoMateria => alumnoMateria.Alumno)
            .WithMany(alumno => alumno.AlumnoMaterias)
            .HasForeignKey(alumnoMateria => alumnoMateria.AlumnoId)
            .OnDelete(DeleteBehavior.Restrict);

        entidad.HasOne(alumnoMateria => alumnoMateria.Materia)
            .WithMany(materia => materia.AlumnoMaterias)
            .HasForeignKey(alumnoMateria => alumnoMateria.MateriaId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigurarClase(ModelBuilder modelBuilder)
    {
        var entidad = modelBuilder.Entity<Clase>();

        entidad.ToTable("Clases");

        entidad.HasKey(clase => clase.Id);

        entidad.Property(clase => clase.Titulo)
            .IsRequired()
            .HasMaxLength(160);

        entidad.Property(clase => clase.Objetivo)
            .IsRequired()
            .HasMaxLength(500);

        entidad.Property(clase => clase.ContenidoTrabajado)
            .HasMaxLength(2000);

        entidad.Property(clase => clase.Observaciones)
            .HasMaxLength(2000);

        entidad.Property(clase => clase.Estado)
            .HasConversion<string>()
            .HasMaxLength(30);

        entidad.HasOne(clase => clase.Usuario)
            .WithMany(usuario => usuario.Clases)
            .HasForeignKey(clase => clase.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        entidad.HasOne(clase => clase.AlumnoMateria)
            .WithMany(alumnoMateria => alumnoMateria.Clases)
            .HasForeignKey(clase => clase.AlumnoMateriaId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigurarMaterialClase(ModelBuilder modelBuilder)
    {
        var entidad = modelBuilder.Entity<MaterialClase>();

        entidad.ToTable("MaterialesClase");

        entidad.HasKey(material => material.Id);

        entidad.Property(material => material.Titulo)
            .IsRequired()
            .HasMaxLength(160);

        entidad.Property(material => material.Tipo)
            .HasConversion<string>()
            .HasMaxLength(30);

        entidad.Property(material => material.Url)
            .HasMaxLength(500);

        entidad.Property(material => material.Descripcion)
            .HasMaxLength(1000);

        entidad.HasOne(material => material.Clase)
            .WithMany(clase => clase.Materiales)
            .HasForeignKey(material => material.ClaseId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void ConfigurarEjercicioClase(ModelBuilder modelBuilder)
    {
        var entidad = modelBuilder.Entity<EjercicioClase>();

        entidad.ToTable("EjerciciosClase");

        entidad.HasKey(ejercicio => ejercicio.Id);

        entidad.Property(ejercicio => ejercicio.Consigna)
            .IsRequired()
            .HasMaxLength(1000);

        entidad.Property(ejercicio => ejercicio.RespuestaEsperada)
            .HasMaxLength(1000);

        entidad.Property(ejercicio => ejercicio.Nivel)
            .HasConversion<string>()
            .HasMaxLength(30);

        entidad.HasOne(ejercicio => ejercicio.Clase)
            .WithMany(clase => clase.Ejercicios)
            .HasForeignKey(ejercicio => ejercicio.ClaseId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void ConfigurarSeguimientoClase(ModelBuilder modelBuilder)
    {
        var entidad = modelBuilder.Entity<SeguimientoClase>();

        entidad.ToTable("SeguimientosClase");

        entidad.HasKey(seguimiento => seguimiento.Id);

        entidad.Property(seguimiento => seguimiento.Participacion)
            .HasMaxLength(1000);

        entidad.Property(seguimiento => seguimiento.Dificultades)
            .HasMaxLength(1000);

        entidad.Property(seguimiento => seguimiento.Avances)
            .HasMaxLength(1000);

        entidad.Property(seguimiento => seguimiento.ProximoPaso)
            .HasMaxLength(1000);

        entidad.HasOne(seguimiento => seguimiento.Clase)
            .WithOne(clase => clase.Seguimiento)
            .HasForeignKey<SeguimientoClase>(seguimiento => seguimiento.ClaseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}