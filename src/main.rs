use regex::Regex;
use std::env;
use std::fmt;
use std::str::FromStr;

const C: f64 = 299_792_458.0;
const PI: f64 = 3.14159;
const NMEV: f64 = 1239.8; 

#[derive(Debug, Clone, Copy)]
enum Unit {
    Nm,
    Um,
    Mm,
    Cm,
    M,
    Hz,
    MHz,
    GHz,
    THz,
    RadPerSec,
    RadPerM,
    RadPerCm,
    RadPerUm,
    InvCm,
    EV,
}

impl FromStr for Unit {
    type Err = String;

    fn from_str(s: &str) -> Result<Self, Self::Err> {
        match s {
            "nm" => Ok(Unit::Nm),
            "um" => Ok(Unit::Um),
            "mm" => Ok(Unit::Mm),
            "cm" => Ok(Unit::Cm),
            "m" => Ok(Unit::M),
            "Hz" => Ok(Unit::Hz),
            "MHz" => Ok(Unit::MHz),
            "GHz" => Ok(Unit::GHz),
            "THz" => Ok(Unit::THz),
            "rad/s" => Ok(Unit::RadPerSec),
            "rad/m" => Ok(Unit::RadPerM),
            "rad/cm" => Ok(Unit::RadPerCm),
            "rad/um" => Ok(Unit::RadPerUm),
            "cm-1" | "cm^-1" => Ok(Unit::InvCm),
            "eV" => Ok(Unit::EV),
            _ => Err(format!("ERROR: Unknown Unit: {}", s)),
        }
    }
}

impl fmt::Display for Unit {
    fn fmt(&self, f: &mut fmt::Formatter<'_>) -> fmt::Result {
        let s = match self {
            Unit::Nm => "nm",
            Unit::Um => "um",
            Unit::Mm => "mm",
            Unit::Cm => "cm",
            Unit::M => "m",
            Unit::Hz => "Hz",
            Unit::MHz => "MHz",
            Unit::GHz => "GHz",
            Unit::THz => "THz",
            Unit::RadPerSec => "rad/s",
            Unit::RadPerM => "rad/m",
            Unit::RadPerCm => "rad/cm",
            Unit::RadPerUm => "rad/um",
            Unit::InvCm => "cm^-1",
            Unit::EV => "eV",
        };

        write!(f, "{}", s)
    }
}

struct Quantity {
    value: f64,
    unit: Unit,
}

impl Quantity {
    fn new(value: f64, unit: Unit) -> Quantity {
        Quantity{value: value, unit: unit}
    }

    fn meterisation(&self) -> Quantity {
        let value = match self.unit {
            Unit::Nm => self.value*1e-9,
            Unit::Um => self.value*1e-6,
            Unit::Mm => self.value*1e-3,
            Unit::Cm => self.value*1e-2,
            Unit::M => self.value,
            Unit::Hz => C/self.value,
            Unit::MHz => C/(self.value*1e6),
            Unit::GHz => C/(self.value*1e9),
            Unit::THz => C/(self.value*1e12),
            Unit::RadPerSec => (2.0*PI*C)/(self.value),
            Unit::RadPerM => (2.0*PI)/(self.value),
            Unit::RadPerCm => (2.0*PI)/(self.value/1e-2),
            Unit::RadPerUm => (2.0*PI)/(self.value/1e-6),
            Unit::InvCm => 1.0/(self.value/1e-2),
            Unit::EV => (NMEV/self.value)*1e-9,
        };

        Quantity::new(value, Unit::M)
    }

    fn convert(&self, unit: Unit) -> Quantity {
        let mut value = self.meterisation().value;
        value = match unit {
            Unit::Nm => value/1e-9,
            Unit::Um => value/1e-6,
            Unit::Mm => value/1e-3,
            Unit::Cm => value/1e-2,
            Unit::M => value,
            Unit::Hz => C/value,
            Unit::MHz => C/value/1e6,
            Unit::GHz => C/value/1e9,
            Unit::THz => C/value/1e12,
            Unit::RadPerSec => (2.0*PI*C)/(value),
            Unit::RadPerM => (2.0*PI)/(value),
            Unit::RadPerCm => (2.0*PI)/value*1e-2,
            Unit::RadPerUm => (2.0*PI)/value*1e-6,
            Unit::InvCm => 1.0/value*1e-2,
            Unit::EV => NMEV/(value/1e-9),
        };

        Quantity::new(value, unit)
    }
}

impl fmt::Display for Quantity {
    fn fmt(&self, f: &mut fmt::Formatter<'_>) -> fmt::Result {
        if (self.value < 0.1) || (self.value >= 100_000.0) {
            write!(f, "{:.3e} {}", self.value, self.unit)
        } else {
            write!(f, "{:.3} {}", self.value, self.unit)
        }
    }
}

fn main() -> Result<(), String> {
    let text: String = env::args().skip(1).collect();
    let (out, dest_unit) = intp(&text)?;
    let converted = out.meterisation().convert(dest_unit);

    println!("Input: {}\nConverted: {}", out, converted);
    Ok(())
}

fn intp(text: &str) -> Result<(Quantity, Unit), String> {
    let re = Regex::new(r"^(\d+(?:\.\d+)?(?:[eE][+-]?\d+)?)([a-zA-Z/]+(?:\^?-1)?)(to)([a-zA-Z/]+(?:\^?-1)?)").unwrap();
    let (number, unit, _, dest_unit) = if let Some(caps) = re.captures(text) {
        (
            caps[1].parse().unwrap(),
            caps[2].parse()?,
            "to",
            caps[4].parse()?,
        )
    } else {
        return Err("ERROR: Cannot Parse Argument. Should be [Value]+[Unit] to [Unit] Form.".to_string())
    };

    Ok((Quantity::new(number, unit), dest_unit))
}
