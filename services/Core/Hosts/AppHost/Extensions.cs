namespace Core.Hosts.AppHost
{

    internal static class Extensions
    {

        public static IResourceBuilder<ProjectResource> WithDatabase(this IResourceBuilder<ProjectResource> builder,
            IResourceBuilder<IResourceWithConnectionString>? adminDb,
            params IResourceBuilder<IResourceWithConnectionString>?[] tenantDbs)
        {
            if (adminDb == null) return builder;

            builder.WithReference(adminDb);

            builder.WaitFor(adminDb);
            foreach (var tenantDb in tenantDbs)
            {
                if (tenantDb != null)
                {
                    builder.WaitFor(tenantDb);
                }
            }

            return builder;
        }

    }

}