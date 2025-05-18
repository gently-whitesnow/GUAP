package org.openapitools.service;

import org.openapitools.model.BibliographicEntry;
import org.openapitools.repository.BibliographicEntryRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;
import java.util.Optional;

@Service
@Transactional
public class BibliographicEntryService {
    
    private final BibliographicEntryRepository repository;

    @Autowired
    public BibliographicEntryService(BibliographicEntryRepository repository) {
        this.repository = repository;
    }

    public List<BibliographicEntry> findAll() {
        return repository.findAll();
    }

    public Optional<BibliographicEntry> findById(Integer id) {
        return repository.findById(id);
    }

    public BibliographicEntry save(BibliographicEntry entry) {
        return repository.save(entry);
    }

    public void deleteById(Integer id) {
        repository.deleteById(id);
    }
} 