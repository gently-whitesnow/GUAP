#!/usr/bin/env bash


# $1 - test suit
# $2 - TASKID
# $3 - field ID
readData()
{
    if [[ $1 -eq 1 ]]
    then
        grep "^$2\s" test/full.txt | cut -f $3
    # elif [[ $1 -eq 2 ]]
    # then
    #     grep "^$2\s" test/full.txt | cut -f $3
    else
        "Error!"
    fi
}


test_TASKID()
{
    TASKID=$(head -n 1 TASKID.txt)
    echo "TASKID is "$TASKID
    assertNotEquals "Did you forget to set the TASKID variable?" -1 $TASKID
    assertTrue "TASKID is too small" "[ $TASKID -gt 0 ]"
    assertTrue "TASKID is too large" "[ $TASKID -lt 31 ]"
}

test_build()
{
    g++ lab4.cpp -o lab4 -std=c++11
    build_status=$?
    assertTrue "Build failed" "[ $build_status -eq 0 ]"
}

test_algorithm1()
{
    TASKID=$(head -n 1 TASKID.txt)
    input_file=$(readData 1 $TASKID 2)
    output_file=$(readData 1 $TASKID 3)
    # echo $input_file
    # echo $output_file
    if [[ (-n $input_file) && (-n $output_file) ]]
    then
        cat $input_file | ./lab4 1 > output1.txt
        diff_lines=$(diff $output_file output1.txt | wc -l)
        assertTrue "Test for algorithm 1 failed. Output is not correct." "[ $diff_lines -eq 0 ]"
        if [[ $diff_lines -gt 0 ]]
        then
            echo "diff "$output_file" output1.txt"
            diff $output_file output1.txt
        fi
    else
        fail "No input data found. Did you mess up with the code?"
    fi
}

test_algorithm2()
{
    TASKID=$(head -n 1 TASKID.txt)
    input_file=$(readData 1 $TASKID 4)
    output_file=$(readData 1 $TASKID 5)
    # echo $input_file
    # echo $output_file
    if [[ (-n $input_file) && (-n $output_file) ]]
    then
        cat $input_file | ./lab4 2 > output2.txt
        diff_lines=$(diff $output_file output2.txt | wc -l)
        assertTrue "Test for algorithm 2 failed. Output is not correct." "[ $diff_lines -eq 0 ]"
        if [[ $diff_lines -gt 0 ]]
        then
            echo "diff "$output_file" output2.txt"
            diff $output_file output2.txt
        fi
    else
        fail "No input data found. Did you mess up with the code?"
    fi
}


setUp()
{
    TASKID=-1
    LC_ALL=C
}


if [[ -d "shunit2-2.1.8" ]]
then
    echo "Found shunit2-2.1.8"
else
    echo "Downloading shunit2-2.1.8 ..."
    curl https://codeload.github.com/kward/shunit2/tar.gz/v2.1.8 --output shunit2-2.1.8.tar.gz
    tar -xzf shunit2-2.1.8.tar.gz
fi
# load shunit2
. shunit2-2.1.8/shunit2
