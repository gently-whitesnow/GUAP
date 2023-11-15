#pragma once
#include <models/review/review_db_model.h>
#include <repositories/repository.h>

class ReviewRepository : public FileRepository<ReviewDbModel> {

};