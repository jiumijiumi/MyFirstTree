namespace MyFirstTree
{
    public class GetFirstAlphabet
    {

        /// <summary>
        /// 得到一个汉字的拼音第一个字母，如果是一个英文字母则直接返回大写字母
        /// </summary>
        /// <param name="cnChar">单个汉字</param>
        /// <returns>单个大写字母</returns>
        public static char GetCharSpellCode(string cnChar)
        {
            byte[] ZW = System.Text.Encoding.Default.GetBytes(cnChar);

            //如果是字母，则直接返回大写字母
            if (ZW.Length == 1)
            {
                return  cnChar.ToUpper()[0];
            }
            // get the array of byte from the single char
            int i1 = ZW[0];
            int i2 = ZW[1];
            long iCnChar = i1 * 256 + i2;

            // iCnChar match the constant
            if ((iCnChar >= 45217) && (iCnChar <= 45252))
            {return 'A';}
            if ((iCnChar >= 45253) && (iCnChar <= 45760))
            {return 'B';}
            if ((iCnChar >= 45761) && (iCnChar <= 46317))
            {return 'C';}
            if ((iCnChar >= 46318) && (iCnChar <= 46825))
            {return 'D';}
            if ((iCnChar >= 46826) && (iCnChar <= 47009))
            {return 'E';}
            if ((iCnChar >= 47010) && (iCnChar <= 47296))
            {return 'F';}
            if ((iCnChar >= 47297) && (iCnChar <= 47613))
            {return 'G';}
            if ((iCnChar >= 47614) && (iCnChar <= 48118))
            {return 'H';}
            if ((iCnChar >= 48119) && (iCnChar <= 49061))
            {return 'J';}
            if ((iCnChar >= 49062) && (iCnChar <= 49323))
            {return 'K';}
            if ((iCnChar >= 49324) && (iCnChar <= 49895))
            {return 'L';}
            if ((iCnChar >= 49896) && (iCnChar <= 50370))
            {return 'M';}
            if ((iCnChar >= 50371) && (iCnChar <= 50613))
            {return 'N';}
            if ((iCnChar >= 50614) && (iCnChar <= 50621))
            {return 'O';}
            if ((iCnChar >= 50622) && (iCnChar <= 50905))
            {return 'P';}
            if ((iCnChar >= 50906) && (iCnChar <= 51386))
            {return 'Q';}
            if ((iCnChar >= 51387) && (iCnChar <= 51445))
            {return 'R';}
            if ((iCnChar >= 51446) && (iCnChar <= 52217))
            {return 'S';}
            if ((iCnChar >= 52218) && (iCnChar <= 52697))
            {return 'T';}
            if ((iCnChar >= 52698) && (iCnChar <= 52979))
            {return 'W';}
            if ((iCnChar >= 52980) && (iCnChar <= 53640))
            {return 'X';}
            if ((iCnChar >= 53689) && (iCnChar <= 54480))
            {return 'Y';}
            if ((iCnChar >= 54481) && (iCnChar <= 55289))
            {return 'Z';}
            return cnChar.ToUpper()[0];//比较稳妥的方法
        }
    }
}