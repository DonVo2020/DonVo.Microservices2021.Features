using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.DbModels.EntityWiseConfiguration
{
    public class ApiResourceMappingConfiguration : IEntityTypeConfiguration<ApiResourceMapping>
    {
        public void Configure(EntityTypeBuilder<ApiResourceMapping> builder)
        {
            builder.Property(e => e.Controller).HasMaxLength(50);
        }
    }
}
