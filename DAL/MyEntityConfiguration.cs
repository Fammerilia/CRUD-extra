    using Microsoft.EntityFrameworkCore.Metadata.Builders;
using   Microsoft.EntityFrameworkCore;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.OrderId);

        builder.Property(o => o.OrderDate)
               .IsRequired();

        builder.Property(o => o.OrderDescription)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(o => o.OrderAmount)
               .IsRequired();

        builder.Property(o => o.OrderStatus)
               .HasMaxLength(50)
               .IsRequired();

    }
}

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(c => c.ClientId);

        builder.Property(c => c.EmpName)
               .HasMaxLength(10)
               .IsRequired();

        builder.Property(c => c.EmpSex)
               .IsRequired();

        builder.Property(c => c.Status);


        builder.Property(c => c.DiscountType)
               .HasDefaultValue(5);

        builder.Property(c => c.EmpMiddlename)
            .HasMaxLength(20);
        builder.Property(c => c.EmpSurname)
           .IsRequired()
           .HasMaxLength(20);
    }
}

public class ClientContactConfiguration : IEntityTypeConfiguration<ClientContact>
{
    public void Configure(EntityTypeBuilder<ClientContact> builder)
    {
        builder.HasKey(c => c.ClientContactId);

        builder.Property(c => c.EmpContact)
               .IsRequired()
               .HasMaxLength(30)
               .HasAnnotation("RegularExpression", @"^\+?[0-9]*$")
               .HasAnnotation("RegularExpressionErrorMessage", "Invalid phone number format.");

        builder.Property(c => c.Contact_type)
               .IsRequired();
    }
}


public class ClientAddressesConfiguration : IEntityTypeConfiguration<ClientAddresses>
{
    public void Configure(EntityTypeBuilder<ClientAddresses> builder)
    {
        builder.HasKey(c => c.ClientAddressId);

        builder.Property(c => c.EmpAddress)
               .IsRequired()
               .HasMaxLength(30)
               .HasAnnotation("RegularExpression", @"^[a-zA-Z0-9\s]+$")
               .HasAnnotation("RegularExpressionErrorMessage", "Invalid address format.");

        builder.HasOne(c => c.Client)
               .WithMany(c => c.ClientAddresses)
               .HasForeignKey(c => c.ClientId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(c => c.Address_type)
               .IsRequired();
    }
}

public class ClientEmailConfiguration : IEntityTypeConfiguration<ClientEmail>
{
    public void Configure(EntityTypeBuilder<ClientEmail> builder)
    {
        builder.HasKey(c => c.ClientEmailId);

        builder.Property(c => c.EmpEmail)
               .IsRequired()
               .HasMaxLength(30)
               .HasConversion(email => email.ToLower(), email => email)
               .HasAnnotation("RegularExpression", @"^[^@\s]+@[^@\s]+\.[^@\s]+$")
               .HasAnnotation("RegularExpressionErrorMessage", "Invalid email format.");

        builder.Property(c => c.Email_type)
               .IsRequired();
    }
}
