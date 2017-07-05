using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Wallet.Data;

namespace Wallet.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170628111212_Transaction-take1-migration")]
    partial class Transactiontake1migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Wallet.Models.BankAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountId");

                    b.Property<decimal>("Balance");

                    b.Property<string>("Bank");

                    b.Property<DateTime>("DateTimeStamp");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Wallet.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<int>("BankAccountId");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("BankAccountId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("Wallet.Models.Transaction", b =>
                {
                    b.HasOne("Wallet.Models.BankAccount")
                        .WithMany("Transactions")
                        .HasForeignKey("BankAccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
