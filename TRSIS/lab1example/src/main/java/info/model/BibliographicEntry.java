package info.model;

public class BibliographicEntry {
    private Long id;
    private String authors;
    private String title;
    private String publisher;
    private int year;
    private int pages;
    private String isbn;

    // Геттеры и сеттеры
    public Long getId() { return id; }
    public void setId(Long id) { this.id = id; }
    public String getAuthors() { return authors; }
    public void setAuthors(String authors) { this.authors = authors; }
    public String getTitle() { return title; }
    public void setTitle(String title) { this.title = title; }
    public String getPublisher() { return publisher; }
    public void setPublisher(String publisher) { this.publisher = publisher; }
    public int getYear() { return year; }
    public void setYear(int year) { this.year = year; }
    public int getPages() { return pages; }
    public void setPages(int pages) { this.pages = pages; }
    public String getIsbn() { return isbn; }
    public void setIsbn(String isbn) { this.isbn = isbn; }

    @Override
    public String toString() {
        return String.format("%s. %s. — %s, %d. — %d с. — ISBN %s",
            authors, title, publisher, year, pages, isbn);
    }
} 