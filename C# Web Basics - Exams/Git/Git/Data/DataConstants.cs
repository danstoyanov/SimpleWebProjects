namespace Git.Data
{
    public class DataConstants
    {
        public const int IdMaxLength = 40;
        public const int DefaultMaxLength = 20;
        public const int UserMinUsername = 4;
        public const int UserMinPassword = 5;
        public const string UserEmailRegularExpression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        public const string RepositotyTypeOne = "Public";
        public const string RepositotyTypeTwo = "Private";
        public const int RepoNameMinLegnth = 3;
        public const int RepoNameMaxLegnth = 10;

        public const int CommitMinDescriptionLength = 5;
    }
}
