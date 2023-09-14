using System.Text.RegularExpressions;

class Program {
    public static int Main(string[] args){
        double pi = Math.PI;
        double c = 3e8;

        string cmd = string.Join(' ',args);
        string pattern = "([0-9])?[.]?[0-9]+([eE][+-]?[0-9]+)? *((nm)|(um)|(rad[/]s)|(Hz)|(THz)|(rad[/]cm)|(rad[/]um)|(cm-1)|(eV))";

        if (cmd == "--help" || cmd == "-h") {
            Console.WriteLine(@"
WaveConverter(conv) Usage:

conv <value> <unit>

Converts value with specific unit to other electromagnetic wave properties. Input property is decided by input unit.
Value and unit can be seperated by blank or not. Capital letter must be distinguished in unit.

Support properties and unit:

Wavelength - nm, um
Frequency - Hz, THz
Angular Frequency - rad/s
Angular Wavenumber - rad/cm, rad/um
Linear Wavenumber - cm-1
Photon Energy - eV
            ");
            return 0;
        }

        if (!Regex.IsMatch(cmd, pattern)) {
            Console.WriteLine("Command Cannot Be Interpreted.");
            return 1;
        }

        cmd = Regex.Match(cmd,pattern).Value;
        string unit = Regex.Match(cmd,"(nm)|(um)|(rad[/]s)|(Hz)|(THz)|(rad[/]cm)|(rad[/]um)|(cm-1)|(eV)").Value;
        double val = double.Parse(Regex.Match(cmd,"([0-9])?[.]?[0-9]+([eE][+-]?[0-9]+)?").Value);
        switch (unit)
        {
            case "nm":
                Console.WriteLine("Converted :");
                val *= 1e-9;
                Console.WriteLine("f = {0:e2} Hz",c/val);
                Console.WriteLine("ω = {0:e2} rad/s",2*pi*c/val);
                if (2*pi/val/100 < 50000) {
                    Console.WriteLine("k = {0:f2} rad/cm",2*pi/val/100);
                    Console.WriteLine(" or {0:f2} cm-1",1/val/100);
                    }
                else {
                    Console.WriteLine("k = {0:f2} rad/μm",2*pi/val/1e6);
                    Console.WriteLine(" or {0:f2} μm-1",1/val/1e6);
                    }
                Console.WriteLine("E = {0:f2} eV",1239.8/val/1e9);
                break;
            case "um":
                Console.WriteLine("Converted :");
                val *= 1e-6;
                Console.WriteLine("f = {0:e2} Hz",c/val);
                Console.WriteLine("ω = {0:e2} rad/s",2*pi*c/val);
                if (2*pi/val/100 < 50000) {
                    Console.WriteLine("k = {0:f2} rad/cm",2*pi/val/100);
                    Console.WriteLine(" or {0:f2} cm-1",1/val/100);
                    }
                else {
                    Console.WriteLine("k = {0:f2} rad/μm",2*pi/val/1e6);
                    Console.WriteLine(" or {0:f2} μm-1",1/val/1e6);
                    }
                Console.WriteLine("E = {0:f2} eV",1239.8/val/1e9);
                break;
            case "rad/s":
                Console.WriteLine("Converted :");
                if (2*pi*c/val*1e9 < 2000) {Console.WriteLine("λ = {0:f2} nm",2*pi*c/val*1e9);}
                else {Console.WriteLine("λ = {0:f2} μm",2*pi*c/val*1e6);}
                Console.WriteLine("f = {0:e2} Hz",val/2/pi);
                if (val/c/100 < 50000) {
                    Console.WriteLine("k = {0:f2} rad/cm",val/c/100);
                    Console.WriteLine(" or {0:f2} cm-1",val/c/2/pi/100);
                    }
                else {
                    Console.WriteLine("k = {0:f2} rad/μm",val/c/1e6);
                    Console.WriteLine(" or {0:f2} μm-1",val/c/2/pi/1e6);
                    }
                Console.WriteLine("E = {0:f2} eV",1239.8/(2*pi*c/val*1e9));
                break;
            case "Hz":
                Console.WriteLine("Converted :");
                if (c/val*1e9 < 2000) {Console.WriteLine("λ = {0:f2} nm",c/val*1e9);}
                else {Console.WriteLine("λ = {0:f2} μm",c/val*1e6);}
                Console.WriteLine("ω = {0:e2} rad/s",val*2*pi);
                if (2*pi*val/c/100 < 50000) {
                    Console.WriteLine("k = {0:f2} rad/cm",2*pi*val/c/100);
                    Console.WriteLine(" or {0:f2} cm-1",1*val/c/100);
                    }
                else {
                    Console.WriteLine("k = {0:f2} rad/μm",2*pi*val/c/1e6);
                    Console.WriteLine(" or {0:f2} μm-1",1*val/c/1e6);
                    }
                Console.WriteLine("E = {0:f2} eV",1239.8/(c/val*1e9));
                break;
            case "THz":
                Console.WriteLine("Converted :");
                val *= 1e12;
                if (c/val*1e9 < 2000) {Console.WriteLine("λ = {0:f2} nm",c/val*1e9);}
                else {Console.WriteLine("λ = {0:f2} μm",c/val*1e6);}
                Console.WriteLine("ω = {0:e2} rad/s",val*2*pi);
                if (2*pi*val/c/100 < 50000) {
                    Console.WriteLine("k = {0:f2} rad/cm",2*pi*val/c/100);
                    Console.WriteLine(" or {0:f2} cm-1",1*val/c/100);
                    }
                else {
                    Console.WriteLine("k = {0:f2} rad/μm",2*pi*val/c/1e6);
                    Console.WriteLine(" or {0:f2} μm-1",1*val/c/1e6);
                    }
                Console.WriteLine("E = {0:f2} eV",1239.8/(c/val*1e9));
                break;
            case "rad/cm":
                Console.WriteLine("Converted :");
                val *= 1e2;
                if (2*pi/val*1e9 < 2000) {Console.WriteLine("λ = {0:f2} nm",2*pi/val*1e9);}
                else {Console.WriteLine("λ = {0:f2} μm",2*pi/val*1e6);}
                Console.WriteLine("f = {0:e2} Hz",c*val/2/pi);
                Console.WriteLine("ω = {0:e2} rad/s",c*val);
                Console.WriteLine("k(lin) = {0:f2} cm-1",val/2/pi/1e2);
                Console.WriteLine("E = {0:f2} eV",1239.8/(2*pi/val*1e9));
                break;
            case "rad/um":
                Console.WriteLine("Converted :");
                val *= 1e6;
                if (2*pi/val*1e9 < 2000) {Console.WriteLine("λ = {0:f2} nm",2*pi/val*1e9);}
                else {Console.WriteLine("λ = {0:f2} μm",2*pi/val*1e6);}
                Console.WriteLine("f = {0:e2} Hz",c*val/2/pi);
                Console.WriteLine("ω = {0:e2} rad/s",c*val);
                Console.WriteLine("k(lin) = {0:f2} μm-1",val/2/pi/1e6);
                Console.WriteLine("E = {0:f2} eV",1239.8/(2*pi/val*1e9));
                break;
            case "cm-1":
                Console.WriteLine("Converted :");
                val *= 1e2*2*pi;
                if (2*pi/val*1e9 < 2000) {Console.WriteLine("λ = {0:f2} nm",2*pi/val*1e9);}
                else {Console.WriteLine("λ = {0:f2} μm",2*pi/val*1e6);}
                Console.WriteLine("f = {0:e2} Hz",c*val/2/pi);
                Console.WriteLine("ω = {0:e2} rad/s",c*val);
                Console.WriteLine("k(ang) = {0:f2} cm-1",val/1e2);
                Console.WriteLine("E = {0:f2} eV",1239.8/(2*pi/val*1e9));
                break;
            case "eV":
                Console.WriteLine("Converted :");
                if (1239.8/val < 2000) {Console.WriteLine("λ = {0:f2} nm",1239.8/val);}
                else {Console.WriteLine("λ = {0:f2} μm",1239.8/val*1e-3);}
                val = 1239.8/val*1e-9;
                Console.WriteLine("f = {0:e2} Hz",c/val);
                Console.WriteLine("ω = {0:e2} rad/s",2*pi*c/val);
                if (2*pi/val/100 < 50000) {
                    Console.WriteLine("k = {0:f2} rad/cm",2*pi/val/100);
                    Console.WriteLine(" or {0:f2} cm-1",1/val/100);
                    }
                else {
                    Console.WriteLine("k = {0:f2} rad/μm",2*pi/val/1e6);
                    Console.WriteLine(" or {0:f2} μm-1",1/val/1e6);
                    }
                break;
            default:
                Console.WriteLine("Command Cannot Be Interpreted.");
                break;
        }
        return 0;
    }
}