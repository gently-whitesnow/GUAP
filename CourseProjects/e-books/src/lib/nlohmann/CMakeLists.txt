project(nlohmann)

set(SOURCE_LIB json.hpp)

add_library(nlohmann STATIC ${SOURCE_LIB})

set_target_properties(nlohmann PROPERTIES LINKER_LANGUAGE CXX)