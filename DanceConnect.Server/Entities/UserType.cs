namespace DanceConnect.Server.Entities
{
    public enum UserType
    {
        Instructor,
        User,
        Admin,
        SuperAdmin
    }
    public static class UserTypeExtensions
    {
        public static UserType ConverStringToUserType(string value)
        {
            switch (value.ToLower())
            {
                case "admin":
                    return UserType.Admin;
                case "user":
                    return UserType.User;
                case "instructor":
                    return UserType.Instructor;
                default:
                    return UserType.User;
            }
        }
    }
}
