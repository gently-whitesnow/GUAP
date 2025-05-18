package org.openapitools.model;

import java.net.URI;
import java.util.Objects;
import com.fasterxml.jackson.annotation.JsonProperty;
import com.fasterxml.jackson.annotation.JsonCreator;
import org.springframework.lang.Nullable;
import org.openapitools.jackson.nullable.JsonNullable;
import java.time.OffsetDateTime;
import jakarta.validation.Valid;
import jakarta.validation.constraints.*;
import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.persistence.*;


import java.util.*;
import jakarta.annotation.Generated;

/**
 * BibliographicEntry
 */

@Entity
@Table(name = "bibliographic_entries")
@Generated(value = "org.openapitools.codegen.languages.SpringCodegen", date = "2025-05-17T22:32:28.625814+03:00[Europe/Moscow]", comments = "Generator version: 7.13.0")
public class BibliographicEntry {

  @Id
  @GeneratedValue(strategy = GenerationType.IDENTITY)
  private @Nullable Integer id;

  @NotNull
  @Column(nullable = false)
  private String authors;

  @NotNull
  @Column(nullable = false)
  private String title;

  @NotNull
  @Column(nullable = false)
  private String publisher;

  @NotNull
  @Column(name = "publication_year", nullable = false)
  private Integer year;

  @Column
  private @Nullable Integer pages;

  @Column
  private @Nullable String isbn;

  public BibliographicEntry() {
    super();
  }

  /**
   * Constructor with only required parameters
   */
  public BibliographicEntry(String authors, String title, String publisher, Integer year) {
    this.authors = authors;
    this.title = title;
    this.publisher = publisher;
    this.year = year;
  }

  public BibliographicEntry id(Integer id) {
    this.id = id;
    return this;
  }

  /**
   * Get id
   * @return id
   */
  
  @Schema(name = "id", requiredMode = Schema.RequiredMode.NOT_REQUIRED)
  @JsonProperty("id")
  public Integer getId() {
    return id;
  }

  public void setId(Integer id) {
    this.id = id;
  }

  public BibliographicEntry authors(String authors) {
    this.authors = authors;
    return this;
  }

  /**
   * Get authors
   * @return authors
   */
  @NotNull 
  @Schema(name = "authors", requiredMode = Schema.RequiredMode.REQUIRED)
  @JsonProperty("authors")
  public String getAuthors() {
    return authors;
  }

  public void setAuthors(String authors) {
    this.authors = authors;
  }

  public BibliographicEntry title(String title) {
    this.title = title;
    return this;
  }

  /**
   * Get title
   * @return title
   */
  @NotNull 
  @Schema(name = "title", requiredMode = Schema.RequiredMode.REQUIRED)
  @JsonProperty("title")
  public String getTitle() {
    return title;
  }

  public void setTitle(String title) {
    this.title = title;
  }

  public BibliographicEntry publisher(String publisher) {
    this.publisher = publisher;
    return this;
  }

  /**
   * Get publisher
   * @return publisher
   */
  @NotNull 
  @Schema(name = "publisher", requiredMode = Schema.RequiredMode.REQUIRED)
  @JsonProperty("publisher")
  public String getPublisher() {
    return publisher;
  }

  public void setPublisher(String publisher) {
    this.publisher = publisher;
  }

  public BibliographicEntry year(Integer year) {
    this.year = year;
    return this;
  }

  /**
   * Get year
   * @return year
   */
  @NotNull 
  @Schema(name = "year", requiredMode = Schema.RequiredMode.REQUIRED)
  @JsonProperty("year")
  public Integer getYear() {
    return year;
  }

  public void setYear(Integer year) {
    this.year = year;
  }

  public BibliographicEntry pages(Integer pages) {
    this.pages = pages;
    return this;
  }

  /**
   * Get pages
   * @return pages
   */
  
  @Schema(name = "pages", requiredMode = Schema.RequiredMode.NOT_REQUIRED)
  @JsonProperty("pages")
  public Integer getPages() {
    return pages;
  }

  public void setPages(Integer pages) {
    this.pages = pages;
  }

  public BibliographicEntry isbn(String isbn) {
    this.isbn = isbn;
    return this;
  }

  /**
   * Get isbn
   * @return isbn
   */
  
  @Schema(name = "isbn", requiredMode = Schema.RequiredMode.NOT_REQUIRED)
  @JsonProperty("isbn")
  public String getIsbn() {
    return isbn;
  }

  public void setIsbn(String isbn) {
    this.isbn = isbn;
  }

  @Override
  public boolean equals(Object o) {
    if (this == o) {
      return true;
    }
    if (o == null || getClass() != o.getClass()) {
      return false;
    }
    BibliographicEntry bibliographicEntry = (BibliographicEntry) o;
    return Objects.equals(this.id, bibliographicEntry.id) &&
        Objects.equals(this.authors, bibliographicEntry.authors) &&
        Objects.equals(this.title, bibliographicEntry.title) &&
        Objects.equals(this.publisher, bibliographicEntry.publisher) &&
        Objects.equals(this.year, bibliographicEntry.year) &&
        Objects.equals(this.pages, bibliographicEntry.pages) &&
        Objects.equals(this.isbn, bibliographicEntry.isbn);
  }

  @Override
  public int hashCode() {
    return Objects.hash(id, authors, title, publisher, year, pages, isbn);
  }

  @Override
  public String toString() {
    StringBuilder sb = new StringBuilder();
    sb.append("class BibliographicEntry {\n");
    sb.append("    id: ").append(toIndentedString(id)).append("\n");
    sb.append("    authors: ").append(toIndentedString(authors)).append("\n");
    sb.append("    title: ").append(toIndentedString(title)).append("\n");
    sb.append("    publisher: ").append(toIndentedString(publisher)).append("\n");
    sb.append("    year: ").append(toIndentedString(year)).append("\n");
    sb.append("    pages: ").append(toIndentedString(pages)).append("\n");
    sb.append("    isbn: ").append(toIndentedString(isbn)).append("\n");
    sb.append("}");
    return sb.toString();
  }

  /**
   * Convert the given object to string with each line indented by 4 spaces
   * (except the first line).
   */
  private String toIndentedString(Object o) {
    if (o == null) {
      return "null";
    }
    return o.toString().replace("\n", "\n    ");
  }
}

