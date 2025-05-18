package org.openapitools.api;

import org.openapitools.model.BibliographicEntry;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RestController;

import java.util.*;
import java.util.concurrent.atomic.AtomicInteger;

@RestController
public class ApiImpl implements Api {

    private final Map<Integer, BibliographicEntry> storage = new HashMap<>();
    private final AtomicInteger counter = new AtomicInteger(1);

    @Override
    public ResponseEntity<List<BibliographicEntry>> apiBooksGet() {
        return ResponseEntity.ok(new ArrayList<>(storage.values()));
    }

    @Override
    public ResponseEntity<BibliographicEntry> apiBooksIdGet(Integer id) {
        BibliographicEntry entry = storage.get(id);
        if (entry == null) {
            return ResponseEntity.notFound().build();
        }
        return ResponseEntity.ok(entry);
    }

    @Override
    public ResponseEntity<Void> apiBooksIdDelete(Integer id) {
        if (storage.remove(id) != null) {
            return ResponseEntity.noContent().build();
        }
        return ResponseEntity.notFound().build();
    }

    @Override
    public ResponseEntity<Void> apiBooksPost(BibliographicEntry bibliographicEntry) {
        int newId = counter.getAndIncrement();
        bibliographicEntry.setId(newId); // если setId принимает Integer
        storage.put(newId, bibliographicEntry);
        return ResponseEntity.status(201).build();
    }
}