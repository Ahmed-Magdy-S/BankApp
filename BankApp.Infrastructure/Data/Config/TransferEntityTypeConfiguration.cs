using BankApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankApp.Infrastructure.Data.Config
{
    public class TransferEntityTypeConfiguration : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).UseSerialColumn();
            builder.Property(p => p.FromAccountId).IsRequired();
            builder.Property(p => p.ToAccountId).IsRequired();
            builder.Property(p => p.Amount).HasColumnType("money").IsRequired();
            builder.Property(p => p.CreatedAt).HasColumnType("timestamp with time zone").IsRequired().HasDefaultValueSql("now()");

            builder.HasOne<Account>().WithMany().HasForeignKey(p => p.FromAccountId);
            builder.HasOne<Account>().WithMany().HasForeignKey(p => p.ToAccountId);

        }
    }
}
