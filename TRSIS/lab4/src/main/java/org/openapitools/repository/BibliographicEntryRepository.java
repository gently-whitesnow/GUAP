package org.openapitools.repository;

import org.openapitools.model.BibliographicEntry;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface BibliographicEntryRepository extends JpaRepository<BibliographicEntry, Integer> {
} 