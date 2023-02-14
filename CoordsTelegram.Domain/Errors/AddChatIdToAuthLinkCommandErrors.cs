namespace CoordsTelegram.Domain.Errors
{
    public static class AddChatIdToAuthLinkCommandErrors
    {
        public static string ChatIdRequired => "Chat Id is required.";
        public static string KeyRequired => "Key is required.";
    }
}
