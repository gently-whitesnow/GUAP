#include <iostream>

class Angle {
   public:
    Angle() = default;

    int GetGradus() const;

    void SetGradus(const int& gradus);

    int GetMin() const;

    void SetMin(const int& min);

    int GetSec() const;

    void SetSec(const int& sec);

    double ToRadians() const;

   private:
    int _gradus;
    int _min;
    int _sec;
    const double _pi = 3.1415926535;

    bool IsTimeValid(const int& value) const;
};

std::ostream& operator<<(std::ostream& out, const Angle& angle) {
    out << angle.GetGradus() << " grad " << angle.GetMin() << " min "
        << angle.GetSec() << " sec";
    return out;
}

int Angle::GetGradus() const { return _gradus; }

void Angle::SetGradus(const int& gradus) { _gradus = gradus; }

int Angle::GetMin() const { return _min; }

void Angle::SetMin(const int& min) {
    if (IsTimeValid(min)) _min = min;
}

int Angle::GetSec() const { return _sec; }

void Angle::SetSec(const int& sec) {
    if (IsTimeValid(sec)) _sec = sec;
}

double Angle::ToRadians() const {
    double sec_normalized = _sec != 0 ? _sec / 60 : 0;
    double min_mormalized =
        (_min + sec_normalized) != 0 ? (_min + sec_normalized) / 60 : 0;
    if (_gradus + min_mormalized == 0) return 0;

    return (_gradus + min_mormalized) * _pi / 180;
}

bool Angle::IsTimeValid(const int& value) const {
    if (value >= 0 && value < 60) return true;

    std::cerr << "Wrong value, required value > 0 and value < 60!" << std::endl;
    return false;
}