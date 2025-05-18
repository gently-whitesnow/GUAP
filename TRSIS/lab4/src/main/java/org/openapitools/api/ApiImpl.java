package org.openapitools.api;

import org.openapitools.model.BibliographicEntry;
import org.openapitools.service.BibliographicEntryService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
public class ApiImpl implements Api {

    private final BibliographicEntryService service;

    @Autowired
    public ApiImpl(BibliographicEntryService service) {
        this.service = service;
    }

    @Override
    public ResponseEntity<List<BibliographicEntry>> apiBooksGet() {
        return ResponseEntity.ok(service.findAll());
    }

    @Override
    public ResponseEntity<BibliographicEntry> apiBooksIdGet(Integer id) {
        return service.findById(id)
                .map(ResponseEntity::ok)
                .orElse(ResponseEntity.notFound().build());
    }

    @Override
    public ResponseEntity<Void> apiBooksIdDelete(Integer id) {
        if (service.findById(id).isPresent()) {
            service.deleteById(id);
            return ResponseEntity.noContent().build();
        }
        return ResponseEntity.notFound().build();
    }

    @Override
    public ResponseEntity<Void> apiBooksPost(BibliographicEntry bibliographicEntry) {
        service.save(bibliographicEntry);
        return ResponseEntity.status(201).build();
    }
}