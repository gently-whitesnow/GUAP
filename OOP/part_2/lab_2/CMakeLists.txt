project(lab_2)

set(CMAKE_CXX_STANDARD 20)
set(CMAKE_INCLUDE_CURRENT_DIR ON)
set(CMAKE_PREFIX_PATH "/opt/homebrew/opt/qt@5/lib/cmake")

find_package(Qt5 COMPONENTS Widgets Sql REQUIRED)
file(GLOB_RECURSE all_headers LIST_DIRECTORIES FALSE "*.h")

set(CMAKE_AUTOMOC ON)
set(CMAKE_AUTOUIC ON)
set(CMAKE_AUTORCC ON)

add_executable(lab_2 ${all_headers} main.cpp service/repository.cpp models/book.cpp models/reservation.cpp views/content.cpp views/current_user.cpp views/root.cpp views/table.cpp)

target_include_directories(lab_2 PUBLIC ${CMAKE_CURRENT_SOURCE_DIR})

target_link_libraries(lab_2 Qt5::Widgets Qt5::Sql)


