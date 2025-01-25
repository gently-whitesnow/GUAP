#!/usr/bin/env bash
cd test
g++ ../lab2.cpp tests.cpp -lpthread -lgtest -o runTests -I gtest/include -L gtest

./runTests
