project(e-books)

file(GLOB_RECURSE all_headers LIST_DIRECTORIES FALSE "*.h")

add_executable(e-books)

target_sources(e-books PRIVATE
    ${all_headers}
    main.cpp
)

target_include_directories(e-books PUBLIC ${CMAKE_CURRENT_SOURCE_DIR})