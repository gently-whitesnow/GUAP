#pragma once

#include <QCheckBox>
#include <QComboBox>
#include <QGroupBox>
#include <QHBoxLayout>
#include <QLabel>
#include <QMainWindow>
#include <QPushButton>
#include <QRadioButton>
#include <QVBoxLayout>
#include <iostream>
#include <map>

class MainWindow : public QMainWindow {
   public:
    MainWindow() : QMainWindow() {
        QGroupBox* group = new QGroupBox("Зайцев Александр Сергеевич Z1431");
        QVBoxLayout* mainLayout = new QVBoxLayout();

        QComboBox* autoSelector = new QComboBox();

        autoSelector->addItem("Жигули", "jiga.jpeg");
        autoSelector->addItem("Газель", "gaz.jpeg");
        autoSelector->addItem("Москвич 3", "china.jpeg");
        autoSelector->addItem("УАЗ Патриот", "patriot.jpeg");

        setPicture("jiga.jpeg");

        QObject::connect(
            autoSelector, QOverload<int>::of(&QComboBox::currentIndexChanged),
            [autoSelector, this](int index) {
                if (index >= 0 && index < autoSelector->count()) {
                    QString imagePath =
                        autoSelector->currentData()
                            .toString();  // Получаем путь к изображению
                    setPicture(imagePath);
                    baseAutoPrice = 100000 + index * 100000;
                    showResult(0);
                }
            });

        mainLayout->addWidget(autoSelector);
        mainLayout->addWidget(picture);

        QLabel* autoColor = new QLabel("Цвет автомобиля:");
        mainLayout->addWidget(autoColor);

        QRadioButton* red = new QRadioButton("Красный");
        red->setChecked(true);
        QRadioButton* blue = new QRadioButton("Синий");
        QRadioButton* green = new QRadioButton("Зеленый");
        mainLayout->addWidget(red);
        mainLayout->addWidget(blue);
        mainLayout->addWidget(green);

        QLabel* staffLabel = new QLabel("Комплектующие:");
        mainLayout->addWidget(staffLabel);

        staff.reserve(staffProperties.size());
        for (const auto& [staffName, price] : staffProperties) {
            QCheckBox* staffItem = new QCheckBox(staffName);
            mainLayout->addWidget(staffItem);
            staff.emplace_back(staffItem);
            QObject::connect(staffItem,
                             QOverload<bool>::of(&QCheckBox::toggled),
                             [this](bool) { showResult(0); });
        }

        QHBoxLayout* sumLayout = new QHBoxLayout();

        QPushButton* button = new QPushButton("Рассчитать стоимость");

        QObject::connect(
            button, QOverload<bool>::of(&QPushButton::clicked), [&](bool) {
                uint32_t price = baseAutoPrice;
                price += 30000;  // color radio button
                short checked = 0;
                for (const auto* staffItem : staff) {
                    if (staffItem->isChecked()) {
                        price += staffProperties.at(staffItem->text());
                        checked++;
                    }
                }

                showResult(price, checked == staff.size());
            });

        sumLayout->addWidget(button);
        sumLayout->addWidget(result);

        mainLayout->addLayout(sumLayout);
        group->setLayout(mainLayout);
        setCentralWidget(group);
    }
   private slots:
    void onAutoChanged(int id) {}

   private:
    void setPicture(const QString& imagePath) {
        QPixmap pixmap(imagePath);
        picture->setPixmap(pixmap.scaled(250, 250, Qt::KeepAspectRatio));
    }

    void showResult(uint32_t price, bool discount = false) {
        if (price == 0) {
            result->setText("");
        } else {
            auto resultText = QString("С вас %1 рублей").arg(price);
            if (discount) {
                resultText.append(" (скидка 10%)");
            }
            result->setText(resultText);
        }
    }

    uint32_t baseAutoPrice = 0;
    QLabel* result = new QLabel();
    QLabel* picture = new QLabel();
    std::vector<QCheckBox*> staff;
    std::map<QString, uint32_t> staffProperties = {
        {"Шины", 250000},
        {"Диски", 100000},
        {"Стеклоподъемники", 50000},
        {"Пахучка", 300},
    };
};