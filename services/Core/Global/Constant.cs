namespace Core.Global
{
    public static class Constant
    {

        public static class API
        {
            public const string FrontendOriginDomain = "localhost";
            public const string DefaultFrontendOriginSchema = "http";
            public const string DefaultFrontendOriginHost = "localhost";
            public const int DefaultFrontendOriginPort = 3030;

            public static string GetDefaultFrontendOrigin =>
                $"{DefaultFrontendOriginSchema}://{DefaultFrontendOriginHost}:{DefaultFrontendOriginPort}";
        }

        public static class Admin
        {
            public const string DatabaseTag = "admin-database";
        }

        public static class Tenant
        {

            public const string DeepBlueDatabaseTag = "deep-blue-database";

            public const string StormSharkDatabaseTag = "storm-shark-database";

        }

    }
}