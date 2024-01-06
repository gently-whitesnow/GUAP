#include "mainwindow.h"
#include <QApplication>
#include <QLabel> // 1
int main(int argc, char *argv[])
{
 QApplication a(argc, argv);
 MainWindow w;
 QLabel lb1("Здравствуй Qt!"); // 2
 w.show();
 lb1.show(); // 3
 return a.exec();
}