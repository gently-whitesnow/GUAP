#pragma once
#include <models/user/user_db_model.h>
#include <repositories/repository.h>

class UserRepository : public FileRepository<UserDbModel> {

};