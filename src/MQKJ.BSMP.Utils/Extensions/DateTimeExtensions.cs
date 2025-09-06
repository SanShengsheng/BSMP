using System;

namespace MQKJ.BSMP.Utils.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 获取年龄
        /// </summary>
        /// <param name="birthdate">出生日期</param>
        /// <returns></returns>
        public static int GetAgeByBirthdate(this DateTime birthdate)
        {
            DateTime now = DateTime.Now;
            int age = now.Year - birthdate.Year;
            if (now.Month < birthdate.Month || (now.Month == birthdate.Month && now.Day < birthdate.Day))
            {
                age--;
            }
            return age < 0 ? 0 : age;
        }
        /// <summary>
        /// 获取星座
        /// </summary>
        /// <param name="birthday">出生日期</param>
        /// <returns></returns>
        public static string GetAtomFromBirthday(this DateTime birthday)
        {
            float birthdayF = 0.00F;

            if (birthday.Month == 1 && birthday.Day < 20)
            {
                birthdayF = float.Parse(string.Format("13.{0}", birthday.Day));
            }
            else
            {
                birthdayF = float.Parse(string.Format("{0}.{1}", birthday.Month, birthday.Day));
            }
            float[] atomBound = { 1.20F, 2.20F, 3.21F, 4.21F, 5.21F, 6.22F, 7.23F, 8.23F, 9.23F, 10.23F, 11.21F, 12.22F, 13.20F };
            string[] atoms = { "水瓶座", "双鱼座", "白羊座", "金牛座", "双子座", "巨蟹座", "狮子座", "处女座", "天秤座", "天蝎座", "射手座", "魔羯座" };

            string ret = "外星人";
            for (int i = 0; i < atomBound.Length - 1; i++)
            {
                if (atomBound[i] <= birthdayF && atomBound[i + 1] > birthdayF)
                {
                    ret = atoms[i];
                    break;
                }
            }
            return ret;
        }

        public static DateTime ToDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day);
        }

        public static int GetTimestamp(this DateTime date)
        {
            return (int)(((date.Date- new DateTime(1970, 1, 1, 0, 0, 0, 0))).TotalSeconds);
        }
    }
}
