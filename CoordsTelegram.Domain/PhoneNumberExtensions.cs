namespace CoordsTelegram.Domain
{
    public static class PhoneNumberExtensions
    {
        public static string ChangePhoneFormat(this string phone)
        {
            if (phone.StartsWith("+380"))
            {
                var phoneTemp = phone.Replace("+380", "0");
                var part1 = phoneTemp.Substring(0, 3);
                var part2 = phoneTemp.Substring(3, 3);
                var part3 = phoneTemp.Substring(6, 2);
                var part4 = phoneTemp.Substring(8, 2);

                var res = $"{part1}-{part2}-{part3}-{part4}";
                return res;
            }

            return phone;
        }
    }
}
