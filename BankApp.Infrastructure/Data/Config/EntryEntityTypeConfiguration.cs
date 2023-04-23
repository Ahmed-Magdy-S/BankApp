using BankApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankApp.Infrastructure.Data.Config
{
    public class EntryEntityTypeConfiguration : IEntityTypeConfiguration<Entry>
    {
        public void Configure(EntityTypeBuilder<Entry> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).UseSerialColumn();
            builder.Property(p => p.AccountId).IsRequired();
            builder.Property(p => p.Amount).HasColumnType("money").IsRequired();
            builder.Property(p => p.CreatedAt).HasColumnType("timestamp with time zone").IsRequired().HasDefaultValueSql("now()");

            builder.HasIndex(p => p.AccountId);

            builder.HasOne<Account>().WithMany().HasForeignKey(p => p.AccountId);
        }
    }
}
