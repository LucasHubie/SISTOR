﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SISTOR.Configuration;

namespace SISTOR.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220104001541_InicialCreate")]
    partial class InicialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SISTOR.Models.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("IdPessoa")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdPessoa");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("SISTOR.Models.Funcionario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("IdPessoa")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdPessoa");

                    b.ToTable("Funcionario");
                });

            modelBuilder.Entity("SISTOR.Models.Itens", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("IdOrcamento")
                        .HasColumnType("int");

                    b.Property<int?>("IdOrdemServico")
                        .HasColumnType("int");

                    b.Property<int?>("IdProduto")
                        .HasColumnType("int");

                    b.Property<string>("Observacoes")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorItem")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("IdOrcamento");

                    b.HasIndex("IdOrdemServico");

                    b.HasIndex("IdProduto");

                    b.ToTable("Itens");
                });

            modelBuilder.Entity("SISTOR.Models.Orcamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("IdCliente")
                        .HasColumnType("int");

                    b.Property<decimal>("MaoDeObra")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.Property<string>("TagIdentificacao")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("IdCliente");

                    b.ToTable("Orcamento");
                });

            modelBuilder.Entity("SISTOR.Models.OrdemServico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnType("datetime2");

                    b.Property<string>("HorasTrabalhadas")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("IdCliente")
                        .HasColumnType("int");

                    b.Property<int?>("IdFuncionario")
                        .HasColumnType("int");

                    b.Property<int?>("IdOrcamento")
                        .HasColumnType("int");

                    b.Property<decimal>("MaoDeObra")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Observacoes")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("IdCliente");

                    b.HasIndex("IdFuncionario");

                    b.HasIndex("IdOrcamento");

                    b.ToTable("OrdemServico");
                });

            modelBuilder.Entity("SISTOR.Models.Pessoa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CEP")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("CNPJ")
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("CPF")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Celular")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Complemento")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Endereco")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("NomeFantasia")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<string>("RG")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("RazaoSocial")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Referencia")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Telefone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("TipoPessoa")
                        .HasColumnType("int");

                    b.Property<string>("UF")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.HasKey("Id");

                    b.ToTable("Pessoa");
                });

            modelBuilder.Entity("SISTOR.Models.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Codigo")
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("TipoMedida")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("SISTOR.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Login")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nome")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Senha")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("SISTOR.Models.Cliente", b =>
                {
                    b.HasOne("SISTOR.Models.Pessoa", "Pessoa")
                        .WithMany()
                        .HasForeignKey("IdPessoa");

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("SISTOR.Models.Funcionario", b =>
                {
                    b.HasOne("SISTOR.Models.Pessoa", "Pessoa")
                        .WithMany()
                        .HasForeignKey("IdPessoa");

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("SISTOR.Models.Itens", b =>
                {
                    b.HasOne("SISTOR.Models.Orcamento", "Orcamento")
                        .WithMany()
                        .HasForeignKey("IdOrcamento");

                    b.HasOne("SISTOR.Models.OrdemServico", "OrdemServico")
                        .WithMany()
                        .HasForeignKey("IdOrdemServico");

                    b.HasOne("SISTOR.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("IdProduto");

                    b.Navigation("Orcamento");

                    b.Navigation("OrdemServico");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("SISTOR.Models.Orcamento", b =>
                {
                    b.HasOne("SISTOR.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("IdCliente");

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("SISTOR.Models.OrdemServico", b =>
                {
                    b.HasOne("SISTOR.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("IdCliente");

                    b.HasOne("SISTOR.Models.Funcionario", "Funcionario")
                        .WithMany()
                        .HasForeignKey("IdFuncionario");

                    b.HasOne("SISTOR.Models.Orcamento", "Orcamento")
                        .WithMany()
                        .HasForeignKey("IdOrcamento");

                    b.Navigation("Cliente");

                    b.Navigation("Funcionario");

                    b.Navigation("Orcamento");
                });
#pragma warning restore 612, 618
        }
    }
}