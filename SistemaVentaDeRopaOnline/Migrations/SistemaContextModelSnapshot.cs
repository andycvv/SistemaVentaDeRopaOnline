﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SistemaVentaDeRopaOnline.Data;

#nullable disable

namespace SistemaVentaDeRopaOnline.Migrations
{
    [DbContext(typeof(SistemaContext))]
    partial class SistemaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("SistemaVentaDeRopaOnline.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("SistemaVentaDeRopaOnline.Models.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Colores");
                });

            modelBuilder.Entity("SistemaVentaDeRopaOnline.Models.DetallePedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("InventarioId")
                        .HasColumnType("int");

                    b.Property<int>("PedidoId")
                        .HasColumnType("int");

                    b.Property<double>("Precio")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("InventarioId");

                    b.HasIndex("PedidoId");

                    b.ToTable("DetallePedidos");
                });

            modelBuilder.Entity("SistemaVentaDeRopaOnline.Models.Imagen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("ProductoId");

                    b.ToTable("Imagenes");
                });

            modelBuilder.Entity("SistemaVentaDeRopaOnline.Models.Inventario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ColorId")
                        .HasColumnType("int");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<int>("TallaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ColorId");

                    b.HasIndex("ProductoId");

                    b.HasIndex("TallaId");

                    b.ToTable("Inventarios");
                });

            modelBuilder.Entity("SistemaVentaDeRopaOnline.Models.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.Property<string>("UsuarioId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("VentaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("SistemaVentaDeRopaOnline.Models.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double>("Precio")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("SistemaVentaDeRopaOnline.Models.Talla", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Tallas");
                });

            modelBuilder.Entity("SistemaVentaDeRopaOnline.Models.Usuario", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("SistemaVentaDeRopaOnline.Models.Venta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("MetodoPago")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PedidoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId")
                        .IsUnique();

                    b.ToTable("Ventas");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SistemaVentaDeRopaOnline.Models.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SistemaVentaDeRopaOnline.Models.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaVentaDeRopaOnline.Models.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SistemaVentaDeRopaOnline.Models.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SistemaVentaDeRopaOnline.Models.DetallePedido", b =>
                {
                    b.HasOne("SistemaVentaDeRopaOnline.Models.Inventario", "Inventario")
                        .WithMany("DetallePedidos")
                        .HasForeignKey("InventarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaVentaDeRopaOnline.Models.Pedido", "Pedido")
                        .WithMany("DetallePedidos")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inventario");

                    b.Navigation("Pedido");
                });

            modelBuilder.Entity("SistemaVentaDeRopaOnline.Models.Imagen", b =>
                {
                    b.HasOne("SistemaVentaDeRopaOnline.Models.Producto", "Producto")
                        .WithMany("ImagenProductos")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("SistemaVentaDeRopaOnline.Models.Inventario", b =>
                {
                    b.HasOne("SistemaVentaDeRopaOnline.Models.Color", "Color")
                        .WithMany("Inventarios")
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaVentaDeRopaOnline.Models.Producto", "Producto")
                        .WithMany("Inventarios")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaVentaDeRopaOnline.Models.Talla", "Talla")
                        .WithMany("Inventarios")
                        .HasForeignKey("TallaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Color");

                    b.Navigation("Producto");

                    b.Navigation("Talla");
                });

            modelBuilder.Entity("SistemaVentaDeRopaOnline.Models.Pedido", b =>
                {
                    b.HasOne("SistemaVentaDeRopaOnline.Models.Usuario", "Usuario")
                        .WithMany("Pedidos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("SistemaVentaDeRopaOnline.Models.Producto", b =>
                {
                    b.HasOne("SistemaVentaDeRopaOnline.Models.Categoria", "Categoria")
                        .WithMany("Productos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("SistemaVentaDeRopaOnline.Models.Venta", b =>
                {
                    b.HasOne("SistemaVentaDeRopaOnline.Models.Pedido", "Pedido")
                        .WithOne("Venta")
                        .HasForeignKey("SistemaVentaDeRopaOnline.Models.Venta", "PedidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pedido");
                });

            modelBuilder.Entity("SistemaVentaDeRopaOnline.Models.Categoria", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("SistemaVentaDeRopaOnline.Models.Color", b =>
                {
                    b.Navigation("Inventarios");
                });

            modelBuilder.Entity("SistemaVentaDeRopaOnline.Models.Inventario", b =>
                {
                    b.Navigation("DetallePedidos");
                });

            modelBuilder.Entity("SistemaVentaDeRopaOnline.Models.Pedido", b =>
                {
                    b.Navigation("DetallePedidos");

                    b.Navigation("Venta");
                });

            modelBuilder.Entity("SistemaVentaDeRopaOnline.Models.Producto", b =>
                {
                    b.Navigation("ImagenProductos");

                    b.Navigation("Inventarios");
                });

            modelBuilder.Entity("SistemaVentaDeRopaOnline.Models.Talla", b =>
                {
                    b.Navigation("Inventarios");
                });

            modelBuilder.Entity("SistemaVentaDeRopaOnline.Models.Usuario", b =>
                {
                    b.Navigation("Pedidos");
                });
#pragma warning restore 612, 618
        }
    }
}
