#pragma once
#include <models/reservation/reservation_db_model.h>
#include <repositories/repository.h>

class ReservationRepository : public FileRepository<ReservationDbModel> {

};