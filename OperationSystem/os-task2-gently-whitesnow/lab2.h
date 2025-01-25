//
// lab2 definitions header file
//

#ifndef LAB2_LAB2_H
#define LAB2_LAB2_H

// global includes
#include <iostream>

#include <random>


const int sleep_time = 1;

void sleep_ms(int milliseconds); // cross-platform sleep function

void computation();

int lab2_init();

unsigned int lab2_thread_graph_id();

const char* lab2_unsynchronized_threads();

const char* lab2_sequential_threads();


#endif //LAB2_LAB2_H
