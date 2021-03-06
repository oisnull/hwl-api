﻿using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace HWL.Service.Generic
{
    public class GenericUtility
    {
        public static String FormatDate(DateTime? dt)
        {
            if (dt == null) return null;
            return dt.Value.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static String FormatDate2(DateTime? dt)
        {
            if (dt == null) return null;
            return dt.Value.ToString("yyyy-MM-dd HH:mm:ss ff");
        }

        public static bool IsValidMail(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress?.Trim())) return false;

            try
            {
                MailAddress m = new MailAddress(emailAddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        /// <summary>
        /// 判断输入的字符串是否是一个合法的手机号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsValidPhone(string phoneString)
        {
            if (string.IsNullOrEmpty(phoneString?.Trim())) return false;

            Regex regex = new Regex("^1[34578]\\d{9}$");
            return regex.IsMatch(phoneString);
        }

        /// <summary>
        /// 0代表相等，1代表version1大于version2，-1代表version1小于version2
        /// </summary>
        /// <param name="version1"></param>
        /// <param name="version2"></param>
        /// <returns></returns>
        public static int CompareVersion(String version1, String version2)
        {
            if (version1.Equals(version2))
            {
                return 0;
            }
            String[] version1Array = version1.Split('.');
            String[] version2Array = version2.Split('.');
            int index = 0;
            // 获取最小长度值
            int minLen = Math.Min(version1Array.Length, version2Array.Length);
            int diff = 0;
            // 循环判断每位的大小
            while (index < minLen
                    && (diff = int.Parse(version1Array[index])
                            - int.Parse(version2Array[index])) == 0)
            {
                index++;
            }
            if (diff == 0)
            {
                // 如果位数不一致，比较多余位数
                for (int i = index; i < version1Array.Length; i++)
                {
                    if (int.Parse(version1Array[i]) > 0)
                    {
                        return 1;
                    }
                }

                for (int i = index; i < version2Array.Length; i++)
                {
                    if (int.Parse(version2Array[i]) > 0)
                    {
                        return -1;
                    }
                }
                return 0;
            }
            else
            {
                return diff > 0 ? 1 : -1;
            }
        }
    }
}
