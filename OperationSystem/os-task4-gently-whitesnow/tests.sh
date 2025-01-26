#!/usr/bin/env bash


alg=0

if [[ -f ALGORITHM.txt ]]
then
    alg=$(head -n 1 ALGORITHM.txt)
fi

if [[ $alg -eq 1 ]]
then
    echo "Running incomplete test suite"
    echo "ALGORITHM is "$alg
    echo "Grading reduced by 50%"
    bash run_test.sh -- test_TASKID test_build test_algorithm1
elif [[ $alg -eq 2 ]]
then
    echo "Running incomplete test suite"
    echo "ALGORITHM is "$alg
    echo "Grading reduced by 50%"
    bash run_test.sh -- test_TASKID test_build test_algorithm2
else
    bash run_test.sh
fi
