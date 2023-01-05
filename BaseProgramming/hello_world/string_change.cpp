#include "string_change.h"
#include <algorithm>

void SwapBytes(char* str, unsigned int len) {
    for (unsigned int i = 0; i < len / 2; ++i) {
        std::swap(*(str + i), *(str + len - 1 - i));
    }
}