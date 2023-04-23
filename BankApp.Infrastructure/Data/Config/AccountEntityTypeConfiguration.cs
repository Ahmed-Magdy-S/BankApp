using BankApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankApp.Infrastructure.Data.Config
{
    public class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).UseSerialColumn();
            builder.Property(p => p.OwnerName).HasColumnType("varchar").IsRequired();
            builder.Property(p => p.Balance).HasColumnType("money").IsRequired();
            builder.Property(p => p.Currency).HasColumnType("varchar").IsRequired();
            builder.Property(p => p.CreatedAt).HasColumnType("timestamp with time zone").IsRequired().HasDefaultValueSql("now()");

            builder.HasIndex(p => p.OwnerName);

     /*       builder.HasMany<Entry>().WithOne();
            builder.HasMany<Transfer>().WithOne();*/

        }
    }
}
