//
// main faunction is located here, please, DO NOT TOUCH this file
//

#include "lab3.h"

std::random_device rd;     // only used once to initialise (seed) engine
std::mt19937 rng(rd());    // random-number engine used (Mersenne-Twister in this case)
// std::uniform_int_distribution<int> uni(0, sleep_time*2); // guaranteed unbiased
std::uniform_int_distribution<int> uni_delay(10, 200); // guaranteed unbiased
std::uniform_int_distribution<int> uni_chance(0, 100); // guaranteed unbiased

#if defined WIN32 || defined _WIN32
#include <windows.h>
#elif _POSIX_C_SOURCE >= 199309L
#include <time.h>   // for nanosleep
#else
#include <unistd.h> // for usleep
#endif

void sleep_ms(int milliseconds) // cross-platform sleep function
{
    // auto random_integer = uni(rng);
    // milliseconds += random_integer;
#if defined WIN32 || defined _WIN32
    Sleep(milliseconds);
#elif _POSIX_C_SOURCE >= 199309L
    struct timespec ts;
    ts.tv_sec = milliseconds / 1000;
    ts.tv_nsec = (milliseconds % 1000) * 1000000;
    nanosleep(&ts, NULL);
#else
    usleep(milliseconds * 1000);
#endif
}


void computation()
{
    auto delay = uni_delay(rng);
    auto chance = uni_chance(rng);
    sleep_ms(delay);
    if (chance < 10) {
        sleep_ms(delay);
    }
}


int main(int argc, char **argv)
{
    return lab3_init();
}
