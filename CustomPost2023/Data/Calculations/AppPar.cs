using System;

namespace CustomPost2023.Data.Calculations
{
    public class AppPar
    {
        public double hours;
        public double cost;
        public double getHours(string oto, string sto, string pgm, string pKPvGM, string ioht, int mass, double prodPrice)
        {
            double x1 = 0.5;
            double x2 = 0.4;
            double x3 = 0.2;
            double x4 = 0.8;
            double x5 = 0.3;
            double x6 = 0.003;

            int OTO = GetOTOInt(oto);
            int STO = GetSTOInt(sto);
            int PGM = GetPGMInt(pgm);
            int PKPvGM = GetPKPvGMInt(pKPvGM);
            int IOHT = GetIOHTInt(ioht);

            hours = x1 * OTO + x2 * STO + x3 * PGM + x4 * PKPvGM + x5 * IOHT + x6 * mass + prodPrice * 0.000004;

            return hours;
        }
        public double getCost(int prodType, double prodPrice)
        {

            cost = hours * 320 + 100 * prodType + prodPrice* 0.009;

            return cost;
        }
        public string ConvertToTime(double value)
        {
            int hours = (int)value;
            int minutes = (int)((value - hours) * 60);
            return $"{hours} ч. {minutes} м.";
        }
        static int GetOTOInt(string value)
        {
            switch (value.ToLower())
            {
                case "100%":
                    return 3;
                case "50%":
                    return 2;
                case "10%":
                    return 1;
                default:
                    return 0;
            }
        }
        static int GetType(int id)
        {
            switch (id)
            {
                case 1:
                    return 10;
                case 2:
                    return 7;
                case 3:
                    return 3;
                case 4:
                    return 18;
                case 5:
                    return 7;
                case 6:
                    return 13;
                case 7:
                    return 11;
                case 8:
                    return 2;
                case 9:
                    return 7;
                case 10:
                    return 8;
                case 11:
                    return 3;
                case 12:
                    return 9;
                default:
                    return 0;
            }
        }
        static int GetSTOInt(string value)
        {
            switch (value.ToLower())
            {
                case "выборочное":
                    return 1;
                case "полное":
                    return 2;
                default:
                    return 0;
            }
        }
        static int GetPGMInt(string value)
        {
            switch (value.ToLower())
            {
                case "выборочное вскрытие":
                    return 1;
                case "полное выскрытие":
                    return 2;
                default:
                    return 0;
            }
        }
        static int GetPKPvGMInt(string value)
        {
            switch (value.ToLower())
            {
                case "выборочно":
                    return 1;
                case "во всех":
                    return 2;
                default:
                    return 0;
            }
        }
        static int GetIOHTInt(string value)
        {
            switch (value.ToLower())
            {
                case "полное":
                    return 2;
                case "частичное":
                    return 1;
                default:
                    return 0;
            }
        }
    }

}
