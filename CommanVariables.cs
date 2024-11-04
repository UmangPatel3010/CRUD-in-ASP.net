namespace StaticCRUD
{
    public class CommanVariables
    {
        private static IHttpContextAccessor _HttpContextAccessor;

        static CommanVariables()
        {
            _HttpContextAccessor = new HttpContextAccessor();
        }

        public static int? GetUserID()
        {
            if(_HttpContextAccessor.HttpContext.Session.GetString("UserID") == null)
            {
                return null;
            }
            return Convert.ToInt32(_HttpContextAccessor.HttpContext.Session.GetString("UserID"));
        }
        public static string? GetUserName()
        {
            if (_HttpContextAccessor.HttpContext.Session.GetString("UserName") == null)
            {
                return null;
            }
            return _HttpContextAccessor.HttpContext.Session.GetString("UserName") ;
        }
        public static string? GetEmailAddress()
        {
            if (_HttpContextAccessor.HttpContext.Session.GetString("EmailAddress") == null)
            {
                return null;
            }
            return _HttpContextAccessor.HttpContext.Session.GetString("EmailAddress") ;
        }
    }
}
