#pragma once
#include <iostream>
#include <sstream>
#include <string>

class Medic {
   public:
    Medic(const std::string& name, const std::string& surname,
          const std::string& patronymic, const std::string& birthDate,
          const std::string& address, const std::string& education)
        : _name(name),
          _surname(surname),
          _patronymic(patronymic),
          _birthDate(birthDate),
          _address(address),
          _education(education) {}

    void Print() const {
        std::cout << "Medic(name: " << _name << ", surname: " << _surname
                  << ", patronymic: " << _patronymic
                  << ", birthDate: " << _birthDate << ", address: " << _address
                  << ", education: " << _education << ")" << std::endl;
    }

    ~Medic() {
        std::cout << "destruction" << std::endl;
        Print();
    }

   private:
    std::string _name;
    std::string _surname;
    std::string _patronymic;
    std::string _birthDate;
    std::string _address;
    std::string _education;
};

class Nurse : public Medic {
   public:
    Nurse(const std::string& name, const std::string& surname,
          const std::string& patronymic, const std::string& birthDate,
          const std::string& address, const std::string& education,
          double salaryPerHour)
        : Medic(name, surname, patronymic, birthDate, address, education),
          _salaryPerHour(salaryPerHour) {}

    double GetSalary(int hours) {
        if (hours <= 0) return 0;

        return hours * _salaryPerHour;
    }

   private:
    double _salaryPerHour;
};

class Surgeone : public Medic {
   public:
    Surgeone(const std::string& name, const std::string& surname,
             const std::string& patronymic, const std::string& birthDate,
             const std::string& address, const std::string& education,
             double salaryPerHour)
        : Medic(name, surname, patronymic, birthDate, address, education),
          _salaryPerHour(salaryPerHour) {}

    double GetSalary(int peopleCount) {
        if (peopleCount <= 0) return 0;

        return peopleCount * _salaryPerHour;
    }

   private:
    double _salaryPerHour;
};