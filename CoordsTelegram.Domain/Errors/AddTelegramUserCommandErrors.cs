namespace CoordsTelegram.Domain.Errors
{
    public static class AddTelegramUserCommandErrors
    {
        public static string ChatIdRequired => "Chat Id is required.";
        public static string PhoneNumberRequired => "PhoneNumber is required.";
        public static string PhoneNumberLength => "PhoneNumber must be equal to 13 characters.";
        public static string PhoneNumberInvalid => "PhoneNumber is not valid.";
    }
}
