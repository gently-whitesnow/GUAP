package info.servlet;

import info.model.BibliographicEntry;
import jakarta.servlet.ServletException;
import jakarta.servlet.annotation.WebServlet;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.util.List;

@WebServlet("/add")
public class AddServlet extends HttpServlet {
    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response) 
            throws ServletException, IOException {
        request.setCharacterEncoding("UTF-8");
        
        BibliographicEntry entry = new BibliographicEntry();
        entry.setId(System.currentTimeMillis());
        entry.setAuthors(request.getParameter("authors"));
        entry.setTitle(request.getParameter("title"));
        entry.setPublisher(request.getParameter("publisher"));
        entry.setYear(Integer.parseInt(request.getParameter("year")));
        entry.setPages(Integer.parseInt(request.getParameter("pages")));
        entry.setIsbn(request.getParameter("isbn"));
        
        // in-memory storage
        List<BibliographicEntry> entries = ListServlet.getEntries();
        entries.add(entry);
        
        response.sendRedirect("list");
    }
} 