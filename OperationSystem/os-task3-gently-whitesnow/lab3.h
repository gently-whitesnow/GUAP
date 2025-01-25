//
// lab3 definitions header file
//

#ifndef LAB3_LAB3_H
#define LAB3_LAB3_H

// global includes
#include <iostream>

#include <random>


const int sleep_time = 1;

void sleep_ms(int milliseconds); // cross-platform sleep function

void computation();

int lab3_init();

unsigned int lab3_thread_graph_id();

const char* lab3_unsynchronized_threads();

const char* lab3_sequential_threads();


#endif //LAB3_LAB3_H
