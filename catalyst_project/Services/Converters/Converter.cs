using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project
{
    class Converter
    {
        public double convertToGL(string unit, string value) {
            if (String.IsNullOrWhiteSpace(value))
            {
                return 0;
            }
            else if (unit.Equals("g/ft³"))
            {
                double d = Convert.ToDouble(value);
                double result = Math.Round(d / 28.317, 4);
                return result;
          
            }
            else if (unit.Equals("g/l")) {
                double d = Convert.ToDouble(value);
                return d;
            }
            return 0;
        }

        public int convertToLiter(string unit, string value) {
            if (String.IsNullOrWhiteSpace(value))
            {
                return 0;
            }
            else if (unit.Equals("liter"))
            {
                int v = Convert.ToInt32(value);
                return v;
            }
            else if (unit.Equals("mm³"))
            {
                int v = Convert.ToInt32(value);
                int result = Convert.ToInt32(v / 1000000);
                return result;
                
            }
            else if (unit.Equals("cm³"))
            {
                int v = Convert.ToInt32(value);
                int result = Convert.ToInt32(v / 1000);
                return result;
            }
            else if (unit.Equals("m³"))
            {
                int v = Convert.ToInt32(value);
                int result = Convert.ToInt32(v / 0.001);
                return result;
            }
            else if (unit.Equals("ft³"))
            {
                int v = Convert.ToInt32(value);
                int result = Convert.ToInt32(v / 0.0353);
                return result;
            }
            else if (unit.Equals("inch³"))
            {
                int v = Convert.ToInt32(value);
                int result = Convert.ToInt32(v / 61.024);
                return result;
            }

            return 0;
        }

        public double convertToMMDouble(string unit, string value) {
            if (String.IsNullOrWhiteSpace(value))
            {
                return 0;
            }
            else if (unit.Equals("inch"))
            {
                double i = Convert.ToDouble(value);
                double result = Math.Round(i/0.0394, 4);
                return result;
            }
            else if (unit.Equals("cm"))
            {
                double i = Convert.ToDouble(value);
                double result = Math.Round(i / 0.1 , 4);
                return result;
            }
            else if (unit.Equals("mm"))
            {
                double i = Convert.ToDouble(value);
                return i;
            }
            else if (unit.Equals("m"))
            {
                double i = Convert.ToDouble(value);
                double result = Math.Round(i / 0.001, 4);
                return result;
            }
            return 0;
        }

        public int convertToMil(string unit, string value) {
            if (String.IsNullOrWhiteSpace(value))
            {
                return 0;
            }
            else if (unit.Equals("mil")) {
                int v = Convert.ToInt32(value);
                return v;
            }
            else if (unit.Equals("mm"))
            {
                int v = Convert.ToInt32(value);
                int result = Convert.ToInt32(v / 0.0254);
                return result;
            }
            return 0;
        }

        public double convertToInch(string unit, string value) {
            if (String.IsNullOrWhiteSpace(value))
            {
                return 0;
            }
            else if (unit.Equals("inch"))
            {
                double i = Convert.ToDouble(value);
                return i;
            }
            else if (unit.Equals("cm"))
            {
                double i = Convert.ToDouble(value);
                double result = Math.Round(i / 2.54, 4);
                return result;
            }
            else if (unit.Equals("mm"))
            {
                double i = Convert.ToDouble(value);
                double result = Math.Round(i / 25.4, 4);
                return result;
            }
            else if (unit.Equals("m"))
            {
                double i = Convert.ToDouble(value);
                double result = Math.Round(i / 0.0254, 4);
                return result;
            }
            return 0;
        }

        public double convertToCelsius(string unit, string value) {
            if (String.IsNullOrWhiteSpace(value))
            {
                return 0;
            }
            else if (unit.Equals("°C"))
            {
                double i = Convert.ToDouble(value);
                return i;
            }
            else if (unit.Equals("°F"))
            {
                double i = Convert.ToDouble(value);
                double result = Math.Round((i - 32) * 0.5556, 4);
                return result;
            }
            else if (unit.Equals("K"))
            {
                double i = Convert.ToDouble(value);
                double result = Math.Round(i-273.15, 4);
                return result;
            }
            return 0;
        }
    }
}
