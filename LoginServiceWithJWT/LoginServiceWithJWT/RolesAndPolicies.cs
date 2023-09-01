namespace LoginServiceWithJWT
{
    public class RolesAndPolicies
    {
        public static Dictionary<string, string> Policies { get; }

        static RolesAndPolicies()
        {
            Policies = new Dictionary<string, string>()
        {
                {"Admin", "Write" },
                {"User", "Read" }
            };
        }
    }
}
