using System;
using System.Collections.Generic;

namespace CDHB_Official.sakila;

public partial class Migrationhistory
{
    public string MigrationId { get; set; } = null!;

    public string ContextKey { get; set; } = null!;

    public byte[] Model { get; set; } = null!;

    public string ProductVersion { get; set; } = null!;
}
