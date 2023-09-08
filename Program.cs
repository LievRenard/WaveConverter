using System.Text.RegularExpressions;

class Program {
    public static int Main(string[] args){
        double pi = Math.PI;
        double c = 3e8;

        string cmd = string.Join(' ',args);
        string pattern = "([0-9])?[.]?[0-9]+([eE][+-]?[0-9]+)? *((nm)|(rad[/]s)|(Hz)|(rad[/]cm))";

        if (!Regex.IsMatch(cmd, pattern)) {
            Console.WriteLine("Command Cannot Be Interpreted.");
            return 1;
        }

        cmd = Regex.Match(cmd,pattern).Value;
        string unit = Regex.Match(cmd,"(nm)|(rad[/]s)|(Hz)|(rad[/]cm)").Value;
        double val = double.Parse(Regex.Match(cmd,"([0-9])?[.]?[0-9]+([eE][+-]?[0-9]+)?").Value);
        switch (unit)
        {
            case "nm":
                Console.WriteLine("Converted :");
                val *= 1e-9;
                Console.WriteLine("f = {0:e2} Hz",c/val);
                Console.WriteLine("ω = {0:e2} rad/s",2*pi*c/val);
                if (2*pi/val/100 < 5000) {Console.WriteLine("k = {0:f2} rad/cm",2*pi/val/100);}
                else {Console.WriteLine("k = {0:f2} rad/μm",2*pi/val/1e6);}
                break;
            case "rad/s":
                Console.WriteLine("Converted :");
                if (2*pi*c/val*1e9 < 2000) {Console.WriteLine("λ = {0:f2} nm",2*pi*c/val*1e9);}
                else {Console.WriteLine("λ = {0:f2} μm",2*pi*c/val*1e6);}
                Console.WriteLine("f = {0:e2} Hz",val/2/pi);
                if (val/c/100 < 5000) {Console.WriteLine("k = {0:f2} rad/cm",val/c/100);}
                else {Console.WriteLine("k = {0:f2} rad/μm",val/c/1e6);}
                break;
            case "Hz":
                Console.WriteLine("Converted :");
                if (c/val*1e9 < 2000) {Console.WriteLine("λ = {0:f2} nm",c/val*1e9);}
                else {Console.WriteLine("λ = {0:f2} μm",c/val*1e6);}
                Console.WriteLine("ω = {0:e2} rad/s",val*2*pi);
                if (2*pi*val/c/100 < 5000) {Console.WriteLine("k = {0:f2} rad/cm",2*pi*val/c/100);}
                else {Console.WriteLine("k = {0:f2} rad/μm",2*pi*val/c/1e6);}
                break;
            case "rad/cm":
                Console.WriteLine("Converted :");
                val *= 1e2;
                if (2*pi/val*1e9 < 2000) {Console.WriteLine("λ = {0:f2} nm",2*pi/val*1e9);}
                else {Console.WriteLine("λ = {0:f2} μm",2*pi/val*1e6);}
                Console.WriteLine("f = {0:e2} Hz",c*val/2/pi);
                Console.WriteLine("ω = {0:e2} rad/s",c*val);
                break;
            default:
                Console.WriteLine("Command Cannot Be Interpreted.");
                break;
        }
        return 0;
    }
}